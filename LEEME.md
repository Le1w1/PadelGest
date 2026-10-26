# PadelGest — Trabajo de Diploma

Sistema de Información de escritorio desarrollado para **AirPadel**, club ubicado en General San Martín, provincia de Buenos Aires. Su objetivo es centralizar la gestión de reservas de canchas, clientes y, de manera progresiva, la venta de productos del buffet.

## 1. Información académica

| Dato | Información |
|---|---|
| Sistema | PadelGest |
| Organización | AirPadel |
| Universidad | Universidad Abierta Interamericana (UAI) |
| Carrera | Ingeniería en Sistemas Informáticos |
| Asignatura | Trabajo de Diploma |
| Alumno | Rodríguez Leonel Jesús |
| Comisión | 3-B |
| Turno | Mañana |
| Sede | Centro |
| Año de cursada | 2026 |
| Docente | Leonel Jiménez Gamboa |

## 2. Tecnologías y arquitectura

- **Lenguaje:** C#.
- **Plataforma:** .NET 9 y Windows Forms (aplicación de escritorio para Windows).
- **Base de datos:** Microsoft SQL Server; acceso mediante Microsoft.Data.SqlClient.
- **Entorno de desarrollo:** Visual Studio 2022.
- **Seguridad de contraseñas:** hash SHA-256, de acuerdo con la implementación académica actual.
- **Idiomas:** español e inglés mediante recursos JSON.
- **Exportación PDF:** iTextSharp (auditoría de eventos).

La solución se organiza en proyectos separados:

| Proyecto | Responsabilidad |
|---|---|
| **UI** | Formularios de Windows Forms e interacción con el usuario. |
| **BE** | Entidades de negocio, como Cliente, Reserva, Cancha, Factura, Pago y Equipamiento. |
| **BLL** | Validaciones y reglas de negocio; coordinación de operaciones. |
| **DAL** | Acceso a SQL Server, persistencia y transacciones. |
| **Servicios** | Funcionalidades transversales: sesión, permisos, traducción, criptografía, integridad y simulación bancaria. |

Se utilizan **Singleton** (sesión y traducción), **Observer** (actualización de idioma) y **Composite** (Roles, Familias y Permisos Simples).

## 3. Estado de implementación

### Funcionalidades implementadas en el repositorio

**Seguridad y administración**
- Inicio y cierre de sesión, re-login y cambio de contraseña.
- Control de intentos fallidos y bloqueo de usuarios.
- Gestión de usuarios y asignación de roles.
- Gestión de Roles, Familias y Permisos Simples.
- Aplicación de permisos según el usuario autenticado.
- Cambio dinámico entre español e inglés.
- Registro y consulta de Bitácora de Eventos, con exportación a PDF.
- Verificación de integridad mediante dígitos verificadores horizontales y verticales (DVH/DVV), y tratamiento de inconsistencias.
- Creación y restauración de respaldos de SQL Server.

**PN1 — Reserva de Cancha**
- Consulta de disponibilidad y selección de fecha, horario, cancha y tarifa.
- Incorporación opcional de paletas y/o pelotas.
- Búsqueda, selección y registro de clientes.
- Preparación de la factura y cobro con tarjeta mediante **autorización bancaria simulada**.
- Registro de la reserva tras el pago aprobado, con posibilidad de reintentar el registro sin volver a cobrar cuando corresponda.
- Visualización de la confirmación y los datos de la reserva registrada.

### Funcionalidades documentadas o pendientes de desarrollo

**PN2 — Venta de Productos del Buffet** está definido en la carpeta de Trabajo de Diploma mediante los casos de uso **CUN08 Generar Factura**, **CUN09 Registrar Cliente** y **CUN10 Cobrar Factura**. Contempla selección de productos, detalle de factura, cobro simulado, registro de venta y actualización automática del stock. **No está implementado aún en el código de esta versión del repositorio.**

El menú principal también contempla apartados para **Vendedor Buffet**, **Encargado de Canchas** y **Dueño**, cuyas funcionalidades específicas se incorporarán progresivamente. La ayuda desde el menú principal y la documentación final en PDF se encuentran pendientes.

**Importante:** la presencia de permisos, entradas de menú o especificaciones funcionales no implica que el módulo correspondiente esté programado.

## 4. Requisitos para ejecutar el proyecto

- Windows 10 u 11.
- Procesador Intel Core i3 o equivalente, 4 GB de RAM como mínimo y pantalla de 1366 × 768 o superior (requisitos de referencia de la carpeta).
- Microsoft SQL Server 2019 o posterior; SQL Server Express es suficiente para el entorno académico.
- SQL Server Management Studio (SSMS), para preparar la base de datos.
- Visual Studio 2022 compatible con .NET 9.
- .NET 9 SDK, para compilar el proyecto.

El sistema puede funcionar sin Internet si SQL Server está disponible localmente o mediante una red interna.

## 5. Instalación y ejecución rápida

