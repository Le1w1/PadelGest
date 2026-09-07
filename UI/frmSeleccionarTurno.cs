using BE;
using BLL;
using Servicios;

namespace UI
{
    public partial class frmSeleccionarTurno : Form, IObservadorIdioma
    {
        private readonly CanchaBLL _canchaBLL;
        private readonly TarifaBLL _tarifaBLL;
        private TarifaBE? _tarifaActual;

        public DateTime FechaSeleccionada { get; private set; }
        public TimeSpan HorarioSeleccionado { get; private set; }
        public CanchaBE? CanchaSeleccionada { get; private set; }
        public TarifaBE? TarifaSeleccionada { get; private set; }
        public int CantidadPaletas { get; private set; }
        public int CantidadPelotas { get; private set; }
        public decimal ImporteEquipamiento { get; private set; }
        public ClienteBE? ClienteSeleccionado { get; private set; }
        public FacturaBE? FacturaPagada { get; private set; }
        public PagoBE? PagoAprobado { get; private set; }
        public ReservaBE? ReservaRegistrada { get; private set; }

        public frmSeleccionarTurno()
        {
            InitializeComponent();

            _canchaBLL = new CanchaBLL();
            _tarifaBLL = new TarifaBLL();

            cboHorario.Format += cboHorario_Format;

            ActualizarIdioma();
        }

        private void frmSeleccionarTurno_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);

            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.MaxDate = DateTime.Today.AddDays(7);
            dtpFecha.Value = DateTime.Today;

