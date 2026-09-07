using BE;
using BLL;
using Servicios;

namespace UI
{
    public partial class frmAgregarEquipamiento : Form, IObservadorIdioma
    {
        private readonly EquipamientoBLL _equipamientoBLL;
        private readonly DateTime _fecha;
        private readonly TimeSpan _horario;
        private readonly int _cantidadPaletasInicial;
        private readonly int _cantidadPelotasInicial;

        private EquipamientoBE? _paleta;
        private EquipamientoBE? _pelota;

        public int CantidadPaletas { get; private set; }
        public int CantidadPelotas { get; private set; }
        public decimal ImporteEquipamiento { get; private set; }

        public frmAgregarEquipamiento(DateTime fecha,TimeSpan horario,int cantidadPaletasInicial = 0,int cantidadPelotasInicial = 0)
        {
            InitializeComponent();

            _equipamientoBLL = new EquipamientoBLL();
            _fecha = fecha.Date;
            _horario = horario;
            _cantidadPaletasInicial = cantidadPaletasInicial;
            _cantidadPelotasInicial = cantidadPelotasInicial;

            ActualizarIdioma();
        }

        private void frmAgregarEquipamiento_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            CargarEquipamiento();
        }

        private void frmAgregarEquipamiento_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            Text = t.Traducir("frmAgregarEquipamiento.Title");
            lblTitulo.Text = t.Traducir("frmAgregarEquipamiento.LblTitulo");
            gbEquipamiento.Text = t.Traducir("frmAgregarEquipamiento.GbEquipamiento");

            lblPaletas.Text = t.Traducir("frmAgregarEquipamiento.LblPaletas");
            lblPelotas.Text = t.Traducir("frmAgregarEquipamiento.LblPelotas");
            lblCantidadPaletas.Text = t.Traducir("frmAgregarEquipamiento.LblCantidad");
            lblCantidadPelotas.Text = t.Traducir("frmAgregarEquipamiento.LblCantidad");
            lblStockPaletasTitulo.Text = t.Traducir("frmAgregarEquipamiento.LblStock");
            lblStockPelotasTitulo.Text = t.Traducir("frmAgregarEquipamiento.LblStock");
            lblImportePaletasTitulo.Text = t.Traducir("frmAgregarEquipamiento.LblImporteUnitario");
            lblImportePelotasTitulo.Text = t.Traducir("frmAgregarEquipamiento.LblImporteUnitario");
            lblTotalTitulo.Text = t.Traducir("frmAgregarEquipamiento.LblTotal");

            btnConfirmar.Text = t.Traducir("frmAgregarEquipamiento.BtnConfirmar");
            btnVolver.Text = t.Traducir("frmAgregarEquipamiento.BtnVolver");
        }

        private void CargarEquipamiento()
        {
            try
            {
                errorProvider.Clear();
                lblMensaje.Text = string.Empty;

                List<EquipamientoBE> equipamientos = _equipamientoBLL.ObtenerEquipamientosActivos(_fecha,_horario);

                _paleta = equipamientos.FirstOrDefault(e => e.Tipo.Equals("Paleta", StringComparison.OrdinalIgnoreCase));

                _pelota = equipamientos.FirstOrDefault(e => e.Tipo.Equals("Pelota", StringComparison.OrdinalIgnoreCase));

                ConfigurarPaletas();
                ConfigurarPelotas();
                ActualizarTotalVista();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                btnConfirmar.Enabled = false;
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarPaletas()
        {
            int stock = _paleta?.StockDisponible ?? 0;
            int maximo = Math.Min(stock, EquipamientoBLL.MaximoPaletasPorReserva);

            nudPaletas.Minimum = 0;
            nudPaletas.Maximum = maximo;
            nudPaletas.Value = Math.Min(_cantidadPaletasInicial, maximo);

            lblStockPaletasValor.Text = stock.ToString();
            lblImportePaletasValor.Text = _paleta == null ? "-" : _paleta.Importe.ToString("C");
            nudPaletas.Enabled = _paleta != null && stock > 0;
        }

        private void ConfigurarPelotas()
        {
            int stock = _pelota?.StockDisponible ?? 0;
            int maximo = Math.Min(stock, EquipamientoBLL.MaximoPelotasPorReserva);

            nudPelotas.Minimum = 0;
            nudPelotas.Maximum = maximo;
            nudPelotas.Value = Math.Min(_cantidadPelotasInicial, maximo);

            lblStockPelotasValor.Text = stock.ToString();
            lblImportePelotasValor.Text = _pelota == null ? "-" : _pelota.Importe.ToString("C");
            nudPelotas.Enabled = _pelota != null && stock > 0;
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            lblMensaje.Text = string.Empty;
            ActualizarTotalVista();
        }

        private void ActualizarTotalVista()
        {
            decimal total = 0;

            if (_paleta != null)
            {
                total += nudPaletas.Value * _paleta.Importe;
            }

            if (_pelota != null)
            {
                total += nudPelotas.Value * _pelota.Importe;
            }

            lblTotalValor.Text = total.ToString("C");
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider.Clear();
                lblMensaje.Text = string.Empty;

                int cantidadPaletas = Convert.ToInt32(nudPaletas.Value);
                int cantidadPelotas = Convert.ToInt32(nudPelotas.Value);

                if (cantidadPaletas == 0 && cantidadPelotas == 0)
                {
                    string mensaje = Traductor.Instancia.Traducir("Errores.Equipamiento.DebeSeleccionar");
                    errorProvider.SetError(gbEquipamiento, mensaje);
                    lblMensaje.Text = mensaje;
                    return;
                }

                decimal importe = _equipamientoBLL.ValidarYCalcularImporte(_fecha,_horario,cantidadPaletas,cantidadPelotas);

                CantidadPaletas = cantidadPaletas;
                CantidadPelotas = cantidadPelotas;
                ImporteEquipamiento = importe;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // El stock pudo cambiar desde que se abrio la pantalla.
                CargarEquipamiento();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
