using BE;
using Servicios;
using UI.Servicios;

namespace UI
{
    public partial class frmRegistrarReserva : Form, IObservadorIdioma
    {
        #region "Campos"
        private readonly ReservaBE _reserva;
        private readonly ClienteBE _cliente;
        private readonly CanchaBE _cancha;
        private readonly TarifaBE _tarifa;
        private readonly FacturaBE _facturaPagada;
        #endregion

        // La reserva ya llega registrada. Este formulario solo muestra el resultado
        // del CUN06 y permite finalizar el proceso de reserva.
        public frmRegistrarReserva(
            ReservaBE reserva,
            ClienteBE cliente,
            CanchaBE cancha,
            TarifaBE tarifa,
            FacturaBE facturaPagada)
        {
            InitializeComponent();

            _reserva = reserva;
            _cliente = cliente;
            _cancha = cancha;
            _tarifa = tarifa;
            _facturaPagada = facturaPagada;

            ActualizarIdioma();
            MostrarDatos();
        }

        private void frmRegistrarReserva_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
        }

        private void frmRegistrarReserva_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            Text = t.Traducir("frmRegistrarReserva.Title");
            lblTitulo.Text = t.Traducir("frmRegistrarReserva.LblTitulo");
            gbDatos.Text = t.Traducir("frmRegistrarReserva.GbDatos");
            lblClienteTitulo.Text = t.Traducir("frmRegistrarReserva.LblCliente");
            lblFechaTitulo.Text = t.Traducir("frmRegistrarReserva.LblFecha");
            lblHorarioTitulo.Text = t.Traducir("frmRegistrarReserva.LblHorario");
            lblCanchaTitulo.Text = t.Traducir("frmRegistrarReserva.LblCancha");
            lblTarifaTitulo.Text = t.Traducir("frmRegistrarReserva.LblTarifa");
            lblEquipamientoTitulo.Text = t.Traducir("frmRegistrarReserva.LblEquipamiento");
            lblImporteTitulo.Text = t.Traducir("frmRegistrarReserva.LblImporte");
            btnFinalizar.Text = t.Traducir("frmRegistrarReserva.BtnFinalizar");
            btnExportarPdf.Text = t.Traducir("frmRegistrarReserva.BtnExportarPdf");

            lblMensaje.Text = string.Format(
                t.Traducir("frmRegistrarReserva.MsgRegistrada"),
                _reserva.Codigo);
        }

        private void MostrarDatos()
        {
            lblClienteValor.Text = $"{_cliente.DNI} - {_cliente.Nombre} {_cliente.Apellido}";
            lblFechaValor.Text = _reserva.Fecha.ToString("d");
            lblHorarioValor.Text = _reserva.Horario.ToString(@"hh\:mm");
            lblCanchaValor.Text = _cancha.Nombre;
            lblTarifaValor.Text = $"{_tarifa.TipoTarifa} - {_tarifa.Importe:C}";
            lblEquipamientoValor.Text = $"Paletas: {_reserva.CantidadPaletas} | Pelotas: {_reserva.CantidadPelotas}";
            lblImporteValor.Text = _facturaPagada.ImporteTotal.ToString("C");
        }

        // El comprobante se exporta únicamente con la reserva ya registrada.
        // Un fallo de PDF no altera la reserva ni vuelve a cobrar.
        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            var t = Traductor.Instancia;
            using SaveFileDialog guardar = new SaveFileDialog
            {
                Title = t.Traducir("Pdf.GuardarReserva"),
                Filter = "PDF (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true,
                OverwritePrompt = true,
                FileName = "Reserva_" + _reserva.Codigo + ".pdf"
            };
            if (guardar.ShowDialog(this) != DialogResult.OK) return;

            // El archivo y la exportación son independientes de la transacción comercial.
            try
            {
                ComprobantePdf.ExportarReserva(
                    guardar.FileName, _reserva, _cliente, _cancha, _tarifa, _facturaPagada);
                AuditoriaExportacionPdf.Registrar("Reserva", _reserva.Codigo, true);
            }
            catch (Exception ex)
            {
                AuditoriaExportacionPdf.Registrar("Reserva", _reserva.Codigo, false);
                MessageBox.Show(t.Traducir("Pdf.ErrorExportacion") + "\n" + ex.Message,
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si falla la apertura con el visor del equipo, el PDF ya está
            // generado; no se registra un falso error de exportación.
            DialogResult abrir = MessageBox.Show(
                t.Traducir("Pdf.ExportacionOk") + "\n" + t.Traducir("Pdf.AbrirArchivo"),
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (abrir != DialogResult.Yes) return;

            try
            {
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(guardar.FileName)
                    {
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Traducir("Pdf.ErrorAbrirArchivo") + "\n" + ex.Message,
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

                private void btnFinalizar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
