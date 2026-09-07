namespace UI
{
    partial class frmGestionDeUsuario
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
            lblTitulo = new Label();
            gbFiltros = new GroupBox();
            rbTodos = new RadioButton();
            rbBloqueados = new RadioButton();
            rbActivos = new RadioButton();
            lblCantidadUsuarios = new Label();
            gbDatosUsuario = new GroupBox();
            lblRol = new Label();
            cboRol = new ComboBox();
            txtNombreUsuario = new TextBox();
            lblNombreUsuario = new Label();
            chkActivo = new CheckBox();
            txtEmail = new TextBox();
            txtDNI = new TextBox();
            lblEmail = new Label();
            lblDNI = new Label();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            lblApellido = new Label();
            lblNombre = new Label();
            dgvUsuarios = new DataGridView();
            btnCrearUsuario = new Button();
            btnModificarUsuario = new Button();
            btnDesbloquearUsuario = new Button();
            btnActivarDesactivarUsuario = new Button();
            btnLimpiar = new Button();
            btnVolver = new Button();
            lblMensaje = new Label();
            gbFiltros.SuspendLayout();
            gbDatosUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 16F);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            // AirPadel style preview BEGIN
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            // AirPadel style preview END
            lblTitulo.Size = new Size(890, 35);
            lblTitulo.TabIndex = 11;
            lblTitulo.Text = "Gestion de Usuarios";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbFiltros
            // 
            gbFiltros.BackColor = Color.LightSkyBlue;
            gbFiltros.Controls.Add(rbTodos);
            gbFiltros.Controls.Add(rbBloqueados);
            gbFiltros.Controls.Add(rbActivos);
            gbFiltros.Location = new Point(20, 60);
            gbFiltros.Name = "gbFiltros";
            // AirPadel style preview BEGIN
            gbFiltros.BackColor = Color.FromArgb(24, 70, 138);
            gbFiltros.ForeColor = Color.White;
            gbFiltros.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbFiltros.Size = new Size(330, 65);
            gbFiltros.TabIndex = 12;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "Filtros de Usuarios";
            // 
            // rbTodos
            // 
            rbTodos.AutoSize = true;
            rbTodos.Location = new Point(235, 28);
            rbTodos.Name = "rbTodos";
            // AirPadel style preview BEGIN
            rbTodos.ForeColor = Color.White;
            // AirPadel style preview END
            rbTodos.Size = new Size(57, 19);
            rbTodos.TabIndex = 2;
            rbTodos.Text = "Todos";
            rbTodos.UseVisualStyleBackColor = true;
            rbTodos.CheckedChanged += rbFiltros_CheckedChanged;
            // 
            // rbBloqueados
            // 
            rbBloqueados.AutoSize = true;
            rbBloqueados.Location = new Point(120, 28);
            rbBloqueados.Name = "rbBloqueados";
            // AirPadel style preview BEGIN
            rbBloqueados.ForeColor = Color.White;
            // AirPadel style preview END
            rbBloqueados.Size = new Size(87, 19);
            rbBloqueados.TabIndex = 1;
            rbBloqueados.Text = "Bloqueados";
            rbBloqueados.UseVisualStyleBackColor = true;
            rbBloqueados.CheckedChanged += rbFiltros_CheckedChanged;
            // 
            // rbActivos
            // 
            rbActivos.AutoSize = true;
            rbActivos.Checked = true;
            rbActivos.Location = new Point(15, 28);
            rbActivos.Name = "rbActivos";
            // AirPadel style preview BEGIN
            rbActivos.ForeColor = Color.White;
            // AirPadel style preview END
            rbActivos.Size = new Size(64, 19);
            rbActivos.TabIndex = 0;
            rbActivos.TabStop = true;
            rbActivos.Text = "Activos";
            rbActivos.UseVisualStyleBackColor = true;
            rbActivos.CheckedChanged += rbFiltros_CheckedChanged;
            // 
            // lblCantidadUsuarios
            // 
            lblCantidadUsuarios.Font = new Font("Segoe UI", 10F);
            lblCantidadUsuarios.Location = new Point(650, 80);
            lblCantidadUsuarios.Name = "lblCantidadUsuarios";
            // AirPadel style preview BEGIN
            lblCantidadUsuarios.ForeColor = Color.White;
            // AirPadel style preview END
            lblCantidadUsuarios.Size = new Size(260, 25);
            lblCantidadUsuarios.TabIndex = 13;
            lblCantidadUsuarios.Text = "Número de Usuarios: 0";
            lblCantidadUsuarios.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbDatosUsuario
            // 
            gbDatosUsuario.BackColor = Color.LightSkyBlue;
            gbDatosUsuario.Controls.Add(lblRol);
            gbDatosUsuario.Controls.Add(cboRol);
            gbDatosUsuario.Controls.Add(txtNombreUsuario);
            gbDatosUsuario.Controls.Add(lblNombreUsuario);
            gbDatosUsuario.Controls.Add(chkActivo);
            gbDatosUsuario.Controls.Add(txtEmail);
            gbDatosUsuario.Controls.Add(txtDNI);
            gbDatosUsuario.Controls.Add(lblEmail);
            gbDatosUsuario.Controls.Add(lblDNI);
            gbDatosUsuario.Controls.Add(txtApellido);
            gbDatosUsuario.Controls.Add(txtNombre);
            gbDatosUsuario.Controls.Add(lblApellido);
            gbDatosUsuario.Controls.Add(lblNombre);
            gbDatosUsuario.Location = new Point(20, 365);
            gbDatosUsuario.Name = "gbDatosUsuario";
            // AirPadel style preview BEGIN
            gbDatosUsuario.BackColor = Color.FromArgb(24, 70, 138);
            gbDatosUsuario.ForeColor = Color.White;
            gbDatosUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbDatosUsuario.Size = new Size(610, 188);
            gbDatosUsuario.TabIndex = 14;
            gbDatosUsuario.TabStop = false;
            gbDatosUsuario.Text = "Datos del Usuario";
            // 
            // lblRol
            // 
            lblRol.Location = new Point(20, 141);
            lblRol.Name = "lblRol";
            // AirPadel style preview BEGIN
            lblRol.ForeColor = Color.White;
            // AirPadel style preview END
            lblRol.Size = new Size(120, 23);
            lblRol.TabIndex = 15;
            lblRol.Text = "Rol:";
            // 
            // cboRol
            // 
            cboRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRol.Font = new Font("Segoe UI", 10F);
            cboRol.FormattingEnabled = true;
            cboRol.Location = new Point(150, 139);
            cboRol.Margin = new Padding(3, 2, 3, 2);
            cboRol.Name = "cboRol";
            // AirPadel style preview BEGIN
            cboRol.BackColor = Color.White;
            cboRol.ForeColor = Color.FromArgb(18, 18, 18);
            cboRol.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cboRol.Size = new Size(224, 25);
            cboRol.TabIndex = 16;
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(150, 100);
            txtNombreUsuario.Name = "txtNombreUsuario";
            // AirPadel style preview BEGIN
            txtNombreUsuario.BackColor = Color.White;
            txtNombreUsuario.ForeColor = Color.FromArgb(18, 18, 18);
            txtNombreUsuario.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtNombreUsuario.Size = new Size(160, 23);
            txtNombreUsuario.TabIndex = 14;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.Location = new Point(20, 103);
            lblNombreUsuario.Name = "lblNombreUsuario";
            // AirPadel style preview BEGIN
            lblNombreUsuario.ForeColor = Color.White;
            // AirPadel style preview END
            lblNombreUsuario.Size = new Size(120, 23);
            lblNombreUsuario.TabIndex = 13;
            lblNombreUsuario.Text = "Nombre de Usuario:";
            // 
            // chkActivo
            // 
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Location = new Point(430, 100);
            chkActivo.Name = "chkActivo";
            // AirPadel style preview BEGIN
            chkActivo.ForeColor = Color.White;
            // AirPadel style preview END
            chkActivo.RightToLeft = RightToLeft.No;
            chkActivo.Size = new Size(108, 23);
            chkActivo.TabIndex = 11;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(430, 62);
            txtEmail.Name = "txtEmail";
            // AirPadel style preview BEGIN
            txtEmail.BackColor = Color.White;
            txtEmail.ForeColor = Color.FromArgb(18, 18, 18);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtEmail.Size = new Size(150, 23);
            txtEmail.TabIndex = 7;
            // 
            // txtDNI
            // 
            txtDNI.Location = new Point(150, 62);
            txtDNI.Name = "txtDNI";
            // AirPadel style preview BEGIN
            txtDNI.BackColor = Color.White;
            txtDNI.ForeColor = Color.FromArgb(18, 18, 18);
            txtDNI.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtDNI.Size = new Size(160, 23);
            txtDNI.TabIndex = 6;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(330, 65);
            lblEmail.Name = "lblEmail";
            // AirPadel style preview BEGIN
            lblEmail.ForeColor = Color.White;
            // AirPadel style preview END
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email:";
            // 
            // lblDNI
            // 
            lblDNI.Location = new Point(20, 65);
            lblDNI.Name = "lblDNI";
            // AirPadel style preview BEGIN
            lblDNI.ForeColor = Color.White;
            // AirPadel style preview END
            lblDNI.Size = new Size(120, 23);
            lblDNI.TabIndex = 4;
            lblDNI.Text = "DNI:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(430, 27);
            txtApellido.Name = "txtApellido";
            // AirPadel style preview BEGIN
            txtApellido.BackColor = Color.White;
            txtApellido.ForeColor = Color.FromArgb(18, 18, 18);
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtApellido.Size = new Size(150, 23);
            txtApellido.TabIndex = 3;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(150, 27);
            txtNombre.Name = "txtNombre";
            // AirPadel style preview BEGIN
            txtNombre.BackColor = Color.White;
            txtNombre.ForeColor = Color.FromArgb(18, 18, 18);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtNombre.Size = new Size(160, 23);
            txtNombre.TabIndex = 2;
            // 
            // lblApellido
            // 
            lblApellido.Location = new Point(330, 30);
            lblApellido.Name = "lblApellido";
            // AirPadel style preview BEGIN
            lblApellido.ForeColor = Color.White;
            // AirPadel style preview END
            lblApellido.Size = new Size(100, 23);
            lblApellido.TabIndex = 1;
            lblApellido.Text = "Apellido:";
            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(20, 30);
            lblNombre.Name = "lblNombre";
            // AirPadel style preview BEGIN
            lblNombre.ForeColor = Color.White;
            // AirPadel style preview END
            lblNombre.Size = new Size(120, 23);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AllowUserToResizeColumns = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(20, 152);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            // AirPadel style preview BEGIN
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.GridColor = Color.FromArgb(64, 103, 166);
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvUsuarios.ColumnHeadersHeight = 36;
            dgvUsuarios.RowTemplate.Height = 30;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(214, 246, 36);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(18, 18, 18);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 230, 248);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 18, 18);
            // AirPadel style preview END
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(722, 171);
            dgvUsuarios.TabIndex = 1;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.Location = new Point(747, 152);
            btnCrearUsuario.Name = "btnCrearUsuario";
            // AirPadel style preview BEGIN
            btnCrearUsuario.FlatStyle = FlatStyle.Flat;
            btnCrearUsuario.FlatAppearance.BorderSize = 1;
            btnCrearUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCrearUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnCrearUsuario.ForeColor = Color.White;
            btnCrearUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCrearUsuario.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnCrearUsuario.Size = new Size(162, 38);
            btnCrearUsuario.TabIndex = 15;
            btnCrearUsuario.Text = "Crear Usuario";
            btnCrearUsuario.UseVisualStyleBackColor = false;
            btnCrearUsuario.Click += btnCrearUsuario_Click;
            // 
            // btnModificarUsuario
            // 
            btnModificarUsuario.Location = new Point(748, 196);
            btnModificarUsuario.Name = "btnModificarUsuario";
            // AirPadel style preview BEGIN
            btnModificarUsuario.FlatStyle = FlatStyle.Flat;
            btnModificarUsuario.FlatAppearance.BorderSize = 1;
            btnModificarUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnModificarUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnModificarUsuario.ForeColor = Color.White;
            btnModificarUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnModificarUsuario.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnModificarUsuario.Size = new Size(162, 38);
            btnModificarUsuario.TabIndex = 16;
            btnModificarUsuario.Text = "Modificar Usuario";
            btnModificarUsuario.UseVisualStyleBackColor = false;
            btnModificarUsuario.Click += btnModificarUsuario_Click;
            // 
            // btnDesbloquearUsuario
            // 
            btnDesbloquearUsuario.Location = new Point(747, 241);
            btnDesbloquearUsuario.Name = "btnDesbloquearUsuario";
            // AirPadel style preview BEGIN
            btnDesbloquearUsuario.FlatStyle = FlatStyle.Flat;
            btnDesbloquearUsuario.FlatAppearance.BorderSize = 1;
            btnDesbloquearUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnDesbloquearUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnDesbloquearUsuario.ForeColor = Color.White;
            btnDesbloquearUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDesbloquearUsuario.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnDesbloquearUsuario.Size = new Size(162, 38);
            btnDesbloquearUsuario.TabIndex = 17;
            btnDesbloquearUsuario.Text = "Desbloquear Usuario";
            btnDesbloquearUsuario.UseVisualStyleBackColor = false;
            btnDesbloquearUsuario.Click += btnDesbloquearUsuario_Click;
            // 
            // btnActivarDesactivarUsuario
            // 
            btnActivarDesactivarUsuario.Location = new Point(747, 285);
            btnActivarDesactivarUsuario.Name = "btnActivarDesactivarUsuario";
            // AirPadel style preview BEGIN
            btnActivarDesactivarUsuario.FlatStyle = FlatStyle.Flat;
            btnActivarDesactivarUsuario.FlatAppearance.BorderSize = 1;
            btnActivarDesactivarUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnActivarDesactivarUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnActivarDesactivarUsuario.ForeColor = Color.White;
            btnActivarDesactivarUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnActivarDesactivarUsuario.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnActivarDesactivarUsuario.Size = new Size(162, 38);
            btnActivarDesactivarUsuario.TabIndex = 18;
            btnActivarDesactivarUsuario.Text = "Activar / Desactivar Usuario";
            btnActivarDesactivarUsuario.UseVisualStyleBackColor = false;
            btnActivarDesactivarUsuario.Click += btnActivarDesactivarUsuario_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(680, 410);
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
            btnLimpiar.Size = new Size(110, 35);
            btnLimpiar.TabIndex = 20;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(800, 410);
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
            btnVolver.Size = new Size(110, 35);
            btnVolver.TabIndex = 21;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click_1;
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 525);
            lblMensaje.Name = "lblMensaje";
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(890, 25);
            lblMensaje.TabIndex = 22;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmGestionDeUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(934, 561);
            Controls.Add(lblMensaje);
            Controls.Add(btnVolver);
            Controls.Add(btnLimpiar);
            Controls.Add(btnActivarDesactivarUsuario);
            Controls.Add(btnDesbloquearUsuario);
            Controls.Add(btnModificarUsuario);
            Controls.Add(btnCrearUsuario);
            Controls.Add(gbDatosUsuario);
            Controls.Add(lblCantidadUsuarios);
            Controls.Add(gbFiltros);
            Controls.Add(lblTitulo);
            Controls.Add(dgvUsuarios);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmGestionDeUsuario";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Gestion de Usuarios";
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            gbDatosUsuario.ResumeLayout(false);
            gbDatosUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblTitulo;
        private GroupBox gbFiltros;
        private RadioButton rbBloqueados;
        private RadioButton rbActivos;
        private RadioButton rbTodos;
        private Label lblCantidadUsuarios;
        private GroupBox gbDatosUsuario;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private Label lblApellido;
        private Label lblNombre;
        private DataGridView dgvUsuarios;
        private TextBox txtEmail;
        private TextBox txtDNI;
        private Label lblEmail;
        private Label lblDNI;
        private CheckBox chkActivo;
        private Button btnCrearUsuario;
        private Button btnModificarUsuario;
        private Button btnDesbloquearUsuario;
        private Button btnActivarDesactivarUsuario;
        private Button btnLimpiar;
        private Button btnVolver;
        private Label lblMensaje;
        private TextBox txtNombreUsuario;
        private Label lblNombreUsuario;
        private Label lblRol;
        private ComboBox cboRol;
    }
}