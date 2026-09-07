namespace UI
{
    partial class frmAuditarEventos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gbFiltros = new GroupBox();
            txtDescripcion = new TextBox();
            cmbAccion = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            cmbResultado = new ComboBox();
            label5 = new Label();
            cmbCriticidad = new ComboBox();
            label4 = new Label();
            cmbModulo = new ComboBox();
            label3 = new Label();
            txtUsuario = new TextBox();
            label2 = new Label();
            lblFechaHasta = new Label();
            dtpFechaHasta = new DateTimePicker();
            DtpFechaDesde = new DateTimePicker();
            lblFechaDesde = new Label();
            dgvEventos = new DataGridView();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            btnVolver = new Button();
            lblMensaje = new Label();
            btnImprimir = new Button();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            label8 = new Label();
            label9 = new Label();
            gbDetalleReserva = new GroupBox();
            lblDescripcionReserva = new Label();
            txtDescripcionReserva = new TextBox();
            gbFiltros.SuspendLayout();
            gbDetalleReserva.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEventos).BeginInit();
            SuspendLayout();
            // 
            // gbFiltros
            // 
            gbFiltros.BackColor = Color.LightSkyBlue;
            gbFiltros.Controls.Add(txtDescripcion);
            gbFiltros.Controls.Add(cmbAccion);
            gbFiltros.Controls.Add(label7);
            gbFiltros.Controls.Add(label6);
            gbFiltros.Controls.Add(cmbResultado);
            gbFiltros.Controls.Add(label5);
            gbFiltros.Controls.Add(cmbCriticidad);
            gbFiltros.Controls.Add(label4);
            gbFiltros.Controls.Add(cmbModulo);
            gbFiltros.Controls.Add(label3);
            gbFiltros.Controls.Add(txtUsuario);
            gbFiltros.Controls.Add(label2);
            gbFiltros.Controls.Add(lblFechaHasta);
            gbFiltros.Controls.Add(dtpFechaHasta);
            gbFiltros.Controls.Add(DtpFechaDesde);
            gbFiltros.Controls.Add(lblFechaDesde);
            gbFiltros.Location = new Point(12, 278);
            gbFiltros.Margin = new Padding(3, 2, 3, 2);
            gbFiltros.Name = "gbFiltros";
            // AirPadel style preview BEGIN
            gbFiltros.BackColor = Color.FromArgb(24, 70, 138);
            gbFiltros.ForeColor = Color.White;
            gbFiltros.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbFiltros.Padding = new Padding(3, 2, 3, 2);
            gbFiltros.Size = new Size(794, 218);
            gbFiltros.TabIndex = 1;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "Filtros de busqueda";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(512, 53);
            txtDescripcion.Margin = new Padding(3, 2, 3, 2);
            txtDescripcion.Name = "txtDescripcion";
            // AirPadel style preview BEGIN
            txtDescripcion.BackColor = Color.White;
            txtDescripcion.ForeColor = Color.FromArgb(18, 18, 18);
            txtDescripcion.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtDescripcion.Size = new Size(261, 23);
            txtDescripcion.TabIndex = 15;
            // 
            // cmbAccion
            // 
            cmbAccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccion.FormattingEnabled = true;
            cmbAccion.Location = new Point(110, 191);
            cmbAccion.Margin = new Padding(3, 2, 3, 2);
            cmbAccion.Name = "cmbAccion";
            // AirPadel style preview BEGIN
            cmbAccion.BackColor = Color.White;
            cmbAccion.ForeColor = Color.FromArgb(18, 18, 18);
            cmbAccion.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cmbAccion.Size = new Size(261, 23);
            cmbAccion.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 194);
            label7.Name = "label7";
            // AirPadel style preview BEGIN
            label7.ForeColor = Color.White;
            // AirPadel style preview END
            label7.Size = new Size(43, 15);
            label7.TabIndex = 13;
            label7.Text = "Evento";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(409, 56);
            label6.Name = "label6";
            // AirPadel style preview BEGIN
            label6.ForeColor = Color.White;
            // AirPadel style preview END
            label6.Size = new Size(69, 15);
            label6.TabIndex = 12;
            label6.Text = "Descripcion";
            // 
            // cmbResultado
            // 
            cmbResultado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbResultado.FormattingEnabled = true;
            cmbResultado.Location = new Point(110, 159);
            cmbResultado.Margin = new Padding(3, 2, 3, 2);
            cmbResultado.Name = "cmbResultado";
            // AirPadel style preview BEGIN
            cmbResultado.BackColor = Color.White;
            cmbResultado.ForeColor = Color.FromArgb(18, 18, 18);
            cmbResultado.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cmbResultado.Size = new Size(261, 23);
            cmbResultado.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 159);
            label5.Name = "label5";
            // AirPadel style preview BEGIN
            label5.ForeColor = Color.White;
            // AirPadel style preview END
            label5.Size = new Size(65, 15);
            label5.TabIndex = 11;
            label5.Text = "Resultado: ";
            // 
            // cmbCriticidad
            // 
            cmbCriticidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCriticidad.FormattingEnabled = true;
            cmbCriticidad.Location = new Point(110, 126);
            cmbCriticidad.Margin = new Padding(3, 2, 3, 2);
            cmbCriticidad.Name = "cmbCriticidad";
            // AirPadel style preview BEGIN
            cmbCriticidad.BackColor = Color.White;
            cmbCriticidad.ForeColor = Color.FromArgb(18, 18, 18);
            cmbCriticidad.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cmbCriticidad.Size = new Size(261, 23);
            cmbCriticidad.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 128);
            label4.Name = "label4";
            // AirPadel style preview BEGIN
            label4.ForeColor = Color.White;
            // AirPadel style preview END
            label4.Size = new Size(64, 15);
            label4.TabIndex = 9;
            label4.Text = "Criticidad: ";
            // 
            // cmbModulo
            // 
            cmbModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModulo.FormattingEnabled = true;
            cmbModulo.Location = new Point(110, 88);
            cmbModulo.Margin = new Padding(3, 2, 3, 2);
            cmbModulo.Name = "cmbModulo";
            // AirPadel style preview BEGIN
            cmbModulo.BackColor = Color.White;
            cmbModulo.ForeColor = Color.FromArgb(18, 18, 18);
            cmbModulo.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cmbModulo.Size = new Size(261, 23);
            cmbModulo.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 90);
            label3.Name = "label3";
            // AirPadel style preview BEGIN
            label3.ForeColor = Color.White;
            // AirPadel style preview END
            label3.Size = new Size(55, 15);
            label3.TabIndex = 7;
            label3.Text = "Modulo: ";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(110, 53);
            txtUsuario.Margin = new Padding(3, 2, 3, 2);
            txtUsuario.Name = "txtUsuario";
            // AirPadel style preview BEGIN
            txtUsuario.BackColor = Color.White;
            txtUsuario.ForeColor = Color.FromArgb(18, 18, 18);
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtUsuario.Size = new Size(261, 23);
            txtUsuario.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 53);
            label2.Name = "label2";
            // AirPadel style preview BEGIN
            label2.ForeColor = Color.White;
            // AirPadel style preview END
            label2.Size = new Size(53, 15);
            label2.TabIndex = 5;
            label2.Text = "Usuario: ";
            // 
            // lblFechaHasta
            // 
            lblFechaHasta.AutoSize = true;
            lblFechaHasta.Location = new Point(409, 23);
            lblFechaHasta.Name = "lblFechaHasta";
            // AirPadel style preview BEGIN
            lblFechaHasta.ForeColor = Color.White;
            // AirPadel style preview END
            lblFechaHasta.Size = new Size(74, 15);
            lblFechaHasta.TabIndex = 4;
            lblFechaHasta.Text = "Fecha Hasta:";
            // 
            // dtpFechaHasta
            // 
            dtpFechaHasta.Location = new Point(512, 20);
            dtpFechaHasta.Margin = new Padding(3, 2, 3, 2);
            dtpFechaHasta.Name = "dtpFechaHasta";
            // AirPadel style preview BEGIN
            dtpFechaHasta.CalendarForeColor = Color.FromArgb(18, 18, 18);
            dtpFechaHasta.CalendarMonthBackground = Color.White;
            dtpFechaHasta.CalendarTitleBackColor = Color.FromArgb(24, 70, 138);
            dtpFechaHasta.CalendarTitleForeColor = Color.White;
            // AirPadel style preview END
            dtpFechaHasta.Size = new Size(261, 23);
            dtpFechaHasta.TabIndex = 3;
            // 
            // DtpFechaDesde
            // 
            DtpFechaDesde.Location = new Point(110, 19);
            DtpFechaDesde.Margin = new Padding(3, 2, 3, 2);
            DtpFechaDesde.Name = "DtpFechaDesde";
            // AirPadel style preview BEGIN
            DtpFechaDesde.CalendarForeColor = Color.FromArgb(18, 18, 18);
            DtpFechaDesde.CalendarMonthBackground = Color.White;
            DtpFechaDesde.CalendarTitleBackColor = Color.FromArgb(24, 70, 138);
            DtpFechaDesde.CalendarTitleForeColor = Color.White;
            // AirPadel style preview END
            DtpFechaDesde.Size = new Size(261, 23);
            DtpFechaDesde.TabIndex = 2;
            DtpFechaDesde.Value = new DateTime(2026, 6, 2, 21, 21, 5, 0);
            // 
            // lblFechaDesde
            // 
            lblFechaDesde.AutoSize = true;
            lblFechaDesde.Location = new Point(14, 22);
            lblFechaDesde.Name = "lblFechaDesde";
            // AirPadel style preview BEGIN
            lblFechaDesde.ForeColor = Color.White;
            // AirPadel style preview END
            lblFechaDesde.Size = new Size(79, 15);
            lblFechaDesde.TabIndex = 0;
            lblFechaDesde.Text = "Fecha Desde: ";
            // 
            // dgvEventos
            // 
            dgvEventos.AllowUserToAddRows = false;
            dgvEventos.AllowUserToDeleteRows = false;
            dgvEventos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEventos.Location = new Point(12, 11);
            dgvEventos.Margin = new Padding(3, 2, 3, 2);
            dgvEventos.Name = "dgvEventos";
            // AirPadel style preview BEGIN
            dgvEventos.BackgroundColor = Color.White;
            dgvEventos.BorderStyle = BorderStyle.None;
            dgvEventos.GridColor = Color.FromArgb(64, 103, 166);
            dgvEventos.EnableHeadersVisualStyles = false;
            dgvEventos.RowHeadersVisible = false;
            dgvEventos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEventos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvEventos.ColumnHeadersHeight = 36;
            dgvEventos.RowTemplate.Height = 30;
            dgvEventos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(214, 246, 36);
            dgvEventos.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(18, 18, 18);
            dgvEventos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 230, 248);
            dgvEventos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 18, 18);
            // AirPadel style preview END
            dgvEventos.ReadOnly = true;
            dgvEventos.RowHeadersWidth = 51;
            dgvEventos.Size = new Size(1013, 211);
            dgvEventos.TabIndex = 2;
            dgvEventos.SelectionChanged += dgvEventos_SelectionChanged;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(861, 278);
            btnBuscar.Margin = new Padding(3, 2, 3, 2);
            btnBuscar.Name = "btnBuscar";
            // AirPadel style preview BEGIN
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.BackColor = Color.FromArgb(214, 246, 36);
            btnBuscar.ForeColor = Color.FromArgb(18, 18, 18);
            btnBuscar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnBuscar.Size = new Size(128, 44);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Aplicar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(861, 334);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            // AirPadel style preview BEGIN
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 1;
            btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnLimpiar.BackColor = Color.FromArgb(24, 70, 138);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLimpiar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnLimpiar.Size = new Size(129, 44);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(861, 449);
            btnVolver.Margin = new Padding(3, 2, 3, 2);
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
            btnVolver.Size = new Size(129, 43);
            btnVolver.TabIndex = 5;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Location = new Point(900, 233);
            lblMensaje.Name = "lblMensaje";
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(0, 15);
            lblMensaje.TabIndex = 6;
            // 
            // btnImprimir
            // 
            btnImprimir.Location = new Point(861, 393);
            btnImprimir.Margin = new Padding(3, 2, 3, 2);
            btnImprimir.Name = "btnImprimir";
            // AirPadel style preview BEGIN
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.BackColor = Color.FromArgb(214, 246, 36);
            btnImprimir.ForeColor = Color.FromArgb(18, 18, 18);
            btnImprimir.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnImprimir.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnImprimir.Size = new Size(129, 42);
            btnImprimir.TabIndex = 7;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(122, 233);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            // AirPadel style preview BEGIN
            txtNombre.BackColor = Color.White;
            txtNombre.ForeColor = Color.FromArgb(18, 18, 18);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtNombre.Size = new Size(127, 23);
            txtNombre.TabIndex = 16;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(358, 233);
            txtApellido.Margin = new Padding(3, 2, 3, 2);
            txtApellido.Name = "txtApellido";
            // AirPadel style preview BEGIN
            txtApellido.BackColor = Color.White;
            txtApellido.ForeColor = Color.FromArgb(18, 18, 18);
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtApellido.Size = new Size(127, 23);
            txtApellido.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(48, 235);
            label8.Name = "label8";
            // AirPadel style preview BEGIN
            label8.ForeColor = Color.White;
            // AirPadel style preview END
            label8.Size = new Size(51, 15);
            label8.TabIndex = 18;
            label8.Text = "Nombre";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(295, 235);
            label9.Name = "label9";
            // AirPadel style preview BEGIN
            label9.ForeColor = Color.White;
            // AirPadel style preview END
            label9.Size = new Size(51, 15);
            label9.TabIndex = 19;
            label9.Text = "Apellido";
            // 
            // gbDetalleReserva
            // 
            gbDetalleReserva.BackColor = Color.LightSkyBlue;
            gbDetalleReserva.Controls.Add(txtDescripcionReserva);
            gbDetalleReserva.Controls.Add(lblDescripcionReserva);
            gbDetalleReserva.Location = new Point(12, 278);
            gbDetalleReserva.Name = "gbDetalleReserva";
            // AirPadel style preview BEGIN
            gbDetalleReserva.BackColor = Color.FromArgb(24, 70, 138);
            gbDetalleReserva.ForeColor = Color.White;
            gbDetalleReserva.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbDetalleReserva.Size = new Size(794, 85);
            gbDetalleReserva.TabIndex = 20;
            gbDetalleReserva.TabStop = false;
            gbDetalleReserva.Text = "Detalle de la reserva";
            gbDetalleReserva.Visible = false;
            // 
            // lblDescripcionReserva
            // 
            lblDescripcionReserva.AutoSize = true;
            lblDescripcionReserva.Location = new Point(14, 24);
            lblDescripcionReserva.Name = "lblDescripcionReserva";
            // AirPadel style preview BEGIN
            lblDescripcionReserva.ForeColor = Color.White;
            // AirPadel style preview END
            lblDescripcionReserva.Size = new Size(123, 15);
            lblDescripcionReserva.TabIndex = 0;
            lblDescripcionReserva.Text = "Descripción completa:";
            // 
            // txtDescripcionReserva
            // 
            txtDescripcionReserva.Location = new Point(145, 20);
            txtDescripcionReserva.Multiline = true;
            txtDescripcionReserva.Name = "txtDescripcionReserva";
            // AirPadel style preview BEGIN
            txtDescripcionReserva.BackColor = Color.FromArgb(236, 241, 248);
            txtDescripcionReserva.ForeColor = Color.FromArgb(18, 18, 18);
            txtDescripcionReserva.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtDescripcionReserva.ReadOnly = true;
            txtDescripcionReserva.ScrollBars = ScrollBars.Vertical;
            txtDescripcionReserva.Size = new Size(628, 52);
            txtDescripcionReserva.TabIndex = 1;
            // 
            // frmAuditarEventos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(1064, 507);
            Controls.Add(gbDetalleReserva);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(btnImprimir);
            Controls.Add(lblMensaje);
            Controls.Add(btnVolver);
            Controls.Add(btnLimpiar);
            Controls.Add(btnBuscar);
            Controls.Add(dgvEventos);
            Controls.Add(gbFiltros);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAuditarEventos";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
            Text = "Auditoria de Eventos";
            Load += frmAuditarEventos_Load;
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            gbDetalleReserva.ResumeLayout(false);
            gbDetalleReserva.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEventos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox gbFiltros;
        private ComboBox cmbModulo;
        private Label label4;
        private ComboBox cmbCriticidad;
        private Label label3;
        private TextBox txtUsuario;
        private Label label2;
        private Label lblFechaHasta;
        private DateTimePicker dtpFechaHasta;
        private DateTimePicker DtpFechaDesde;
        private Label lblFechaDesde;
        private ComboBox cmbResultado;
        private Label label5;
        private DataGridView dgvEventos;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnVolver;
        private Label lblMensaje;
        private TextBox txtDescripcion;
        private ComboBox cmbAccion;
        private Label label7;
        private Label label6;
        private Button btnImprimir;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private Label label8;
        private Label label9;
        private GroupBox gbDetalleReserva;
        private Label lblDescripcionReserva;
        private TextBox txtDescripcionReserva;
    }
}