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
        // Cada idioma tiene secciones explicitamente redactadas para usuarios no tecnicos.
        // Formato: Titulo|Explicacion, una seccion por linea.
        private static readonly Dictionary<string, string[]> Textos =
            new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
            { "frmMenuPrincipal", new[] { "Inicio|Desde acá podés abrir las distintas funciones de PadelGest.\nQué podés hacer|Elegí una opción del menú para trabajar con reservas, buffet, clientes o reportes.\nTené en cuenta|Vas a ver solamente las opciones disponibles para tu cuenta.", "Home|Open the different PadelGest features from here.\nWhat you can do|Choose a menu option to manage bookings, buffet sales, customers or reports.\nGood to know|You will only see features available to your account." } },
            { "frmLogin", new[] { "Cómo ingresar|Escribí tu usuario y contraseña.\nDespués|Presioná Ingresar para abrir el programa.\n¿No podés entrar?|Revisá los datos que escribiste. Si el problema continúa, consultá al responsable del sistema.", "How to sign in|Enter your username and password.\nNext|Select Sign in to open the program.\nHaving trouble?|Check the information you entered. If the problem continues, contact the person in charge." } },
            { "frmCambiarClave", new[] { "Cambiar tu contraseña|Ingresá tu contraseña actual y la nueva contraseña.\nPara terminar|Confirmá la nueva contraseña y presioná Guardar.\nRecordá|Si volvés sin guardar, tu contraseña seguirá siendo la misma.", "Change your password|Enter your current password and the new one.\nTo finish|Confirm the new password and select Save.\nRemember|If you go back without saving, your password stays the same." } },
            { "frmCambiarIdioma", new[] { "Elegir el idioma|Seleccioná Español o Inglés.\nPara terminar|Presioná Guardar para aplicar el cambio.\nRecordá|Las ayudas también aparecerán en el idioma que elijas.", "Choose a language|Select Spanish or English.\nTo finish|Select Save to apply the change.\nRemember|Help messages will also appear in your chosen language." } },
            { "frmGestionDeUsuario", new[] { "¿Para qué sirve?|Permite administrar las personas que pueden usar el programa.\nQué podés hacer|Crear o modificar cuentas, asignar funciones y habilitar o bloquear el acceso.\nCómo empezar|Seleccioná una persona de la lista o elegí crear una cuenta nueva.", "What is this for?|Manage the people who can use the program.\nWhat you can do|Create or edit accounts, assign responsibilities and allow or restrict access.\nGetting started|Choose a person from the list or create a new account." } },
            { "frmGestionarRolesYFamilias", new[] { "¿Para qué sirve?|Permite decidir qué opciones del programa puede utilizar cada grupo de usuarios.\nCómo usarlo|Seleccioná un grupo, revisá sus opciones y realizá los cambios necesarios.\nPara terminar|Guardá los cambios antes de salir.", "What is this for?|Choose which features each group of users can access.\nHow to use it|Select a group, review its options and make the necessary changes.\nTo finish|Save your changes before leaving." } },
            { "frmAuditarEventos", new[] { "Consultar actividades|Esta pantalla permite ver las acciones realizadas en el programa.\nCómo buscar|Elegí los filtros que necesites y presioná Buscar.\nOtras opciones|Podés limpiar la búsqueda o guardar un informe de los resultados.", "Review activity|See actions carried out in the program.\nHow to search|Choose the filters you need and select Search.\nOther options|Clear the search or save a report of the results." } },
            { "frmGestionarRespaldo", new[] { "Guardar una copia|Elegí una carpeta y presioná Realizar respaldo para guardar una copia de los datos.\nRecuperar una copia|Seleccioná un respaldo guardado anteriormente y elegí Restaurar.\nImportante|Restaurar reemplaza los datos actuales. Usá esta opción solamente cuando sea necesario.", "Save a copy|Choose a folder and select Back up to save a copy of your information.\nRestore a copy|Choose a previously saved backup and select Restore.\nImportant|Restoring replaces current information. Use this option only when necessary." } },
            { "frmRepararIntegridad", new[] { "¿Cuándo usar esta pantalla?|Cuando el programa avisa que hay un problema con la información guardada.\nQué podés hacer|Intentar corregir el problema o recuperar una copia de seguridad.\nImportante|Si no sabés qué opción elegir, consultá al responsable antes de continuar.", "When should you use this screen?|When the program warns you about a problem with saved information.\nWhat you can do|Try to fix the problem or recover a backup.\nImportant|If you are unsure which option to choose, ask the person in charge before continuing." } },
            { "frmGestionClientes", new[] { "Consultar clientes|Buscá y seleccioná una persona de la lista.\nQué podés hacer|Agregar clientes nuevos, corregir sus datos o realizar otras acciones disponibles.\nOtras opciones|Podés guardar o consultar listas de clientes cuando tengas permiso.", "Find customers|Search for and select someone from the list.\nWhat you can do|Add new customers, update their details or use other available options.\nOther options|You may save or view customer lists if you have permission." } },
            { "frmSeleccionarCliente", new[] { "Buscar un cliente|Ingresá los ocho números del DNI.\nDespués|Elegí al cliente encontrado y confirmá para continuar.\nSi no aparece|Podés registrarlo si tu cuenta tiene esa opción habilitada.", "Find a customer|Enter the customer's eight-digit ID number.\nNext|Select the customer and confirm to continue.\nIf you cannot find them|You can register a new customer if your account allows it." } },
            { "frmRegistrarCliente", new[] { "Agregar un cliente|Completá DNI, nombre, apellido, teléfono y correo electrónico.\nPara terminar|Revisá los datos y presioná Registrar.\nImportante|Cada cliente debe tener su propio DNI. Revisalo antes de guardar.", "Add a customer|Enter their ID number, first name, last name, phone number and email.\nTo finish|Check the details and select Register.\nImportant|Each customer must have a unique ID number. Check it before saving." } },
            { "frmGenerarReserva", new[] { "Elegir un turno|Seleccioná la fecha, el horario y la cancha disponible.\nCompletar la reserva|Elegí al cliente y, si lo necesita, agregá paletas o pelotas.\nAntes de seguir|Revisá todos los datos y continuá con el pago.", "Choose a time slot|Select an available date, time and court.\nComplete the booking|Choose the customer and add rackets or balls if needed.\nBefore continuing|Check all details and proceed to payment." } },
            { "frmAgregarEquipamiento", new[] { "Agregar equipamiento|Indicá cuántas paletas y pelotas necesita el cliente.\nPara terminar|Revisá las cantidades y presioná Confirmar.\nTené en cuenta|Solo podés agregar equipamiento que esté disponible.", "Add equipment|Enter how many rackets and balls the customer needs.\nTo finish|Check the quantities and select Confirm.\nGood to know|You can only add equipment that is available." } },
            { "frmCobrarReserva", new[] { "Cobrar una reserva|Revisá el importe que debe pagar el cliente.\nPara continuar|Ingresá los datos de la tarjeta y confirmá el pago.\nImportante|Cuando el pago sea aprobado, continuá hasta completar el registro de la reserva.", "Charge a booking|Check the amount the customer needs to pay.\nTo continue|Enter the card details and confirm payment.\nImportant|Once payment is approved, continue until the booking is fully registered." } },
            { "frmRegistrarReserva", new[] { "Finalizar la reserva|Esta pantalla permite completar una reserva que ya fue pagada.\nQué hacer|Revisá los datos y finalizá el registro.\nImportante|No cierres la operación sin registrar la reserva. Si está disponible, también podés guardar el comprobante en PDF.", "Finish the booking|Complete a booking that has already been paid for.\nWhat to do|Review the details and finish registration.\nImportant|Do not close this step without registering the booking. You may also save a PDF receipt if available." } },
            { "frmGenerarFactura", new[] { "Preparar una venta|Seleccioná los productos del buffet y las cantidades que pidió el cliente.\nRevisar el pedido|Podés agregar, cambiar o quitar productos antes de cobrar.\nPara continuar|Comprobá el total y seguí con el pago.", "Prepare a sale|Choose the buffet products and quantities the customer requested.\nReview the order|You can add, change or remove products before charging.\nTo continue|Check the total and proceed to payment." } },
            { "frmCobrarFactura", new[] { "Cobrar una venta|Revisá los productos y el importe total.\nPara continuar|Ingresá los datos de la tarjeta y confirmá el pago.\nDespués|El programa completará la venta y actualizará las cantidades disponibles.", "Charge a sale|Check the products and the total amount.\nTo continue|Enter the card details and confirm payment.\nAfterward|The program completes the sale and updates the available quantities." } },
            { "frmFacturaRegistrada", new[] { "Venta finalizada|La venta del buffet ya quedó registrada.\nGuardar un comprobante|Presioná Exportar PDF si querés conservar una copia.\nPara terminar|Cerrá esta pantalla cuando hayas finalizado.", "Sale completed|The buffet sale has been recorded.\nSave a receipt|Select Export PDF if you would like to keep a copy.\nTo finish|Close this screen when you are done." } },
            { "frmGestionProductos", new[] { "Administrar productos|Consultá los artículos y las cantidades disponibles del buffet.\nQué podés hacer|Agregar o modificar productos, cambiar su disponibilidad y reponer mercadería.\nCómo empezar|Seleccioná un producto de la lista o elegí agregar uno nuevo.", "Manage products|View buffet items and their available quantities.\nWhat you can do|Add or edit products, change availability and restock items.\nGetting started|Choose a product from the list or add a new one." } },
            { "frmAuditarCambios", new[] { "Consultar cambios|Revisá qué datos de productos se modificaron anteriormente.\nCómo buscar|Usá los filtros para encontrar un cambio.\nOtras opciones|Si tenés permiso, podés recuperar información anterior de un producto.", "Review changes|See which product details were changed in the past.\nHow to search|Use the filters to find a change.\nOther options|If you have permission, you can restore previous product information." } },
            { "frmReportePredictivo", new[] { "¿Para qué sirve?|Ayuda a decidir qué productos del buffet conviene reponer.\nPanorama|Muestra un resumen de las ventas y de lo que podría necesitarse durante la próxima semana.\nProductos y prioridades|Consultá las cantidades disponibles y los productos que conviene reponer primero.\nCategorías|Compará las ventas y las estimaciones entre grupos de productos.\nGuardar el informe|Usá Exportar PDF para conservar una copia con los gráficos y resultados.\nRecordá|Las cantidades futuras son estimadas. El informe no realiza compras.", "What is this for?|Helps you decide which buffet products may need restocking.\nOverview|Shows recent sales and what may be needed over the next week.\nProducts and priorities|Check available quantities and see which products should be restocked first.\nCategories|Compare sales and estimates across product groups.\nSave the report|Select Export PDF to keep a copy with charts and results.\nRemember|Future quantities are estimates. This report does not place orders." } },
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
                    MostrarAyuda(formulario, texto);

                };
                formulario.Controls.Add(botonAyuda);
                botonAyuda.BringToFront();
                formulario.FormClosed += (_, _) => tooltip.Dispose();
            };
        }

        // Ventana compacta, con altura calculada segun el contenido y scroll si hace falta.
        private static void MostrarAyuda(Form propietario, string descripcion)
        {
            bool ingles = Ingles();
            string[] secciones = descripcion.Split(new[] { '\n' },
                StringSplitOptions.RemoveEmptyEntries);
            const int ancho = 620;
            const int anchoTexto = 544;
            int altoContenido = 0;

            using Form ventana = new Form
            {
                Text = TituloAyuda(),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F),
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
                Padding = new Padding(20, 12, 20, 10)
            };
            disposicion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            disposicion.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            disposicion.RowStyles.Add(new RowStyle(SizeType.Absolute, 47));
            Label encabezado = new Label
            {
                Text = ingles ? "HELP" : "AYUDA",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
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
                Font = new Font("Segoe UI", 10F),
                ScrollBars = RichTextBoxScrollBars.Vertical,
                DetectUrls = false,
                TabStop = false
            };
            using Font fuenteTitulo = new Font("Segoe UI", 10F, FontStyle.Bold);
            using Font fuenteTexto = new Font("Segoe UI", 10F);
            foreach (string seccion in secciones)
            {
                string[] partes = seccion.Split(new[] { '|' }, 2);
                if (partes.Length != 2) continue;
                contenido.SelectionFont = fuenteTitulo;
                contenido.SelectionColor = Color.FromArgb(24, 70, 138);
                contenido.AppendText(partes[0] + Environment.NewLine);
                contenido.SelectionFont = fuenteTexto;
                contenido.SelectionColor = Color.FromArgb(35, 44, 60);
                contenido.AppendText(partes[1] + Environment.NewLine + Environment.NewLine);
                int altoTexto = TextRenderer.MeasureText(partes[1], fuenteTexto,
                    new Size(anchoTexto, 1000),
                    TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl).Height;
                altoContenido += 23 + Math.Max(23, altoTexto) + 12;
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
            System.Drawing.Rectangle area = Screen.FromControl(propietario).WorkingArea;
            int altoMaximo = Math.Max(250, area.Height - 80);
            int altoDeseado = altoContenido + 45 + 47 + 22 + 38;
            ventana.ClientSize = new Size(ancho, Math.Min(altoMaximo,
                Math.Max(215, altoDeseado)));
            ventana.AcceptButton = cerrar;
            ventana.ShowDialog(propietario);
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
