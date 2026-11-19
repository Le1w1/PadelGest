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
        // Título ES, título EN, instrucciones ES e instrucciones EN.
        // Cada línea es una viñeta independiente, sin secciones adicionales.
        private static readonly Dictionary<string, string[]> Textos =
            new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
           
                { "frmMenuPrincipal", new[] { "Menú principal", "Main menu", "Desde este formulario podés acceder a las distintas funciones del programa.\nSeleccioná una opción del menú para acceder a sus funcionalidades.\nLas opciones disponibles dependen de tu rol en el sistema.", "Use this screen to access the different features of the program.\nSelect a menu option to access its functionalities.\nAvailable options depend on your role in the system." } },
          
                { "frmLogin", new[] { "Inicio de sesión", "Sign in", "Este formulario se utiliza para ingresar a PadelGest.\nEscribí tu usuario y contraseña, luego presioná el botón Ingresar.", "Use this screen to sign in to PadelGest.\nEnter your username and password, then select Sign in." } },
           
                { "frmCambiarClave", new[] { "Cambiar contraseña", "Change password", "Este formulario permite cambiar la contraseña de tu cuenta.\nIngresá tu contraseña actual y la nueva contraseña, confirmala y presioná Guardar.", "Use this screen to change your account password.\nEnter your current password and the new password, confirm it and select Save." } },
          
                { "frmCambiarIdioma", new[] { "Cambiar idioma", "Change language", "Este formulario permite cambiar el idioma del programa.\nSeleccioná Español o Inglés y presioná Guardar.", "Use this screen to change the program language.\nSelect Spanish or English and choose Save." } },
          
                { "frmGestionDeUsuario", new[] { "Gestión de usuarios", "User management", "Este formulario permite administrar las cuentas de los usuarios que utilizan el programa.\nPodés crear o modificar usuarios, asignarles roles, habilitar o bloquear su acceso.\nSeleccioná un usuario de la lista para realizar cambios.", "Use this screen to manage the accounts of users who use the program.\nYou can create or edit users, assign roles and enable or block access.\nSelect a user from the list to make changes." } },
          
                { "frmGestionarRolesYFamilias", new[] { "Gestión de roles y permisos", "Roles and permissions", "Este formulario permite elegir qué funciones puede utilizar cada rol asignable a los usuarios.\nSeleccioná un rol o familia, realizá los cambios necesarios y presioná Guardar.", "Use this screen to choose which features each group of users can access.\nSelect a role or group, make the changes you need and choose Save." } },
         
                { "frmAuditarEventos", new[] { "Consulta de actividades", "Activity history", "Este formulario permite consultar las actividades realizadas por los usuarios en el programa.\nSeleccioná los filtros y presioná Buscar para ver los resultados.\nTambién podés guardar un informe de las actividades encontradas.", "Use this screen to review activities carried out in the program by users.\nChoose the filters and select Search to view results.\nYou can also save a report of the activities found." } },
           
                { "frmGestionarRespaldo", new[] { "Copias de seguridad", "Backups", "Este formulario permite guardar una copia de seguridad de los datos del programa.\nPara crear una copia, elegí una carpeta y presioná Realizar respaldo.\nPara recuperar una copia anterior, seleccioná el archivo y presioná Restaurar. Esto reemplaza los datos actuales.", "Use this screen to save a backup copy of your program information.\nTo create a backup, choose a folder and select Back up.\nTo recover an earlier copy, choose the file and select Restore. This replaces current information." } },
          
                { "frmRepararIntegridad", new[] { "Reparar información", "Repair information", "Este formulario permite solucionar problemas detectados en la información guardada en la base de datos.\nPodés intentar repararla mediante un recalculo de digitos verifiacadores o recuperar una copia de seguridad (backup).", "Use this screen to fix problems found in saved information.\nYou can try to repair the information or restore a backup.\nIf you are unsure which option to use, ask the person in charge before continuing." } },
          
                { "frmGestionClientes", new[] { "Gestión de clientes", "Customer management", "Este formulario permite consultar y administrar los clientes registrados.\nPodés buscar un cliente, agregar uno nuevo o modificar sus datos.\nTambién podés guardar o consultar una lista de clientes serializados.", "Use this screen to view and manage registered customers.\nYou can search for a customer, add a new one or update their details.\nYou can also save or view a customer list serialized" } },
         
                { "frmSeleccionarCliente", new[] { "Seleccionar cliente", "Select customer", "Este formulario permite encontrar al cliente que realizará la operación.\nIngresá su DNI para buscarlo y confirmá el cliente encontrado.\nSi todavía no está registrado, podés agregarlo si tenés permiso.", "Use this screen to find the customer for the current operation.\nEnter their ID number to search, then confirm the matching customer.\nIf they are not registered, you can add them when permitted." } },
          
                { "frmRegistrarCliente", new[] { "Registrar cliente", "Register customer", "Este formulario permite agregar un nuevo cliente al programa.\nCompletá su DNI, nombre, apellido, teléfono y correo electrónico.\nRevisá la información y presioná Registrar.", "Use this screen to add a new customer.\nEnter their ID number, first name, last name, phone number and email.\nReview the information and select Register." } },
          
                { "frmGenerarReserva", new[] { "Generar reserva", "Create booking", "Este formulario permite generar una reserva de cancha.\nSeleccioná la fecha, el horario y la cancha disponible.\nElegí al cliente, agregá paletas o pelotas si las necesita y continuá con el cobro de la reserva.", "Use this screen to generate a court booking.\nChoose an available date, time and court.\nSelect the customer, add rackets or balls if needed and continue to payment." } },
         
                { "frmAgregarEquipamiento", new[] { "Agregar equipamiento", "Add equipment", "Este formulario permite agregar paletas y pelotas a una reserva.\nIndicá las cantidades que necesita el cliente y presioná Confirmar.\nSolo se puede seleccionar equipamiento disponible.", "Use this screen to add rackets and balls to a booking.\nEnter the quantities the customer needs and select Confirm.\nOnly available equipment can be selected." } },
           
                { "frmCobrarReserva", new[] { "Cobrar reserva", "Charge booking", "Este formulario permite cobrar una reserva con tarjeta.\nVerificá el total, ingresá los datos de la tarjeta y confirmá el cobro.\nSi el pago se aprueba, continuá con el registro de la reserva.", "Use this screen to charge a booking by card.\nCheck the total, enter the card details and confirm payment.\nOnce payment is approved, continue to register the booking." } },
          
                { "frmRegistrarReserva", new[] { "Registrar reserva", "Register booking", "Este formulario permite registrar una reserva que ya fue pagada.\nRevisá los datos y completá el registro de la reserva.\nSi está disponible, podés guardar una copia del comprobante en PDF.", "Use this screen to register a booking that has already been paid for.\nCheck the details and complete the booking registration.\nIf available, you can save a PDF copy of the receipt." } },
          
                { "frmGenerarFactura", new[] { "Venta de buffet", "Buffet sale", "Este formulario permite preparar una venta de productos del buffet.\nSeleccioná los productos y las cantidades solicitadas.\nSelecciona el cliente al cual le queres asignar la venta de productos.\nRevisá el pedido y el total antes de continuar con el cobro.", "Use this screen to prepare a buffet sale.\nSelect the products and quantities requested.\nSelect the customer who want you assign the sale \nCheck the order and total before proceeding to payment." } },
          
                { "frmCobrarFactura", new[] { "Cobrar venta de buffet", "Charge buffet sale", "Este formulario permite cobrar una venta de buffet con tarjeta.\nRevisá el importe, ingresá los datos de la tarjeta y confirmá el pago.\nAl completar la venta, el programa actualiza las cantidades disponibles.", "Use this screen to charge a buffet sale by card.\nCheck the amount, enter the card details and confirm payment.\nAfter the sale, the program updates the available quantities." } },
         
                { "frmFacturaRegistrada", new[] { "Factura registrada", "Registered invoice", "Este formulario muestra una venta de buffet que ya fue registrada.\nPodés guardar el comprobante en PDF mediante el botón Exportar PDF.\nCuando termines, cerrá la pantalla para finalizar.", "This screen shows a buffet sale that has already been registered.\nYou can save the receipt using the Export PDF button.\nWhen finished, close the screen." } },
         
                { "frmGestionProductos", new[] { "Gestión de productos", "Product management", "Este formulario permite administrar los productos del buffet.\nPodés consultar las cantidades disponibles, agregar productos, desactivar productos o modificar sus datos.\nSeleccioná un producto para realizar cambios o reponer mercadería.", "Use this screen to manage buffet products.\nYou can check available quantities, add products, disable products or edit their information.\nSelect a product to make changes or restock it." } },
        
                { "frmAuditarCambios", new[] { "Historial de cambios", "Change history", "Este formulario permite consultar los cambios historicos realizados en los productos.\nUtilizá los filtros para buscar un producto, revisar sus detalles y cambios historicos.", "Use this screen to review changes made to products.\nUse the filters to find a change and view its details." } },
      
                { "frmReportePredictivo", new[] { "Reporte inteligente", "Smart report", "Este formulario permite consultar las ventas del buffet y estimar qué productos conviene reponer.\nEn las pestañas podés ver el el Panorama general, Detalle por producto, Prioridades de reposición y el Analisis por Categorías.\nPresioná Exportar PDF para guardar una copia del informe con sus gráficos y resultados.", "Use this screen to review buffet sales and estimate which products may need restocking.\nUse the tabs to view the overview, products details, restocking priorities and categories.\nSelect Export PDF to save a copy of the report with its charts and results.\nFuture quantities are estimates: the report does not place orders." } },
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
                    Location = formulario is frmLogin? new Point(12, 10): new Point(Math.Max(4, formulario.ClientSize.Width - 42),formulario is frmMenuPrincipal ? 31 : 9),
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
                    bool ingles = Ingles();
                    MostrarAyuda(formulario, ingles ? descripcion[1] : descripcion[0],ingles ? descripcion[3] : descripcion[2]);

                };
                formulario.Controls.Add(botonAyuda);
                botonAyuda.BringToFront();
                formulario.FormClosed += (_, _) => tooltip.Dispose();
            };
        }

        // Muestra el nombre del formulario y sus indicaciones en una ventana compacta.
        private static void MostrarAyuda(Form propietario, string titulo, string descripcion)
        {
            bool ingles = Ingles();
            string[] puntos = descripcion.Split(new[] { '\n' },
                StringSplitOptions.RemoveEmptyEntries);
            const int ancho = 600;
            const int anchoTexto = 525;
            int altoContenido = 12;
            using Font fuenteTitulo = new Font("Segoe UI", 15F, FontStyle.Bold);
            using Font fuenteTexto = new Font("Segoe UI", 10F);

            using Form ventana = new Form
            {
                Text = TituloAyuda(),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                Font = fuenteTexto,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ShowIcon = false
            };
            TableLayoutPanel disposicion = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                Padding = new Padding(20, 10, 20, 10)
            };
            disposicion.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            disposicion.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            disposicion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            Label encabezado = new Label
            {
                Text = titulo,
                Dock = DockStyle.Fill,
                Font = fuenteTitulo,
                ForeColor = Color.FromArgb(24, 70, 138),
                TextAlign = ContentAlignment.MiddleLeft
            };
            RichTextBox contenido = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(35, 44, 60),
                Font = fuenteTexto,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                DetectUrls = false,
                TabStop = false
            };
            foreach (string punto in puntos)
            {
                string linea = "•  " + punto;
                contenido.AppendText(linea + Environment.NewLine + Environment.NewLine);
                int altura = TextRenderer.MeasureText(linea, fuenteTexto,new Size(anchoTexto, 2000),TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl).Height;
                altoContenido += Math.Max(23, altura) + 10;
            }
            contenido.SelectionStart = 0;
            contenido.ScrollToCaret();

            Button cerrar = new Button
            {
                Text = ingles ? "Close" : "Entendido",
                Dock = DockStyle.Right,
                Width = 120,
                BackColor = Color.FromArgb(24, 70, 138),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            cerrar.FlatAppearance.BorderSize = 0;
            cerrar.Click += (_, _) => ventana.Close();
            disposicion.Controls.Add(encabezado, 0, 0);
            disposicion.Controls.Add(contenido, 0, 1);
            disposicion.Controls.Add(cerrar, 0, 2);
            ventana.Controls.Add(disposicion);

            Rectangle area = Screen.FromControl(propietario).WorkingArea;
            int altoMaximo = Math.Max(230, area.Height - 80);
            int altoDeseado = altoContenido + 48 + 45 + 20 + 32;
            ventana.ClientSize = new Size(ancho, Math.Min(altoMaximo,Math.Max(180, altoDeseado)));
            ventana.AcceptButton = cerrar;
            ventana.ShowDialog(propietario);
        }

        private static bool Ingles()
        {
            return string.Equals(Traductor.Instancia.CodigoIdiomaActual, "EN",StringComparison.OrdinalIgnoreCase);
        }

        private static string TituloAyuda()
        {
            return Ingles() ? "Screen help" : "Ayuda de esta pantalla";
        }
    }
}
