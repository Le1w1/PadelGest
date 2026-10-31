using BE;
using BLL;
using Servicios;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    // ABM exclusivo del Maestro. No altera la tabla Cliente.
    public partial class frmGestionClientes : Form
    {
        private readonly ClienteBLL _clientes = new ClienteBLL();
        public frmGestionClientes()
        {
            InitializeComponent();
            Load += (_, _) =>
            {
                try { SM.Instancia.RequierePermiso("CLI_MODIFICAR"); Cargar(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); Close(); }
            };
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
