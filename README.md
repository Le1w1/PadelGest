# PadelGest

Sistema de gestión desarrollado para **AirPadel**, un club de pádel ubicado en General San Martín, Provincia de Buenos Aires. El proyecto busca centralizar y ordenar la operatoria del club, reemplazando registros manuales por una solución de escritorio con control de accesos, auditoría e integridad de datos.

## Información académica

| Dato | Valor |
|---|---|
| **Sistema** | PadelGest |
| **Organización** | AirPadel |
| **Materia** | Trabajo de Diploma |
| **Carrera** | Ingeniería en Sistemas Informáticos |
| **Comisión** | 3-B |
| **Turno** | Mañana |
| **Sede** | Centro |
| **Año** | 2026 |
| **Universidad** | UAI – Universidad Abierta Interamericana |
| **Alumno** | Rodríguez Leonel Jesús |
| **Docente** | Leonel Jimenez Gamboa |

## Objetivo del sistema

PadelGest está orientado a la administración de las actividades principales de AirPadel. El alcance funcional contempla la gestión de reservas de canchas y clientes, la venta de productos del buffet, la consulta y administración de canchas, y las funciones administrativas y técnicas necesarias para operar el sistema de forma segura y trazable.

Actualmente el repositorio contiene la infraestructura técnica del sistema y la base de control de accesos sobre la cual se implementarán progresivamente los procesos de negocio.

## Tecnologías utilizadas

- **Lenguaje:** C#
- **Framework:** .NET 9.0
- **Interfaz gráfica:** Windows Forms
- **Base de datos:** Microsoft SQL Server
- **Acceso a datos:** `Microsoft.Data.SqlClient`
- **IDE:** Microsoft Visual Studio 2022
- **Internacionalización:** archivos JSON (`es.json` y `en.json`)
- **Generación de PDF:** iTextSharp
- **Hash de contraseñas:** SHA-256

## Arquitectura

El proyecto utiliza una arquitectura en capas distribuida en los siguientes proyectos:

| Proyecto | Responsabilidad |
|---|---|
| `UI` | Formularios Windows Forms, interacción con el usuario y control visual de permisos |
| `BLL` | Reglas de negocio, validaciones, auditoría, integridad, respaldos y gestión de usuarios/perfiles |
| `DAL` | Acceso a SQL Server, consultas, persistencia y operaciones de backup/restore |
| `Servicios` | Servicios transversales como sesión, traducción, criptografía y componentes del modelo de permisos |
| `BE` | Capa destinada a las entidades del dominio funcional de PadelGest |

La capa `BE` se encuentra preparada para incorporar las entidades del negocio a medida que avance la implementación de los procesos funcionales.

## Patrones de diseño

El sistema implementa actualmente los siguientes patrones:

- **Singleton:** utilizado en `SM` para la sesión activa y en `Traductor` para el servicio de idiomas.
- **Observer:** permite actualizar dinámicamente los formularios abiertos cuando el usuario cambia el idioma.
- **Composite:** utilizado en el esquema de autorización mediante `Rol`, `Familia` y `PermisoSimple`, permitiendo construir jerarquías de permisos reutilizables.

## Seguridad y control de acceso

PadelGest utiliza un esquema de autorización basado en permisos simples agrupados mediante Familias y Roles. Cada usuario posee un Rol, pero las operaciones habilitadas se determinan por sus permisos efectivos.

El sistema permite de esta forma que un usuario tenga acceso únicamente a las operaciones que le corresponden. Los formularios y acciones verifican permisos específicos mediante la sesión activa, evitando depender únicamente del nombre del Rol.

### Roles definidos actualmente

- Administrador
- Dueño
- Recepcionista
- Vendedor de Buffet
- Encargado de Canchas

### Áreas de permisos

Actualmente existen permisos para:

- Sesión y cambio de credenciales.
- Gestión de usuarios.
- Auditoría de bitácora.
- Gestión de Roles, Familias y Permisos.
- Gestión de respaldos.
- Reservas de canchas.
- Gestión de clientes.
- Venta y consulta de stock del buffet.
- Consulta de agenda de canchas.

## Módulos técnicos implementados

