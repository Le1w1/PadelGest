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
            gbFactura.Size = new Size(720, 220);
            gbFactura.TabIndex = 1;
            gbFactura.TabStop = false;
            gbFactura.Text = "Factura de Reserva";
            // Cliente
            lblClienteTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClienteTitulo.Location = new Point(25, 30);
            lblClienteTitulo.Name = "lblClienteTitulo";
            lblClienteTitulo.Size = new Size(95, 23);
            lblClienteTitulo.Text = "Cliente:";
            lblClienteValor.Location = new Point(130, 30);
            lblClienteValor.Name = "lblClienteValor";
            lblClienteValor.Size = new Size(550, 23);
            lblClienteValor.Text = "-";
            // Turno
            lblTurnoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTurnoTitulo.Location = new Point(25, 65);
            lblTurnoTitulo.Name = "lblTurnoTitulo";
            lblTurnoTitulo.Size = new Size(95, 23);
            lblTurnoTitulo.Text = "Turno:";
            lblTurnoValor.Location = new Point(130, 65);
            lblTurnoValor.Name = "lblTurnoValor";
            lblTurnoValor.Size = new Size(200, 23);
            lblTurnoValor.Text = "-";
            // Cancha
            lblCanchaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCanchaTitulo.Location = new Point(365, 65);
            lblCanchaTitulo.Name = "lblCanchaTitulo";
            lblCanchaTitulo.Size = new Size(80, 23);
            lblCanchaTitulo.Text = "Cancha:";
            lblCanchaValor.Location = new Point(455, 65);
            lblCanchaValor.Name = "lblCanchaValor";
            lblCanchaValor.Size = new Size(225, 23);
            lblCanchaValor.Text = "-";
            // Tarifa
            lblTarifaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTarifaTitulo.Location = new Point(25, 100);
            lblTarifaTitulo.Name = "lblTarifaTitulo";
            lblTarifaTitulo.Size = new Size(95, 23);
            lblTarifaTitulo.Text = "Tarifa:";
            lblTarifaValor.Location = new Point(130, 100);
            lblTarifaValor.Name = "lblTarifaValor";
            lblTarifaValor.Size = new Size(550, 23);
            lblTarifaValor.Text = "-";
            // Equipamiento
            lblEquipamientoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEquipamientoTitulo.Location = new Point(25, 135);
            lblEquipamientoTitulo.Name = "lblEquipamientoTitulo";
            lblEquipamientoTitulo.Size = new Size(100, 23);
            lblEquipamientoTitulo.Text = "Equipamiento:";
            lblEquipamientoValor.Location = new Point(130, 135);
            lblEquipamientoValor.Name = "lblEquipamientoValor";
            lblEquipamientoValor.Size = new Size(550, 23);
            lblEquipamientoValor.Text = "-";
            // Total
            lblTotalTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalTitulo.Location = new Point(455, 175);
            lblTotalTitulo.Name = "lblTotalTitulo";
            lblTotalTitulo.Size = new Size(75, 28);
            lblTotalTitulo.Text = "Total:";
            lblTotalValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalValor.Location = new Point(535, 175);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(145, 28);
            lblTotalValor.Text = "-";
            lblTotalValor.TextAlign = ContentAlignment.MiddleRight;
            // gbTarjeta
            gbTarjeta.BackColor = Color.LightSkyBlue;
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
            gbTarjeta.Size = new Size(720, 170);
            gbTarjeta.TabIndex = 2;
            gbTarjeta.TabStop = false;
            gbTarjeta.Text = "Datos de la tarjeta";
            // Banco
            lblBanco.AutoSize = true;
            lblBanco.Location = new Point(30, 38);
            lblBanco.Name = "lblBanco";
            lblBanco.Text = "Banco:";
            txtBanco.Location = new Point(170, 35);
            txtBanco.MaxLength = 80;
            txtBanco.Name = "txtBanco";
            txtBanco.Size = new Size(500, 23);
            txtBanco.TextChanged += Campo_TextChanged;
            // Numero Tarjeta
            lblNumeroTarjeta.AutoSize = true;
            lblNumeroTarjeta.Location = new Point(30, 78);
            lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            lblNumeroTarjeta.Text = "Número de tarjeta:";
            txtNumeroTarjeta.Location = new Point(170, 75);
            txtNumeroTarjeta.MaxLength = 23;
            txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            txtNumeroTarjeta.Size = new Size(250, 23);
            txtNumeroTarjeta.TextChanged += Campo_TextChanged;
            txtNumeroTarjeta.KeyPress += SoloNumeros_KeyPress;
            // Vencimiento
            lblVencimiento.AutoSize = true;
            lblVencimiento.Location = new Point(445, 78);
            lblVencimiento.Name = "lblVencimiento";
            lblVencimiento.Text = "Vencimiento:";
            dtpVencimiento.Location = new Point(545, 75);
            dtpVencimiento.Name = "dtpVencimiento";
            dtpVencimiento.Size = new Size(125, 23);
            // Codigo seguridad
            lblCodigoSeguridad.AutoSize = true;
            lblCodigoSeguridad.Location = new Point(30, 118);
            lblCodigoSeguridad.Name = "lblCodigoSeguridad";
            lblCodigoSeguridad.Text = "Código de seguridad:";
            txtCodigoSeguridad.Location = new Point(170, 115);
            txtCodigoSeguridad.MaxLength = 4;
            txtCodigoSeguridad.Name = "txtCodigoSeguridad";
            txtCodigoSeguridad.PasswordChar = '●';
            txtCodigoSeguridad.Size = new Size(100, 23);
            txtCodigoSeguridad.TextChanged += Campo_TextChanged;
            txtCodigoSeguridad.KeyPress += SoloNumeros_KeyPress;
            // lblMensaje
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 500);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(720, 40);
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // btnCobrar
            btnCobrar.Location = new Point(580, 555);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(160, 34);
            btnCobrar.Text = "Cobrar Reserva";
            btnCobrar.UseVisualStyleBackColor = true;
            btnCobrar.Click += btnCobrar_Click;
            // btnVolver
            btnVolver.Location = new Point(460, 555);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // errorProvider
            errorProvider.ContainerControl = this;
            // frmCobrarReserva
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(760, 610);
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
