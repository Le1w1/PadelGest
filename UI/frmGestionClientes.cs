using BE;
using BLL;
using Servicios;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    // ABM exclusivo del Maestro. No altera la tabla Cliente.
    public class frmGestionClientes : Form
    {
        private readonly ClienteBLL _clientes = new ClienteBLL();
        private readonly DataGridView grilla = new DataGridView();
        private readonly TextBox dni = new TextBox();
        private readonly TextBox nombre = new TextBox();
        private readonly TextBox apellido = new TextBox();
        private readonly TextBox telefono = new TextBox();
        private readonly TextBox correo = new TextBox();

        public frmGestionClientes()
        {
            Text = "Gestion de Clientes";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10);
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18),
                ColumnCount = 1, RowCount = 3 };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            Controls.Add(panel);

            var datos = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true };
            AgregarCampo(datos, "DNI", dni, 85);
            AgregarCampo(datos, "Nombre", nombre, 130);
            AgregarCampo(datos, "Apellido", apellido, 130);
            AgregarCampo(datos, "Telefono", telefono, 120);
            AgregarCampo(datos, "Correo", correo, 185);
            panel.Controls.Add(datos, 0, 0);

            grilla.Dock = DockStyle.Fill;
            grilla.ReadOnly = true;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.MultiSelect = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoGenerateColumns = false;
            grilla.BackgroundColor = Color.White;
            grilla.DefaultCellStyle.ForeColor = Color.Black;
            grilla.DefaultCellStyle.SelectionForeColor = Color.Black;
            grilla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            foreach (var field in new[] { "DNI", "Nombre", "Apellido", "Telefono", "CorreoElectronico" })
                grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = field,
                    DataPropertyName = field, HeaderText = field == "CorreoElectronico" ? "Correo" : field,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grilla.SelectionChanged += (_, _) => Seleccionar();
            panel.Controls.Add(grilla, 0, 1);

            var acciones = new FlowLayoutPanel { Dock = DockStyle.Fill };
            AgregarBoton(acciones, "Nuevo", (_, _) => Limpiar());
            AgregarBoton(acciones, "Registrar", (_, _) => Ejecutar(() =>
                _clientes.RegistrarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text)));
            AgregarBoton(acciones, "Modificar", (_, _) => Ejecutar(() =>
                _clientes.ModificarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text)));
            AgregarBoton(acciones, "Eliminar", (_, _) =>
            {
                if (MessageBox.Show("¿Eliminar al cliente seleccionado?", "Confirmacion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Ejecutar(() => _clientes.EliminarCliente(dni.Text));
            });
            AgregarBoton(acciones, "Actualizar", (_, _) => Cargar());
            AgregarBoton(acciones, "Volver", (_, _) => Close());
            panel.Controls.Add(acciones, 0, 2);
            Load += (_, _) =>
            {
                try { SM.Instancia.RequierePermiso("CLI_MODIFICAR"); Cargar(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); Close(); }
            };
        }

        private static void AgregarCampo(FlowLayoutPanel panel, string titulo, TextBox input, int ancho)
        {
            var bloque = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown,
                Width = ancho + 12, Height = 70, WrapContents = false };
            bloque.Controls.Add(new Label { Text = titulo, AutoSize = true });
            input.Width = ancho;
            bloque.Controls.Add(input);
            panel.Controls.Add(bloque);
        }

        private static void AgregarBoton(FlowLayoutPanel panel, string titulo, EventHandler accion)
        {
            var boton = new Button { Text = titulo, Width = 125, Height = 38,
                BackColor = Color.FromArgb(24, 70, 138), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            boton.Click += accion;
            panel.Controls.Add(boton);
        }

        private void Seleccionar()
        {
            if (grilla.CurrentRow?.Selected != true ||
                grilla.CurrentRow.DataBoundItem is not ClienteBE c) return;
            dni.Text = c.DNI;
            dni.ReadOnly = true; // Identificador primario; no se modifica.
            nombre.Text = c.Nombre;
            apellido.Text = c.Apellido;
            telefono.Text = c.Telefono;
            correo.Text = c.CorreoElectronico;
        }

        private void Limpiar()
        {
            grilla.ClearSelection();
            dni.ReadOnly = false;
            foreach (TextBox t in new[] { dni, nombre, apellido, telefono, correo }) t.Clear();
            dni.Focus();
        }

        private void Cargar()
        {
            grilla.DataSource = null;
            grilla.DataSource = _clientes.ListarTodos();
            Limpiar();
        }

        private void Ejecutar(Action accion)
        {
            try
            {
                accion();
                Cargar();
                MessageBox.Show("Operacion realizada correctamente.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
