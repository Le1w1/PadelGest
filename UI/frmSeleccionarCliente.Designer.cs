namespace UI
{
    partial class frmSeleccionarCliente
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
            txtDNI = new TextBox();
            lblDNI = new Label();
            gbDatosCliente = new GroupBox();
            lblCorreoValor = new Label();
            lblCorreoTitulo = new Label();
            lblTelefonoValor = new Label();
            lblTelefonoTitulo = new Label();
            lblApellidoValor = new Label();
            lblApellidoTitulo = new Label();
            lblNombreValor = new Label();
            lblNombreTitulo = new Label();
            lblDNIValor = new Label();
            lblDNIValorTitulo = new Label();
            lblMensaje = new Label();
            btnConfirmar = new Button();
            btnRegistrarCliente = new Button();
            btnVolver = new Button();
            errorProvider = new ErrorProvider(components);
            gbBusqueda.SuspendLayout();
            gbDatosCliente.SuspendLayout();
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
            lblTitulo.Size = new Size(660, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Seleccionar Cliente";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // gbBusqueda
            gbBusqueda.BackColor = Color.LightSkyBlue;
            gbBusqueda.Controls.Add(btnBuscar);
            gbBusqueda.Controls.Add(txtDNI);
            gbBusqueda.Controls.Add(lblDNI);
            gbBusqueda.Location = new Point(20, 78);
            gbBusqueda.Name = "gbBusqueda";
            // AirPadel style preview BEGIN
            gbBusqueda.BackColor = Color.FromArgb(24, 70, 138);
            gbBusqueda.ForeColor = Color.White;
            gbBusqueda.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbBusqueda.Size = new Size(660, 95);
            gbBusqueda.TabIndex = 1;
            gbBusqueda.TabStop = false;
            gbBusqueda.Text = "Búsqueda por DNI";
            // btnBuscar
            btnBuscar.Location = new Point(455, 34);
            btnBuscar.Name = "btnBuscar";
            // AirPadel style preview BEGIN
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.BackColor = Color.FromArgb(214, 246, 36);
            btnBuscar.ForeColor = Color.FromArgb(18, 18, 18);
            btnBuscar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnBuscar.Size = new Size(165, 32);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Seleccionar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // txtDNI
            txtDNI.Location = new Point(130, 39);
            txtDNI.MaxLength = 8;
            txtDNI.Name = "txtDNI";
            // AirPadel style preview BEGIN
            txtDNI.BackColor = Color.White;
            txtDNI.ForeColor = Color.FromArgb(18, 18, 18);
            txtDNI.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtDNI.Size = new Size(220, 23);
            txtDNI.TabIndex = 1;
            txtDNI.TextChanged += txtDNI_TextChanged;
            txtDNI.KeyPress += txtDNI_KeyPress;
            // lblDNI
            lblDNI.AutoSize = true;
            lblDNI.Location = new Point(35, 42);
            lblDNI.Name = "lblDNI";
            // AirPadel style preview BEGIN
            lblDNI.ForeColor = Color.White;
            // AirPadel style preview END
            lblDNI.Size = new Size(30, 15);
            lblDNI.TabIndex = 0;
            lblDNI.Text = "DNI:";
            // gbDatosCliente
            gbDatosCliente.BackColor = Color.PowderBlue;
            gbDatosCliente.Controls.Add(lblCorreoValor);
            gbDatosCliente.Controls.Add(lblCorreoTitulo);
            gbDatosCliente.Controls.Add(lblTelefonoValor);
            gbDatosCliente.Controls.Add(lblTelefonoTitulo);
            gbDatosCliente.Controls.Add(lblApellidoValor);
            gbDatosCliente.Controls.Add(lblApellidoTitulo);
            gbDatosCliente.Controls.Add(lblNombreValor);
            gbDatosCliente.Controls.Add(lblNombreTitulo);
            gbDatosCliente.Controls.Add(lblDNIValor);
            gbDatosCliente.Controls.Add(lblDNIValorTitulo);
            gbDatosCliente.Location = new Point(20, 188);
            gbDatosCliente.Name = "gbDatosCliente";
            // AirPadel style preview BEGIN
            gbDatosCliente.BackColor = Color.FromArgb(24, 70, 138);
            gbDatosCliente.ForeColor = Color.White;
            gbDatosCliente.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbDatosCliente.Size = new Size(660, 190);
            gbDatosCliente.TabIndex = 2;
            gbDatosCliente.TabStop = false;
            gbDatosCliente.Text = "Datos del Cliente";
            // DNI
            lblDNIValorTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDNIValorTitulo.Location = new Point(35, 35);
            lblDNIValorTitulo.Name = "lblDNIValorTitulo";
            // AirPadel style preview BEGIN
            lblDNIValorTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblDNIValorTitulo.Size = new Size(85, 23);
            lblDNIValorTitulo.TabIndex = 0;
            lblDNIValorTitulo.Text = "DNI:";
            lblDNIValor.Location = new Point(135, 35);
            lblDNIValor.Name = "lblDNIValor";
            // AirPadel style preview BEGIN
            lblDNIValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblDNIValor.Size = new Size(170, 23);
            lblDNIValor.TabIndex = 1;
            lblDNIValor.Text = "-";
            // Nombre
            lblNombreTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombreTitulo.Location = new Point(340, 35);
            lblNombreTitulo.Name = "lblNombreTitulo";
            // AirPadel style preview BEGIN
            lblNombreTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblNombreTitulo.Size = new Size(85, 23);
            lblNombreTitulo.TabIndex = 2;
            lblNombreTitulo.Text = "Nombre:";
            lblNombreValor.Location = new Point(440, 35);
            lblNombreValor.Name = "lblNombreValor";
            // AirPadel style preview BEGIN
            lblNombreValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblNombreValor.Size = new Size(180, 23);
            lblNombreValor.TabIndex = 3;
            lblNombreValor.Text = "-";
            // Apellido
            lblApellidoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApellidoTitulo.Location = new Point(35, 80);
            lblApellidoTitulo.Name = "lblApellidoTitulo";
            // AirPadel style preview BEGIN
            lblApellidoTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblApellidoTitulo.Size = new Size(85, 23);
            lblApellidoTitulo.TabIndex = 4;
            lblApellidoTitulo.Text = "Apellido:";
            lblApellidoValor.Location = new Point(135, 80);
            lblApellidoValor.Name = "lblApellidoValor";
            // AirPadel style preview BEGIN
            lblApellidoValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblApellidoValor.Size = new Size(170, 23);
            lblApellidoValor.TabIndex = 5;
            lblApellidoValor.Text = "-";
            // Telefono
            lblTelefonoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTelefonoTitulo.Location = new Point(340, 80);
            lblTelefonoTitulo.Name = "lblTelefonoTitulo";
            // AirPadel style preview BEGIN
            lblTelefonoTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblTelefonoTitulo.Size = new Size(85, 23);
            lblTelefonoTitulo.TabIndex = 6;
            lblTelefonoTitulo.Text = "Teléfono:";
            lblTelefonoValor.Location = new Point(440, 80);
            lblTelefonoValor.Name = "lblTelefonoValor";
            // AirPadel style preview BEGIN
            lblTelefonoValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblTelefonoValor.Size = new Size(180, 23);
            lblTelefonoValor.TabIndex = 7;
            lblTelefonoValor.Text = "-";
            // Correo
            lblCorreoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCorreoTitulo.Location = new Point(35, 125);
            lblCorreoTitulo.Name = "lblCorreoTitulo";
            // AirPadel style preview BEGIN
            lblCorreoTitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblCorreoTitulo.Size = new Size(85, 23);
            lblCorreoTitulo.TabIndex = 8;
            lblCorreoTitulo.Text = "Correo:";
            lblCorreoValor.Location = new Point(135, 125);
            lblCorreoValor.Name = "lblCorreoValor";
            // AirPadel style preview BEGIN
            lblCorreoValor.ForeColor = Color.White;
            // AirPadel style preview END
            lblCorreoValor.Size = new Size(485, 23);
            lblCorreoValor.TabIndex = 9;
            lblCorreoValor.Text = "-";
            // lblMensaje
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 392);
            lblMensaje.Name = "lblMensaje";
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(660, 40);
            lblMensaje.TabIndex = 3;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // btnConfirmar
            btnConfirmar.Enabled = false;
            btnConfirmar.Location = new Point(520, 445);
            btnConfirmar.Name = "btnConfirmar";
            // AirPadel style preview BEGIN
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.BackColor = Color.FromArgb(214, 246, 36);
            btnConfirmar.ForeColor = Color.FromArgb(18, 18, 18);
            btnConfirmar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConfirmar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnConfirmar.Size = new Size(160, 34);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar Cliente";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // btnRegistrarCliente
            btnRegistrarCliente.Enabled = false;
            btnRegistrarCliente.Location = new Point(215, 445);
            btnRegistrarCliente.Name = "btnRegistrarCliente";
            // AirPadel style preview BEGIN
            btnRegistrarCliente.FlatStyle = FlatStyle.Flat;
            btnRegistrarCliente.FlatAppearance.BorderSize = 1;
            btnRegistrarCliente.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnRegistrarCliente.BackColor = Color.FromArgb(24, 70, 138);
            btnRegistrarCliente.ForeColor = Color.White;
            btnRegistrarCliente.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegistrarCliente.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnRegistrarCliente.Size = new Size(165, 34);
            btnRegistrarCliente.TabIndex = 5;
            btnRegistrarCliente.Text = "Registrar Cliente";
            btnRegistrarCliente.UseVisualStyleBackColor = false;
            btnRegistrarCliente.Visible = false;
            btnRegistrarCliente.Click += btnRegistrarCliente_Click;
            // btnVolver
            btnVolver.Location = new Point(400, 445);
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
            btnVolver.TabIndex = 5;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // errorProvider
            errorProvider.ContainerControl = this;
            // frmSeleccionarCliente
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(700, 500);
            Controls.Add(btnVolver);
            Controls.Add(btnRegistrarCliente);
            Controls.Add(btnConfirmar);
            Controls.Add(lblMensaje);
            Controls.Add(gbDatosCliente);
            Controls.Add(gbBusqueda);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSeleccionarCliente";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Seleccionar Cliente";
            FormClosed += frmSeleccionarCliente_FormClosed;
            Load += frmSeleccionarCliente_Load;
            gbBusqueda.ResumeLayout(false);
            gbBusqueda.PerformLayout();
            gbDatosCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private GroupBox gbBusqueda;
        private Label lblDNI;
        private TextBox txtDNI;
        private Button btnBuscar;
        private GroupBox gbDatosCliente;
        private Label lblDNIValorTitulo;
        private Label lblDNIValor;
        private Label lblNombreTitulo;
        private Label lblNombreValor;
        private Label lblApellidoTitulo;
        private Label lblApellidoValor;
        private Label lblTelefonoTitulo;
        private Label lblTelefonoValor;
        private Label lblCorreoTitulo;
        private Label lblCorreoValor;
        private Label lblMensaje;
        private Button btnConfirmar;
        private Button btnRegistrarCliente;
        private Button btnVolver;
        private ErrorProvider errorProvider;
    }
}
