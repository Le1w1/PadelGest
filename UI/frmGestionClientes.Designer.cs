namespace UI
{
    partial class frmGestionClientes
    {
        private System.ComponentModel.IContainer? components = null;
        private DataGridView grilla = null!;
        private TextBox dni = null!, nombre = null!, apellido = null!, telefono = null!, correo = null!;
        private Button btnNuevo = null!, btnRegistrar = null!, btnModificar = null!, btnEliminar = null!, btnActualizar = null!, btnVolver = null!;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Color fondo = Color.FromArgb(14, 43, 92);
            Color panel = Color.FromArgb(24, 70, 138);
            Color lima = Color.FromArgb(214, 246, 36);
            grilla = new DataGridView();
            dni = new TextBox(); nombre = new TextBox(); apellido = new TextBox();
            telefono = new TextBox(); correo = new TextBox();
            btnNuevo = new Button(); btnRegistrar = new Button(); btnModificar = new Button();
            btnEliminar = new Button(); btnActualizar = new Button(); btnVolver = new Button();
            SuspendLayout();
            Text = "Gestión de Clientes";
            ClientSize = new Size(1165, 665);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = fondo;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F);
            Controls.Add(new Label {
                Text = "GESTIÓN DE CLIENTES", ForeColor = lima,
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                Location = new Point(25, 14), Size = new Size(800, 45)
            });
            var gbDatos = new GroupBox {
                Text = "Datos del cliente", Location = new Point(25, 65), Size = new Size(1115, 135),
                BackColor = panel, ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(gbDatos);
            AgregarCampo(gbDatos, "DNI", dni, 20, 105, 8);
            AgregarCampo(gbDatos, "Nombre", nombre, 140, 180, 50);
            AgregarCampo(gbDatos, "Apellido", apellido, 340, 180, 50);
            AgregarCampo(gbDatos, "Teléfono", telefono, 540, 165, 15);
            AgregarCampo(gbDatos, "Correo electrónico", correo, 725, 350, 150);

            Controls.Add(new Label {
                Text = "CLIENTES REGISTRADOS", Location = new Point(25, 214),
                Size = new Size(500, 28), ForeColor = lima,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            });
            grilla.Location = new Point(25, 246);
            grilla.Size = new Size(1115, 310);
            grilla.Name = "dgvClientes";
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.AutoGenerateColumns = false;
            grilla.ReadOnly = true;
            grilla.MultiSelect = false;
            grilla.RowHeadersVisible = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.BorderStyle = BorderStyle.None;
            grilla.BackgroundColor = Color.White;
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersHeight = 34;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = panel;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grilla.DefaultCellStyle.BackColor = Color.White;
            grilla.DefaultCellStyle.ForeColor = Color.Black;
            grilla.DefaultCellStyle.SelectionBackColor = lima;
            grilla.DefaultCellStyle.SelectionForeColor = Color.Black;
            grilla.RowsDefaultCellStyle.ForeColor = Color.Black;
            grilla.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            foreach (string campo in new[] { "DNI", "Nombre", "Apellido", "Telefono", "CorreoElectronico" })
                grilla.Columns.Add(new DataGridViewTextBoxColumn {
                    Name = "col" + campo, DataPropertyName = campo,
                    HeaderText = campo == "CorreoElectronico" ? "Correo electrónico" :
                        campo == "Telefono" ? "Teléfono" : campo,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
            grilla.SelectionChanged += (_, _) => Seleccionar();
            Controls.Add(grilla);

            ConfigurarBoton(btnNuevo, "Nuevo", 25, panel, Color.White);
            ConfigurarBoton(btnRegistrar, "Registrar", 210, lima, Color.Black);
            ConfigurarBoton(btnModificar, "Modificar", 395, panel, Color.White);
            ConfigurarBoton(btnEliminar, "Eliminar", 580, panel, Color.White);
            ConfigurarBoton(btnActualizar, "Actualizar", 765, panel, Color.White);
            ConfigurarBoton(btnVolver, "Volver", 950, lima, Color.Black);
            btnNuevo.Click += (_, _) => Limpiar();
            btnRegistrar.Click += (_, _) => Ejecutar(() =>
                _clientes.RegistrarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text));
            btnModificar.Click += (_, _) => Ejecutar(() =>
                _clientes.ModificarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text));
            btnEliminar.Click += (_, _) => {
                if (MessageBox.Show("¿Eliminar al cliente seleccionado?", "Confirmación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Ejecutar(() => _clientes.EliminarCliente(dni.Text));
            };
            btnActualizar.Click += (_, _) => Cargar();
            btnVolver.Click += (_, _) => Close();
            ResumeLayout(false);
        }

        private static void AgregarCampo(GroupBox contenedor, string titulo, TextBox campo, int x, int ancho, int maximo)
        {
            contenedor.Controls.Add(new Label { Text = titulo,
                Location = new Point(x, 27), Size = new Size(ancho, 24), ForeColor = Color.White });
            campo.Location = new Point(x, 58);
            campo.Size = new Size(ancho, 28);
            campo.MaxLength = maximo;
            campo.ForeColor = Color.Black;
            campo.BackColor = Color.White;
            campo.Font = new Font("Segoe UI", 10F);
            contenedor.Controls.Add(campo);
        }

        private void ConfigurarBoton(Button boton, string titulo, int x, Color fondo, Color texto)
        {
            boton.Text = titulo;
            boton.Name = "btn" + titulo;
            boton.Location = new Point(x, 580);
            boton.Size = new Size(165, 45);
            boton.BackColor = fondo;
            boton.ForeColor = texto;
            boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            Controls.Add(boton);
        }
    }
}
