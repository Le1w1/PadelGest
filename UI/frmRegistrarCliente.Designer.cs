namespace UI
{
    partial class frmRegistrarCliente
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
            gbDatos = new GroupBox();
            txtCorreo = new TextBox();
            lblCorreo = new Label();
            txtTelefono = new TextBox();
            lblTelefono = new Label();
            txtApellido = new TextBox();
            lblApellido = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            txtDNI = new TextBox();
            lblDNI = new Label();
            lblMensaje = new Label();
            btnRegistrar = new Button();
            btnVolver = new Button();
            errorProvider = new ErrorProvider(components);
            gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(620, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registrar Cliente";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // gbDatos
            gbDatos.BackColor = Color.LightSkyBlue;
            gbDatos.Controls.Add(txtCorreo);
            gbDatos.Controls.Add(lblCorreo);
            gbDatos.Controls.Add(txtTelefono);
            gbDatos.Controls.Add(lblTelefono);
            gbDatos.Controls.Add(txtApellido);
            gbDatos.Controls.Add(lblApellido);
            gbDatos.Controls.Add(txtNombre);
            gbDatos.Controls.Add(lblNombre);
            gbDatos.Controls.Add(txtDNI);
            gbDatos.Controls.Add(lblDNI);
            gbDatos.Location = new Point(20, 80);
            gbDatos.Name = "gbDatos";
            gbDatos.Size = new Size(620, 260);
            gbDatos.TabIndex = 1;
            gbDatos.TabStop = false;
            gbDatos.Text = "Datos del Cliente";
            // DNI
            lblDNI.AutoSize = true;
            lblDNI.Location = new Point(35, 40);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(30, 15);
            lblDNI.TabIndex = 0;
            lblDNI.Text = "DNI:";
            txtDNI.Location = new Point(180, 37);
            txtDNI.MaxLength = 8;
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(380, 23);
            txtDNI.TabIndex = 1;
            txtDNI.TextChanged += Campo_TextChanged;
            txtDNI.KeyPress += SoloNumeros_KeyPress;
            // Nombre
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(35, 82);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            txtNombre.Location = new Point(180, 79);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(380, 23);
            txtNombre.TabIndex = 3;
            txtNombre.TextChanged += Campo_TextChanged;
            // Apellido
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(35, 124);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido:";
            txtApellido.Location = new Point(180, 121);
            txtApellido.MaxLength = 50;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(380, 23);
            txtApellido.TabIndex = 5;
            txtApellido.TextChanged += Campo_TextChanged;
            // Telefono
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(35, 166);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(55, 15);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "Teléfono:";
            txtTelefono.Location = new Point(180, 163);
            txtTelefono.MaxLength = 15;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(380, 23);
            txtTelefono.TabIndex = 7;
            txtTelefono.TextChanged += Campo_TextChanged;
            txtTelefono.KeyPress += SoloNumeros_KeyPress;
            // Correo
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(35, 208);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(108, 15);
            lblCorreo.TabIndex = 8;
            lblCorreo.Text = "Correo electrónico:";
            txtCorreo.Location = new Point(180, 205);
            txtCorreo.MaxLength = 150;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(380, 23);
            txtCorreo.TabIndex = 9;
            txtCorreo.TextChanged += Campo_TextChanged;
            // lblMensaje
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 355);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(620, 45);
            lblMensaje.TabIndex = 2;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // btnRegistrar
            btnRegistrar.Location = new Point(480, 415);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(160, 34);
            btnRegistrar.TabIndex = 3;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // btnVolver
            btnVolver.Location = new Point(360, 415);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.TabIndex = 4;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // errorProvider
            errorProvider.ContainerControl = this;
            // frmRegistrarCliente
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(660, 470);
            Controls.Add(btnVolver);
            Controls.Add(btnRegistrar);
            Controls.Add(lblMensaje);
            Controls.Add(gbDatos);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmRegistrarCliente";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Registrar Cliente";
            FormClosed += frmRegistrarCliente_FormClosed;
            Load += frmRegistrarCliente_Load;
            gbDatos.ResumeLayout(false);
            gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private GroupBox gbDatos;
        private Label lblDNI;
        private TextBox txtDNI;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblMensaje;
        private Button btnRegistrar;
        private Button btnVolver;
        private ErrorProvider errorProvider;
    }
}
