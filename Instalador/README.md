# Instalador y reinstalador de PadelGest

Instalador Inno Setup para PadelGest. El ejecutable publicado debe recompilarse y probarse en Windows cada vez que se modifique la aplicacion o el script SQL.

## Requisitos de la PC donde se instala

- Windows x64.
- SQL Server local (instancia `.` o `localhost\SQLEXPRESS`) en ejecucion; version 2019 o posterior.
- `sqlcmd.exe` disponible en PATH.
- Usuario de Windows con permisos para crear una base de datos en esa instancia.
- El instalador solicita privilegios de administrador.

No se instala SQL Server automaticamente: debe estar disponible previamente.

## Publicar y compilar

En la raiz del repositorio, con .NET 9 SDK:

```powershell
dotnet publish .\UI\UI.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o .\Instalador\publish
```

Abrir `Instalador/PadelGest.iss` con Inno Setup Compiler y elegir **Build > Compile**.
El instalador generado se guardara en `Instalador/Output/`.

Importante: no subir ni ejecutar una carpeta `publish` de CineGest. Publicar la aplicacion PadelGest actual.

## Comportamiento

**Instalacion desde cero:** verifica SQL Server a traves de `sqlcmd`; si `PadelGestDB` no existe, ejecuta el script limpio de `Script/`, que crea `Producto` y `Producto_C` vacias. Los nuevos productos se cargan desde la aplicacion; sus versiones iniciales se registran por trigger.

**Reinstalacion:** antes de iniciar, el asistente consulta si `PadelGestDB` existe y muestra una confirmacion explicita indicando que se actualizaran los archivos del programa y se conservaran los datos. Si el usuario elige No, no comienza la instalacion. Si confirma, no toca las tablas ni los registros existentes; actualiza archivos publicados y accesos directos.

**Desinstalacion:** muestra tres opciones:
1. Solo el programa (conserva `PadelGestDB`).
2. Eliminar todo.
3. Backup y eliminar todo: pide una carpeta, ejecuta `BACKUP DATABASE`, comprueba `RESTORE VERIFYONLY` y solo despues elimina la base.

La instancia debe ser **local**. SQL Server (cuenta de servicio) necesita permisos de escritura en la carpeta elegida para el `.bak`. Si la copia falla o se cancela, **no comienza la desinstalacion**: se conservan el programa y la base, y se vuelve al menu para elegir una alternativa. Evitar seleccionar `C:\` y otras ubicaciones protegidas.

El script auxiliar solo apunta a una base de nombre fijo: `PadelGestDB`.

## Pruebas obligatorias antes de entrega

1. Compilar el .iss en Windows y solucionar errores que informe Inno Setup.
2. Probar nueva instalacion en una instancia SQL sin `PadelGestDB`.
3. Confirmar login administrador, integridad DVH/DVV, `Producto` = 0 y `Producto_C` = 0, y DVV inicial de `Producto` consistente con una tabla vacia.
4. Crear un cliente, registrar un producto nuevo y comprobar que se genere su primera version en `Producto_C`; luego modificar ese producto y verificar el historial.
5. Reinstalar: confirmar que cliente, modificacion e historial se conservan.
6. Desinstalar **solo programa** y volver a instalar: datos intactos.
7. Desinstalar con **backup y eliminar**: verificar `.bak`, confirmar ausencia de base y luego restaurarla.
8. Simular carpeta sin permisos para SQL Server: debe fallar backup, conservar **la base y el programa**, y regresar a las opciones para intentar de nuevo o elegir solo el programa.
9. Probar **eliminar todo sin backup** solamente en una instancia de prueba.
10. Verificar que app iniciada desde Setup use la instancia SQL que se selecciono.

**Advertencia:** no ejecutar pruebas de eliminacion sobre bases con datos reales.
