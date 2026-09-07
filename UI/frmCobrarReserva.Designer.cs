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
            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            // AirPadel style preview BEGIN
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            // AirPadel style preview END
            lblTitulo.Size = new Size(720, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cobrar Reserva";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // gbFactura
            gbFactura.BackColor = Color.PowderBlue;
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
            gbFactura.Location = new Point(20, 78);
            gbFactura.Name = "gbFactura";
            // AirPadel style preview BEGIN
            gbFactura.BackColor = Color.FromArgb(24, 70, 138);
            gbFactura.ForeColor = Color.White;
            gbFactura.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbFactura.Size = new Size(720, 220);
            gbFactura.TabIndex = 1;
            gbFactura.TabStop = false;
            gbFactura.Text = "Factura de Reserva";
            // Cliente
            lblClienteTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClienteTitulo.Location = new Point(25, 30);
            lblClienteTitulo.Name = "lblClienteTitulo";
            // AirPadel style preview BEGIN
            lblClienteTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblClienteTitulo.Size = new Size(95, 23);
            lblClienteTitulo.Text = "Cliente:";
            lblClienteValor.Location = new Point(130, 30);
            lblClienteValor.Name = "lblClienteValor";
            // AirPadel style preview BEGIN
            lblClienteValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblClienteValor.Size = new Size(550, 23);
            lblClienteValor.Text = "-";
            // Turno
            lblTurnoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTurnoTitulo.Location = new Point(25, 65);
            lblTurnoTitulo.Name = "lblTurnoTitulo";
            // AirPadel style preview BEGIN
            lblTurnoTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblTurnoTitulo.Size = new Size(95, 23);
            lblTurnoTitulo.Text = "Turno:";
            lblTurnoValor.Location = new Point(130, 65);
            lblTurnoValor.Name = "lblTurnoValor";
            // AirPadel style preview BEGIN
            lblTurnoValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblTurnoValor.Size = new Size(200, 23);
            lblTurnoValor.Text = "-";
            // Cancha
            lblCanchaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCanchaTitulo.Location = new Point(365, 65);
            lblCanchaTitulo.Name = "lblCanchaTitulo";
            // AirPadel style preview BEGIN
            lblCanchaTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblCanchaTitulo.Size = new Size(80, 23);
            lblCanchaTitulo.Text = "Cancha:";
            lblCanchaValor.Location = new Point(455, 65);
            lblCanchaValor.Name = "lblCanchaValor";
            // AirPadel style preview BEGIN
            lblCanchaValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblCanchaValor.Size = new Size(225, 23);
            lblCanchaValor.Text = "-";
            // Tarifa
            lblTarifaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTarifaTitulo.Location = new Point(25, 100);
            lblTarifaTitulo.Name = "lblTarifaTitulo";
            // AirPadel style preview BEGIN
            lblTarifaTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblTarifaTitulo.Size = new Size(95, 23);
            lblTarifaTitulo.Text = "Tarifa:";
            lblTarifaValor.Location = new Point(130, 100);
            lblTarifaValor.Name = "lblTarifaValor";
            // AirPadel style preview BEGIN
            lblTarifaValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblTarifaValor.Size = new Size(550, 23);
            lblTarifaValor.Text = "-";
            // Equipamiento
            lblEquipamientoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEquipamientoTitulo.Location = new Point(25, 135);
            lblEquipamientoTitulo.Name = "lblEquipamientoTitulo";
            // AirPadel style preview BEGIN
            lblEquipamientoTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblEquipamientoTitulo.Size = new Size(100, 23);
            lblEquipamientoTitulo.Text = "Equipamiento:";
            lblEquipamientoValor.Location = new Point(130, 135);
            lblEquipamientoValor.Name = "lblEquipamientoValor";
            // AirPadel style preview BEGIN
            lblEquipamientoValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblEquipamientoValor.Size = new Size(550, 23);
            lblEquipamientoValor.Text = "-";
            // Total
            lblTotalTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalTitulo.Location = new Point(455, 175);
            lblTotalTitulo.Name = "lblTotalTitulo";
            // AirPadel style preview BEGIN
            lblTotalTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblTotalTitulo.Size = new Size(75, 28);
            lblTotalTitulo.Text = "Total:";
            lblTotalValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalValor.Location = new Point(535, 175);
            lblTotalValor.Name = "lblTotalValor";
            // AirPadel style preview BEGIN
            lblTotalValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblTotalValor.Size = new Size(145, 28);
            lblTotalValor.Text = "-";
            lblTotalValor.TextAlign = ContentAlignment.MiddleRight;
            // gbTarjeta
            gbTarjeta.BackColor = Color.LightSkyBlue;
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
            gbTarjeta.Location = new Point(20, 315);
            gbTarjeta.Name = "gbTarjeta";
            // AirPadel style preview BEGIN
            gbTarjeta.BackColor = Color.FromArgb(24, 70, 138);
            gbTarjeta.ForeColor = Color.White;
            gbTarjeta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbTarjeta.Size = new Size(720, 210);
            gbTarjeta.TabIndex = 2;
            gbTarjeta.TabStop = false;
            gbTarjeta.Text = "Datos de la tarjeta";
            // Banco
            lblBanco.AutoSize = true;
            lblBanco.Location = new Point(30, 38);
            lblBanco.Name = "lblBanco";
            // AirPadel style preview BEGIN
            lblBanco.ForeColor = Color.White;
            // AirPadel style preview END
            lblBanco.Text = "Banco:";
            txtBanco.Location = new Point(170, 35);
            txtBanco.MaxLength = 80;
            txtBanco.Name = "txtBanco";
            // AirPadel style preview BEGIN
            txtBanco.BackColor = Color.White;
            txtBanco.ForeColor = Color.FromArgb(18, 18, 18);
            txtBanco.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtBanco.Size = new Size(500, 23);
            txtBanco.TextChanged += Campo_TextChanged;
            // Numero Tarjeta
            lblNumeroTarjeta.AutoSize = true;
            lblNumeroTarjeta.Location = new Point(30, 78);
            lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            // AirPadel style preview BEGIN
            lblNumeroTarjeta.ForeColor = Color.White;
            // AirPadel style preview END
            lblNumeroTarjeta.Text = "Número de tarjeta:";
            txtNumeroTarjeta.Location = new Point(170, 75);
            txtNumeroTarjeta.MaxLength = 23;
            txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            // AirPadel style preview BEGIN
            txtNumeroTarjeta.BackColor = Color.White;
            txtNumeroTarjeta.ForeColor = Color.FromArgb(18, 18, 18);
            txtNumeroTarjeta.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtNumeroTarjeta.Size = new Size(250, 23);
            txtNumeroTarjeta.TextChanged += Campo_TextChanged;
            txtNumeroTarjeta.KeyPress += SoloNumeros_KeyPress;
            // Vencimiento
            lblVencimiento.AutoSize = true;
            lblVencimiento.Location = new Point(445, 78);
            lblVencimiento.Name = "lblVencimiento";
            // AirPadel style preview BEGIN
            lblVencimiento.ForeColor = Color.White;
            // AirPadel style preview END
            lblVencimiento.Text = "Vencimiento:";
            dtpVencimiento.Location = new Point(545, 75);
            dtpVencimiento.Name = "dtpVencimiento";
            // AirPadel style preview BEGIN
            dtpVencimiento.CalendarForeColor = Color.FromArgb(18, 18, 18);
            dtpVencimiento.CalendarMonthBackground = Color.White;
            dtpVencimiento.CalendarTitleBackColor = Color.FromArgb(24, 70, 138);
            dtpVencimiento.CalendarTitleForeColor = Color.White;
            // AirPadel style preview END
            dtpVencimiento.Size = new Size(125, 23);
            // Codigo seguridad
            lblCodigoSeguridad.AutoSize = true;
            lblCodigoSeguridad.Location = new Point(30, 118);
            lblCodigoSeguridad.Name = "lblCodigoSeguridad";
            // AirPadel style preview BEGIN
            lblCodigoSeguridad.ForeColor = Color.White;
            // AirPadel style preview END
            lblCodigoSeguridad.Text = "Código de seguridad:";
            txtCodigoSeguridad.Location = new Point(170, 115);
            txtCodigoSeguridad.MaxLength = 4;
            txtCodigoSeguridad.Name = "txtCodigoSeguridad";
            // AirPadel style preview BEGIN
            txtCodigoSeguridad.BackColor = Color.White;
            txtCodigoSeguridad.ForeColor = Color.FromArgb(18, 18, 18);
            txtCodigoSeguridad.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtCodigoSeguridad.PasswordChar = '●';
            txtCodigoSeguridad.Size = new Size(100, 23);
            txtCodigoSeguridad.TextChanged += Campo_TextChanged;
            txtCodigoSeguridad.KeyPress += SoloNumeros_KeyPress;
            // Respuesta Banco
            lblRespuestaBanco.AutoSize = true;
            lblRespuestaBanco.Location = new Point(310, 118);
            lblRespuestaBanco.Name = "lblRespuestaBanco";
            // AirPadel style preview BEGIN
            lblRespuestaBanco.ForeColor = Color.White;
            // AirPadel style preview END
            lblRespuestaBanco.Text = "Respuesta del Banco:";
            cboRespuestaBanco.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRespuestaBanco.FormattingEnabled = true;
            cboRespuestaBanco.Location = new Point(455, 115);
            cboRespuestaBanco.Name = "cboRespuestaBanco";
            // AirPadel style preview BEGIN
            cboRespuestaBanco.BackColor = Color.White;
            cboRespuestaBanco.ForeColor = Color.FromArgb(18, 18, 18);
            cboRespuestaBanco.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cboRespuestaBanco.Size = new Size(215, 23);
            cboRespuestaBanco.TabIndex = 8;
            // lblMensaje
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 540);
            lblMensaje.Name = "lblMensaje";
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(720, 40);
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // btnCobrar
            btnCobrar.Location = new Point(580, 595);
            btnCobrar.Name = "btnCobrar";
            // AirPadel style preview BEGIN
            btnCobrar.FlatStyle = FlatStyle.Flat;
            btnCobrar.FlatAppearance.BorderSize = 1;
            btnCobrar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCobrar.BackColor = Color.FromArgb(24, 70, 138);
            btnCobrar.ForeColor = Color.White;
            btnCobrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCobrar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnCobrar.Size = new Size(160, 34);
            btnCobrar.Text = "Cobrar Reserva";
            btnCobrar.UseVisualStyleBackColor = false;
            btnCobrar.Click += btnCobrar_Click;
            // btnVolver
            btnVolver.Location = new Point(460, 595);
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
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // errorProvider
            errorProvider.ContainerControl = this;
            // frmCobrarReserva
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(760, 650);
            Controls.Add(btnVolver);
            Controls.Add(btnCobrar);
            Controls.Add(lblMensaje);
            Controls.Add(gbTarjeta);
            Controls.Add(gbFactura);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCobrarReserva";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
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
