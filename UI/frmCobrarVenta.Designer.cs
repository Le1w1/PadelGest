namespace UI
{
    partial class frmCobrarVenta
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            gbResumen = new GroupBox();
            lblClienteTitulo = new Label();
            lblClienteValor = new Label();
            lblProductosTitulo = new Label();
            lblProductosValor = new Label();
            lblTotalTitulo = new Label();
            lblTotalValor = new Label();
            gbTarjeta = new GroupBox();
            lblBanco = new Label();
            txtBanco = new TextBox();
            lblTarjeta = new Label();
            txtNumeroTarjeta = new TextBox();
            lblVencimiento = new Label();
            dtpVencimiento = new DateTimePicker();
            lblSeguridad = new Label();
            txtCodigoSeguridad = new TextBox();
            lblRespuesta = new Label();
            cboRespuestaBanco = new ComboBox();
            btnConfirmar = new Button();
            btnVolver = new Button();
            lblMensaje = new Label();
            gbResumen.SuspendLayout();
            gbTarjeta.SuspendLayout();
            SuspendLayout();

            Color azul = Color.FromArgb(14, 43, 92);
            Color panel = Color.FromArgb(24, 70, 138);
            Color acento = Color.FromArgb(214, 246, 36);

            lblTitulo.Name = "lblTitulo";
            lblTitulo.Location = new Point(24, 14);
            lblTitulo.Size = new Size(680, 44);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = acento;
            lblTitulo.Text = "COBRAR VENTA";

            gbResumen.Name = "gbResumen";
            gbResumen.Location = new Point(24, 69);
            gbResumen.Size = new Size(680, 142);
            gbResumen.Text = "Resumen de venta";
            gbResumen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbResumen.BackColor = panel;
            gbResumen.ForeColor = Color.White;
            gbResumen.Controls.AddRange(new Control[] {
                lblClienteTitulo, lblClienteValor,
                lblProductosTitulo, lblProductosValor, lblTotalTitulo, lblTotalValor
            });

            lblClienteTitulo.Location = new Point(22, 33);
            lblClienteTitulo.Size = new Size(130, 26);
            lblClienteTitulo.Text = "Cliente:";
            lblClienteValor.Location = new Point(154, 33);
            lblClienteValor.Size = new Size(175, 26);
            lblClienteValor.Text = "-";
            lblClienteValor.ForeColor = Color.White;

            lblProductosTitulo.Location = new Point(360, 33);
            lblProductosTitulo.Size = new Size(135, 26);
            lblProductosTitulo.Text = "Unidades:";
            lblProductosValor.Location = new Point(505, 33);
            lblProductosValor.Size = new Size(135, 26);
            lblProductosValor.Text = "0";
            lblProductosValor.ForeColor = Color.White;

            lblTotalTitulo.Location = new Point(22, 84);
            lblTotalTitulo.Size = new Size(140, 34);
            lblTotalTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalTitulo.Text = "Total:";
            lblTotalValor.Location = new Point(170, 78);
            lblTotalValor.Size = new Size(470, 42);
            lblTotalValor.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTotalValor.Text = "$ 0,00";
            lblTotalValor.ForeColor = acento;
            lblTotalValor.TextAlign = ContentAlignment.MiddleRight;

            gbTarjeta.Name = "gbTarjeta";
            gbTarjeta.Location = new Point(24, 226);
            gbTarjeta.Size = new Size(680, 272);
            gbTarjeta.Text = "Pago con tarjeta (simulado)";
            gbTarjeta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbTarjeta.BackColor = panel;
            gbTarjeta.ForeColor = Color.White;
            gbTarjeta.Controls.AddRange(new Control[] {
                lblBanco, txtBanco, lblTarjeta, txtNumeroTarjeta,
                lblVencimiento, dtpVencimiento, lblSeguridad, txtCodigoSeguridad,
                lblRespuesta, cboRespuestaBanco
            });

            lblBanco.Name = "lblBanco";
            lblBanco.Location = new Point(22, 36);
            lblBanco.Size = new Size(180, 26);
            lblBanco.Text = "Banco:";
            txtBanco.Name = "txtBanco";
            txtBanco.Location = new Point(220, 35);
            txtBanco.Size = new Size(418, 28);
            txtBanco.MaxLength = 80;
            txtBanco.BackColor = Color.White;
            txtBanco.ForeColor = Color.Black;

            lblTarjeta.Name = "lblTarjeta";
            lblTarjeta.Location = new Point(22, 82);
            lblTarjeta.Size = new Size(180, 26);
            lblTarjeta.Text = "Número de tarjeta:";
            txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            txtNumeroTarjeta.Location = new Point(220, 80);
            txtNumeroTarjeta.Size = new Size(418, 28);
            txtNumeroTarjeta.MaxLength = 23;
            txtNumeroTarjeta.BackColor = Color.White;
            txtNumeroTarjeta.ForeColor = Color.Black;
            txtNumeroTarjeta.KeyPress += NumerosTarjeta_KeyPress;

            lblVencimiento.Name = "lblVencimiento";
            lblVencimiento.Location = new Point(22, 127);
            lblVencimiento.Size = new Size(170, 26);
            lblVencimiento.Text = "Vencimiento:";
            dtpVencimiento.Name = "dtpVencimiento";
            dtpVencimiento.Location = new Point(220, 126);
            dtpVencimiento.Size = new Size(155, 28);
            dtpVencimiento.CalendarForeColor = Color.Black;
            lblSeguridad.Name = "lblSeguridad";
            lblSeguridad.Location = new Point(393, 127);
            lblSeguridad.Size = new Size(145, 26);
            lblSeguridad.Text = "Código seguridad:";
            txtCodigoSeguridad.Name = "txtCodigoSeguridad";
            txtCodigoSeguridad.Location = new Point(550, 126);
            txtCodigoSeguridad.Size = new Size(88, 28);
            txtCodigoSeguridad.MaxLength = 4;
            txtCodigoSeguridad.PasswordChar = '*';
            txtCodigoSeguridad.BackColor = Color.White;
            txtCodigoSeguridad.ForeColor = Color.Black;
            txtCodigoSeguridad.KeyPress += SoloNumeros_KeyPress;

            lblRespuesta.Name = "lblRespuesta";
            lblRespuesta.Location = new Point(22, 184);
            lblRespuesta.Size = new Size(192, 28);
            lblRespuesta.Text = "Respuesta del banco:";
            cboRespuestaBanco.Name = "cboRespuestaBanco";
            cboRespuestaBanco.Location = new Point(220, 181);
            cboRespuestaBanco.Size = new Size(418, 28);
            cboRespuestaBanco.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRespuestaBanco.BackColor = Color.White;
            cboRespuestaBanco.ForeColor = Color.Black;

            lblMensaje.Name = "lblMensaje";
            lblMensaje.Location = new Point(24, 510);
            lblMensaje.Size = new Size(680, 42);
            lblMensaje.Text = "";
            lblMensaje.ForeColor = acento;

            btnVolver.Name = "btnVolver";
            btnVolver.Location = new Point(24, 565);
            btnVolver.Size = new Size(188, 42);
            btnVolver.Text = "Volver";
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnVolver.BackColor = panel;
            btnVolver.ForeColor = Color.White;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVolver.Click += btnVolver_Click;

            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Location = new Point(400, 565);
            btnConfirmar.Size = new Size(304, 42);
            btnConfirmar.Text = "Confirmar cobro";
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.BackColor = acento;
            btnConfirmar.ForeColor = Color.Black;
            btnConfirmar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConfirmar.Click += btnConfirmar_Click;

            BackColor = azul;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            ClientSize = new Size(730, 625);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCobrarVenta";
            Text = "PadelGest - Cobrar Venta";
            Controls.Add(lblTitulo);
            Controls.Add(gbResumen);
            Controls.Add(gbTarjeta);
            Controls.Add(lblMensaje);
            Controls.Add(btnVolver);
            Controls.Add(btnConfirmar);
            Load += frmCobrarVenta_Load;
            FormClosed += frmCobrarVenta_FormClosed;
            gbTarjeta.ResumeLayout(false);
            gbTarjeta.PerformLayout();
            gbResumen.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Label lblTitulo;
        private GroupBox gbResumen;
        private Label lblClienteTitulo;
        private Label lblClienteValor;
        private Label lblProductosTitulo;
        private Label lblProductosValor;
        private Label lblTotalTitulo;
        private Label lblTotalValor;
        private GroupBox gbTarjeta;
        private Label lblBanco;
        private TextBox txtBanco;
        private Label lblTarjeta;
        private TextBox txtNumeroTarjeta;
        private Label lblVencimiento;
        private DateTimePicker dtpVencimiento;
        private Label lblSeguridad;
        private TextBox txtCodigoSeguridad;
        private Label lblRespuesta;
        private ComboBox cboRespuestaBanco;
        private Button btnConfirmar;
        private Button btnVolver;
        private Label lblMensaje;
    }
}
