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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            colIdCancha = new DataGridViewTextBoxColumn();
            colCancha = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
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
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(840, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Seleccionar Turno";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbBusqueda
            // 
            gbBusqueda.BackColor = Color.FromArgb(24, 70, 138);
            gbBusqueda.Controls.Add(btnBuscar);
            gbBusqueda.Controls.Add(cboHorario);
            gbBusqueda.Controls.Add(lblHorario);
            gbBusqueda.Controls.Add(dtpFecha);
            gbBusqueda.Controls.Add(lblFecha);
            gbBusqueda.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbBusqueda.ForeColor = Color.White;
            gbBusqueda.Location = new Point(20, 75);
            gbBusqueda.Name = "gbBusqueda";
            gbBusqueda.Size = new Size(840, 95);
            gbBusqueda.TabIndex = 1;
            gbBusqueda.TabStop = false;
            gbBusqueda.Text = "Búsqueda de turno";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(214, 246, 36);
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.FromArgb(18, 18, 18);
            btnBuscar.Location = new Point(650, 35);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(160, 32);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "Buscar disponibilidad";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cboHorario
            // 
            cboHorario.BackColor = Color.White;
            cboHorario.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHorario.FlatStyle = FlatStyle.Flat;
            cboHorario.ForeColor = Color.FromArgb(18, 18, 18);
            cboHorario.FormattingEnabled = true;
            cboHorario.Location = new Point(430, 39);
            cboHorario.Name = "cboHorario";
            cboHorario.Size = new Size(150, 25);
            cboHorario.TabIndex = 3;
            cboHorario.SelectedIndexChanged += cboHorario_SelectedIndexChanged;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.ForeColor = Color.White;
            lblHorario.Location = new Point(365, 42);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(59, 17);
            lblHorario.TabIndex = 2;
            lblHorario.Text = "Horario:";
            // 
            // dtpFecha
            // 
            dtpFecha.CalendarForeColor = Color.FromArgb(18, 18, 18);
            dtpFecha.CalendarMonthBackground = Color.White;
            dtpFecha.CalendarTitleBackColor = Color.FromArgb(24, 70, 138);
            dtpFecha.CalendarTitleForeColor = Color.White;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(110, 39);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(180, 24);
            dtpFecha.TabIndex = 1;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.ForeColor = Color.White;
            lblFecha.Location = new Point(35, 42);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(47, 17);
            lblFecha.TabIndex = 0;
            lblFecha.Text = "Fecha:";
            // 
            // gbDisponibles
            // 
            gbDisponibles.BackColor = Color.FromArgb(24, 70, 138);
            gbDisponibles.Controls.Add(lblTarifaValor);
            gbDisponibles.Controls.Add(lblTarifaTitulo);
            gbDisponibles.Controls.Add(btnSeleccionar);
            gbDisponibles.Controls.Add(dgvCanchas);
            gbDisponibles.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbDisponibles.ForeColor = Color.White;
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
            lblTarifaValor.ForeColor = Color.White;
            lblTarifaValor.Location = new Point(530, 31);
            lblTarifaValor.Name = "lblTarifaValor";
            lblTarifaValor.Size = new Size(280, 23);
            lblTarifaValor.TabIndex = 3;
            lblTarifaValor.Text = "-";
            // 
            // lblTarifaTitulo
            // 
            lblTarifaTitulo.AutoSize = true;
            lblTarifaTitulo.ForeColor = Color.White;
            lblTarifaTitulo.Location = new Point(475, 35);
            lblTarifaTitulo.Name = "lblTarifaTitulo";
            lblTarifaTitulo.Size = new Size(47, 17);
            lblTarifaTitulo.TabIndex = 2;
            lblTarifaTitulo.Text = "Tarifa:";
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.BackColor = Color.FromArgb(214, 246, 36);
            btnSeleccionar.Enabled = false;
            btnSeleccionar.FlatAppearance.BorderSize = 0;
            btnSeleccionar.FlatStyle = FlatStyle.Flat;
            btnSeleccionar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSeleccionar.ForeColor = Color.FromArgb(18, 18, 18);
            btnSeleccionar.Location = new Point(650, 197);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Size = new Size(160, 32);
            btnSeleccionar.TabIndex = 1;
            btnSeleccionar.Text = "Seleccionar turno";
            btnSeleccionar.UseVisualStyleBackColor = false;
            btnSeleccionar.Click += btnSeleccionar_Click;
            // 
            // dgvCanchas
            // 
            dgvCanchas.AllowUserToAddRows = false;
            dgvCanchas.AllowUserToDeleteRows = false;
            dgvCanchas.AllowUserToResizeRows = false;
            dgvCanchas.AutoGenerateColumns = false;
            dgvCanchas.BackgroundColor = Color.White;
            dgvCanchas.BorderStyle = BorderStyle.None;
            dgvCanchas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCanchas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(214, 246, 36);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCanchas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCanchas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCanchas.Columns.AddRange(new DataGridViewColumn[] { colIdCancha, colCancha, colEstado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(211, 230, 248);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvCanchas.DefaultCellStyle = dataGridViewCellStyle2;
            dgvCanchas.EnableHeadersVisualStyles = false;
            dgvCanchas.GridColor = Color.FromArgb(64, 103, 166);
            dgvCanchas.Location = new Point(25, 65);
            dgvCanchas.MultiSelect = false;
            dgvCanchas.Name = "dgvCanchas";
            dgvCanchas.ReadOnly = true;
            dgvCanchas.RowHeadersVisible = false;
            dgvCanchas.RowTemplate.Height = 30;
            dgvCanchas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCanchas.Size = new Size(785, 120);
            dgvCanchas.TabIndex = 0;
            dgvCanchas.SelectionChanged += dgvCanchas_SelectionChanged;
            // 
            // colIdCancha
            // 
            colIdCancha.DataPropertyName = "IdCancha";
            colIdCancha.HeaderText = "ID";
            colIdCancha.Name = "colIdCancha";
            colIdCancha.ReadOnly = true;
            colIdCancha.Visible = false;
            // 
            // colCancha
            // 
            colCancha.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCancha.DataPropertyName = "Nombre";
            colCancha.HeaderText = "Cancha";
            colCancha.Name = "colCancha";
            colCancha.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colEstado.DataPropertyName = "Estado";
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // gbSeleccionado
            // 
            gbSeleccionado.BackColor = Color.FromArgb(24, 70, 138);
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
            gbSeleccionado.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbSeleccionado.ForeColor = Color.White;
            gbSeleccionado.Location = new Point(20, 445);
            gbSeleccionado.Name = "gbSeleccionado";
            gbSeleccionado.Size = new Size(840, 145);
            gbSeleccionado.TabIndex = 3;
            gbSeleccionado.TabStop = false;
            gbSeleccionado.Text = "Turno seleccionado";
            // 
            // btnAgregarEquipamiento
            // 
            btnAgregarEquipamiento.BackColor = Color.FromArgb(214, 246, 36);
            btnAgregarEquipamiento.Enabled = false;
            btnAgregarEquipamiento.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnAgregarEquipamiento.FlatStyle = FlatStyle.Flat;
            btnAgregarEquipamiento.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnAgregarEquipamiento.ForeColor = Color.Black;
            btnAgregarEquipamiento.Location = new Point(650, 102);
            btnAgregarEquipamiento.Name = "btnAgregarEquipamiento";
            btnAgregarEquipamiento.Size = new Size(160, 30);
            btnAgregarEquipamiento.TabIndex = 10;
            btnAgregarEquipamiento.Text = "Agregar Equipamiento";
            btnAgregarEquipamiento.UseVisualStyleBackColor = false;
            btnAgregarEquipamiento.Click += btnAgregarEquipamiento_Click;
            // 
            // lblEquipamientoSelValor
            // 
            lblEquipamientoSelValor.ForeColor = Color.White;
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
            lblEquipamientoSelTitulo.ForeColor = Color.White;
            lblEquipamientoSelTitulo.Location = new Point(35, 108);
            lblEquipamientoSelTitulo.Name = "lblEquipamientoSelTitulo";
            lblEquipamientoSelTitulo.Size = new Size(86, 15);
            lblEquipamientoSelTitulo.TabIndex = 8;
            lblEquipamientoSelTitulo.Text = "Equipamiento:";
            // 
            // lblTarifaSelValor
            // 
            lblTarifaSelValor.ForeColor = Color.White;
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
            lblTarifaSelTitulo.ForeColor = Color.White;
            lblTarifaSelTitulo.Location = new Point(450, 68);
            lblTarifaSelTitulo.Name = "lblTarifaSelTitulo";
            lblTarifaSelTitulo.Size = new Size(41, 15);
            lblTarifaSelTitulo.TabIndex = 6;
            lblTarifaSelTitulo.Text = "Tarifa:";
            // 
            // lblCanchaSelValor
            // 
            lblCanchaSelValor.ForeColor = Color.White;
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
            lblCanchaSelTitulo.ForeColor = Color.White;
            lblCanchaSelTitulo.Location = new Point(35, 68);
            lblCanchaSelTitulo.Name = "lblCanchaSelTitulo";
            lblCanchaSelTitulo.Size = new Size(49, 15);
            lblCanchaSelTitulo.TabIndex = 4;
            lblCanchaSelTitulo.Text = "Cancha:";
            // 
            // lblHorarioSelValor
            // 
            lblHorarioSelValor.ForeColor = Color.White;
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
            lblHorarioSelTitulo.ForeColor = Color.White;
            lblHorarioSelTitulo.Location = new Point(450, 33);
            lblHorarioSelTitulo.Name = "lblHorarioSelTitulo";
            lblHorarioSelTitulo.Size = new Size(52, 15);
            lblHorarioSelTitulo.TabIndex = 2;
            lblHorarioSelTitulo.Text = "Horario:";
            // 
            // lblFechaSelValor
            // 
            lblFechaSelValor.ForeColor = Color.White;
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
            lblFechaSelTitulo.ForeColor = Color.White;
            lblFechaSelTitulo.Location = new Point(35, 33);
            lblFechaSelTitulo.Name = "lblFechaSelTitulo";
            lblFechaSelTitulo.Size = new Size(42, 15);
            lblFechaSelTitulo.TabIndex = 0;
            lblFechaSelTitulo.Text = "Fecha:";
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 11F);
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            lblMensaje.Location = new Point(20, 653);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(540, 40);
            lblMensaje.TabIndex = 4;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnContinuar
            // 
            btnContinuar.BackColor = Color.FromArgb(214, 246, 36);
            btnContinuar.Enabled = false;
            btnContinuar.FlatAppearance.BorderSize = 0;
            btnContinuar.FlatStyle = FlatStyle.Flat;
            btnContinuar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnContinuar.ForeColor = Color.FromArgb(18, 18, 18);
            btnContinuar.Location = new Point(340, 605);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(160, 34);
            btnContinuar.TabIndex = 5;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = false;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // btnCobrarReserva
            // 
            btnCobrarReserva.BackColor = Color.FromArgb(214, 246, 36);
            btnCobrarReserva.Enabled = false;
            btnCobrarReserva.FlatAppearance.BorderSize = 0;
            btnCobrarReserva.FlatStyle = FlatStyle.Flat;
            btnCobrarReserva.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCobrarReserva.ForeColor = Color.FromArgb(18, 18, 18);
            btnCobrarReserva.Location = new Point(520, 605);
            btnCobrarReserva.Name = "btnCobrarReserva";
            btnCobrarReserva.Size = new Size(160, 34);
            btnCobrarReserva.TabIndex = 7;
            btnCobrarReserva.Text = "Cobrar Reserva";
            btnCobrarReserva.UseVisualStyleBackColor = false;
            btnCobrarReserva.Click += btnCobrarReserva_Click;
            // 
            // btnRegistrarReserva
            // 
            btnRegistrarReserva.BackColor = Color.FromArgb(214, 246, 36);
            btnRegistrarReserva.Enabled = false;
            btnRegistrarReserva.FlatAppearance.BorderSize = 0;
            btnRegistrarReserva.FlatStyle = FlatStyle.Flat;
            btnRegistrarReserva.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegistrarReserva.ForeColor = Color.FromArgb(18, 18, 18);
            btnRegistrarReserva.Location = new Point(700, 605);
            btnRegistrarReserva.Name = "btnRegistrarReserva";
            btnRegistrarReserva.Size = new Size(160, 34);
            btnRegistrarReserva.TabIndex = 8;
            btnRegistrarReserva.Text = "Registrar Reserva";
            btnRegistrarReserva.UseVisualStyleBackColor = false;
            btnRegistrarReserva.Click += btnRegistrarReserva_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(20, 605);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.TabIndex = 6;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // frmSeleccionarTurno
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
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
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
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
