using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI.Servicios
{
    // Ayudas contextuales para los formularios: no altera los Designer ni la logica de negocio.
    internal static class AyudaContextual
    {
        private static readonly Dictionary<string, string[]> Descripciones =
            new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
            { "frmAuditarEventos.btnBuscar", new[] { "Busca eventos en la bitácora según los filtros seleccionados.", "Search audit-log events using the selected filters." } },
            { "frmAuditarEventos.btnLimpiar", new[] { "Limpia los filtros de búsqueda.", "Clear the search filters." } },
            { "frmAuditarEventos.btnVolver", new[] { "Regresa a la pantalla anterior.", "Return to the previous screen." } },
            { "frmAuditarEventos.btnImprimir", new[] { "Exporta a PDF los eventos filtrados.", "Export the filtered events to PDF." } },
            { "frmCambiarClave.btnGuardar", new[] { "Guarda la nueva contraseña del usuario.", "Save the user's new password." } },
            { "frmCambiarClave.btnLimpiar", new[] { "Borra los campos ingresados.", "Clear the entered fields." } },
            { "frmCambiarClave.btnVolver", new[] { "Regresa sin cambiar la contraseña.", "Return without changing the password." } },
            { "frmCambiarIdioma.btnGuardar", new[] { "Guarda el idioma seleccionado.", "Save the selected language." } },
            { "frmCambiarIdioma.btnVolver", new[] { "Regresa a la pantalla anterior.", "Return to the previous screen." } },
            { "frmGestionDeUsuario.btnCrearUsuario", new[] { "Crea un usuario con los datos ingresados.", "Create a user with the entered details." } },
            { "frmGestionDeUsuario.btnModificarUsuario", new[] { "Modifica los datos del usuario seleccionado.", "Update the selected user's details." } },
            { "frmGestionDeUsuario.btnDesbloquearUsuario", new[] { "Desbloquea la cuenta del usuario seleccionado.", "Unlock the selected user's account." } },
            { "frmGestionDeUsuario.btnActivarDesactivarUsuario", new[] { "Activa o desactiva el acceso del usuario seleccionado.", "Enable or disable the selected user's access." } },
            { "frmGestionDeUsuario.btnLimpiar", new[] { "Limpia los datos del formulario.", "Clear the form fields." } },
            { "frmGestionDeUsuario.btnVolver", new[] { "Regresa al menú anterior.", "Return to the previous menu." } },
            { "frmGestionarRespaldo.btnExaminarCarpeta", new[] { "Selecciona la carpeta donde guardar el respaldo.", "Choose the folder where the backup will be saved." } },
            { "frmGestionarRespaldo.btnRealizarBackup", new[] { "Crea una copia de seguridad de la base de datos.", "Create a database backup." } },
            { "frmGestionarRespaldo.btnExaminarArchivo", new[] { "Selecciona un archivo .bak para restaurar.", "Choose a .bak file to restore." } },
            { "frmGestionarRespaldo.btnRestaurar", new[] { "Restaura la base de datos desde el respaldo seleccionado.", "Restore the database from the selected backup." } },
            { "frmGestionarRolesYFamilias.btnCrear", new[] { "Crea el rol o la familia con su composición de permisos.", "Create a role or family with its permission composition." } },
            { "frmGestionarRolesYFamilias.btnEliminarSeleccionado", new[] { "Elimina el elemento seleccionado de la composición.", "Remove the selected item from the composition." } },
            { "frmGestionarRolesYFamilias.btnGuardarCambios", new[] { "Guarda los cambios realizados en el rol o la familia.", "Save changes to the role or family." } },
            { "frmGestionarRolesYFamilias.btnCancelar", new[] { "Cancela la operación actual.", "Cancel the current operation." } },
            { "frmGestionarRolesYFamilias.btnEliminar", new[] { "Elimina el rol o la familia seleccionados.", "Delete the selected role or family." } },
            { "frmLogin.btnIngresar", new[] { "Inicia sesión con el usuario y la contraseña indicados.", "Sign in using the supplied credentials." } },
            { "frmLogin.btnSalir", new[] { "Cierra PadelGest.", "Exit PadelGest." } },
            { "frmLogin.btnVolver", new[] { "Regresa a la pantalla anterior.", "Return to the previous screen." } },
            { "frmRepararIntegridad.btnRestaurar", new[] { "Abre la restauración de un respaldo para recuperar la integridad.", "Open backup restoration to recover data integrity." } },
            { "frmRepararIntegridad.btnRecalcular", new[] { "Recalcula los dígitos verificadores de las tablas protegidas.", "Recalculate verification digits for protected tables." } },
            { "frmRepararIntegridad.btnCancelar", new[] { "Cierra la reparación sin realizar cambios.", "Close repair without making changes." } },
            { "frmAgregarEquipamiento.btnConfirmar", new[] { "Confirma las cantidades de paletas y pelotas para la reserva.", "Confirm the quantities of rackets and balls for the booking." } },
            { "frmAgregarEquipamiento.btnVolver", new[] { "Regresa sin confirmar el equipamiento.", "Return without confirming equipment." } },
            { "frmAuditarCambios.btnBuscar", new[] { "Busca cambios de productos según los filtros.", "Search product changes using the selected filters." } },
            { "frmAuditarCambios.btnLimpiar", new[] { "Limpia los filtros aplicados.", "Clear the applied filters." } },
            { "frmAuditarCambios.btnVolver", new[] { "Regresa al menú anterior.", "Return to the previous menu." } },
            { "frmAuditarCambios.btnRestaurar", new[] { "Restaura los valores anteriores del cambio seleccionado.", "Restore previous values from the selected change." } },
            { "frmCobrarFactura.btnConfirmar", new[] { "Confirma el pago con tarjeta del buffet.", "Confirm the buffet card payment." } },
            { "frmCobrarFactura.btnVolver", new[] { "Regresa sin confirmar el pago.", "Return without confirming payment." } },
            { "frmCobrarReserva.btnCobrar", new[] { "Procesa el pago con tarjeta de la reserva.", "Process the booking's card payment." } },
            { "frmCobrarReserva.btnVolver", new[] { "Regresa a la reserva sin cobrar.", "Return to the booking without charging." } },
            { "frmFacturaRegistrada.btnExportarPdf", new[] { "Genera el comprobante de la factura en PDF.", "Generate a PDF copy of the invoice." } },
            { "frmFacturaRegistrada.btnFinalizar", new[] { "Finaliza la operación y cierra el comprobante.", "Finish the operation and close the receipt." } },
            { "frmGenerarFactura.btnAgregar", new[] { "Agrega el producto seleccionado a la venta.", "Add the selected product to the sale." } },
            { "frmGenerarFactura.btnModificar", new[] { "Modifica la cantidad del producto seleccionado.", "Change the quantity of the selected product." } },
            { "frmGenerarFactura.btnQuitar", new[] { "Quita el producto seleccionado de la venta.", "Remove the selected product from the sale." } },
            { "frmGenerarFactura.btnActualizarProductos", new[] { "Actualiza el listado y stock de productos.", "Refresh the product list and stock." } },
            { "frmGenerarFactura.btnSeleccionarCliente", new[] { "Busca o registra el cliente de la venta.", "Find or register the customer for this sale." } },
            { "frmGenerarFactura.btnCobrar", new[] { "Inicia el cobro de la factura del buffet.", "Start payment for the buffet invoice." } },
            { "frmGenerarFactura.btnVolver", new[] { "Regresa al menú anterior.", "Return to the previous menu." } },
            { "frmGenerarReserva.btnBuscar", new[] { "Consulta los turnos disponibles según los filtros.", "Search available booking slots using the filters." } },
            { "frmGenerarReserva.btnConfirmarTurno", new[] { "Confirma la cancha, fecha y horario seleccionados.", "Confirm the selected court, date and time." } },
            { "frmGenerarReserva.btnAgregarEquipamiento", new[] { "Permite indicar paletas y pelotas para la reserva.", "Choose rackets and balls for the booking." } },
            { "frmGenerarReserva.btnContinuar", new[] { "Avanza al siguiente paso de la reserva.", "Continue to the next booking step." } },
            { "frmGenerarReserva.btnCobrarReserva", new[] { "Inicia el cobro con tarjeta de la reserva.", "Start the booking's card payment." } },
            { "frmGenerarReserva.btnReintentarRegistro", new[] { "Reintenta registrar la reserva después de un cobro aprobado.", "Retry booking registration after an approved payment." } },
            { "frmGenerarReserva.btnVolver", new[] { "Regresa al paso anterior cuando está permitido.", "Return to the previous step when allowed." } },
            { "frmGestionClientes.btnElegirExportar", new[] { "Elige la ubicación del archivo XML de salida.", "Choose the destination for the XML export." } },
            { "frmGestionClientes.btnElegirImportar", new[] { "Selecciona un archivo XML de clientes para visualizar.", "Select a customer XML file to preview." } },
            { "frmGestionClientes.btnSerializar", new[] { "Guarda todos los clientes cargados en la grilla en un XML.", "Export all customers loaded in the grid to XML." } },
            { "frmGestionClientes.btnDeserializar", new[] { "Carga los clientes del XML en la grilla sin modificar SQL Server.", "Load XML customers into the grid without changing SQL Server." } },
            { "frmGestionClientes.btnLimpiarRutas", new[] { "Limpia las rutas de importación y exportación.", "Clear the import and export paths." } },
            { "frmGestionClientes.btnNuevo", new[] { "Limpia los datos para crear un cliente nuevo.", "Clear the fields to create a new customer." } },
            { "frmGestionClientes.btnRegistrar", new[] { "Guarda un cliente nuevo en la base de datos.", "Save a new customer to the database." } },
            { "frmGestionClientes.btnModificar", new[] { "Actualiza los datos del cliente seleccionado.", "Update the selected customer's details." } },
            { "frmGestionClientes.btnEliminar", new[] { "Elimina el cliente seleccionado si no tiene operaciones asociadas.", "Delete the selected customer if it has no linked transactions." } },
            { "frmGestionClientes.btnActualizar", new[] { "Recarga el listado de clientes desde la base de datos.", "Reload customers from the database." } },
            { "frmGestionClientes.btnVolver", new[] { "Regresa al menú anterior.", "Return to the previous menu." } },
            { "frmGestionProductos.btnActualizar", new[] { "Actualiza el catálogo de productos.", "Refresh the product catalog." } },
            { "frmGestionProductos.btnNuevo", new[] { "Limpia los campos para un producto nuevo.", "Clear the fields to create a new product." } },
            { "frmGestionProductos.btnCrear", new[] { "Registra un producto nuevo.", "Register a new product." } },
            { "frmGestionProductos.btnModificar", new[] { "Modifica los datos del producto seleccionado.", "Update the selected product." } },
            { "frmGestionProductos.btnEstado", new[] { "Cambia el estado activo del producto seleccionado.", "Change the selected product's active status." } },
            { "frmGestionProductos.btnReponer", new[] { "Registra la reposición de unidades en el stock.", "Record a stock replenishment." } },
            { "frmGestionProductos.btnVolver", new[] { "Regresa al menú anterior.", "Return to the previous menu." } },
            { "frmRegistrarCliente.btnRegistrar", new[] { "Registra un cliente con los datos ingresados.", "Register a customer with the entered information." } },
            { "frmRegistrarCliente.btnVolver", new[] { "Regresa sin registrar el cliente.", "Return without registering the customer." } },
            { "frmRegistrarReserva.btnFinalizar", new[] { "Finaliza el registro de la reserva cobrada.", "Finish registering the paid booking." } },
            { "frmRegistrarReserva.btnExportarPdf", new[] { "Genera el comprobante de la reserva en PDF.", "Generate a PDF booking receipt." } },
            { "frmSeleccionarCliente.btnBuscar", new[] { "Busca un cliente por DNI; con ocho dígitos se realiza automáticamente.", "Search a customer by ID; eight digits trigger automatic search." } },
            { "frmSeleccionarCliente.btnConfirmar", new[] { "Confirma el cliente seleccionado para continuar.", "Confirm the selected customer to proceed." } },
            { "frmSeleccionarCliente.btnRegistrarCliente", new[] { "Abre el registro de un cliente que no existe.", "Open registration for a customer not found." } },
            { "frmSeleccionarCliente.btnVolver", new[] { "Regresa sin seleccionar un cliente.", "Return without selecting a customer." } },
            // Ayudas de los cinco menus principales de la barra superior.
            { "frmMenuPrincipal.mnuSesion", new[] { "Gestiona tu sesión, contraseña e idioma.", "Manage your session, password and language." } },
            { "frmMenuPrincipal.RecepcionistaToolStripMenuItem", new[] { "Accede a la funcion para reservar canchas de pádel.", "Access padel court booking function." } },
            { "frmMenuPrincipal.vendedorBuffetToolStripMenuItem", new[] { "Accede a la seccion de facturación de productos del buffet.", "Access buffet product sales." } },
            { "frmMenuPrincipal.mnuMaestros", new[] { "Administra los maestros de clientes, productos y la bitacora de cambios de productos.", "View and manage customer and product master records." } },
            { "frmMenuPrincipal.mnuAdministrador", new[] { "Accede a la gestion de usuarios, roles, bitácoras de eventos y respaldos según tus permisos.", "Access users, roles, audit logs and backups according to your permissions." } },
            { "frmMenuPrincipal.reportePredictivoToolStripMenuItem", new[] { "Consulta la demanda estimada del buffet y la reposición sugerida.", "View forecast buffet demand and suggested replenishment." } },
            { "frmReportePredictivo.btnActualizar", new[] { "Vuelve a calcular el pronóstico con las ventas registradas.", "Recalculate the forecast using recorded sales." } },
            { "frmReportePredictivo.btnVolver", new[] { "Regresa al menú principal.", "Return to the main menu." } },
            { "frmMenuPrincipal.reLoginToolStripMenuItem", new[] { "Permite iniciar sesión con otro usuario.", "Sign in as another user." } },
            { "frmMenuPrincipal.cambiarClaveToolStripMenuItem", new[] { "Cambia tu contraseña de acceso.", "Change your login password." } },
            { "frmMenuPrincipal.cambiarIdiomaToolStripMenuItem", new[] { "Selecciona el idioma de la aplicación.", "Change the application's language." } },
            { "frmMenuPrincipal.cerrarSesionToolStripMenuItem", new[] { "Cierra la sesión activa.", "Sign out of the current session." } },
            { "frmMenuPrincipal.generarReservaToolStripMenuItem", new[] { "Inicia una reserva de cancha de pádel.", "Start a padel court booking." } },
            { "frmMenuPrincipal.generarCarritoToolStripMenuItem", new[] { "Inicia una facturacion de productos del buffet.", "Start a buffet product sale." } },
            { "frmMenuPrincipal.gestionarProductosToolStripMenuItem", new[] { "Abre el maestro de productos.", "Open the product master." } },
            { "frmMenuPrincipal.auditarCambiosToolStripMenuItem", new[] { "Consulta la bitacora de cambios de los productos registrados.", "Review recorded product changes." } },
            { "frmMenuPrincipal.gestionarClientesToolStripMenuItem", new[] { "Abre el maestro de clientes.", "Open the customer master." } },
            { "frmMenuPrincipal.usuariosToolStripMenuItem", new[] { "Administra los usuarios y sus roles.", "Manage users and their roles." } },
            { "frmMenuPrincipal.bitacoraEventosToolStripMenuItem", new[] { "Consulta y exporta la bitácora de eventos.", "Review and export the event log." } },
            { "frmMenuPrincipal.gestionarPerfilToolStripMenuItem", new[] { "Administra roles, familias y permisos.", "Manage roles, families and permissions." } },
            { "frmMenuPrincipal.gestionDeRespaldoToolStripMenuItem", new[] { "Gestiona copias de seguridad y restauración.", "Manage database backup and restoration." } },
        };

        public static void Configurar(Form formulario)
        {
            ToolTip ayuda = new ToolTip
            {
                AutomaticDelay = 350,
                AutoPopDelay = 10000,
                ReshowDelay = 150,
                ShowAlways = true
            };

            // Shown garantiza que la interfaz y los menus dinamicos ya estan construidos.
            formulario.Shown += (_, _) =>
            {
                foreach (Control control in Recorrer(formulario))
                {
                    if (control is Button boton && Descripciones.TryGetValue(formulario.GetType().Name + "." + boton.Name, out string[] textos))
                    {
                        ayuda.SetToolTip(boton, Traducir(textos));
                        boton.MouseEnter += (_, _) => ayuda.SetToolTip(boton, Traducir(textos));
                    }
                    if (control is MenuStrip menu)
                    {
                        menu.ShowItemToolTips = true;
                        ConfigurarMenu(menu.Items, formulario.GetType().Name);
                    }
                }
            };

            formulario.FormClosed += (_, _) => ayuda.Dispose();
        }

        private static void ConfigurarMenu(ToolStripItemCollection items, string nombreFormulario)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem opcion)
                {
                    if (Descripciones.TryGetValue(nombreFormulario + "." + opcion.Name, out string[] textos))
                    {
                        opcion.ToolTipText = Traducir(textos);
                        opcion.MouseEnter += (_, _) => opcion.ToolTipText = Traducir(textos);
                    }
                    ConfigurarMenu(opcion.DropDownItems, nombreFormulario);
                }
            }
        }

        // Recorrido recursivo de todos los controles hijos de un control padre.
        //yield return permite devolver un elemento a la vez, y el foreach que lo llama lo recibe uno por uno.
        private static IEnumerable<Control> Recorrer(Control padre)
        {
            foreach (Control control in padre.Controls)
            {
                yield return control;
                foreach (Control hijo in Recorrer(control))
                    yield return hijo;
            }
        }

        private static string Traducir(string[] textos)
        {
            bool ingles = string.Equals(Traductor.Instancia.CodigoIdiomaActual,"EN", StringComparison.OrdinalIgnoreCase);
            return textos[ingles ? 1 : 0];
        }
    }
}
