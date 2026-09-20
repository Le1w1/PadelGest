using BE;
using BLL;
using Servicios;

namespace UI
{
    public partial class frmGenerarReserva : Form, IObservadorIdioma
    {
        #region "Campos"
        private readonly CanchaBLL _canchaBLL;
        private readonly TarifaBLL _tarifaBLL;
        private readonly EquipamientoBLL _equipamientoBLL;
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
        #endregion

        public frmGenerarReserva()
        {
            InitializeComponent();
            _canchaBLL = new CanchaBLL();
            _tarifaBLL = new TarifaBLL();
            _equipamientoBLL = new EquipamientoBLL();

            cboHorario.Format += cboHorario_Format;
            FormClosing += frmGenerarReserva_FormClosing;

            ActualizarIdioma();
        }

        private void frmGenerarReserva_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);

            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.MaxDate = DateTime.Today.AddDays(7);
            dtpFecha.Value = DateTime.Today;

            LimpiarResultados();
            CargarHorarios();
        }

        private void frmGenerarReserva_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        // Si el pago ya fue aprobado, el proceso no puede abandonarse hasta registrar la reserva.
        private void frmGenerarReserva_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (PagoAprobado != null && ReservaRegistrada == null && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            Text = t.Traducir("frmGenerarReserva.Title");
            lblTitulo.Text = t.Traducir("frmGenerarReserva.LblTitulo");
            gbBusqueda.Text = t.Traducir("frmGenerarReserva.GbBusqueda");
            lblFecha.Text = t.Traducir("frmGenerarReserva.LblFecha");
            lblHorario.Text = t.Traducir("frmGenerarReserva.LblHorario");
            btnBuscar.Text = t.Traducir("frmGenerarReserva.BtnBuscar");
            gbDisponibles.Text = t.Traducir("frmGenerarReserva.GbDisponibles");
            colCancha.HeaderText = t.Traducir("frmGenerarReserva.ColCancha");
            colEstado.HeaderText = t.Traducir("frmGenerarReserva.ColEstado");
            lblTarifaTitulo.Text = t.Traducir("frmGenerarReserva.LblTarifa");
            btnSeleccionar.Text = t.Traducir("frmGenerarReserva.BtnSeleccionar");
            gbSeleccionado.Text = t.Traducir("frmGenerarReserva.GbSeleccionado");
            lblFechaSelTitulo.Text = t.Traducir("frmGenerarReserva.LblFecha");
            lblHorarioSelTitulo.Text = t.Traducir("frmGenerarReserva.LblHorario");
            lblCanchaSelTitulo.Text = t.Traducir("frmGenerarReserva.LblCancha");
            lblTarifaSelTitulo.Text = t.Traducir("frmGenerarReserva.LblTarifa");
            lblEquipamientoSelTitulo.Text = t.Traducir("frmGenerarReserva.LblEquipamiento");
            btnAgregarEquipamiento.Text = t.Traducir("frmGenerarReserva.BtnAgregarEquipamiento");
            btnCobrarReserva.Text = t.Traducir("frmGenerarReserva.BtnCobrarReserva");
            btnRegistrarReserva.Text = t.Traducir("frmGenerarReserva.BtnRegistrarReserva");
            btnContinuar.Text = t.Traducir("frmGenerarReserva.BtnContinuar");
            btnVolver.Text = t.Traducir("frmGenerarReserva.BtnVolver");

            ActualizarResumenEquipamiento();
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


        // Formatea la visualización de los horarios en el ComboBox.
        private void cboHorario_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is TimeSpan horario)
            {
                e.Value = horario.ToString(@"hh\:mm");
            }
        }


        // Carga los horarios disponibles para la fecha seleccionada en el ComboBox.
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
                    lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgSinHorarios");
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


        // Busca las canchas disponibles para la fecha y horario seleccionados, y muestra la tarifa correspondiente.
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider.Clear();
                lblMensaje.Text = string.Empty;
                LimpiarSeleccion();

                // Validación de selección de horario
                if (cboHorario.SelectedItem is not TimeSpan horario)
                {
                    string mensaje = Traductor.Instancia.Traducir("frmGenerarReserva.MsgSeleccioneHorario");
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
                    lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgSinCanchas");
                }
            }
            // Manejo de excepciones para mostrar mensajes de error y limpiar resultados en caso de fallo.
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

        // Habilita o deshabilita el botón de selección según la cantidad de filas seleccionadas y si hay una tarifa actual.
        private void dgvCanchas_SelectionChanged(object sender, EventArgs e)
        {
            btnSeleccionar.Enabled = dgvCanchas.SelectedRows.Count == 1 && dgvCanchas.SelectedRows[0].DataBoundItem is CanchaBE && _tarifaActual != null;
        }


        // Valida la selección de cancha y horario, y guarda los datos seleccionados para su posterior uso.
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (dgvCanchas.SelectedRows.Count != 1 || dgvCanchas.SelectedRows[0].DataBoundItem is not CanchaBE cancha)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgSeleccioneCancha");
                return;
            }

            if (cboHorario.SelectedItem is not TimeSpan horario || _tarifaActual == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgBuscarNuevamente");
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


        // Abre el formulario para agregar equipamiento, y actualiza la cantidad y el importe del equipamiento seleccionado.
        private void btnAgregarEquipamiento_Click(object sender, EventArgs e)
        {
            if (CanchaSeleccionada == null || TarifaSeleccionada == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgSeleccioneCancha");
                return;
            }

            using frmAgregarEquipamiento formEquipamiento = new frmAgregarEquipamiento(FechaSeleccionada, HorarioSeleccionado, CantidadPaletas, CantidadPelotas);

            if (formEquipamiento.ShowDialog(this) == DialogResult.OK)
            {
                CantidadPaletas = formEquipamiento.CantidadPaletas;
                CantidadPelotas = formEquipamiento.CantidadPelotas;
                ImporteEquipamiento = formEquipamiento.ImporteEquipamiento;
                ActualizarResumenEquipamiento();
                lblMensaje.Text = string.Empty;
            }
        }


        // Actualiza el resumen del equipamiento seleccionado en la interfaz de usuario, mostrando la cantidad de paletas, pelotas y el importe total.
        private void ActualizarResumenEquipamiento()
        {
            if (CantidadPaletas == 0 && CantidadPelotas == 0)
            {
                lblEquipamientoSelValor.Text = Traductor.Instancia.Traducir("frmGenerarReserva.SinEquipamiento");
                return;
            }

            lblEquipamientoSelValor.Text = string.Format(Traductor.Instancia.Traducir("frmGenerarReserva.ResumenEquipamiento"),CantidadPaletas, CantidadPelotas, ImporteEquipamiento);
        }


        // Abre el formulario para seleccionar un cliente, y guarda el cliente seleccionado para su posterior uso.
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (CanchaSeleccionada == null || TarifaSeleccionada == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgSeleccioneCancha");
                return;
            }

            using frmSeleccionarCliente formCliente = new frmSeleccionarCliente();

            if (formCliente.ShowDialog(this) == DialogResult.OK && formCliente.ClienteSeleccionado != null)
            {
                ClienteSeleccionado = formCliente.ClienteSeleccionado;
                lblMensaje.Text = string.Format(Traductor.Instancia.Traducir("frmGenerarReserva.MsgClienteSeleccionado"),ClienteSeleccionado.DNI, ClienteSeleccionado.Nombre, ClienteSeleccionado.Apellido);
                btnCobrarReserva.Enabled = true;
            }
        }

        // Revalida turno y equipamiento inmediatamente antes de autorizar el cobro.
        private void ValidarDisponibilidadAntesDelCobro()
        {
            if (CanchaSeleccionada == null)
                throw new Exception(Traductor.Instancia.Traducir("frmGenerarReserva.MsgSeleccioneCancha"));

            List<CanchaBE> disponibles = _canchaBLL.ObtenerCanchasDisponibles(FechaSeleccionada, HorarioSeleccionado);
            if (!disponibles.Any(c => c.IdCancha == CanchaSeleccionada.IdCancha))
                throw new Exception(Traductor.Instancia.Traducir("Errores.Reserva.TurnoYaNoDisponible"));

            if (CantidadPaletas > 0 || CantidadPelotas > 0)
            {
                ImporteEquipamiento = _equipamientoBLL.ValidarYCalcularImporte(FechaSeleccionada, HorarioSeleccionado, CantidadPaletas, CantidadPelotas);
                ActualizarResumenEquipamiento();
            }
        }

        private void btnCobrarReserva_Click(object sender, EventArgs e)
        {
            if (ClienteSeleccionado == null || CanchaSeleccionada == null || TarifaSeleccionada == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgFaltanDatosCobro");
                return;
            }

            try
            {
                ValidarDisponibilidadAntesDelCobro();

                using frmCobrarReserva formCobro = new frmCobrarReserva(ClienteSeleccionado, CanchaSeleccionada, TarifaSeleccionada, FechaSeleccionada,HorarioSeleccionado, CantidadPaletas, CantidadPelotas, ImporteEquipamiento);

                if (formCobro.ShowDialog(this) == DialogResult.OK &&
                    formCobro.FacturaPagada != null && formCobro.PagoAprobado != null)
                {
                    FacturaPagada = formCobro.FacturaPagada;
                    PagoAprobado = formCobro.PagoAprobado;

                    lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgPagoAprobado");
                    BloquearDatosLuegoDelPago();

                    // Un pago aprobado obliga a continuar inmediatamente con el registro.
                    RegistrarReservaObligatoria();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRegistrarReserva_Click(object sender, EventArgs e)
        {
            RegistrarReservaObligatoria();
        }

        private void RegistrarReservaObligatoria()
        {
            if (ReservaRegistrada != null)
                return;

            if (ClienteSeleccionado == null || CanchaSeleccionada == null || TarifaSeleccionada == null ||
                FacturaPagada == null || PagoAprobado == null)
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("frmGenerarReserva.MsgFaltanDatosRegistro");
                return;
            }

            using frmRegistrarReserva formReserva = new frmRegistrarReserva(ClienteSeleccionado, CanchaSeleccionada, TarifaSeleccionada, FacturaPagada, PagoAprobado,FechaSeleccionada, HorarioSeleccionado, CantidadPaletas, CantidadPelotas);

            if (formReserva.ShowDialog(this) == DialogResult.OK && formReserva.ReservaRegistrada != null)
            {
                ReservaRegistrada = formReserva.ReservaRegistrada;
                lblMensaje.Text = string.Format(Traductor.Instancia.Traducir("frmGenerarReserva.MsgReservaRegistrada"),ReservaRegistrada.Codigo);

                btnRegistrarReserva.Enabled = false;
                btnVolver.Enabled = true;
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
            btnContinuar.Enabled = false;
            btnRegistrarReserva.Enabled = false;
            btnVolver.Enabled = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (PagoAprobado != null && ReservaRegistrada == null)
                return;

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
            lblEquipamientoSelValor.Text = Traductor.Instancia.Traducir("frmGenerarReserva.SinEquipamiento");
            btnAgregarEquipamiento.Enabled = false;
            btnContinuar.Enabled = false;
        }
    }
}
