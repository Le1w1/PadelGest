using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UI.Servicios
{
    // Ayuda general por pantalla. No modifica el Designer.
    internal static class AyudaPantalla
    {
        private static readonly Dictionary<string, string[]> Textos =
            new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
            { "frmMenuPrincipal", new[] { "Desde el menú superior podés acceder a las operaciones de canchas, buffet, maestros, administración y reportes. Solo aparecen las funciones habilitadas para tu usuario. El menú Usuario permite cambiar de idioma, contraseña o sesión.", "Use the top menu to access court bookings, buffet sales, master records, administration and reports. Only features authorized for your account are displayed. The User menu lets you change language, password or session." } },
            { "frmLogin", new[] { "Ingresá con tu nombre de usuario y contraseña para abrir PadelGest. El sistema verifica la cuenta y la integridad de la base de datos antes de habilitar las operaciones.", "Enter your username and password to access PadelGest. The application checks your account and database integrity before enabling operations." } },
            { "frmCambiarClave", new[] { "Permite cambiar la contraseña de tu cuenta. Completá los datos solicitados y seleccioná Guardar. Si cancelás o volvés, no se modifica la contraseña.", "Change your account password. Fill in the required information and select Save. Cancelling or going back does not update your password." } },
            { "frmCambiarIdioma", new[] { "Elegí el idioma que querés utilizar en PadelGest y guardá el cambio. Las pantallas y las ayudas contextuales se muestran en el idioma activo.", "Choose the language to use in PadelGest and save the change. Screens and contextual help use the selected language." } },
            { "frmGestionDeUsuario", new[] { "Administra las cuentas que pueden ingresar al sistema. Permite crear y modificar usuarios, asignar roles, desbloquear cuentas y activar o desactivar accesos. Las operaciones disponibles dependen de tus permisos.", "Manage accounts allowed to sign in. Create or edit users, assign roles, unlock accounts, and enable or disable access. Available actions depend on your permissions." } },
            { "frmGestionarRolesYFamilias", new[] { "Administra el esquema de seguridad basado en roles, familias y permisos. Seleccioná el rol o la familia, modificá su composición y guardá los cambios. Los permisos determinan qué operaciones puede realizar cada usuario.", "Manage role-based security using roles, families and permissions. Select a role or family, edit its composition and save. Permissions determine which actions each user may perform." } },
            { "frmAuditarEventos", new[] { "Consulta los eventos de la bitácora para conocer qué operaciones se realizaron en el sistema. Utilizá los filtros para localizar registros y la opción de impresión o exportación para obtener un informe.", "Review the audit log to see operations performed in the system. Use filters to find entries and print or export an audit report." } },
            { "frmGestionarRespaldo", new[] { "Permite crear una copia de seguridad de la base de datos o restaurar una copia existente. Seleccioná una carpeta para respaldar o un archivo .bak para restaurar. La restauración reemplaza los datos actuales por los del respaldo.", "Create a database backup or restore an existing one. Choose a folder to back up or a .bak file to restore. Restoring replaces current database contents with the backed-up contents." } },
            { "frmRepararIntegridad", new[] { "Esta pantalla se utiliza cuando se detectan inconsistencias de integridad. Podés recalcular los dígitos verificadores o restaurar un respaldo válido. La reparación requiere autorización y deja registro de la operación.", "Use this screen when integrity inconsistencies are detected. You can recalculate verification digits or restore a valid backup. Repair requires authorization and is logged." } },
            { "frmGestionClientes", new[] { "Permite consultar, registrar, modificar y eliminar clientes según tus permisos. También podés exportar los clientes visibles en la grilla a XML o cargar un XML para visualizar sus datos. Los teléfonos se muestran según el permiso de desencriptación.", "View, register, edit and delete customers according to your permissions. You can also export customers in the grid to XML or load an XML to preview their details. Phone visibility depends on decryption permission." } },
            { "frmSeleccionarCliente", new[] { "Ingresá el DNI del cliente que realizará la operación. Al completar ocho dígitos, el sistema lo busca automáticamente. Si no existe y tenés permiso, se abre el registro de clientes. Confirmá el cliente para continuar.", "Enter the customer's ID number. Once eight digits are entered, the system searches automatically. If the customer is missing and you are authorized, customer registration opens. Confirm the customer to proceed." } },
            { "frmRegistrarCliente", new[] { "Completá DNI, nombre, apellido, teléfono y correo del nuevo cliente. El sistema valida los campos y evita registrar dos clientes con el mismo DNI. Seleccioná Registrar para guardar.", "Enter the new customer's ID, first name, last name, phone and email. The system validates the fields and prevents duplicate IDs. Select Register to save." } },
            { "frmGenerarReserva", new[] { "Seleccioná fecha, horario y cancha disponibles; después elegí el cliente y, si corresponde, el equipamiento. Verificá los datos antes de iniciar el cobro. Una reserva cobrada debe registrarse para completar la operación.", "Choose an available date, time and court; then select the customer and optional equipment. Review the details before payment. A paid booking must be registered to complete the process." } },
            { "frmAgregarEquipamiento", new[] { "Indicá cuántas paletas y pelotas se agregarán a la reserva, respetando los límites y las existencias. Confirmar devuelve las cantidades seleccionadas al proceso de reserva.", "Choose the number of rackets and balls for the booking, subject to limits and available stock. Confirm returns the selected quantities to the booking process." } },
            { "frmCobrarReserva", new[] { "Muestra el importe de la reserva y permite realizar el pago con tarjeta. Verificá el total antes de cobrar. Si el pago es aprobado, se debe continuar con el registro de la reserva.", "Shows the booking total and processes card payment. Check the amount before charging. If payment is approved, the booking must then be registered." } },
            { "frmRegistrarReserva", new[] { "Completa el registro de una reserva cuyo pago fue aprobado. Permite finalizar y, cuando está disponible, exportar el comprobante a PDF. No se debe abandonar la operación dejando una reserva cobrada sin registrar.", "Complete registration of a booking with approved payment. Finish the process and, when available, export the receipt as PDF. Do not leave a paid booking unregistered." } },
            { "frmGenerarFactura", new[] { "Armá una venta del buffet seleccionando productos y cantidades. Podés agregar, modificar o quitar artículos del carrito, consultar stock y seleccionar un cliente. Antes de cobrar, revisá el total.", "Build a buffet sale by selecting products and quantities. Add, change or remove items, review stock and select a customer. Check the total before payment." } },
            { "frmCobrarFactura", new[] { "Confirma el cobro con tarjeta de una factura del buffet. Verificá los datos de la operación y finalizá el pago. El sistema registra la factura y actualiza el stock cuando corresponde.", "Confirm card payment for a buffet invoice. Review the transaction details and complete payment. The system records the invoice and updates stock when applicable." } },
            { "frmFacturaRegistrada", new[] { "Muestra el resultado de una factura del buffet registrada. Permite generar su comprobante en PDF y finalizar la operación. Conservá el comprobante si necesitás respaldar la venta.", "Shows a registered buffet invoice. You can export its receipt as PDF and finish the process. Keep the receipt when proof of sale is needed." } },
            { "frmGestionProductos", new[] { "Administra el catálogo del buffet: alta, modificación, activación o desactivación de productos y reposición de stock. Seleccioná un producto de la grilla para editarlo. Las ventas descuentan existencias automáticamente.", "Manage the buffet catalog: create, edit, enable or disable products, and replenish stock. Select an item in the grid to edit. Sales automatically decrease inventory." } },
            { "frmAuditarCambios", new[] { "Consulta el historial de modificaciones de productos. Podés filtrar registros, revisar sus valores anteriores y, si estás autorizado, restaurar una versión anterior. Las restauraciones también quedan registradas.", "Review product change history. Filter entries, inspect previous values and, when authorized, restore an older version. Restorations are also recorded." } },
            { "frmReportePredictivo", new[] { "Estima la demanda del buffet a partir de las facturas pagadas durante los últimos 28 días completos. Muestra unidades vendidas, promedio diario, previsión a siete días, stock actual y reposición sugerida. El gráfico compara los ocho productos con mayor demanda estimada. Es una estimación orientativa: no genera compras ni modifica existencias.", "Estimate buffet demand using paid invoices from the previous 28 full days. The report shows units sold, daily average, seven-day forecast, current stock and suggested replenishment. The chart compares the eight products with the highest forecast demand. This is a planning estimate: no purchase orders are created and stock is not changed." } },
        };

        public static void Configurar(Form formulario)
        {
            if (!Textos.TryGetValue(formulario.GetType().Name, out string[]? descripcion))
                return;

            formulario.Shown += (_, _) =>
            {
                // Icono accesible en la esquina superior derecha.
                Button botonAyuda = new Button
                {
                    Name = "btnAyudaGeneral",
                    Text = "?",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.FromArgb(214, 246, 36),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(30, 30),
                    Location = formulario is frmLogin
                        ? new Point(12, 10)
                        : new Point(Math.Max(4, formulario.ClientSize.Width - 42),
                            formulario is frmMenuPrincipal ? 31 : 9),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    TabStop = true,
                    AccessibleName = "Ayuda de esta pantalla"
                };
                botonAyuda.FlatAppearance.BorderSize = 0;
                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(botonAyuda, TituloAyuda());
                botonAyuda.MouseEnter += (_, _) => tooltip.SetToolTip(botonAyuda, TituloAyuda());
                botonAyuda.Click += (_, _) =>
                {
                    string texto = Ingles() ? descripcion[1] : descripcion[0];
                    MessageBox.Show(formulario, texto, TituloAyuda(),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                formulario.Controls.Add(botonAyuda);
                botonAyuda.BringToFront();
                formulario.FormClosed += (_, _) => tooltip.Dispose();
            };
        }

        private static bool Ingles()
        {
            return string.Equals(Traductor.Instancia.CodigoIdiomaActual, "EN",
                StringComparison.OrdinalIgnoreCase);
        }

        private static string TituloAyuda()
        {
            return Ingles() ? "Screen help" : "Ayuda de esta pantalla";
        }
    }
}
