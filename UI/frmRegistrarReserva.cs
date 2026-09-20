using BE;
using Servicios;

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

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