| Módulo | Funcionalidad |
|---|---|
| **Sesión** | Inicio y cierre de sesión, re-login, cambio de contraseña y bloqueo por intentos fallidos |
| **Usuarios** | Alta, modificación, activación/desactivación, desbloqueo y asignación de Rol |
| **Roles y Familias** | Gestión del modelo Composite y asignación jerárquica de permisos |
| **Idiomas** | Cambio dinámico entre Español e Inglés mediante Observer |
| **Bitácora** | Registro y auditoría de eventos relevantes del sistema |
| **PDF** | Exportación de resultados de auditoría |
| **Dígitos verificadores** | Control de integridad mediante DVH y DVV |
| **Respaldos** | Creación y restauración de backups de la base de datos con control de permisos |

## Funcionalidades del negocio en desarrollo

Los procesos principales definidos para PadelGest son:

- **Reserva de Canchas:** clientes, disponibilidad, tarifas, reservas, pagos y comprobantes.
- **Venta de Productos del Buffet:** productos, ventas y control de stock.
- **Gestión de Canchas:** consulta de agenda y posteriores operaciones de bloqueo/mantenimiento.

La implementación funcional se incorporará de manera incremental sobre la infraestructura ya existente.

## Integridad de datos

El sistema implementa Dígitos Verificadores Horizontales (**DVH**) y Verticales (**DVV**) para detectar modificaciones no autorizadas sobre información crítica.

La lógica de integridad se encuentra centralizada en `DigitoVerificadorBLL` y contempla tanto registros individuales como el conjunto de registros de las tablas protegidas.

## Bitácora

Las operaciones relevantes generan eventos de auditoría con información como:

- Usuario.
- Fecha y hora.
- Módulo.
- Acción realizada.
- Criticidad.
- Resultado.
- Descripción.

La bitácora puede consultarse mediante filtros y exportarse a PDF.

## Respaldos de base de datos

El módulo de respaldos permite ejecutar operaciones `BACKUP DATABASE` y `RESTORE DATABASE` sobre `PadelGestDB`.

El acceso se encuentra protegido mediante el permiso:

```text
BAK_GESTIONAR
```

Los archivos de backup generados utilizan el formato:

```text
PadelGestDB_Backup_yyyyMMdd_HHmmss.bak
```

## Base de datos

La base utilizada por el sistema es:

```text
PadelGestDB
```

El script disponible actualmente se encuentra en:

```text
Script/PadelGestDB.sql
```

El script contiene la estructura de tablas, relaciones y datos utilizados por el entorno académico actual del proyecto.

## Instalación

### Requisitos

- Windows 10 u 11.
- Microsoft SQL Server 2019 o superior.
- SQL Server Management Studio.
- .NET 9.0 SDK.
- Microsoft Visual Studio 2022 o compatible con .NET 9.0.

### Pasos

1. Clonar o descargar el repositorio.
2. Crear en SQL Server una base de datos llamada `PadelGestDB`.
3. Ejecutar `Script/PadelGestDB.sql` sobre esa base.
4. Verificar la configuración de conexión en `DAL/DAO_AccesoDatos.cs`.
5. Abrir `PadelGest.slnx` en Visual Studio 2022.
6. Establecer `UI` como proyecto de inicio.
7. Compilar la solución y ejecutar la aplicación.

### Configuración de conexión

El acceso a datos utiliza la variable de entorno:

```text
PADELGEST_ENTORNO
```

Actualmente se encuentran configurados los siguientes entornos:

| Valor | SQL Server |
|---|---|
| `LEO` | `localhost\SQLEXPRESS` |
| `UAI` | `.` |

Si la variable no está definida, el sistema utiliza `LEO` como valor predeterminado. Para ejecutar el proyecto sobre otra instancia de SQL Server se deben adaptar las cadenas definidas en `DAL/DAO_AccesoDatos.cs`.

## Estructura del repositorio

```text
PadelGest/
├── BE/                    # Entidades de negocio
├── BLL/                   # Lógica y reglas del sistema
├── DAL/                   # Acceso a datos
├── Servicios/             # Servicios transversales
├── UI/                    # Aplicación Windows Forms
│   └── Recursos/Idiomas/  # Archivos es.json y en.json
├── Script/
│   └── PadelGestDB.sql    # Script de base de datos
├── PadelGest.slnx         # Solución de Visual Studio
└── README.md
```

## Estado del proyecto

PadelGest se encuentra **en desarrollo**. La infraestructura técnica de seguridad, perfiles, internacionalización, auditoría, integridad y respaldos ya está implementada. Los módulos funcionales correspondientes a los procesos de negocio de AirPadel se incorporarán en las próximas etapas del Trabajo de Diploma.

---

**PadelGest — Trabajo de Diploma, Ingeniería en Sistemas Informáticos, UAI 2026.**
