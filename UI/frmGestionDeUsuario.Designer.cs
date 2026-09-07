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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(890, 35);
            lblTitulo.TabIndex = 11;
            lblTitulo.Text = "Gestion de Usuarios";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbFiltros
            // 
            gbFiltros.BackColor = Color.FromArgb(24, 70, 138);
            gbFiltros.Controls.Add(rbTodos);
            gbFiltros.Controls.Add(rbBloqueados);
            gbFiltros.Controls.Add(rbActivos);
            gbFiltros.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbFiltros.ForeColor = Color.White;
            gbFiltros.Location = new Point(20, 60);
            gbFiltros.Name = "gbFiltros";
            gbFiltros.Size = new Size(330, 65);
            gbFiltros.TabIndex = 12;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "Filtros de Usuarios";
            // 
            // rbTodos
            // 
            rbTodos.AutoSize = true;
            rbTodos.ForeColor = Color.White;
            rbTodos.Location = new Point(235, 28);
            rbTodos.Name = "rbTodos";
            rbTodos.Size = new Size(63, 21);
            rbTodos.TabIndex = 2;
            rbTodos.Text = "Todos";
            rbTodos.UseVisualStyleBackColor = true;
            rbTodos.CheckedChanged += rbFiltros_CheckedChanged;
            // 
            // rbBloqueados
            // 
            rbBloqueados.AutoSize = true;
            rbBloqueados.ForeColor = Color.White;
            rbBloqueados.Location = new Point(120, 28);
            rbBloqueados.Name = "rbBloqueados";
            rbBloqueados.Size = new Size(98, 21);
            rbBloqueados.TabIndex = 1;
            rbBloqueados.Text = "Bloqueados";
            rbBloqueados.UseVisualStyleBackColor = true;
            rbBloqueados.CheckedChanged += rbFiltros_CheckedChanged;
            // 
            // rbActivos
            // 
            rbActivos.AutoSize = true;
            rbActivos.Checked = true;
            rbActivos.ForeColor = Color.White;
            rbActivos.Location = new Point(15, 28);
            rbActivos.Name = "rbActivos";
            rbActivos.Size = new Size(71, 21);
            rbActivos.TabIndex = 0;
            rbActivos.TabStop = true;
            rbActivos.Text = "Activos";
            rbActivos.UseVisualStyleBackColor = true;
            rbActivos.CheckedChanged += rbFiltros_CheckedChanged;
            // 
            // lblCantidadUsuarios
            // 
            lblCantidadUsuarios.Font = new Font("Segoe UI", 10F);
            lblCantidadUsuarios.ForeColor = Color.White;
            lblCantidadUsuarios.Location = new Point(650, 80);
            lblCantidadUsuarios.Name = "lblCantidadUsuarios";
            lblCantidadUsuarios.Size = new Size(260, 25);
            lblCantidadUsuarios.TabIndex = 13;
            lblCantidadUsuarios.Text = "Número de Usuarios: 0";
            lblCantidadUsuarios.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbDatosUsuario
            // 
            gbDatosUsuario.BackColor = Color.FromArgb(24, 70, 138);
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
            gbDatosUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbDatosUsuario.ForeColor = Color.White;
            gbDatosUsuario.Location = new Point(20, 361);
            gbDatosUsuario.Name = "gbDatosUsuario";
            gbDatosUsuario.Size = new Size(610, 188);
            gbDatosUsuario.TabIndex = 14;
            gbDatosUsuario.TabStop = false;
            gbDatosUsuario.Text = "Datos del Usuario";
            // 
            // lblRol
            // 
            lblRol.ForeColor = Color.White;
            lblRol.Location = new Point(20, 141);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(120, 23);
            lblRol.TabIndex = 15;
            lblRol.Text = "Rol:";
            // 
            // cboRol
            // 
            cboRol.BackColor = Color.White;
            cboRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRol.FlatStyle = FlatStyle.Flat;
            cboRol.Font = new Font("Segoe UI", 10F);
            cboRol.ForeColor = Color.FromArgb(18, 18, 18);
            cboRol.FormattingEnabled = true;
            cboRol.Location = new Point(150, 139);
            cboRol.Margin = new Padding(3, 2, 3, 2);
            cboRol.Name = "cboRol";
            cboRol.Size = new Size(224, 25);
            cboRol.TabIndex = 16;
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.BackColor = Color.White;
            txtNombreUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtNombreUsuario.ForeColor = Color.FromArgb(18, 18, 18);
            txtNombreUsuario.Location = new Point(150, 100);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(160, 24);
            txtNombreUsuario.TabIndex = 14;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.ForeColor = Color.White;
            lblNombreUsuario.Location = new Point(20, 103);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(120, 23);
            lblNombreUsuario.TabIndex = 13;
            lblNombreUsuario.Text = "Nombre de Usuario:";
            // 
            // chkActivo
            // 
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(430, 100);
            chkActivo.Name = "chkActivo";
            chkActivo.RightToLeft = RightToLeft.No;
            chkActivo.Size = new Size(108, 23);
            chkActivo.TabIndex = 11;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.ForeColor = Color.FromArgb(18, 18, 18);
            txtEmail.Location = new Point(430, 62);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(150, 24);
            txtEmail.TabIndex = 7;
            // 
            // txtDNI
            // 
            txtDNI.BackColor = Color.White;
            txtDNI.BorderStyle = BorderStyle.FixedSingle;
            txtDNI.ForeColor = Color.FromArgb(18, 18, 18);
            txtDNI.Location = new Point(150, 62);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(160, 24);
            txtDNI.TabIndex = 6;
            // 
            // lblEmail
            // 
            lblEmail.ForeColor = Color.White;
            lblEmail.Location = new Point(330, 65);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email:";
            // 
            // lblDNI
            // 
            lblDNI.ForeColor = Color.White;
            lblDNI.Location = new Point(20, 65);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(120, 23);
            lblDNI.TabIndex = 4;
            lblDNI.Text = "DNI:";
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.White;
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            txtApellido.ForeColor = Color.FromArgb(18, 18, 18);
            txtApellido.Location = new Point(430, 27);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(150, 24);
            txtApellido.TabIndex = 3;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.White;
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.ForeColor = Color.FromArgb(18, 18, 18);
            txtNombre.Location = new Point(150, 27);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(160, 24);
            txtNombre.TabIndex = 2;
            // 
            // lblApellido
            // 
            lblApellido.ForeColor = Color.White;
            lblApellido.Location = new Point(330, 30);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(100, 23);
            lblApellido.TabIndex = 1;
            lblApellido.Text = "Apellido:";
            // 
            // lblNombre
            // 
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(20, 30);
            lblNombre.Name = "lblNombre";
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
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(214, 246, 36);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(211, 230, 248);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.GridColor = Color.FromArgb(64, 103, 166);
            dgvUsuarios.Location = new Point(20, 152);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvUsuarios.RowTemplate.Height = 30;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(722, 171);
            dgvUsuarios.TabIndex = 1;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnCrearUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCrearUsuario.FlatStyle = FlatStyle.Flat;
            btnCrearUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCrearUsuario.ForeColor = Color.White;
            btnCrearUsuario.Location = new Point(747, 152);
            btnCrearUsuario.Name = "btnCrearUsuario";
            btnCrearUsuario.Size = new Size(162, 38);
            btnCrearUsuario.TabIndex = 15;
            btnCrearUsuario.Text = "Crear Usuario";
            btnCrearUsuario.UseVisualStyleBackColor = false;
            btnCrearUsuario.Click += btnCrearUsuario_Click;
            // 
            // btnModificarUsuario
            // 
            btnModificarUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnModificarUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnModificarUsuario.FlatStyle = FlatStyle.Flat;
            btnModificarUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnModificarUsuario.ForeColor = Color.White;
            btnModificarUsuario.Location = new Point(748, 196);
            btnModificarUsuario.Name = "btnModificarUsuario";
            btnModificarUsuario.Size = new Size(162, 38);
            btnModificarUsuario.TabIndex = 16;
            btnModificarUsuario.Text = "Modificar Usuario";
            btnModificarUsuario.UseVisualStyleBackColor = false;
            btnModificarUsuario.Click += btnModificarUsuario_Click;
            // 
            // btnDesbloquearUsuario
            // 
            btnDesbloquearUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnDesbloquearUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnDesbloquearUsuario.FlatStyle = FlatStyle.Flat;
            btnDesbloquearUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDesbloquearUsuario.ForeColor = Color.White;
            btnDesbloquearUsuario.Location = new Point(747, 241);
            btnDesbloquearUsuario.Name = "btnDesbloquearUsuario";
            btnDesbloquearUsuario.Size = new Size(162, 38);
            btnDesbloquearUsuario.TabIndex = 17;
            btnDesbloquearUsuario.Text = "Desbloquear Usuario";
            btnDesbloquearUsuario.UseVisualStyleBackColor = false;
            btnDesbloquearUsuario.Click += btnDesbloquearUsuario_Click;
            // 
            // btnActivarDesactivarUsuario
            // 
            btnActivarDesactivarUsuario.BackColor = Color.FromArgb(24, 70, 138);
            btnActivarDesactivarUsuario.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnActivarDesactivarUsuario.FlatStyle = FlatStyle.Flat;
            btnActivarDesactivarUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnActivarDesactivarUsuario.ForeColor = Color.White;
            btnActivarDesactivarUsuario.Location = new Point(747, 285);
            btnActivarDesactivarUsuario.Name = "btnActivarDesactivarUsuario";
            btnActivarDesactivarUsuario.Size = new Size(162, 38);
            btnActivarDesactivarUsuario.TabIndex = 18;
            btnActivarDesactivarUsuario.Text = "Activar / Desactivar Usuario";
            btnActivarDesactivarUsuario.UseVisualStyleBackColor = false;
            btnActivarDesactivarUsuario.Click += btnActivarDesactivarUsuario_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(24, 70, 138);
            btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(680, 410);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(110, 35);
            btnLimpiar.TabIndex = 20;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(800, 410);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(110, 35);
            btnVolver.TabIndex = 21;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click_1;
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            lblMensaje.Location = new Point(20, 552);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(890, 25);
            lblMensaje.TabIndex = 22;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmGestionDeUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
            ClientSize = new Size(934, 584);
            Controls.Add(lblMensaje);
            Controls.Add(gbDatosUsuario);
            Controls.Add(btnVolver);
            Controls.Add(btnLimpiar);
            Controls.Add(btnActivarDesactivarUsuario);
            Controls.Add(btnDesbloquearUsuario);
            Controls.Add(btnModificarUsuario);
            Controls.Add(btnCrearUsuario);
            Controls.Add(lblCantidadUsuarios);
            Controls.Add(gbFiltros);
            Controls.Add(lblTitulo);
            Controls.Add(dgvUsuarios);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmGestionDeUsuario";
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