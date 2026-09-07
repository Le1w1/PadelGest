namespace UI
{
    partial class frmCobrarReserva
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
            gbFactura = new GroupBox();
            lblTotalValor = new Label();
            lblTotalTitulo = new Label();
            lblEquipamientoValor = new Label();
            lblEquipamientoTitulo = new Label();
            lblTarifaValor = new Label();
            lblTarifaTitulo = new Label();
            lblCanchaValor = new Label();
            lblCanchaTitulo = new Label();
            lblTurnoValor = new Label();
            lblTurnoTitulo = new Label();
            lblClienteValor = new Label();
            lblClienteTitulo = new Label();
            gbTarjeta = new GroupBox();
            cboRespuestaBanco = new ComboBox();
            lblRespuestaBanco = new Label();
            txtCodigoSeguridad = new TextBox();
            lblCodigoSeguridad = new Label();
            dtpVencimiento = new DateTimePicker();
            lblVencimiento = new Label();
            txtNumeroTarjeta = new TextBox();
            lblNumeroTarjeta = new Label();
            txtBanco = new TextBox();
            lblBanco = new Label();
            lblMensaje = new Label();
            btnCobrar = new Button();
            btnVolver = new Button();
            errorProvider = new ErrorProvider(components);
            gbFactura.SuspendLayout();
            gbTarjeta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(720, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cobrar Reserva";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbFactura
            // 
            gbFactura.BackColor = Color.FromArgb(24, 70, 138);
            gbFactura.Controls.Add(lblTotalValor);
            gbFactura.Controls.Add(lblTotalTitulo);
            gbFactura.Controls.Add(lblEquipamientoValor);
            gbFactura.Controls.Add(lblEquipamientoTitulo);
            gbFactura.Controls.Add(lblTarifaValor);
            gbFactura.Controls.Add(lblTarifaTitulo);
            gbFactura.Controls.Add(lblCanchaValor);
            gbFactura.Controls.Add(lblCanchaTitulo);
            gbFactura.Controls.Add(lblTurnoValor);
            gbFactura.Controls.Add(lblTurnoTitulo);
            gbFactura.Controls.Add(lblClienteValor);
            gbFactura.Controls.Add(lblClienteTitulo);
            gbFactura.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbFactura.ForeColor = Color.White;
            gbFactura.Location = new Point(20, 78);
            gbFactura.Name = "gbFactura";
            gbFactura.Size = new Size(720, 220);
            gbFactura.TabIndex = 1;
            gbFactura.TabStop = false;
            gbFactura.Text = "Factura de Reserva";
            // 
            // lblTotalValor
            // 
            lblTotalValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalValor.ForeColor = Color.White;
            lblTotalValor.Location = new Point(535, 175);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(145, 28);
            lblTotalValor.TabIndex = 0;
            lblTotalValor.Text = "-";
            lblTotalValor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotalTitulo
            // 
            lblTotalTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalTitulo.ForeColor = Color.White;
            lblTotalTitulo.Location = new Point(455, 175);
            lblTotalTitulo.Name = "lblTotalTitulo";
            lblTotalTitulo.Size = new Size(75, 28);
            lblTotalTitulo.TabIndex = 1;
            lblTotalTitulo.Text = "Total:";
            // 
            // lblEquipamientoValor
            // 
            lblEquipamientoValor.ForeColor = Color.White;
            lblEquipamientoValor.Location = new Point(130, 135);
            lblEquipamientoValor.Name = "lblEquipamientoValor";
            lblEquipamientoValor.Size = new Size(550, 23);
            lblEquipamientoValor.TabIndex = 2;
            lblEquipamientoValor.Text = "-";
            // 
            // lblEquipamientoTitulo
            // 
            lblEquipamientoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEquipamientoTitulo.ForeColor = Color.White;
            lblEquipamientoTitulo.Location = new Point(25, 135);
            lblEquipamientoTitulo.Name = "lblEquipamientoTitulo";
            lblEquipamientoTitulo.Size = new Size(100, 23);
            lblEquipamientoTitulo.TabIndex = 3;
            lblEquipamientoTitulo.Text = "Equipamiento:";
            // 
            // lblTarifaValor
            // 
            lblTarifaValor.ForeColor = Color.White;
            lblTarifaValor.Location = new Point(130, 100);
            lblTarifaValor.Name = "lblTarifaValor";
            lblTarifaValor.Size = new Size(550, 23);
            lblTarifaValor.TabIndex = 4;
            lblTarifaValor.Text = "-";
            // 
            // lblTarifaTitulo
            // 
            lblTarifaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTarifaTitulo.ForeColor = Color.White;
            lblTarifaTitulo.Location = new Point(25, 100);
            lblTarifaTitulo.Name = "lblTarifaTitulo";
            lblTarifaTitulo.Size = new Size(95, 23);
            lblTarifaTitulo.TabIndex = 5;
            lblTarifaTitulo.Text = "Tarifa:";
            // 
            // lblCanchaValor
            // 
            lblCanchaValor.ForeColor = Color.White;
            lblCanchaValor.Location = new Point(455, 65);
            lblCanchaValor.Name = "lblCanchaValor";
            lblCanchaValor.Size = new Size(225, 23);
            lblCanchaValor.TabIndex = 6;
            lblCanchaValor.Text = "-";
            // 
            // lblCanchaTitulo
            // 
            lblCanchaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCanchaTitulo.ForeColor = Color.White;
            lblCanchaTitulo.Location = new Point(365, 65);
            lblCanchaTitulo.Name = "lblCanchaTitulo";
            lblCanchaTitulo.Size = new Size(80, 23);
            lblCanchaTitulo.TabIndex = 7;
            lblCanchaTitulo.Text = "Cancha:";
            // 
            // lblTurnoValor
            // 
            lblTurnoValor.ForeColor = Color.White;
            lblTurnoValor.Location = new Point(130, 65);
            lblTurnoValor.Name = "lblTurnoValor";
            lblTurnoValor.Size = new Size(200, 23);
            lblTurnoValor.TabIndex = 8;
            lblTurnoValor.Text = "-";
            // 
            // lblTurnoTitulo
            // 
            lblTurnoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTurnoTitulo.ForeColor = Color.White;
            lblTurnoTitulo.Location = new Point(25, 65);
            lblTurnoTitulo.Name = "lblTurnoTitulo";
            lblTurnoTitulo.Size = new Size(95, 23);
            lblTurnoTitulo.TabIndex = 9;
            lblTurnoTitulo.Text = "Turno:";
            // 
            // lblClienteValor
            // 
            lblClienteValor.ForeColor = Color.White;
            lblClienteValor.Location = new Point(130, 30);
            lblClienteValor.Name = "lblClienteValor";
            lblClienteValor.Size = new Size(550, 23);
            lblClienteValor.TabIndex = 10;
            lblClienteValor.Text = "-";
            // 
            // lblClienteTitulo
            // 
            lblClienteTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClienteTitulo.ForeColor = Color.White;
            lblClienteTitulo.Location = new Point(25, 30);
            lblClienteTitulo.Name = "lblClienteTitulo";
            lblClienteTitulo.Size = new Size(95, 23);
            lblClienteTitulo.TabIndex = 11;
            lblClienteTitulo.Text = "Cliente:";
            // 
            // gbTarjeta
            // 
            gbTarjeta.BackColor = Color.FromArgb(24, 70, 138);
            gbTarjeta.Controls.Add(cboRespuestaBanco);
            gbTarjeta.Controls.Add(lblRespuestaBanco);
            gbTarjeta.Controls.Add(txtCodigoSeguridad);
            gbTarjeta.Controls.Add(lblCodigoSeguridad);
            gbTarjeta.Controls.Add(dtpVencimiento);
            gbTarjeta.Controls.Add(lblVencimiento);
            gbTarjeta.Controls.Add(txtNumeroTarjeta);
            gbTarjeta.Controls.Add(lblNumeroTarjeta);
            gbTarjeta.Controls.Add(txtBanco);
            gbTarjeta.Controls.Add(lblBanco);
            gbTarjeta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbTarjeta.ForeColor = Color.White;
            gbTarjeta.Location = new Point(20, 315);
            gbTarjeta.Name = "gbTarjeta";
            gbTarjeta.Size = new Size(720, 210);
            gbTarjeta.TabIndex = 2;
            gbTarjeta.TabStop = false;
            gbTarjeta.Text = "Datos de la tarjeta";
            // 
            // cboRespuestaBanco
            // 
            cboRespuestaBanco.BackColor = Color.White;
            cboRespuestaBanco.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRespuestaBanco.FlatStyle = FlatStyle.Flat;
            cboRespuestaBanco.ForeColor = Color.FromArgb(18, 18, 18);
            cboRespuestaBanco.FormattingEnabled = true;
            cboRespuestaBanco.Location = new Point(455, 115);
            cboRespuestaBanco.Name = "cboRespuestaBanco";
            cboRespuestaBanco.Size = new Size(215, 25);
            cboRespuestaBanco.TabIndex = 8;
            // 
            // lblRespuestaBanco
            // 
            lblRespuestaBanco.AutoSize = true;
            lblRespuestaBanco.ForeColor = Color.White;
            lblRespuestaBanco.Location = new Point(310, 118);
            lblRespuestaBanco.Name = "lblRespuestaBanco";
            lblRespuestaBanco.Size = new Size(138, 17);
            lblRespuestaBanco.TabIndex = 9;
            lblRespuestaBanco.Text = "Respuesta del Banco:";
            // 
            // txtCodigoSeguridad
            // 
            txtCodigoSeguridad.BackColor = Color.White;
            txtCodigoSeguridad.BorderStyle = BorderStyle.FixedSingle;
            txtCodigoSeguridad.ForeColor = Color.FromArgb(18, 18, 18);
            txtCodigoSeguridad.Location = new Point(170, 115);
            txtCodigoSeguridad.MaxLength = 4;
            txtCodigoSeguridad.Name = "txtCodigoSeguridad";
            txtCodigoSeguridad.PasswordChar = '●';
            txtCodigoSeguridad.Size = new Size(100, 24);
            txtCodigoSeguridad.TabIndex = 10;
            txtCodigoSeguridad.TextChanged += Campo_TextChanged;
            txtCodigoSeguridad.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblCodigoSeguridad
            // 
            lblCodigoSeguridad.AutoSize = true;
            lblCodigoSeguridad.ForeColor = Color.White;
            lblCodigoSeguridad.Location = new Point(30, 118);
            lblCodigoSeguridad.Name = "lblCodigoSeguridad";
            lblCodigoSeguridad.Size = new Size(140, 17);
            lblCodigoSeguridad.TabIndex = 11;
            lblCodigoSeguridad.Text = "Código de seguridad:";
            // 
            // dtpVencimiento
            // 
            dtpVencimiento.CalendarForeColor = Color.FromArgb(18, 18, 18);
            dtpVencimiento.CalendarMonthBackground = Color.White;
            dtpVencimiento.CalendarTitleBackColor = Color.FromArgb(24, 70, 138);
            dtpVencimiento.CalendarTitleForeColor = Color.White;
            dtpVencimiento.Location = new Point(545, 75);
            dtpVencimiento.Name = "dtpVencimiento";
            dtpVencimiento.Size = new Size(125, 24);
            dtpVencimiento.TabIndex = 12;
            // 
            // lblVencimiento
            // 
            lblVencimiento.AutoSize = true;
            lblVencimiento.ForeColor = Color.White;
            lblVencimiento.Location = new Point(445, 78);
            lblVencimiento.Name = "lblVencimiento";
            lblVencimiento.Size = new Size(89, 17);
            lblVencimiento.TabIndex = 13;
            lblVencimiento.Text = "Vencimiento:";
            // 
            // txtNumeroTarjeta
            // 
            txtNumeroTarjeta.BackColor = Color.White;
            txtNumeroTarjeta.BorderStyle = BorderStyle.FixedSingle;
            txtNumeroTarjeta.ForeColor = Color.FromArgb(18, 18, 18);
            txtNumeroTarjeta.Location = new Point(170, 75);
            txtNumeroTarjeta.MaxLength = 23;
            txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            txtNumeroTarjeta.Size = new Size(250, 24);
            txtNumeroTarjeta.TabIndex = 14;
            txtNumeroTarjeta.TextChanged += Campo_TextChanged;
            txtNumeroTarjeta.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblNumeroTarjeta
            // 
            lblNumeroTarjeta.AutoSize = true;
            lblNumeroTarjeta.ForeColor = Color.White;
            lblNumeroTarjeta.Location = new Point(30, 78);
            lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            lblNumeroTarjeta.Size = new Size(125, 17);
            lblNumeroTarjeta.TabIndex = 15;
            lblNumeroTarjeta.Text = "Número de tarjeta:";
            // 
            // txtBanco
            // 
            txtBanco.BackColor = Color.White;
            txtBanco.BorderStyle = BorderStyle.FixedSingle;
            txtBanco.ForeColor = Color.FromArgb(18, 18, 18);
            txtBanco.Location = new Point(170, 35);
            txtBanco.MaxLength = 80;
            txtBanco.Name = "txtBanco";
            txtBanco.Size = new Size(500, 24);
            txtBanco.TabIndex = 16;
            txtBanco.TextChanged += Campo_TextChanged;
            // 
            // lblBanco
            // 
            lblBanco.AutoSize = true;
            lblBanco.ForeColor = Color.White;
            lblBanco.Location = new Point(30, 38);
            lblBanco.Name = "lblBanco";
            lblBanco.Size = new Size(49, 17);
            lblBanco.TabIndex = 17;
            lblBanco.Text = "Banco:";
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            lblMensaje.Location = new Point(20, 540);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(720, 40);
            lblMensaje.TabIndex = 2;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCobrar
            // 
            btnCobrar.BackColor = Color.FromArgb(214, 246, 36);
            btnCobrar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCobrar.FlatStyle = FlatStyle.Flat;
            btnCobrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCobrar.ForeColor = Color.Black;
            btnCobrar.Location = new Point(580, 595);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(160, 34);
            btnCobrar.TabIndex = 1;
            btnCobrar.Text = "Cobrar Reserva";
            btnCobrar.UseVisualStyleBackColor = false;
            btnCobrar.Click += btnCobrar_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(460, 595);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // frmCobrarReserva
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
            ClientSize = new Size(760, 650);
            Controls.Add(btnVolver);
            Controls.Add(btnCobrar);
            Controls.Add(lblMensaje);
            Controls.Add(gbTarjeta);
            Controls.Add(gbFactura);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCobrarReserva";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Cobrar Reserva";
            FormClosed += frmCobrarReserva_FormClosed;
            Load += frmCobrarReserva_Load;
            gbFactura.ResumeLayout(false);
            gbTarjeta.ResumeLayout(false);
            gbTarjeta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private GroupBox gbFactura;
        private Label lblClienteTitulo;
        private Label lblClienteValor;
        private Label lblTurnoTitulo;
        private Label lblTurnoValor;
        private Label lblCanchaTitulo;
        private Label lblCanchaValor;
        private Label lblTarifaTitulo;
        private Label lblTarifaValor;
        private Label lblEquipamientoTitulo;
        private Label lblEquipamientoValor;
        private Label lblTotalTitulo;
        private Label lblTotalValor;
        private GroupBox gbTarjeta;
        private Label lblRespuestaBanco;
        private ComboBox cboRespuestaBanco;
        private Label lblBanco;
        private TextBox txtBanco;
        private Label lblNumeroTarjeta;
        private TextBox txtNumeroTarjeta;
        private Label lblVencimiento;
        private DateTimePicker dtpVencimiento;
        private Label lblCodigoSeguridad;
        private TextBox txtCodigoSeguridad;
        private Label lblMensaje;
        private Button btnCobrar;
        private Button btnVolver;
        private ErrorProvider errorProvider;
    }
}
