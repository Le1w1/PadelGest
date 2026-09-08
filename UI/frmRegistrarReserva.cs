using BE;
using BLL;
using Servicios;

namespace UI
{
    public partial class frmRegistrarReserva : Form, IObservadorIdioma
    {
        #region "Campos"
        private readonly ReservaBLL _reservaBLL;
        private readonly ClienteBE _cliente;
        private readonly CanchaBE _cancha;
        private readonly TarifaBE _tarifa;
        private readonly FacturaBE _facturaPagada;
        private readonly PagoBE _pagoAprobado;
        private readonly DateTime _fecha;
        private readonly TimeSpan _horario;
        private readonly int _cantidadPaletas;
        private readonly int _cantidadPelotas;
       
        #endregion
        public ReservaBE? ReservaRegistrada { get; private set; }

        //constructor de la clase frmRegistrarReserva que recibe varios parámetros relacionados con la reserva, como cliente, cancha, tarifa, factura pagada, pago aprobado, fecha, horario y cantidades de paletas y pelotas.
        //Inicializa los campos privados de la clase con los valores proporcionados y llama a los métodos para actualizar el idioma y mostrar los datos en el formulario.
        public frmRegistrarReserva(ClienteBE cliente,CanchaBE cancha,TarifaBE tarifa,FacturaBE facturaPagada,PagoBE pagoAprobado,DateTime fecha,TimeSpan horario,int cantidadPaletas,int cantidadPelotas)
        {
            InitializeComponent();
            _reservaBLL = new ReservaBLL();
            _cliente = cliente;
            _cancha = cancha;
            _tarifa = tarifa;
            _facturaPagada = facturaPagada;
            _pagoAprobado = pagoAprobado;
            _fecha = fecha;
            _horario = horario;
            _cantidadPaletas = cantidadPaletas;
            _cantidadPelotas = cantidadPelotas;

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


        // Implementación del método ActualizarIdioma de la interfaz IObservadorIdioma para actualizar los textos de los controles del formulario según el idioma seleccionado.
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

            btnRegistrar.Text = t.Traducir("frmRegistrarReserva.BtnRegistrar");
            btnVolver.Text = t.Traducir("frmRegistrarReserva.BtnVolver");
        }


        // Método privado MostrarDatos que se encarga de mostrar los datos de la reserva en los controles del formulario.
        private void MostrarDatos()
        {
            lblClienteValor.Text =$"{_cliente.DNI} - {_cliente.Nombre} {_cliente.Apellido}";
            lblFechaValor.Text = _fecha.ToString("d");
            lblHorarioValor.Text = _horario.ToString(@"hh\:mm");
            lblCanchaValor.Text = _cancha.Nombre;
            lblTarifaValor.Text = $"{_tarifa.TipoTarifa} - {_tarifa.Importe:C}";
            lblEquipamientoValor.Text = $"Paletas: {_cantidadPaletas} | Pelotas: {_cantidadPelotas}";
            lblImporteValor.Text = _facturaPagada.ImporteTotal.ToString("C");
        }


        // Evento btnRegistrar_Click que se ejecuta cuando se hace clic en el botón de registrar reserva.
        // Intenta registrar la reserva utilizando la lógica de negocio y muestra un mensaje de éxito o error según corresponda.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                btnRegistrar.Enabled = false;
                lblMensaje.Text = string.Empty;

                ReservaRegistrada = _reservaBLL.RegistrarReserva(_cliente,_cancha,_tarifa,_facturaPagada,_pagoAprobado,_fecha,_horario,_cantidadPaletas,_cantidadPelotas);

                string mensaje = string.Format(Traductor.Instancia.Traducir("frmRegistrarReserva.MsgRegistrada"),ReservaRegistrada.Codigo);

                MessageBox.Show(mensaje,Text,MessageBoxButtons.OK,MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnRegistrar.Enabled = true;
                lblMensaje.Text = ex.Message;

                MessageBox.Show(ex.Message,Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
