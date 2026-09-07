namespace UI
{
    partial class frmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblContrasenia = new Label();
            txtContrasenia = new TextBox();
            chkMostrarContrasenia = new CheckBox();
            btnIngresar = new Button();
            btnSalir = new Button();
            btnVolver = new Button();
            cboIdioma = new ComboBox();
            lblMensaje = new Label();
            errorProviderLogin = new ErrorProvider(components);
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            ((System.ComponentModel.ISupportInitialize)errorProviderLogin).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(134, 9);
            lblTitulo.Name = "lblTitulo";
            // AirPadel style preview BEGIN
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            // AirPadel style preview END
            lblTitulo.Size = new Size(136, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "PadelGest";
            //
            // lblSubtitulo
            // 
            lblSubtitulo.Font = new Font("Segoe UI", 13F);
            lblSubtitulo.Location = new Point(124, 46);
            lblSubtitulo.Name = "lblSubtitulo";
            // AirPadel style preview BEGIN
            lblSubtitulo.ForeColor = Color.White;
            // AirPadel style preview END
            lblSubtitulo.Size = new Size(146, 34);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Inicio de Sesión";
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(68, 116);
            lblEmail.Name = "lblEmail";
            // AirPadel style preview BEGIN
            lblEmail.ForeColor = Color.White;
            // AirPadel style preview END
            lblEmail.Size = new Size(41, 19);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(69, 138);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            // AirPadel style preview BEGIN
            txtEmail.BackColor = Color.White;
            txtEmail.ForeColor = Color.FromArgb(18, 18, 18);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtEmail.Size = new Size(242, 25);
            txtEmail.TabIndex = 0;
            // 
            // lblContrasenia
            // 
            lblContrasenia.AutoSize = true;
            lblContrasenia.Location = new Point(69, 184);
            lblContrasenia.Name = "lblContrasenia";
            // AirPadel style preview BEGIN
            lblContrasenia.ForeColor = Color.White;
            // AirPadel style preview END
            lblContrasenia.Size = new Size(79, 19);
            lblContrasenia.TabIndex = 4;
            lblContrasenia.Text = "Contraseña";
            // 
            // txtContrasenia
            // 
            txtContrasenia.Location = new Point(69, 206);
            txtContrasenia.MaxLength = 50;
            txtContrasenia.Name = "txtContrasenia";
            // AirPadel style preview BEGIN
            txtContrasenia.BackColor = Color.White;
            txtContrasenia.ForeColor = Color.FromArgb(18, 18, 18);
            txtContrasenia.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtContrasenia.Size = new Size(242, 25);
            txtContrasenia.TabIndex = 1;
            txtContrasenia.UseSystemPasswordChar = true;
            // 
            // chkMostrarContrasenia
            // 
            chkMostrarContrasenia.AutoSize = true;
            chkMostrarContrasenia.Location = new Point(71, 237);
            chkMostrarContrasenia.Name = "chkMostrarContrasenia";
            // AirPadel style preview BEGIN
            chkMostrarContrasenia.ForeColor = Color.White;
            // AirPadel style preview END
            chkMostrarContrasenia.Size = new Size(77, 23);
            chkMostrarContrasenia.TabIndex = 2;
            chkMostrarContrasenia.Text = "Mostrar";
            chkMostrarContrasenia.UseVisualStyleBackColor = true;
            chkMostrarContrasenia.CheckedChanged += chkMostrarContrasenia_CheckedChanged;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(68, 295);
            btnIngresar.Name = "btnIngresar";
            // AirPadel style preview BEGIN
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.BackColor = Color.FromArgb(214, 246, 36);
            btnIngresar.ForeColor = Color.FromArgb(18, 18, 18);
            btnIngresar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnIngresar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnIngresar.Size = new Size(243, 37);
            btnIngresar.TabIndex = 3;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(68, 358);
            btnSalir.Name = "btnSalir";
            // AirPadel style preview BEGIN
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.FlatAppearance.BorderSize = 1;
            btnSalir.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnSalir.BackColor = Color.FromArgb(24, 70, 138);
            btnSalir.ForeColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSalir.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnSalir.Size = new Size(243, 36);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(68, 358);
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
            btnVolver.Size = new Size(243, 36);
            btnVolver.TabIndex = 5;
            btnVolver.Text = "Volver al Menú Principal";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Visible = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // cboIdioma
            // 
            cboIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIdioma.Font = new Font("Segoe UI", 9F);
            cboIdioma.FormattingEnabled = true;
            cboIdioma.Location = new Point(294, 12);
            cboIdioma.Name = "cboIdioma";
            // AirPadel style preview BEGIN
            cboIdioma.BackColor = Color.White;
            cboIdioma.ForeColor = Color.FromArgb(18, 18, 18);
            cboIdioma.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cboIdioma.Size = new Size(98, 23);
            cboIdioma.TabIndex = 10;
            cboIdioma.SelectedIndexChanged += cboIdioma_SelectedIndexChanged;
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(68, 419);
            lblMensaje.Name = "lblMensaje";
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(243, 53);
            lblMensaje.TabIndex = 9;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // errorProviderLogin
            // 
            errorProviderLogin.ContainerControl = this;
            // 
            // frmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            CancelButton = btnSalir;
            ClientSize = new Size(404, 481);
            Controls.Add(lblTitulo);
            Controls.Add(lblMensaje);
            Controls.Add(cboIdioma);
            Controls.Add(btnVolver);
            Controls.Add(btnSalir);
            Controls.Add(btnIngresar);
            Controls.Add(chkMostrarContrasenia);
            Controls.Add(txtContrasenia);
            Controls.Add(lblContrasenia);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(lblSubtitulo);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "frmLogin";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PadelGest - Incio de Sesion";
            ((System.ComponentModel.ISupportInitialize)errorProviderLogin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblContrasenia;
        private TextBox txtContrasenia;
        private CheckBox chkMostrarContrasenia;
        private Button btnIngresar;
        private Button btnSalir;
        private Button btnVolver;
        private ComboBox cboIdioma;
        private Label lblMensaje;
        private ErrorProvider errorProviderLogin;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
    }
}
