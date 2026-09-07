namespace UI
{
    partial class frmSeleccionarTurno
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitulo = new Label();
            gbBusqueda = new GroupBox();
            btnBuscar = new Button();
            cboHorario = new ComboBox();
            lblHorario = new Label();
            dtpFecha = new DateTimePicker();
            lblFecha = new Label();
            gbDisponibles = new GroupBox();
            lblTarifaValor = new Label();
            lblTarifaTitulo = new Label();
            btnSeleccionar = new Button();
            dgvCanchas = new DataGridView();
            gbSeleccionado = new GroupBox();
            btnAgregarEquipamiento = new Button();
            lblEquipamientoSelValor = new Label();
            lblEquipamientoSelTitulo = new Label();
            lblTarifaSelValor = new Label();
            lblTarifaSelTitulo = new Label();
            lblCanchaSelValor = new Label();
            lblCanchaSelTitulo = new Label();
            lblHorarioSelValor = new Label();
            lblHorarioSelTitulo = new Label();
            lblFechaSelValor = new Label();
            lblFechaSelTitulo = new Label();
            lblMensaje = new Label();
            btnContinuar = new Button();
            btnCobrarReserva = new Button();
            btnRegistrarReserva = new Button();
            btnVolver = new Button();
            errorProvider = new ErrorProvider(components);
            gbBusqueda.SuspendLayout();
            gbDisponibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCanchas).BeginInit();
            gbSeleccionado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(840, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Seleccionar Turno";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbBusqueda
            // 
            gbBusqueda.BackColor = Color.LightSkyBlue;
            gbBusqueda.Controls.Add(btnBuscar);
            gbBusqueda.Controls.Add(cboHorario);
            gbBusqueda.Controls.Add(lblHorario);
            gbBusqueda.Controls.Add(dtpFecha);
            gbBusqueda.Controls.Add(lblFecha);
            gbBusqueda.Location = new Point(20, 75);
            gbBusqueda.Name = "gbBusqueda";
            gbBusqueda.Size = new Size(840, 95);
            gbBusqueda.TabIndex = 1;
            gbBusqueda.TabStop = false;
            gbBusqueda.Text = "Búsqueda de turno";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(650, 35);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(160, 32);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "Buscar disponibilidad";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cboHorario
            // 
            cboHorario.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHorario.FormattingEnabled = true;
            cboHorario.Location = new Point(430, 39);
            cboHorario.Name = "cboHorario";
            cboHorario.Size = new Size(150, 23);
            cboHorario.TabIndex = 3;
            cboHorario.SelectedIndexChanged += cboHorario_SelectedIndexChanged;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Location = new Point(365, 42);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(50, 15);
            lblHorario.TabIndex = 2;
            lblHorario.Text = "Horario:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(110, 39);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(180, 23);
            dtpFecha.TabIndex = 1;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(35, 42);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(41, 15);
            lblFecha.TabIndex = 0;
            lblFecha.Text = "Fecha:";
            // 
            // gbDisponibles
            // 
            gbDisponibles.BackColor = Color.PowderBlue;
            gbDisponibles.Controls.Add(lblTarifaValor);
            gbDisponibles.Controls.Add(lblTarifaTitulo);
            gbDisponibles.Controls.Add(btnSeleccionar);
            gbDisponibles.Controls.Add(dgvCanchas);
            gbDisponibles.Location = new Point(20, 185);
            gbDisponibles.Name = "gbDisponibles";
            gbDisponibles.Size = new Size(840, 245);
            gbDisponibles.TabIndex = 2;
            gbDisponibles.TabStop = false;
            gbDisponibles.Text = "Canchas disponibles";
            // 
            // lblTarifaValor
            // 
            lblTarifaValor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTarifaValor.Location = new Point(530, 31);
            lblTarifaValor.Name = "lblTarifaValor";
            lblTarifaValor.Size = new Size(280, 23);
            lblTarifaValor.TabIndex = 3;
            lblTarifaValor.Text = "-";
            // 
            // lblTarifaTitulo
            // 
            lblTarifaTitulo.AutoSize = true;
            lblTarifaTitulo.Location = new Point(475, 35);
            lblTarifaTitulo.Name = "lblTarifaTitulo";
            lblTarifaTitulo.Size = new Size(39, 15);
            lblTarifaTitulo.TabIndex = 2;
            lblTarifaTitulo.Text = "Tarifa:";
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.Enabled = false;
            btnSeleccionar.Location = new Point(650, 197);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Size = new Size(160, 32);
            btnSeleccionar.TabIndex = 1;
            btnSeleccionar.Text = "Seleccionar turno";
            btnSeleccionar.UseVisualStyleBackColor = true;
            btnSeleccionar.Click += btnSeleccionar_Click;
            // 
            // dgvCanchas
            // 
            dgvCanchas.AllowUserToAddRows = false;
            dgvCanchas.AllowUserToDeleteRows = false;
            dgvCanchas.AllowUserToResizeRows = false;
            dgvCanchas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCanchas.Location = new Point(25, 65);
            dgvCanchas.MultiSelect = false;
            dgvCanchas.Name = "dgvCanchas";
            dgvCanchas.ReadOnly = true;
            dgvCanchas.RowHeadersVisible = false;
            dgvCanchas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCanchas.Size = new Size(785, 120);
            dgvCanchas.TabIndex = 0;
            dgvCanchas.SelectionChanged += dgvCanchas_SelectionChanged;
            // 
            // gbSeleccionado
            // 
            gbSeleccionado.BackColor = Color.LightSkyBlue;
            gbSeleccionado.Controls.Add(btnAgregarEquipamiento);
            gbSeleccionado.Controls.Add(lblEquipamientoSelValor);
            gbSeleccionado.Controls.Add(lblEquipamientoSelTitulo);
            gbSeleccionado.Controls.Add(lblTarifaSelValor);
            gbSeleccionado.Controls.Add(lblTarifaSelTitulo);
            gbSeleccionado.Controls.Add(lblCanchaSelValor);
            gbSeleccionado.Controls.Add(lblCanchaSelTitulo);
            gbSeleccionado.Controls.Add(lblHorarioSelValor);
            gbSeleccionado.Controls.Add(lblHorarioSelTitulo);
            gbSeleccionado.Controls.Add(lblFechaSelValor);
            gbSeleccionado.Controls.Add(lblFechaSelTitulo);
            gbSeleccionado.Location = new Point(20, 445);
            gbSeleccionado.Name = "gbSeleccionado";
            gbSeleccionado.Size = new Size(840, 145);
            gbSeleccionado.TabIndex = 3;
            gbSeleccionado.TabStop = false;
            gbSeleccionado.Text = "Turno seleccionado";
            // 
            // btnAgregarEquipamiento
            // 
            btnAgregarEquipamiento.Enabled = false;
            btnAgregarEquipamiento.Location = new Point(650, 102);
            btnAgregarEquipamiento.Name = "btnAgregarEquipamiento";
            btnAgregarEquipamiento.Size = new Size(160, 30);
            btnAgregarEquipamiento.TabIndex = 10;
            btnAgregarEquipamiento.Text = "Agregar Equipamiento";
            btnAgregarEquipamiento.UseVisualStyleBackColor = true;
            btnAgregarEquipamiento.Click += btnAgregarEquipamiento_Click;
            // 
            // lblEquipamientoSelValor
            // 
            lblEquipamientoSelValor.Location = new Point(140, 105);
            lblEquipamientoSelValor.Name = "lblEquipamientoSelValor";
            lblEquipamientoSelValor.Size = new Size(480, 23);
            lblEquipamientoSelValor.TabIndex = 9;
            lblEquipamientoSelValor.Text = "Sin equipamiento adicional";
            // 
            // lblEquipamientoSelTitulo
            // 
            lblEquipamientoSelTitulo.AutoSize = true;
            lblEquipamientoSelTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEquipamientoSelTitulo.Location = new Point(35, 108);
            lblEquipamientoSelTitulo.Name = "lblEquipamientoSelTitulo";
            lblEquipamientoSelTitulo.Size = new Size(86, 15);
            lblEquipamientoSelTitulo.TabIndex = 8;
            lblEquipamientoSelTitulo.Text = "Equipamiento:";
            // 
            // lblTarifaSelValor
            // 
            lblTarifaSelValor.Location = new Point(510, 65);
            lblTarifaSelValor.Name = "lblTarifaSelValor";
            lblTarifaSelValor.Size = new Size(300, 23);
            lblTarifaSelValor.TabIndex = 7;
            lblTarifaSelValor.Text = "-";
            // 
            // lblTarifaSelTitulo
            // 
            lblTarifaSelTitulo.AutoSize = true;
            lblTarifaSelTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTarifaSelTitulo.Location = new Point(450, 68);
            lblTarifaSelTitulo.Name = "lblTarifaSelTitulo";
            lblTarifaSelTitulo.Size = new Size(41, 15);
            lblTarifaSelTitulo.TabIndex = 6;
            lblTarifaSelTitulo.Text = "Tarifa:";
            // 
            // lblCanchaSelValor
            // 
            lblCanchaSelValor.Location = new Point(110, 65);
            lblCanchaSelValor.Name = "lblCanchaSelValor";
            lblCanchaSelValor.Size = new Size(250, 23);
            lblCanchaSelValor.TabIndex = 5;
            lblCanchaSelValor.Text = "-";
            // 
            // lblCanchaSelTitulo
            // 
            lblCanchaSelTitulo.AutoSize = true;
            lblCanchaSelTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCanchaSelTitulo.Location = new Point(35, 68);
            lblCanchaSelTitulo.Name = "lblCanchaSelTitulo";
            lblCanchaSelTitulo.Size = new Size(49, 15);
            lblCanchaSelTitulo.TabIndex = 4;
            lblCanchaSelTitulo.Text = "Cancha:";
            // 
            // lblHorarioSelValor
            // 
            lblHorarioSelValor.Location = new Point(510, 30);
            lblHorarioSelValor.Name = "lblHorarioSelValor";
            lblHorarioSelValor.Size = new Size(180, 23);
            lblHorarioSelValor.TabIndex = 3;
            lblHorarioSelValor.Text = "-";
            // 
            // lblHorarioSelTitulo
            // 
            lblHorarioSelTitulo.AutoSize = true;
            lblHorarioSelTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHorarioSelTitulo.Location = new Point(450, 33);
            lblHorarioSelTitulo.Name = "lblHorarioSelTitulo";
            lblHorarioSelTitulo.Size = new Size(52, 15);
            lblHorarioSelTitulo.TabIndex = 2;
            lblHorarioSelTitulo.Text = "Horario:";
            // 
            // lblFechaSelValor
            // 
            lblFechaSelValor.Location = new Point(110, 30);
            lblFechaSelValor.Name = "lblFechaSelValor";
            lblFechaSelValor.Size = new Size(180, 23);
            lblFechaSelValor.TabIndex = 1;
            lblFechaSelValor.Text = "-";
            // 
            // lblFechaSelTitulo
            // 
            lblFechaSelTitulo.AutoSize = true;
            lblFechaSelTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaSelTitulo.Location = new Point(35, 33);
            lblFechaSelTitulo.Name = "lblFechaSelTitulo";
            lblFechaSelTitulo.Size = new Size(42, 15);
            lblFechaSelTitulo.TabIndex = 0;
            lblFechaSelTitulo.Text = "Fecha:";
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 11F);
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 653);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(540, 40);
            lblMensaje.TabIndex = 4;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnContinuar
            // 
            btnContinuar.Enabled = false;
            btnContinuar.Location = new Point(340, 605);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(160, 34);
            btnContinuar.TabIndex = 5;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // btnCobrarReserva
            // 
            btnCobrarReserva.Enabled = false;
            btnCobrarReserva.Location = new Point(520, 605);
            btnCobrarReserva.Name = "btnCobrarReserva";
            btnCobrarReserva.Size = new Size(160, 34);
            btnCobrarReserva.TabIndex = 7;
            btnCobrarReserva.Text = "Cobrar Reserva";
            btnCobrarReserva.UseVisualStyleBackColor = true;
            btnCobrarReserva.Click += btnCobrarReserva_Click;
            // 
            // btnRegistrarReserva
            // 
            btnRegistrarReserva.Enabled = false;
            btnRegistrarReserva.Location = new Point(700, 605);
            btnRegistrarReserva.Name = "btnRegistrarReserva";
            btnRegistrarReserva.Size = new Size(160, 34);
            btnRegistrarReserva.TabIndex = 8;
            btnRegistrarReserva.Text = "Registrar Reserva";
            btnRegistrarReserva.UseVisualStyleBackColor = true;
            btnRegistrarReserva.Click += btnRegistrarReserva_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(20, 605);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.TabIndex = 6;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // frmSeleccionarTurno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(880, 712);
            Controls.Add(btnVolver);
            Controls.Add(btnRegistrarReserva);
            Controls.Add(btnCobrarReserva);
            Controls.Add(btnContinuar);
            Controls.Add(lblMensaje);
            Controls.Add(gbSeleccionado);
            Controls.Add(gbDisponibles);
            Controls.Add(gbBusqueda);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSeleccionarTurno";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Seleccionar Turno";
            FormClosed += frmSeleccionarTurno_FormClosed;
            Load += frmSeleccionarTurno_Load;
            gbBusqueda.ResumeLayout(false);
            gbBusqueda.PerformLayout();
            gbDisponibles.ResumeLayout(false);
            gbDisponibles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCanchas).EndInit();
            gbSeleccionado.ResumeLayout(false);
            gbSeleccionado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private GroupBox gbBusqueda;
        private Label lblFecha;
        private DateTimePicker dtpFecha;
        private Label lblHorario;
        private ComboBox cboHorario;
        private Button btnBuscar;
        private GroupBox gbDisponibles;
        private DataGridView dgvCanchas;
        private DataGridViewTextBoxColumn colIdCancha;
        private DataGridViewTextBoxColumn colCancha;
        private DataGridViewTextBoxColumn colEstado;
        private Button btnSeleccionar;
        private Label lblTarifaTitulo;
        private Label lblTarifaValor;
        private GroupBox gbSeleccionado;
        private Label lblFechaSelTitulo;
        private Label lblFechaSelValor;
        private Label lblHorarioSelTitulo;
        private Label lblHorarioSelValor;
        private Label lblCanchaSelTitulo;
        private Label lblCanchaSelValor;
        private Label lblTarifaSelTitulo;
        private Label lblTarifaSelValor;
        private Label lblEquipamientoSelTitulo;
        private Label lblEquipamientoSelValor;
        private Button btnAgregarEquipamiento;
        private Label lblMensaje;
        private Button btnContinuar;
        private Button btnCobrarReserva;
        private Button btnRegistrarReserva;
        private Button btnVolver;
        private ErrorProvider errorProvider;
    }
}
