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
            gbBusqueda.Size = new Size(660, 95);
            gbBusqueda.TabIndex = 1;
            gbBusqueda.TabStop = false;
            gbBusqueda.Text = "Búsqueda por DNI";
            // btnBuscar
            btnBuscar.Location = new Point(455, 34);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(165, 32);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Seleccionar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // txtDNI
            txtDNI.Location = new Point(130, 39);
            txtDNI.MaxLength = 8;
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(220, 23);
            txtDNI.TabIndex = 1;
            txtDNI.TextChanged += txtDNI_TextChanged;
            txtDNI.KeyPress += txtDNI_KeyPress;
            // lblDNI
            lblDNI.AutoSize = true;
            lblDNI.Location = new Point(35, 42);
            lblDNI.Name = "lblDNI";
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
            gbDatosCliente.Size = new Size(660, 190);
            gbDatosCliente.TabIndex = 2;
            gbDatosCliente.TabStop = false;
            gbDatosCliente.Text = "Datos del Cliente";
            // DNI
            lblDNIValorTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDNIValorTitulo.Location = new Point(35, 35);
            lblDNIValorTitulo.Name = "lblDNIValorTitulo";
            lblDNIValorTitulo.Size = new Size(85, 23);
            lblDNIValorTitulo.TabIndex = 0;
            lblDNIValorTitulo.Text = "DNI:";
            lblDNIValor.Location = new Point(135, 35);
            lblDNIValor.Name = "lblDNIValor";
            lblDNIValor.Size = new Size(170, 23);
            lblDNIValor.TabIndex = 1;
            lblDNIValor.Text = "-";
            // Nombre
            lblNombreTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombreTitulo.Location = new Point(340, 35);
            lblNombreTitulo.Name = "lblNombreTitulo";
            lblNombreTitulo.Size = new Size(85, 23);
            lblNombreTitulo.TabIndex = 2;
            lblNombreTitulo.Text = "Nombre:";
            lblNombreValor.Location = new Point(440, 35);
            lblNombreValor.Name = "lblNombreValor";
            lblNombreValor.Size = new Size(180, 23);
            lblNombreValor.TabIndex = 3;
            lblNombreValor.Text = "-";
            // Apellido
            lblApellidoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApellidoTitulo.Location = new Point(35, 80);
            lblApellidoTitulo.Name = "lblApellidoTitulo";
            lblApellidoTitulo.Size = new Size(85, 23);
            lblApellidoTitulo.TabIndex = 4;
            lblApellidoTitulo.Text = "Apellido:";
            lblApellidoValor.Location = new Point(135, 80);
            lblApellidoValor.Name = "lblApellidoValor";
            lblApellidoValor.Size = new Size(170, 23);
            lblApellidoValor.TabIndex = 5;
            lblApellidoValor.Text = "-";
            // Telefono
            lblTelefonoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTelefonoTitulo.Location = new Point(340, 80);
            lblTelefonoTitulo.Name = "lblTelefonoTitulo";
            lblTelefonoTitulo.Size = new Size(85, 23);
            lblTelefonoTitulo.TabIndex = 6;
            lblTelefonoTitulo.Text = "Teléfono:";
            lblTelefonoValor.Location = new Point(440, 80);
            lblTelefonoValor.Name = "lblTelefonoValor";
            lblTelefonoValor.Size = new Size(180, 23);
            lblTelefonoValor.TabIndex = 7;
            lblTelefonoValor.Text = "-";
            // Correo
            lblCorreoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCorreoTitulo.Location = new Point(35, 125);
            lblCorreoTitulo.Name = "lblCorreoTitulo";
            lblCorreoTitulo.Size = new Size(85, 23);
            lblCorreoTitulo.TabIndex = 8;
            lblCorreoTitulo.Text = "Correo:";
            lblCorreoValor.Location = new Point(135, 125);
            lblCorreoValor.Name = "lblCorreoValor";
            lblCorreoValor.Size = new Size(485, 23);
            lblCorreoValor.TabIndex = 9;
            lblCorreoValor.Text = "-";
            // lblMensaje
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 392);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(660, 40);
            lblMensaje.TabIndex = 3;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // btnConfirmar
            btnConfirmar.Enabled = false;
            btnConfirmar.Location = new Point(520, 445);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(160, 34);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar Cliente";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // btnVolver
            btnVolver.Location = new Point(400, 445);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.TabIndex = 5;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // errorProvider
            errorProvider.ContainerControl = this;
            // frmSeleccionarCliente
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(700, 500);
            Controls.Add(btnVolver);
            Controls.Add(btnConfirmar);
            Controls.Add(lblMensaje);
            Controls.Add(gbDatosCliente);
            Controls.Add(gbBusqueda);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSeleccionarCliente";
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
        private Button btnVolver;
        private ErrorProvider errorProvider;
    }
}
