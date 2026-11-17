# PadelGest | Sistema de gestión para clubes de pádel

**Proyecto académico de Ingeniería en Sistemas Informáticos — Universidad Abierta Interamericana (UAI), 2026.**

PadelGest es una aplicación de escritorio desarrollada en **C# y .NET 9 con Windows Forms y SQL Server**, orientada a centralizar procesos operativos y administrativos de **AirPadel**, un club de pádel de General San Martín, Buenos Aires. Integra gestión de reservas, alquiler de equipamiento, ventas del buffet, clientes, control de stock, seguridad, auditoría y herramientas de análisis de información.

> **Versión de referencia:** entrega 3, integrada en `master` en noviembre de 2026. El repositorio contiene una implementación académica con operaciones bancarias **simuladas**; no es una pasarela de cobro real ni un producto comercial certificado.

## Tecnologías

| Área | Tecnologías |
| --- | --- |
| Desarrollo | C#, .NET 9, programación orientada a objetos |
| Interfaz | Windows Forms |
| Persistencia | Microsoft SQL Server 2019 o superior, Microsoft.Data.SqlClient, SQL |
| Arquitectura | Separación por capas: UI, BE, BLL, DAL y Servicios |
| Modelado | Casos de uso, diagramas UML, modelo entidad-relación |
| Documentación y soporte | Git/GitHub, archivos JSON para idiomas, generación de PDF |
| Instalación | Inno Setup, PowerShell y sqlcmd |

## Funcionalidades de la entrega 3

### PN1 — Reservas de canchas

- Consulta de disponibilidad y selección de cancha, fecha, horario y tarifa.
- Búsqueda, selección y registro de clientes.
- Incorporación de equipamiento (paletas y pelotas) a la operación, con validaciones.
- Cobro con tarjeta mediante respuesta bancaria **simulada** (aprobación o rechazo).
- Registro de reserva y emisión del comprobante según el flujo definido.
- Controles y manejo de errores para proteger la consistencia del proceso de reserva.

### PN2 — Venta de productos del buffet

- Selección de productos y armado del detalle de la factura.
- Validación de cantidades, precios, disponibilidad y stock.
- Asociación de un cliente registrado.
- Cobro con tarjeta mediante respuesta bancaria **simulada**.
- Registro de factura, detalle de venta y actualización de existencias.
- Persistencia mediante transacción SQL; la venta, el stock, los dígitos verificadores y la bitácora se coordinan para evitar registros parciales.
- Generación de comprobantes en PDF.

### Maestros y administración

- Gestión de clientes y productos mediante pantallas de mantenimiento.
- Registro y consulta de cambios en productos.
- Inicio/cierre de sesión, cambio de contraseña, gestión de usuarios y control de bloqueos.
- Gestión de roles, familias y permisos, con acceso condicionado por los permisos efectivos del usuario.
- Cambio de idioma español/inglés y actualización de pantallas.
- Bitácora de eventos, auditoría de cambios y exportaciones de auditoría.
- Control de integridad mediante dígitos verificadores horizontales y verticales (DVH/DVV), con mecanismos de reparación.
- Backup y restore de la base de datos SQL Server.

### Reportes y apoyo a decisiones

La aplicación incorpora un **reporte predictivo de demanda y reposición del buffet**:

- Consulta las ventas de los últimos **28 días calendario completos**.
- Calcula promedio diario y estimación de unidades para los próximos **7 días** mediante extrapolación del histórico.
- Estima cobertura de stock, necesidades de reposición e ingreso potencial a precios vigentes.
- Presenta indicadores y vistas por productos, reposición y categorías.

**Alcance del análisis:** es una proyección cuantitativa basada en promedios históricos; **no utiliza un modelo de machine learning**, no genera órdenes de compra automáticamente y no garantiza ventas futuras.

## Arquitectura

```text
PadelGest/
├── UI/          Formularios Windows Forms y presentación
├── BE/          Entidades de negocio
├── BLL/         Reglas de negocio y validaciones
├── DAL/         Consultas, transacciones y persistencia SQL Server
├── Servicios/   Sesión, permisos, idioma y servicios transversales
├── Script/      Scripts de instalación y datos de demostración
├── Instalador/  Proyecto Inno Setup y procedimientos auxiliares
└── PadelGest.slnx
```

