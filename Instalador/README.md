# Instalador y reinstalador de PadelGest

Instalador Inno Setup para PadelGest. El ejecutable publicado debe recompilarse y probarse en Windows cada vez que se modifique la aplicacion o el script SQL.

## Requisitos de la PC donde se instala

- Windows x64.
- SQL Server local (instancia `.` o `localhost\SQLEXPRESS`) en ejecucion; version 2019 o posterior.
- `sqlcmd.exe` disponible en PATH.
- Usuario de Windows con permisos para crear la base (y reemplazarla, si corresponde) en esa instancia.
- Se ejecuta con privilegios del usuario actual; los permisos sobre SQL Server se comprueban al instalar.

No se instala SQL Server automaticamente: debe estar disponible previamente.

## Publicar y compilar

En la raiz del repositorio, con .NET 9 SDK:

```powershell
dotnet publish .\UI\UI.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o .\Instalador\publish
```

Abrir `Instalador/PadelGest.iss` con Inno Setup Compiler y elegir **Build > Compile**.
El instalador generado se guardara en `Instalador/Output/`.

Importante: no subir ni ejecutar una carpeta `publish` de CineGest. Publicar la aplicacion PadelGest actual.

## Comportamiento del asistente

1. Al abrir Setup se elige **Instalar** o **Reinstalar**, antes de elegir el directorio.
2. Se selecciona la instancia SQL local: `localhost\SQLEXPRESS` o `.`.
3. Se consulta si existe `PadelGestDB`; solo se permite continuar si se cumplen las condiciones del modo elegido.
4. Se elige la ubicacion y se confirma el inicio.

**Instalar (programa y base nueva):**
- Si `PadelGestDB` no existe: ejecuta el script `PadelGest_Instalacion_Limpia_SQL2019.sql` y publica el programa.
- Si ya existe: muestra una advertencia sobre perdida de datos con **No** predeterminado. Si no se confirma, no se elimina.
- Si se confirma: **primero** crea un `.bak` con `COPY_ONLY, CHECKSUM` en la carpeta predeterminada de backups de SQL Server y lo valida con `RESTORE VERIFYONLY`. Solo entonces elimina la base anterior, ejecuta el script limpio e instala el programa. Si el backup falla, **no se borra la base**.
- El respaldo previo queda guardado en la carpeta de backups de SQL Server. Conservarlo si se necesita recuperar los datos anteriores.
- Una falla durante la creacion de la nueva base, despues del `DROP DATABASE`, puede dejar la instalacion sin una base operativa. Recuperar el backup verificado antes de reintentar.

**Reinstalar (solo programa):**
- Exige que `PadelGestDB` exista, sea accesible y contenga al menos las tablas `Usuario`, `Cliente` y `Producto`.
- No ejecuta el script de instalacion, ni `BACKUP`, `DROP`, `ALTER`, `INSERT` o `UPDATE` sobre la base.
- Actualiza solamente archivos y accesos directos; se conservan registros, roles, facturas, reservas e historial.
- Si no encuentra una base compatible, bloquea la operacion. Seleccionar **Instalar** si se necesita crear una base nueva.

La existencia y accesibilidad de la base se vuelven a verificar inmediatamente antes de instalar los archivos. Cambiar el modo o servidor requiere volver a pasar la validacion.

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
5. Reinstalar con base existente: confirmar que cliente, modificacion e historial se conservan.
6. Reinstalar sin base o con base incompatible: el asistente debe bloquear el avance.
7. Instalar con base existente: seleccionar No y verificar que no se modifica ni se elimina nada.
8. Instalar con base existente: confirmar en una BD de prueba; verificar la copia .bak, el reemplazo y la nueva BD.
9. Simular falta de permisos para backup y comprobar que no se borra la base.
6. Desinstalar **solo programa** y volver a instalar: datos intactos.
7. Desinstalar con **backup y eliminar**: verificar `.bak`, confirmar ausencia de base y luego restaurarla.
8. Simular carpeta sin permisos para SQL Server: debe fallar backup, conservar **la base y el programa**, y regresar a las opciones para intentar de nuevo o elegir solo el programa.
9. Probar **eliminar todo sin backup** solamente en una instancia de prueba.
10. Verificar que app iniciada desde Setup use la instancia SQL que se selecciono.

**Advertencia:** no ejecutar pruebas de eliminacion sobre bases con datos reales.
