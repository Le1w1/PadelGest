namespace UI
{
    partial class frmGestionarRolesYFamilias
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
            lblTitulo = new Label();
            lblRolFamilia = new Label();
            cboRolFamilia = new ComboBox();
            gbDisponibles = new GroupBox();
            clbDisponibles = new CheckedListBox();
            gbComposicion = new GroupBox();
            tvComposicion = new TreeView();
            gbDatos = new GroupBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            btnCrear = new Button();
            rbFamilia = new RadioButton();
            rbRol = new RadioButton();
            btnEliminarSeleccionado = new Button();
            btnGuardarCambios = new Button();
            btnCancelar = new Button();
            btnEliminar = new Button();
            lblMensaje = new Label();
            gbDisponibles.SuspendLayout();
            gbComposicion.SuspendLayout();
            gbDatos.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            // AirPadel style preview BEGIN
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            // AirPadel style preview END
            lblTitulo.Size = new Size(940, 35);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Roles y Familias";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRolFamilia
            // 
            lblRolFamilia.AutoSize = true;
            lblRolFamilia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRolFamilia.Location = new Point(330, 65);
            lblRolFamilia.Name = "lblRolFamilia";
            // AirPadel style preview BEGIN
            lblRolFamilia.ForeColor = Color.White;
            // AirPadel style preview END
            lblRolFamilia.Size = new Size(97, 19);
            lblRolFamilia.TabIndex = 1;
            lblRolFamilia.Text = "Rol - Familia:";
            // 
            // cboRolFamilia
            // 
            cboRolFamilia.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRolFamilia.Font = new Font("Segoe UI", 10F);
            cboRolFamilia.FormattingEnabled = true;
            cboRolFamilia.Location = new Point(435, 62);
            cboRolFamilia.Name = "cboRolFamilia";
            // AirPadel style preview BEGIN
            cboRolFamilia.BackColor = Color.White;
            cboRolFamilia.ForeColor = Color.FromArgb(18, 18, 18);
            cboRolFamilia.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cboRolFamilia.Size = new Size(280, 25);
            cboRolFamilia.TabIndex = 2;
            cboRolFamilia.SelectedIndexChanged += cboRolFamilia_SelectedIndexChanged;
            // 
            // gbDisponibles
            // 
            gbDisponibles.Controls.Add(clbDisponibles);
            gbDisponibles.Font = new Font("Segoe UI", 9F);
            gbDisponibles.Location = new Point(20, 100);
            gbDisponibles.Name = "gbDisponibles";
            // AirPadel style preview BEGIN
            gbDisponibles.BackColor = Color.FromArgb(24, 70, 138);
            gbDisponibles.ForeColor = Color.White;
            gbDisponibles.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbDisponibles.Size = new Size(290, 360);
            gbDisponibles.TabIndex = 3;
            gbDisponibles.TabStop = false;
            gbDisponibles.Text = "Disponibles (Permisos y Familias)";
            // 
            // clbDisponibles
            // 
            clbDisponibles.CheckOnClick = true;
            clbDisponibles.Dock = DockStyle.Fill;
            clbDisponibles.Font = new Font("Segoe UI", 9F);
            clbDisponibles.FormattingEnabled = true;
            clbDisponibles.Location = new Point(3, 19);
            clbDisponibles.Name = "clbDisponibles";
            // AirPadel style preview BEGIN
            clbDisponibles.BackColor = Color.White;
            clbDisponibles.ForeColor = Color.FromArgb(18, 18, 18);
            clbDisponibles.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            clbDisponibles.Size = new Size(284, 338);
            clbDisponibles.TabIndex = 0;
            clbDisponibles.ItemCheck += clbDisponibles_ItemCheck;
            // 
            // gbComposicion
            // 
            gbComposicion.Controls.Add(tvComposicion);
            gbComposicion.Font = new Font("Segoe UI", 9F);
            gbComposicion.Location = new Point(320, 100);
            gbComposicion.Name = "gbComposicion";
            // AirPadel style preview BEGIN
            gbComposicion.BackColor = Color.FromArgb(24, 70, 138);
            gbComposicion.ForeColor = Color.White;
            gbComposicion.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbComposicion.Size = new Size(395, 360);
            gbComposicion.TabIndex = 4;
            gbComposicion.TabStop = false;
            gbComposicion.Text = "Composición actual";
            // 
            // tvComposicion
            // 
            tvComposicion.Dock = DockStyle.Fill;
            tvComposicion.Font = new Font("Segoe UI", 9F);
            tvComposicion.HideSelection = false;
            tvComposicion.Location = new Point(3, 19);
            tvComposicion.Name = "tvComposicion";
            // AirPadel style preview BEGIN
            tvComposicion.BackColor = Color.White;
            tvComposicion.ForeColor = Color.FromArgb(18, 18, 18);
            tvComposicion.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            tvComposicion.Size = new Size(389, 338);
            tvComposicion.TabIndex = 0;
            // 
            // gbDatos
            // 
            gbDatos.Controls.Add(lblNombre);
            gbDatos.Controls.Add(txtNombre);
            gbDatos.Controls.Add(btnCrear);
            gbDatos.Controls.Add(rbFamilia);
            gbDatos.Controls.Add(rbRol);
            gbDatos.Controls.Add(btnEliminarSeleccionado);
            gbDatos.Font = new Font("Segoe UI", 9F);
            gbDatos.Location = new Point(725, 100);
            gbDatos.Name = "gbDatos";
            // AirPadel style preview BEGIN
            gbDatos.BackColor = Color.FromArgb(24, 70, 138);
            gbDatos.ForeColor = Color.White;
            gbDatos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            // AirPadel style preview END
            gbDatos.Size = new Size(235, 360);
            gbDatos.TabIndex = 5;
            gbDatos.TabStop = false;
            gbDatos.Text = "Crear / Editar";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F);
            lblNombre.Location = new Point(15, 30);
            lblNombre.Name = "lblNombre";
            // AirPadel style preview BEGIN
            lblNombre.ForeColor = Color.White;
            // AirPadel style preview END
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(15, 48);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            // AirPadel style preview BEGIN
            txtNombre.BackColor = Color.White;
            txtNombre.ForeColor = Color.FromArgb(18, 18, 18);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            // AirPadel style preview END
            txtNombre.Size = new Size(205, 25);
            txtNombre.TabIndex = 1;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.LightSkyBlue;
            btnCrear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCrear.Location = new Point(15, 85);
            btnCrear.Name = "btnCrear";
            // AirPadel style preview BEGIN
            btnCrear.FlatStyle = FlatStyle.Flat;
            btnCrear.FlatAppearance.BorderSize = 1;
            btnCrear.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCrear.BackColor = Color.FromArgb(24, 70, 138);
            btnCrear.ForeColor = Color.White;
            btnCrear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCrear.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnCrear.Size = new Size(205, 38);
            btnCrear.TabIndex = 2;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // rbFamilia
            // 
            rbFamilia.AutoSize = true;
            rbFamilia.Checked = true;
            rbFamilia.Font = new Font("Segoe UI", 10F);
            rbFamilia.Location = new Point(15, 140);
            rbFamilia.Name = "rbFamilia";
            // AirPadel style preview BEGIN
            rbFamilia.ForeColor = Color.White;
            // AirPadel style preview END
            rbFamilia.Size = new Size(69, 23);
            rbFamilia.TabIndex = 3;
            rbFamilia.TabStop = true;
            rbFamilia.Text = "Familia";
            rbFamilia.UseVisualStyleBackColor = true;
            // 
            // rbRol
            // 
            rbRol.AutoSize = true;
            rbRol.Font = new Font("Segoe UI", 10F);
            rbRol.Location = new Point(15, 165);
            rbRol.Name = "rbRol";
            // AirPadel style preview BEGIN
            rbRol.ForeColor = Color.White;
            // AirPadel style preview END
            rbRol.Size = new Size(46, 23);
            rbRol.TabIndex = 4;
            rbRol.Text = "Rol";
            rbRol.UseVisualStyleBackColor = true;
            // 
            // btnEliminarSeleccionado
            // 
            btnEliminarSeleccionado.BackColor = Color.LightSalmon;
            btnEliminarSeleccionado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminarSeleccionado.Location = new Point(15, 215);
            btnEliminarSeleccionado.Name = "btnEliminarSeleccionado";
            // AirPadel style preview BEGIN
            btnEliminarSeleccionado.FlatStyle = FlatStyle.Flat;
            btnEliminarSeleccionado.FlatAppearance.BorderSize = 1;
            btnEliminarSeleccionado.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnEliminarSeleccionado.BackColor = Color.FromArgb(24, 70, 138);
            btnEliminarSeleccionado.ForeColor = Color.White;
            btnEliminarSeleccionado.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEliminarSeleccionado.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnEliminarSeleccionado.Size = new Size(205, 50);
            btnEliminarSeleccionado.TabIndex = 5;
            btnEliminarSeleccionado.Text = "Quitar item\r\ndel TreeView";
            btnEliminarSeleccionado.UseVisualStyleBackColor = false;
            btnEliminarSeleccionado.Click += btnEliminarSeleccionado_Click;
            // 
            // btnGuardarCambios
            // 
            btnGuardarCambios.BackColor = Color.LightGreen;
            btnGuardarCambios.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardarCambios.Location = new Point(20, 480);
            btnGuardarCambios.Name = "btnGuardarCambios";
            // AirPadel style preview BEGIN
            btnGuardarCambios.FlatStyle = FlatStyle.Flat;
            btnGuardarCambios.FlatAppearance.BorderSize = 1;
            btnGuardarCambios.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnGuardarCambios.BackColor = Color.FromArgb(24, 70, 138);
            btnGuardarCambios.ForeColor = Color.White;
            btnGuardarCambios.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGuardarCambios.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnGuardarCambios.Size = new Size(170, 42);
            btnGuardarCambios.TabIndex = 6;
            btnGuardarCambios.Text = "Guardar cambios";
            btnGuardarCambios.UseVisualStyleBackColor = false;
            btnGuardarCambios.Click += btnGuardarCambios_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(200, 480);
            btnCancelar.Name = "btnCancelar";
            // AirPadel style preview BEGIN
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCancelar.BackColor = Color.FromArgb(24, 70, 138);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnCancelar.Size = new Size(140, 42);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.IndianRed;
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(755, 480);
            btnEliminar.Name = "btnEliminar";
            // AirPadel style preview BEGIN
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.FlatAppearance.BorderSize = 1;
            btnEliminar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnEliminar.BackColor = Color.FromArgb(24, 70, 138);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEliminar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnEliminar.Size = new Size(170, 63);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar\r\nFamilia / Rol";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 9F);
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 535);
            lblMensaje.Name = "lblMensaje";
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(940, 30);
            lblMensaje.TabIndex = 9;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmGestionarRolesYFamilias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(980, 580);
            Controls.Add(lblTitulo);
            Controls.Add(lblRolFamilia);
            Controls.Add(cboRolFamilia);
            Controls.Add(gbDisponibles);
            Controls.Add(gbComposicion);
            Controls.Add(gbDatos);
            Controls.Add(btnGuardarCambios);
            Controls.Add(btnCancelar);
            Controls.Add(btnEliminar);
            Controls.Add(lblMensaje);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmGestionarRolesYFamilias";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PadelGest - Gestión de Roles y Familias";
            gbDisponibles.ResumeLayout(false);
            gbComposicion.ResumeLayout(false);
            gbDatos.ResumeLayout(false);
            gbDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblRolFamilia;
        private ComboBox cboRolFamilia;
        private GroupBox gbDisponibles;
        private CheckedListBox clbDisponibles;
        private GroupBox gbComposicion;
        private TreeView tvComposicion;
        private GroupBox gbDatos;
        private Label lblNombre;
        private TextBox txtNombre;
        private Button btnCrear;
        private RadioButton rbFamilia;
        private RadioButton rbRol;
        private Button btnEliminarSeleccionado;
        private Button btnGuardarCambios;
        private Button btnCancelar;
        private Button btnEliminar;
        private Label lblMensaje;
    }
}