La estructura favorece la separación de responsabilidades entre la interfaz, los modelos del dominio, las reglas de negocio, el acceso a datos y los servicios compartidos.

**Patrones de diseño usados:** Singleton (gestión de sesión y traducción), Observer (notificación de cambios de idioma) y Composite (jerarquías de roles, familias y permisos).

## Instalación y ejecución

### Requisitos

- Windows 10/11 de 64 bits.
- SQL Server 2019 o superior en funcionamiento.
- Para desarrollo: Visual Studio 2022 compatible con .NET 9 y .NET 9 SDK.
- Para el instalador: `sqlcmd` disponible y permisos de SQL Server para crear la base de datos. El instalador **no instala SQL Server**.

### Alternativa A — Desde el código fuente

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/Le1w1/PadelGest.git
   ```
2. Preparar una **instancia de pruebas** de SQL Server; no ejecutar los scripts sobre una base con información importante.
3. Para una instalación limpia, revisar y ejecutar `Script/PadelGest_Instalacion_Limpia_SQL2019.sql`. Para un entorno con datos de demostración, revisar `Script/PadelGest_Demostracion_2019_Exportacion_Completa.sql`. Son alternativas para distintos escenarios, **no dos pasos consecutivos sobre la misma base**.
4. Configurar la instancia SQL Server utilizada por la aplicación mediante `PADELGEST_ENTORNO`.
5. Abrir `PadelGest.slnx`, restaurar paquetes NuGet, establecer `UI` como proyecto de inicio y compilar/ejecutar.

### Configuración SQL Server

El código de conexión se encuentra en `DAL/Servicios/DAO_AccesoDatos.cs`. Utiliza autenticación integrada de Windows sobre `PadelGestDB`.

| `PADELGEST_ENTORNO` | Instancia |
| --- | --- |
| `UAI` | `.` (instancia local predeterminada) |
| `LEO` | `localhost\SQLEXPRESS` |

**Si la variable no está definida, actualmente el código utiliza `UAI`**. Para otro servidor es necesario adaptar la configuración. Ejemplo en PowerShell, para la sesión actual:

```powershell
$env:PADELGEST_ENTORNO = "LEO"
```

### Alternativa B — Instalador

El proyecto `Instalador/PadelGest.iss` está preparado para generar un instalador de Windows con Inno Setup. Consultar **[la guía específica del instalador](Instalador/README.md)** para compilación, reinstalación, respaldo y desinstalación. El instalador incluido en el repositorio **debe reconstruirse y probarse** en el entorno de destino antes de distribuirlo como release.

## Seguridad, integridad y límites

- Acceso por permisos efectivos y jerarquías de roles.
- Registro de eventos y auditoría de modificaciones.
- DVH/DVV para detectar inconsistencias en datos protegidos.
- Operaciones de respaldo y restauración sujetas a permisos.
- Simulación de aprobaciones/rechazos de tarjeta: **no se realizan cargos bancarios reales**.
- El mecanismo de hash de contraseñas del proyecto es académico y no debe interpretarse como una auditoría o certificación de seguridad para uso productivo.

El código y los scripts deben utilizarse con **datos de prueba**. No publicar credenciales, números de tarjetas reales ni información personal en capturas, incidencias o demostraciones.

## Galería de capturas

Se incorporarán capturas **reales de la aplicación** para mostrar: menú principal, reservas, facturación, buffet, control de accesos, auditorías y reporte predictivo. La ausencia de capturas en este README no implica que se hayan generado imágenes ficticias.

## Estado y contexto

**Entrega 3 finalizada e integrada en `master`.** El proyecto se desarrolló para la materia Trabajo de Diploma de la carrera de Ingeniería en Sistemas Informáticos de la UAI. Este README describe las funcionalidades identificadas en el código de la entrega; no sustituye pruebas de compilación, instalación ni aceptación en un equipo Windows.

**Autor:** Leonel Rodríguez — Universidad Abierta Interamericana (2026).
