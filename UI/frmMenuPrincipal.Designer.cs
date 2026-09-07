namespace UI
{
    partial class frmMenuPrincipal
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
            menuPrincipal = new MenuStrip();
            mnuSesion = new ToolStripMenuItem();
            reLoginToolStripMenuItem = new ToolStripMenuItem();
            cambiarClaveToolStripMenuItem = new ToolStripMenuItem();
            cambiarIdiomaToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            RecepcionistaToolStripMenuItem = new ToolStripMenuItem();
            seleccionarTurnoToolStripMenuItem = new ToolStripMenuItem();
            vendedorBuffetToolStripMenuItem = new ToolStripMenuItem();
            encargadoDeCanchasToolStripMenuItem = new ToolStripMenuItem();
            dueñoToolStripMenuItem = new ToolStripMenuItem();
            mnuAdministrador = new ToolStripMenuItem();
            usuariosToolStripMenuItem = new ToolStripMenuItem();
            bitacoraEventosToolStripMenuItem = new ToolStripMenuItem();
            gestionarPerfilToolStripMenuItem = new ToolStripMenuItem();
            gestionDeRespaldoToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblUsuarioSesion = new ToolStripStatusLabel();
            lblEstadoSesion = new ToolStripStatusLabel();
            pnlInicio = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblDescripcionInicio = new Label();
            lblTituloInicio = new Label();
            menuPrincipal.SuspendLayout();
            statusStrip1.SuspendLayout();
            pnlInicio.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuPrincipal
            // 
            menuPrincipal.BackColor = Color.FromArgb(24, 70, 138);
            menuPrincipal.ForeColor = Color.White;
            menuPrincipal.ImageScalingSize = new Size(20, 20);
            menuPrincipal.Items.AddRange(new ToolStripItem[] { mnuSesion, RecepcionistaToolStripMenuItem, vendedorBuffetToolStripMenuItem, encargadoDeCanchasToolStripMenuItem, dueñoToolStripMenuItem, mnuAdministrador });
            menuPrincipal.Location = new Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            // AirPadel style preview BEGIN
            menuPrincipal.BackColor = Color.FromArgb(24, 70, 138);
            menuPrincipal.ForeColor = Color.White;
            // AirPadel style preview END
            menuPrincipal.Size = new Size(800, 24);
            menuPrincipal.TabIndex = 0;
            menuPrincipal.Text = "menuStrip1";
            // 
            // mnuSesion
            // 
            mnuSesion.DropDownItems.AddRange(new ToolStripItem[] { reLoginToolStripMenuItem, cambiarClaveToolStripMenuItem, cambiarIdiomaToolStripMenuItem, cerrarSesionToolStripMenuItem });
            mnuSesion.Name = "mnuSesion";
            // AirPadel style preview BEGIN
            mnuSesion.BackColor = Color.FromArgb(24, 70, 138);
            mnuSesion.DropDownItems.AddRange(new ToolStripItem[] { reLoginToolStripMenuItem, cambiarClaveToolStripMenuItem, cambiarIdiomaToolStripMenuItem, cerrarSesionToolStripMenuItem });
            mnuSesion.ForeColor = Color.White;
            mnuSesion.Name = "mnuSesion";
            mnuSesion.Size = new Size(59, 20);
            mnuSesion.Text = "Usuario";
            // 
            // reLoginToolStripMenuItem
            // 
            reLoginToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            reLoginToolStripMenuItem.ForeColor = Color.White;
            reLoginToolStripMenuItem.Name = "reLoginToolStripMenuItem";
            reLoginToolStripMenuItem.Size = new Size(159, 22);
            reLoginToolStripMenuItem.Text = "Re-Login";
            reLoginToolStripMenuItem.Click += reLoginToolStripMenuItem_Click;
            // 
            // cambiarClaveToolStripMenuItem
            // 
            cambiarClaveToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            cambiarClaveToolStripMenuItem.ForeColor = Color.White;
            cambiarClaveToolStripMenuItem.Name = "cambiarClaveToolStripMenuItem";
            cambiarClaveToolStripMenuItem.Size = new Size(159, 22);
            cambiarClaveToolStripMenuItem.Text = "Cambiar Clave";
            cambiarClaveToolStripMenuItem.Click += cambiarClaveToolStripMenuItem_Click;
            // 
            // cambiarIdiomaToolStripMenuItem
            // 
            cambiarIdiomaToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            cambiarIdiomaToolStripMenuItem.ForeColor = Color.White;
            cambiarIdiomaToolStripMenuItem.Name = "cambiarIdiomaToolStripMenuItem";
            cambiarIdiomaToolStripMenuItem.Size = new Size(159, 22);
            cambiarIdiomaToolStripMenuItem.Text = "Cambiar Idioma";
            cambiarIdiomaToolStripMenuItem.Click += cambiarIdiomaToolStripMenuItem_Click;
            // 
            // cerrarSesionToolStripMenuItem
            // 
            cerrarSesionToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            cerrarSesionToolStripMenuItem.ForeColor = Color.White;
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesionToolStripMenuItem.Size = new Size(159, 22);
            cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            // 
            // RecepcionistaToolStripMenuItem
            // 
            RecepcionistaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { seleccionarTurnoToolStripMenuItem });
            RecepcionistaToolStripMenuItem.Name = "RecepcionistaToolStripMenuItem";
            // AirPadel style preview BEGIN
            RecepcionistaToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            RecepcionistaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { seleccionarTurnoToolStripMenuItem });
            RecepcionistaToolStripMenuItem.ForeColor = Color.White;
            RecepcionistaToolStripMenuItem.Name = "RecepcionistaToolStripMenuItem";
            RecepcionistaToolStripMenuItem.Size = new Size(92, 20);
            RecepcionistaToolStripMenuItem.Text = "Recepcionista";
            // 
            // seleccionarTurnoToolStripMenuItem
            // 
            seleccionarTurnoToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            seleccionarTurnoToolStripMenuItem.ForeColor = Color.White;
            seleccionarTurnoToolStripMenuItem.Name = "seleccionarTurnoToolStripMenuItem";
            seleccionarTurnoToolStripMenuItem.Size = new Size(180, 22);
            seleccionarTurnoToolStripMenuItem.Text = "Reservar Cancha";
            seleccionarTurnoToolStripMenuItem.Click += seleccionarTurnoToolStripMenuItem_Click;
            // 
            // vendedorBuffetToolStripMenuItem
            // 
            vendedorBuffetToolStripMenuItem.Name = "vendedorBuffetToolStripMenuItem";
            // AirPadel style preview BEGIN
            vendedorBuffetToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            vendedorBuffetToolStripMenuItem.ForeColor = Color.White;
            vendedorBuffetToolStripMenuItem.Name = "vendedorBuffetToolStripMenuItem";
            vendedorBuffetToolStripMenuItem.Size = new Size(104, 20);
            vendedorBuffetToolStripMenuItem.Text = "Vendedor Buffet";
            // 
            // encargadoDeCanchasToolStripMenuItem
            // 
            encargadoDeCanchasToolStripMenuItem.Name = "encargadoDeCanchasToolStripMenuItem";
            // AirPadel style preview BEGIN
            encargadoDeCanchasToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            encargadoDeCanchasToolStripMenuItem.ForeColor = Color.White;
            encargadoDeCanchasToolStripMenuItem.Name = "encargadoDeCanchasToolStripMenuItem";
            encargadoDeCanchasToolStripMenuItem.Size = new Size(139, 20);
            encargadoDeCanchasToolStripMenuItem.Text = "Encargado de Canchas";
            // 
            // dueñoToolStripMenuItem
            // 
            dueñoToolStripMenuItem.Name = "dueñoToolStripMenuItem";
            dueñoToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dueñoToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            dueñoToolStripMenuItem.ForeColor = Color.White;
            dueñoToolStripMenuItem.Size = new Size(54, 20);
            dueñoToolStripMenuItem.Text = "Dueño";
            // 
            // mnuAdministrador
            // 
            mnuAdministrador.DropDownItems.AddRange(new ToolStripItem[] { usuariosToolStripMenuItem, bitacoraEventosToolStripMenuItem, gestionarPerfilToolStripMenuItem, gestionDeRespaldoToolStripMenuItem });
            mnuAdministrador.Name = "mnuAdministrador";
            // AirPadel style preview BEGIN
            mnuAdministrador.BackColor = Color.FromArgb(24, 70, 138);
            mnuAdministrador.DropDownItems.AddRange(new ToolStripItem[] { usuariosToolStripMenuItem, bitacoraEventosToolStripMenuItem, gestionarPerfilToolStripMenuItem, gestionDeRespaldoToolStripMenuItem });
            mnuAdministrador.ForeColor = Color.White;
            mnuAdministrador.Name = "mnuAdministrador";
            mnuAdministrador.Size = new Size(95, 20);
            mnuAdministrador.Text = "Administrador";
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            usuariosToolStripMenuItem.ForeColor = Color.White;
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(219, 22);
            usuariosToolStripMenuItem.Text = "Gestionar Usuarios";
            usuariosToolStripMenuItem.Click += usuariosToolStripMenuItem_Click;
            // 
            // bitacoraEventosToolStripMenuItem
            // 
            bitacoraEventosToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            bitacoraEventosToolStripMenuItem.ForeColor = Color.White;
            bitacoraEventosToolStripMenuItem.Name = "bitacoraEventosToolStripMenuItem";
            bitacoraEventosToolStripMenuItem.Size = new Size(219, 22);
            bitacoraEventosToolStripMenuItem.Text = "Auditar Bitacora de Eventos";
            bitacoraEventosToolStripMenuItem.Click += bitacoraEventosToolStripMenuItem_Click;
            // 
            // gestionarPerfilToolStripMenuItem
            // 
            gestionarPerfilToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            gestionarPerfilToolStripMenuItem.ForeColor = Color.White;
            gestionarPerfilToolStripMenuItem.Name = "gestionarPerfilToolStripMenuItem";
            gestionarPerfilToolStripMenuItem.Size = new Size(219, 22);
            gestionarPerfilToolStripMenuItem.Text = "Gestion de Roles y Familias";
            gestionarPerfilToolStripMenuItem.Click += gestionarPerfilToolStripMenuItem_Click;
            // 
            // gestionDeRespaldoToolStripMenuItem
            // 
            gestionDeRespaldoToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            gestionDeRespaldoToolStripMenuItem.ForeColor = Color.White;
            gestionDeRespaldoToolStripMenuItem.Name = "gestionDeRespaldoToolStripMenuItem";
            gestionDeRespaldoToolStripMenuItem.Size = new Size(219, 22);
            gestionDeRespaldoToolStripMenuItem.Text = "Gestion de Respaldo";
            gestionDeRespaldoToolStripMenuItem.Click += gestionDeRespaldoToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(24, 70, 138);
            statusStrip1.ForeColor = Color.White;
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblUsuarioSesion, lblEstadoSesion });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblUsuarioSesion
            // 
            lblUsuarioSesion.ForeColor = Color.White;
            lblUsuarioSesion.Name = "lblUsuarioSesion";
            lblUsuarioSesion.Size = new Size(50, 17);
            lblUsuarioSesion.Text = "Usuario:";
            lblUsuarioSesion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEstadoSesion
            // 
            lblEstadoSesion.ForeColor = Color.White;
            lblEstadoSesion.Name = "lblEstadoSesion";
            lblEstadoSesion.Size = new Size(735, 17);
            lblEstadoSesion.Spring = true;
            lblEstadoSesion.Text = "Sesión activa";
            lblEstadoSesion.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlInicio
            // 
            pnlInicio.BackColor = Color.FromArgb(24, 70, 138);
            pnlInicio.Controls.Add(tableLayoutPanel1);
            pnlInicio.Dock = DockStyle.Fill;
            pnlInicio.Location = new Point(0, 24);
            pnlInicio.Name = "pnlInicio";
            pnlInicio.Size = new Size(800, 404);
            pnlInicio.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(24, 70, 138);
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblDescripcionInicio, 0, 2);
            tableLayoutPanel1.Controls.Add(lblTituloInicio, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.Size = new Size(800, 404);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // lblDescripcionInicio
            // 
            lblDescripcionInicio.Dock = DockStyle.Fill;
            lblDescripcionInicio.Font = new Font("Segoe UI", 11F);
            lblDescripcionInicio.ForeColor = Color.White;
            lblDescripcionInicio.Location = new Point(3, 181);
            lblDescripcionInicio.Name = "lblDescripcionInicio";
            lblDescripcionInicio.Size = new Size(794, 32);
            lblDescripcionInicio.TabIndex = 1;
            lblDescripcionInicio.Text = "Seleccione un módulo desde el menú superior para comenzar.";
            lblDescripcionInicio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTituloInicio
            // 
            lblTituloInicio.Dock = DockStyle.Fill;
            lblTituloInicio.Font = new Font("Segoe UI", 22F);
            lblTituloInicio.ForeColor = Color.White;
            lblTituloInicio.Location = new Point(3, 121);
            lblTituloInicio.Name = "lblTituloInicio";
            lblTituloInicio.Size = new Size(794, 60);
            lblTituloInicio.TabIndex = 0;
            lblTituloInicio.Text = "Bienvenido a Padelgest";
            lblTituloInicio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(pnlInicio);
            Controls.Add(statusStrip1);
            Controls.Add(menuPrincipal);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuPrincipal;
            Name = "frmMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PadelGest - Menú Principal";
            WindowState = FormWindowState.Maximized;
            FormClosed += frmMenuPrincipal_FormClosed;
            Load += frmMenuPrincipal_Load;
            menuPrincipal.ResumeLayout(false);
            menuPrincipal.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            pnlInicio.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private sealed class AirPadelMenuRenderer : ToolStripProfessionalRenderer
        {
            public AirPadelMenuRenderer()
                : base(new AirPadelColorTable())
            {
            }

            protected override void OnRenderItemText(
                ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor =
                    e.Item.Selected || e.Item.Pressed
                        ? Color.FromArgb(18, 18, 18)
                        : Color.White;

                base.OnRenderItemText(e);
            }
        }

        private sealed class AirPadelColorTable : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin =>
                Color.FromArgb(24, 70, 138);

            public override Color MenuStripGradientEnd =>
                Color.FromArgb(24, 70, 138);

            public override Color MenuItemSelected =>
                Color.FromArgb(214, 246, 36);

            public override Color MenuItemSelectedGradientBegin =>
                Color.FromArgb(214, 246, 36);

            public override Color MenuItemSelectedGradientEnd =>
                Color.FromArgb(214, 246, 36);

            public override Color MenuItemPressedGradientBegin =>
                Color.FromArgb(214, 246, 36);

            public override Color MenuItemPressedGradientMiddle =>
                Color.FromArgb(214, 246, 36);

            public override Color MenuItemPressedGradientEnd =>
                Color.FromArgb(214, 246, 36);

            public override Color ToolStripDropDownBackground =>
                Color.FromArgb(24, 70, 138);

            public override Color ImageMarginGradientBegin =>
                Color.FromArgb(24, 70, 138);

            public override Color ImageMarginGradientMiddle =>
                Color.FromArgb(24, 70, 138);

            public override Color ImageMarginGradientEnd =>
                Color.FromArgb(24, 70, 138);

            public override Color MenuBorder =>
                Color.FromArgb(64, 103, 166);

            public override Color MenuItemBorder =>
                Color.FromArgb(214, 246, 36);
        }

        #endregion

        private MenuStrip menuPrincipal;
        private ToolStripMenuItem mnuSesion;
        private ToolStripMenuItem RecepcionistaToolStripMenuItem;
        private ToolStripMenuItem seleccionarTurnoToolStripMenuItem;
        private ToolStripMenuItem mnuAdministrador;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblUsuarioSesion;
        private ToolStripStatusLabel lblEstadoSesion;
        private Panel pnlInicio;
        private Label lblDescripcionInicio;
        private Label lblTituloInicio;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripMenuItem reLoginToolStripMenuItem;
        private ToolStripMenuItem cambiarClaveToolStripMenuItem;
        private ToolStripMenuItem cambiarIdiomaToolStripMenuItem;
        private ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private ToolStripMenuItem usuariosToolStripMenuItem;
        private ToolStripMenuItem bitacoraEventosToolStripMenuItem;
        private ToolStripMenuItem gestionarPerfilToolStripMenuItem;
        private ToolStripMenuItem encargadoDeCanchasToolStripMenuItem;
        private ToolStripMenuItem dueñoToolStripMenuItem;
        private ToolStripMenuItem gestionDeRespaldoToolStripMenuItem;
        private ToolStripMenuItem vendedorBuffetToolStripMenuItem;
    }
}
