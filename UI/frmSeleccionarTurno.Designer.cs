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
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            // AirPadel style preview BEGIN
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            gbBusqueda.BackColor = Color.FromArgb(24, 70, 138);
            gbBusqueda.ForeColor = Color.White;
            gbBusqueda.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbBusqueda.Size = new Size(840, 95);
            gbBusqueda.TabIndex = 1;
            gbBusqueda.TabStop = false;
            gbBusqueda.Text = "Búsqueda de turno";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(650, 35);
            btnBuscar.Name = "btnBuscar";
            // AirPadel style preview BEGIN
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.BackColor = Color.FromArgb(214, 246, 36);
            btnBuscar.ForeColor = Color.FromArgb(18, 18, 18);
            btnBuscar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnBuscar.Size = new Size(160, 32);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "Buscar disponibilidad";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cboHorario
            // 
            cboHorario.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHorario.FormattingEnabled = true;
            cboHorario.Location = new Point(430, 39);
            cboHorario.Name = "cboHorario";
            // AirPadel style preview BEGIN
            cboHorario.BackColor = Color.White;
            cboHorario.ForeColor = Color.FromArgb(18, 18, 18);
            cboHorario.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cboHorario.Size = new Size(150, 23);
            cboHorario.TabIndex = 3;
            cboHorario.SelectedIndexChanged += cboHorario_SelectedIndexChanged;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Location = new Point(365, 42);
            lblHorario.Name = "lblHorario";
            // AirPadel style preview BEGIN
            lblHorario.ForeColor = Color.White;
            // AirPadel style preview END
            lblHorario.Size = new Size(50, 15);
            lblHorario.TabIndex = 2;
            lblHorario.Text = "Horario:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(110, 39);
            dtpFecha.Name = "dtpFecha";
            // AirPadel style preview BEGIN
            dtpFecha.CalendarForeColor = Color.FromArgb(18, 18, 18);
            dtpFecha.CalendarMonthBackground = Color.White;
            dtpFecha.CalendarTitleBackColor = Color.FromArgb(24, 70, 138);
            dtpFecha.CalendarTitleForeColor = Color.White;
            // AirPadel style preview END
            dtpFecha.Size = new Size(180, 23);
            dtpFecha.TabIndex = 1;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(35, 42);
            lblFecha.Name = "lblFecha";
            // AirPadel style preview BEGIN
            lblFecha.ForeColor = Color.White;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            gbDisponibles.BackColor = Color.FromArgb(24, 70, 138);
            gbDisponibles.ForeColor = Color.White;
            gbDisponibles.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            lblTarifaValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblTarifaValor.Size = new Size(280, 23);
            lblTarifaValor.TabIndex = 3;
            lblTarifaValor.Text = "-";
            // 
            // lblTarifaTitulo
            // 
            lblTarifaTitulo.AutoSize = true;
            lblTarifaTitulo.Location = new Point(475, 35);
            lblTarifaTitulo.Name = "lblTarifaTitulo";
            // AirPadel style preview BEGIN
            lblTarifaTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblTarifaTitulo.Size = new Size(39, 15);
            lblTarifaTitulo.TabIndex = 2;
            lblTarifaTitulo.Text = "Tarifa:";
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.Enabled = false;
            btnSeleccionar.Location = new Point(650, 197);
            btnSeleccionar.Name = "btnSeleccionar";
            // AirPadel style preview BEGIN
            btnSeleccionar.FlatStyle = FlatStyle.Flat;
            btnSeleccionar.FlatAppearance.BorderSize = 0;
            btnSeleccionar.BackColor = Color.FromArgb(214, 246, 36);
            btnSeleccionar.ForeColor = Color.FromArgb(18, 18, 18);
            btnSeleccionar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSeleccionar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
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
            dgvCanchas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCanchas.Columns.AddRange(new DataGridViewColumn[] { colIdCancha, colCancha, colEstado });
            dgvCanchas.Location = new Point(25, 65);
            dgvCanchas.MultiSelect = false;
            dgvCanchas.Name = "dgvCanchas";
            // AirPadel style preview BEGIN
            dgvCanchas.BackgroundColor = Color.White;
            dgvCanchas.BorderStyle = BorderStyle.None;
            dgvCanchas.GridColor = Color.FromArgb(64, 103, 166);
            dgvCanchas.EnableHeadersVisualStyles = false;
            dgvCanchas.RowHeadersVisible = false;
            dgvCanchas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCanchas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCanchas.ColumnHeadersHeight = 36;
            dgvCanchas.RowTemplate.Height = 30;
            dgvCanchas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(214, 246, 36);
            dgvCanchas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(18, 18, 18);
            dgvCanchas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 230, 248);
            dgvCanchas.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 18, 18);
            // AirPadel style preview END
            dgvCanchas.ReadOnly = true;
            dgvCanchas.RowHeadersVisible = false;
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
            // AirPadel style preview BEGIN
            gbSeleccionado.BackColor = Color.FromArgb(24, 70, 138);
            gbSeleccionado.ForeColor = Color.White;
            gbSeleccionado.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            btnAgregarEquipamiento.FlatStyle = FlatStyle.Flat;
            btnAgregarEquipamiento.FlatAppearance.BorderSize = 1;
            btnAgregarEquipamiento.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnAgregarEquipamiento.BackColor = Color.FromArgb(24, 70, 138);
            btnAgregarEquipamiento.ForeColor = Color.White;
            btnAgregarEquipamiento.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAgregarEquipamiento.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnAgregarEquipamiento.Size = new Size(160, 30);
            btnAgregarEquipamiento.TabIndex = 10;
            btnAgregarEquipamiento.Text = "Agregar Equipamiento";
            btnAgregarEquipamiento.UseVisualStyleBackColor = false;
            btnAgregarEquipamiento.Click += btnAgregarEquipamiento_Click;
            // 
            // lblEquipamientoSelValor
            // 
            lblEquipamientoSelValor.Location = new Point(140, 105);
            lblEquipamientoSelValor.Name = "lblEquipamientoSelValor";
            // AirPadel style preview BEGIN
            lblEquipamientoSelValor.ForeColor = Color.White;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            lblEquipamientoSelTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblEquipamientoSelTitulo.Size = new Size(86, 15);
            lblEquipamientoSelTitulo.TabIndex = 8;
            lblEquipamientoSelTitulo.Text = "Equipamiento:";
            // 
            // lblTarifaSelValor
            // 
            lblTarifaSelValor.Location = new Point(510, 65);
            lblTarifaSelValor.Name = "lblTarifaSelValor";
            // AirPadel style preview BEGIN
            lblTarifaSelValor.ForeColor = Color.White;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            lblTarifaSelTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblTarifaSelTitulo.Size = new Size(41, 15);
            lblTarifaSelTitulo.TabIndex = 6;
            lblTarifaSelTitulo.Text = "Tarifa:";
            // 
            // lblCanchaSelValor
            // 
            lblCanchaSelValor.Location = new Point(110, 65);
            lblCanchaSelValor.Name = "lblCanchaSelValor";
            // AirPadel style preview BEGIN
            lblCanchaSelValor.ForeColor = Color.White;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            lblCanchaSelTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblCanchaSelTitulo.Size = new Size(49, 15);
            lblCanchaSelTitulo.TabIndex = 4;
            lblCanchaSelTitulo.Text = "Cancha:";
            // 
            // lblHorarioSelValor
            // 
            lblHorarioSelValor.Location = new Point(510, 30);
            lblHorarioSelValor.Name = "lblHorarioSelValor";
            // AirPadel style preview BEGIN
            lblHorarioSelValor.ForeColor = Color.White;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            lblHorarioSelTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblHorarioSelTitulo.Size = new Size(52, 15);
            lblHorarioSelTitulo.TabIndex = 2;
            lblHorarioSelTitulo.Text = "Horario:";
            // 
            // lblFechaSelValor
            // 
            lblFechaSelValor.Location = new Point(110, 30);
            lblFechaSelValor.Name = "lblFechaSelValor";
            // AirPadel style preview BEGIN
            lblFechaSelValor.ForeColor = Color.White;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            lblFechaSelTitulo.ForeColor = Color.White;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(540, 40);
            lblMensaje.TabIndex = 4;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnContinuar
            // 
            btnContinuar.Enabled = false;
            btnContinuar.Location = new Point(340, 605);
            btnContinuar.Name = "btnContinuar";
            // AirPadel style preview BEGIN
            btnContinuar.FlatStyle = FlatStyle.Flat;
            btnContinuar.FlatAppearance.BorderSize = 0;
            btnContinuar.BackColor = Color.FromArgb(214, 246, 36);
            btnContinuar.ForeColor = Color.FromArgb(18, 18, 18);
            btnContinuar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnContinuar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnContinuar.Size = new Size(160, 34);
            btnContinuar.TabIndex = 5;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = false;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // btnCobrarReserva
            // 
            btnCobrarReserva.Enabled = false;
            btnCobrarReserva.Location = new Point(520, 605);
            btnCobrarReserva.Name = "btnCobrarReserva";
            // AirPadel style preview BEGIN
            btnCobrarReserva.FlatStyle = FlatStyle.Flat;
            btnCobrarReserva.FlatAppearance.BorderSize = 0;
            btnCobrarReserva.BackColor = Color.FromArgb(214, 246, 36);
            btnCobrarReserva.ForeColor = Color.FromArgb(18, 18, 18);
            btnCobrarReserva.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCobrarReserva.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnCobrarReserva.Size = new Size(160, 34);
            btnCobrarReserva.TabIndex = 7;
            btnCobrarReserva.Text = "Cobrar Reserva";
            btnCobrarReserva.UseVisualStyleBackColor = false;
            btnCobrarReserva.Click += btnCobrarReserva_Click;
            // 
            // btnRegistrarReserva
            // 
            btnRegistrarReserva.Enabled = false;
            btnRegistrarReserva.Location = new Point(700, 605);
            btnRegistrarReserva.Name = "btnRegistrarReserva";
            // AirPadel style preview BEGIN
            btnRegistrarReserva.FlatStyle = FlatStyle.Flat;
            btnRegistrarReserva.FlatAppearance.BorderSize = 0;
            btnRegistrarReserva.BackColor = Color.FromArgb(214, 246, 36);
            btnRegistrarReserva.ForeColor = Color.FromArgb(18, 18, 18);
            btnRegistrarReserva.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegistrarReserva.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnRegistrarReserva.Size = new Size(160, 34);
            btnRegistrarReserva.TabIndex = 8;
            btnRegistrarReserva.Text = "Registrar Reserva";
            btnRegistrarReserva.UseVisualStyleBackColor = false;
            btnRegistrarReserva.Click += btnRegistrarReserva_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(20, 605);
            btnVolver.Name = "btnVolver";
            // AirPadel style preview BEGIN
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 1;
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.ForeColor = Color.White;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.UseVisualStyleBackColor = false;
            // AirPadel style preview END
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
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
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