1. Clonar o descargar el repositorio **Le1w1/PadelGest** en un equipo Windows.
2. En SQL Server Management Studio, conectarse a la instancia de SQL Server que se utilizará.
3. **Crear primero una base de datos vacía** llamada PadelGestDB:

   ~~~sql
   CREATE DATABASE [PadelGestDB];
   ~~~

4. Seleccionar esa instancia y ejecutar **Script/PadelGestDB.sql** sobre la base recién creada. El archivo comienza con la instrucción USE [PadelGestDB] y contiene la estructura de tablas y datos iniciales, incluidos usuarios, roles, familias, permisos, idiomas y registros de prueba.
5. Configurar la instancia de SQL Server como se explica en el apartado siguiente.
6. Abrir **PadelGest.slnx** en Visual Studio 2022.
7. Restaurar paquetes NuGet si fuera necesario, establecer **UI** como proyecto de inicio, compilar la solución y ejecutarla con F5.
8. Iniciar sesión con una cuenta habilitada de la base cargada.

**Advertencia sobre el script:** actualmente existe **un único script principal**, Script/PadelGestDB.sql. No crea la base de datos por sí mismo ni es idempotente: contiene instrucciones CREATE TABLE e INSERT directas. Debe ejecutarse **una sola vez sobre una base vacía**. No ejecutarlo encima de una instalación con información que se quiera conservar. Si ya existe una base, realizar antes un respaldo y comprobar qué procedimiento de actualización corresponde.

## 6. Configuración de la conexión SQL Server

El acceso a datos se configura en **DAL/Servicios/DAO_AccesoDatos.cs** mediante la variable de entorno **PADELGEST_ENTORNO**.

| Valor | Instancia SQL Server | Comportamiento |
|---|---|---|
| LEO | localhost\SQLEXPRESS | Configuración predeterminada cuando la variable no está definida. |
| UAI | . (instancia local predeterminada) | Configuración alternativa para el entorno UAI. |

Se utiliza autenticación integrada de Windows contra la base **PadelGestDB**.

Para seleccionar el entorno UAI en una consola de Windows:

~~~bat
setx PADELGEST_ENTORNO UAI
~~~

Para volver al entorno LEO:

~~~bat
setx PADELGEST_ENTORNO LEO
~~~

Después de utilizar setx, **cerrar y volver a abrir Visual Studio** para que el proceso tome el valor actualizado. Si la instancia real tiene otro nombre, será necesario adaptar la configuración en DAL/Servicios/DAO_AccesoDatos.cs antes de ejecutar la aplicación.

## 7. Primer acceso y cuentas

El script SQL incluye cuentas académicas de prueba, entre ellas una cuenta de administrador identificada por **admin1@padelgest.com**.

Por seguridad, **este archivo público no publica contraseñas iniciales ni hashes**. Las credenciales válidas para la demostración deben obtenerse del responsable de la entrega; el almacenamiento y la validación de contraseñas utilizan hashes en la base de datos. Se recomienda cambiar las contraseñas de prueba cuando corresponda.

Los roles previstos en la base son: **Administrador**, **Dueño**, **Recepcionista**, **Vendedor de Buffet** y **Encargado de Canchas**. El acceso a las operaciones depende de los permisos efectivos de la sesión.

## 8. Estructura principal del repositorio

~~~text
PadelGest/
├── BE/
├── BLL/
│   └── Servicios/
├── DAL/
│   └── Servicios/
├── Servicios/
├── UI/
│   └── Servicios/
│       └── Recursos/Idiomas/
├── Script/
│   └── PadelGestDB.sql
├── PadelGest.slnx
├── README.md
└── LEEME.md
~~~

## 9. Consideraciones para la evaluación

- **Alcance de la versión:** se puede ejecutar la infraestructura técnica y el flujo programado de **PN1**. **PN2 permanece documentado y pendiente de implementación**.
- **Banco simulado:** el cobro no se conecta con un servicio bancario real. Durante la demostración se selecciona una respuesta simulada para representar una autorización aprobada o rechazada.
- **Integridad:** los DVH/DVV verifican información protegida; las modificaciones directas en la base fuera del sistema pueden producir alertas de inconsistencia.
- **Auditoría:** las operaciones relevantes se registran en la Bitácora, consultable desde el módulo Administrador cuando la cuenta tiene permiso.
- **Respaldos:** los permisos y la configuración de SQL Server deben permitir las operaciones de respaldo y restauración de PadelGestDB.
- **Conexión:** comprobar que la instancia SQL Server, la base PadelGestDB, la variable PADELGEST_ENTORNO y los permisos de Windows sean compatibles.
- **Presentación:** utilizar las cuentas de prueba facilitadas para la evaluación, sin incorporar contraseñas a los archivos publicados.

Este LEEME describe **el estado del repositorio verificado en octubre de 2026**. Las funcionalidades previstas en la documentación académica pueden incorporarse en entregas posteriores.

---

**PadelGest — Trabajo de Diploma — UAI — 2026.**
