using BE;
using Servicios;
using UI.Servicios;

namespace UI
{
    // Pantalla final del PN2: la factura ya fue registrada y el stock actualizado.
    // Permite consultar el resultado y exportar el comprobante sin volver a cobrar.
    public partial class frmFacturaRegistrada : Form, IObservadorIdioma
    {
        private readonly FacturaBuffetBE _factura;
        private readonly ClienteBE _cliente;

        public frmFacturaRegistrada(FacturaBuffetBE factura, ClienteBE? cliente)
        {
            InitializeComponent();

            if (factura == null || factura.IdFactura <= 0 || factura.Estado != "Pagada")
                throw new ArgumentException("La factura debe estar registrada y pagada.", nameof(factura));

            _factura = factura;
            _cliente = cliente != null && cliente.DNI == factura.DNI? cliente: new ClienteBE { DNI = factura.DNI };

            MostrarDatos();
            ActualizarIdioma();
        }

        private void frmFacturaRegistrada_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
        }

        private void frmFacturaRegistrada_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;
            Text = t.Traducir("frmFacturaRegistrada.Title");
            lblTitulo.Text = t.Traducir("frmFacturaRegistrada.Titulo");
            gbDatos.Text = t.Traducir("frmFacturaRegistrada.Detalle");
            lblNumeroTitulo.Text = t.Traducir("frmFacturaRegistrada.Numero");
            lblFechaTitulo.Text = t.Traducir("frmFacturaRegistrada.Fecha");
            lblClienteTitulo.Text = t.Traducir("frmFacturaRegistrada.Cliente");
            lblDniTitulo.Text = t.Traducir("frmFacturaRegistrada.Dni");
            lblBancoTitulo.Text = t.Traducir("frmFacturaRegistrada.Banco");
            lblTotalTitulo.Text = t.Traducir("frmFacturaRegistrada.Total");
            btnExportarPdf.Text = t.Traducir("frmFacturaRegistrada.ExportarPdf");
            btnFinalizar.Text = t.Traducir("frmFacturaRegistrada.Finalizar");
            lblMensaje.Text = string.Format(t.Traducir("frmFacturaRegistrada.Mensaje"),_factura.NumeroFactura);

            colProducto.HeaderText = t.Traducir("frmFacturaRegistrada.Producto");
            colCantidad.HeaderText = t.Traducir("frmFacturaRegistrada.Cantidad");
            colPrecio.HeaderText = t.Traducir("frmFacturaRegistrada.Precio");
            colSubtotal.HeaderText = t.Traducir("frmFacturaRegistrada.Subtotal");
        }

        private void MostrarDatos()
        {
            lblNumeroValor.Text = _factura.NumeroFactura;
            lblFechaValor.Text = _factura.FechaHoraEmision.ToString("dd/MM/yyyy HH:mm");
            lblClienteValor.Text = ( _cliente.Nombre + " " + _cliente.Apellido ).Trim();
            if (string.IsNullOrEmpty(lblClienteValor.Text))
                lblClienteValor.Text = "-";
            lblDniValor.Text = _factura.DNI;
            lblBancoValor.Text = _factura.Banco + (string.IsNullOrWhiteSpace(_factura.Ultimos4Tarjeta)? "" : "  -  **** " + _factura.Ultimos4Tarjeta);
            lblTotalValor.Text = _factura.ImporteTotal.ToString("C2");

            dgvDetalles.Rows.Clear();
            foreach (DetalleFacturaBE detalle in _factura.Detalles)
                dgvDetalles.Rows.Add(detalle.NombreProducto,detalle.Cantidad,detalle.PrecioUnitario,detalle.Subtotal);
            dgvDetalles.ClearSelection();
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            var t = Traductor.Instancia;
            using SaveFileDialog guardar = new SaveFileDialog
            {
                Title = t.Traducir("Pdf.GuardarFactura"),
                Filter = "PDF (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true,
                OverwritePrompt = true,
                FileName = "FacturaBuffet_" + _factura.NumeroFactura + ".pdf"
            };
            if (guardar.ShowDialog(this) != DialogResult.OK) return;

            // El archivo y la exportación son independientes de la transacción comercial.
            try
            {
                ComprobantePdf.ExportarFacturaBuffet(guardar.FileName, _factura, _cliente);
                AuditoriaExportacionPdf.Registrar("Buffet", _factura.NumeroFactura, true);
            }
            catch (Exception ex)
            {
                AuditoriaExportacionPdf.Registrar("Buffet", _factura.NumeroFactura, false);
                MessageBox.Show(t.Traducir("Pdf.ErrorExportacion") + "\n" + ex.Message,Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si falla la apertura con el visor del equipo, el PDF ya está
            // generado; no se registra un falso error de exportación.
            DialogResult abrir = MessageBox.Show(t.Traducir("Pdf.ExportacionOk") + "\n" + t.Traducir("Pdf.AbrirArchivo"),Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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