            LimpiarResultados();
            CargarHorarios();
        }

        private void frmSeleccionarTurno_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            Text = t.Traducir("frmSeleccionarTurno.Title");
            lblTitulo.Text = t.Traducir("frmSeleccionarTurno.LblTitulo");
            gbBusqueda.Text = t.Traducir("frmSeleccionarTurno.GbBusqueda");
            lblFecha.Text = t.Traducir("frmSeleccionarTurno.LblFecha");
            lblHorario.Text = t.Traducir("frmSeleccionarTurno.LblHorario");
            btnBuscar.Text = t.Traducir("frmSeleccionarTurno.BtnBuscar");

            gbDisponibles.Text = t.Traducir("frmSeleccionarTurno.GbDisponibles");
            colCancha.HeaderText = t.Traducir("frmSeleccionarTurno.ColCancha");
            colEstado.HeaderText = t.Traducir("frmSeleccionarTurno.ColEstado");
            lblTarifaTitulo.Text = t.Traducir("frmSeleccionarTurno.LblTarifa");
            btnSeleccionar.Text = t.Traducir("frmSeleccionarTurno.BtnSeleccionar");

            gbSeleccionado.Text = t.Traducir("frmSeleccionarTurno.GbSeleccionado");
            lblFechaSelTitulo.Text = t.Traducir("frmSeleccionarTurno.LblFecha");
            lblHorarioSelTitulo.Text = t.Traducir("frmSeleccionarTurno.LblHorario");
            lblCanchaSelTitulo.Text = t.Traducir("frmSeleccionarTurno.LblCancha");
            lblTarifaSelTitulo.Text = t.Traducir("frmSeleccionarTurno.LblTarifa");
            lblEquipamientoSelTitulo.Text = t.Traducir("frmSeleccionarTurno.LblEquipamiento");
            btnAgregarEquipamiento.Text = t.Traducir("frmSeleccionarTurno.BtnAgregarEquipamiento");
            btnCobrarReserva.Text = t.Traducir("frmSeleccionarTurno.BtnCobrarReserva");
            btnRegistrarReserva.Text = t.Traducir("frmSeleccionarTurno.BtnRegistrarReserva");
            ActualizarResumenEquipamiento();

            btnContinuar.Text = t.Traducir("frmSeleccionarTurno.BtnContinuar");
            btnVolver.Text = t.Traducir("frmSeleccionarTurno.BtnVolver");
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            LimpiarResultados();
            CargarHorarios();
        }

        private void cboHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarResultados();
        }

        private void cboHorario_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is TimeSpan horario)
            {
                e.Value = horario.ToString(@"hh\:mm");
            }
        }

        private void CargarHorarios()
        {
            try
            {
                errorProvider.Clear();

                List<TimeSpan> horarios = _canchaBLL.ObtenerHorariosDisponibles(dtpFecha.Value.Date);

                cboHorario.DataSource = null;
                cboHorario.DataSource = horarios;

                if (horarios.Count == 0)
                {
                    lblMensaje.Text = Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgSinHorarios");
                }
                else
                {
                    cboHorario.SelectedIndex = -1;
                    lblMensaje.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                cboHorario.DataSource = null;
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider.Clear();
                lblMensaje.Text = string.Empty;
                LimpiarSeleccion();

                if (cboHorario.SelectedItem is not TimeSpan horario)
                {
                    string mensaje = Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgSeleccioneHorario");
                    errorProvider.SetError(cboHorario, mensaje);
                    lblMensaje.Text = mensaje;
                    cboHorario.Focus();
                    return;
                }

                DateTime fecha = dtpFecha.Value.Date;

                _tarifaActual = _tarifaBLL.ObtenerTarifa(horario);
                List<CanchaBE> canchas = _canchaBLL.ObtenerCanchasDisponibles(fecha, horario);

                lblTarifaValor.Text = $"{_tarifaActual.TipoTarifa} - {_tarifaActual.Importe:C}";

                dgvCanchas.DataSource = null;
                dgvCanchas.DataSource = canchas;
                dgvCanchas.ClearSelection();

                btnSeleccionar.Enabled = false;

                if (canchas.Count == 0)
                {
                    lblMensaje.Text = Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgSinCanchas");
                }
            }
            catch (Exception ex)
            {
                dgvCanchas.DataSource = null;
                _tarifaActual = null;
                lblTarifaValor.Text = "-";
                btnSeleccionar.Enabled = false;
                lblMensaje.Text = ex.Message;
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvCanchas_SelectionChanged(object sender, EventArgs e)
        {
            btnSeleccionar.Enabled =
                dgvCanchas.SelectedRows.Count == 1 &&
                dgvCanchas.SelectedRows[0].DataBoundItem is CanchaBE &&
                _tarifaActual != null;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (dgvCanchas.SelectedRows.Count != 1 ||
                dgvCanchas.SelectedRows[0].DataBoundItem is not CanchaBE cancha)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgSeleccioneCancha");
                return;
            }

            if (cboHorario.SelectedItem is not TimeSpan horario || _tarifaActual == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgBuscarNuevamente");
                return;
            }

            FechaSeleccionada = dtpFecha.Value.Date;
            HorarioSeleccionado = horario;
            CanchaSeleccionada = cancha;
            TarifaSeleccionada = _tarifaActual;

            lblFechaSelValor.Text = FechaSeleccionada.ToShortDateString();
            lblHorarioSelValor.Text = HorarioSeleccionado.ToString(@"hh\:mm");
            lblCanchaSelValor.Text = cancha.Nombre;
            lblTarifaSelValor.Text = $"{_tarifaActual.TipoTarifa} - {_tarifaActual.Importe:C}";

            btnAgregarEquipamiento.Enabled = true;
            btnContinuar.Enabled = true;
            lblMensaje.Text = string.Empty;
        }

        private void btnAgregarEquipamiento_Click(object sender, EventArgs e)
        {
            if (CanchaSeleccionada == null || TarifaSeleccionada == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgSeleccioneCancha");
                return;
            }

            using frmAgregarEquipamiento formEquipamiento =
                new frmAgregarEquipamiento(CantidadPaletas, CantidadPelotas);

            if (formEquipamiento.ShowDialog(this) == DialogResult.OK)
            {
                CantidadPaletas = formEquipamiento.CantidadPaletas;
                CantidadPelotas = formEquipamiento.CantidadPelotas;
                ImporteEquipamiento = formEquipamiento.ImporteEquipamiento;

                ActualizarResumenEquipamiento();
                lblMensaje.Text = string.Empty;
            }
        }

        private void ActualizarResumenEquipamiento()
        {
            if (CantidadPaletas == 0 && CantidadPelotas == 0)
            {
                lblEquipamientoSelValor.Text =
                    Traductor.Instancia.Traducir("frmSeleccionarTurno.SinEquipamiento");
                return;
            }

            lblEquipamientoSelValor.Text = string.Format(
                Traductor.Instancia.Traducir("frmSeleccionarTurno.ResumenEquipamiento"),
                CantidadPaletas,
                CantidadPelotas,
                ImporteEquipamiento);
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (CanchaSeleccionada == null || TarifaSeleccionada == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgSeleccioneCancha");
                return;
            }

            using frmSeleccionarCliente formCliente = new frmSeleccionarCliente();

            if (formCliente.ShowDialog(this) == DialogResult.OK &&
                formCliente.ClienteSeleccionado != null)
            {
                ClienteSeleccionado = formCliente.ClienteSeleccionado;

                lblMensaje.Text = string.Format(
                    Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgClienteSeleccionado"),
                    ClienteSeleccionado.DNI,
                    ClienteSeleccionado.Nombre,
                    ClienteSeleccionado.Apellido);

                btnCobrarReserva.Enabled = true;
            }
        }

        private void btnCobrarReserva_Click(object sender, EventArgs e)
        {
            if (ClienteSeleccionado == null ||
                CanchaSeleccionada == null ||
                TarifaSeleccionada == null)
            {
                lblMensaje.Text =
                    Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgFaltanDatosCobro");
                return;
            }

            using frmCobrarReserva formCobro = new frmCobrarReserva(
                ClienteSeleccionado,
                CanchaSeleccionada,
                TarifaSeleccionada,
                FechaSeleccionada,
                HorarioSeleccionado,
                CantidadPaletas,
                CantidadPelotas,
                ImporteEquipamiento);

            if (formCobro.ShowDialog(this) == DialogResult.OK &&
                formCobro.FacturaPagada != null &&
                formCobro.PagoAprobado != null)
            {
                FacturaPagada = formCobro.FacturaPagada;
                PagoAprobado = formCobro.PagoAprobado;

                lblMensaje.Text =
                    Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgPagoAprobado");

                BloquearDatosLuegoDelPago();
                btnRegistrarReserva.Enabled = true;
            }
        }

        private void btnRegistrarReserva_Click(object sender, EventArgs e)
        {
            if (ClienteSeleccionado == null ||
                CanchaSeleccionada == null ||
                TarifaSeleccionada == null ||
                FacturaPagada == null)
            {
                lblMensaje.Text =
                    Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgFaltanDatosRegistro");
                return;
            }

            using frmRegistrarReserva formReserva = new frmRegistrarReserva(
                ClienteSeleccionado,
                CanchaSeleccionada,
                TarifaSeleccionada,
                FacturaPagada,
                FechaSeleccionada,
                HorarioSeleccionado,
                CantidadPaletas,
                CantidadPelotas);

            if (formReserva.ShowDialog(this) == DialogResult.OK &&
                formReserva.ReservaRegistrada != null)
            {
                ReservaRegistrada = formReserva.ReservaRegistrada;

                lblMensaje.Text = string.Format(
                    Traductor.Instancia.Traducir("frmSeleccionarTurno.MsgReservaRegistrada"),
                    ReservaRegistrada.Codigo);

                btnRegistrarReserva.Enabled = false;
            }
        }

        private void BloquearDatosLuegoDelPago()
        {
            dtpFecha.Enabled = false;
            cboHorario.Enabled = false;
            btnBuscar.Enabled = false;
            dgvCanchas.Enabled = false;
            btnSeleccionar.Enabled = false;
            btnAgregarEquipamiento.Enabled = false;
            btnCobrarReserva.Enabled = false;
            btnRegistrarReserva.Enabled = false;
            btnContinuar.Enabled = false;
            btnCobrarReserva.Enabled = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LimpiarResultados()
        {
            dgvCanchas.DataSource = null;
            _tarifaActual = null;
            lblTarifaValor.Text = "-";
            btnSeleccionar.Enabled = false;
            LimpiarSeleccion();
        }

        private void LimpiarSeleccion()
        {
            FechaSeleccionada = default;
            HorarioSeleccionado = default;
            CanchaSeleccionada = null;
            TarifaSeleccionada = null;
            CantidadPaletas = 0;
            CantidadPelotas = 0;
            ImporteEquipamiento = 0;
            ClienteSeleccionado = null;
            FacturaPagada = null;
            PagoAprobado = null;
            ReservaRegistrada = null;

            lblFechaSelValor.Text = "-";
            lblHorarioSelValor.Text = "-";
            lblCanchaSelValor.Text = "-";
            lblTarifaSelValor.Text = "-";
            lblEquipamientoSelValor.Text = Traductor.Instancia.Traducir("frmSeleccionarTurno.SinEquipamiento");
            btnAgregarEquipamiento.Enabled = false;
            btnContinuar.Enabled = false;
        }
    }
}
