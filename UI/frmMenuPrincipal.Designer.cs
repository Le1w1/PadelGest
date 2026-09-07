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
            menuPrincipal.BackColor = Color.LightSkyBlue;
            menuPrincipal.ImageScalingSize = new Size(20, 20);
            menuPrincipal.Items.AddRange(new ToolStripItem[] { mnuSesion, RecepcionistaToolStripMenuItem, vendedorBuffetToolStripMenuItem, encargadoDeCanchasToolStripMenuItem, dueñoToolStripMenuItem, mnuAdministrador });
            menuPrincipal.Location = new Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Renderer = EstiloVisual.CrearMenuRenderer();
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
            mnuSesion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            // AirPadel style preview BEGIN
            mnuSesion.BackColor = Color.FromArgb(24, 70, 138);
            mnuSesion.ForeColor = Color.White;
            // AirPadel style preview END
            mnuSesion.Size = new Size(59, 20);
            mnuSesion.Text = "Usuario";
            // 
            // reLoginToolStripMenuItem
            // 
            reLoginToolStripMenuItem.Name = "reLoginToolStripMenuItem";
            // AirPadel style preview BEGIN
            reLoginToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            reLoginToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            reLoginToolStripMenuItem.Size = new Size(159, 22);
            reLoginToolStripMenuItem.Text = "Re-Login";
            reLoginToolStripMenuItem.Click += reLoginToolStripMenuItem_Click;
            // 
            // cambiarClaveToolStripMenuItem
            // 
            cambiarClaveToolStripMenuItem.Name = "cambiarClaveToolStripMenuItem";
            // AirPadel style preview BEGIN
            cambiarClaveToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            cambiarClaveToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            cambiarClaveToolStripMenuItem.Size = new Size(159, 22);
            cambiarClaveToolStripMenuItem.Text = "Cambiar Clave";
            cambiarClaveToolStripMenuItem.Click += cambiarClaveToolStripMenuItem_Click;
            // 
            // cambiarIdiomaToolStripMenuItem
            // 
            cambiarIdiomaToolStripMenuItem.Name = "cambiarIdiomaToolStripMenuItem";
            // AirPadel style preview BEGIN
            cambiarIdiomaToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            cambiarIdiomaToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            cambiarIdiomaToolStripMenuItem.Size = new Size(159, 22);
            cambiarIdiomaToolStripMenuItem.Text = "Cambiar Idioma";
            cambiarIdiomaToolStripMenuItem.Click += cambiarIdiomaToolStripMenuItem_Click;
            // 
            // cerrarSesionToolStripMenuItem
            // 
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            // AirPadel style preview BEGIN
            cerrarSesionToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            cerrarSesionToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            cerrarSesionToolStripMenuItem.Size = new Size(159, 22);
            cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            // 
            // RecepcionistaToolStripMenuItem
            // 
            RecepcionistaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { seleccionarTurnoToolStripMenuItem });
            RecepcionistaToolStripMenuItem.Name = "RecepcionistaToolStripMenuItem";
            RecepcionistaToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            // AirPadel style preview BEGIN
            RecepcionistaToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            RecepcionistaToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            RecepcionistaToolStripMenuItem.Size = new Size(92, 20);
            RecepcionistaToolStripMenuItem.Text = "Recepcionista";
            // 
            // seleccionarTurnoToolStripMenuItem
            // 
            seleccionarTurnoToolStripMenuItem.Name = "seleccionarTurnoToolStripMenuItem";
            // AirPadel style preview BEGIN
            seleccionarTurnoToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            seleccionarTurnoToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            seleccionarTurnoToolStripMenuItem.Size = new Size(180, 22);
            seleccionarTurnoToolStripMenuItem.Text = "Seleccionar Turno";
            seleccionarTurnoToolStripMenuItem.Click += seleccionarTurnoToolStripMenuItem_Click;
            // 
            // vendedorBuffetToolStripMenuItem
            // 
            vendedorBuffetToolStripMenuItem.Name = "vendedorBuffetToolStripMenuItem";
            vendedorBuffetToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            // AirPadel style preview BEGIN
            vendedorBuffetToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            vendedorBuffetToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            vendedorBuffetToolStripMenuItem.Size = new Size(104, 20);
            vendedorBuffetToolStripMenuItem.Text = "Vendedor Buffet";
            // 
            // encargadoDeCanchasToolStripMenuItem
            // 
            encargadoDeCanchasToolStripMenuItem.Name = "encargadoDeCanchasToolStripMenuItem";
            encargadoDeCanchasToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            // AirPadel style preview BEGIN
            encargadoDeCanchasToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            encargadoDeCanchasToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
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
            mnuAdministrador.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            // AirPadel style preview BEGIN
            mnuAdministrador.BackColor = Color.FromArgb(24, 70, 138);
            mnuAdministrador.ForeColor = Color.White;
            // AirPadel style preview END
            mnuAdministrador.Size = new Size(95, 20);
            mnuAdministrador.Text = "Administrador";
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            // AirPadel style preview BEGIN
            usuariosToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            usuariosToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            usuariosToolStripMenuItem.Size = new Size(219, 22);
            usuariosToolStripMenuItem.Text = "Gestionar Usuarios";
            usuariosToolStripMenuItem.Click += usuariosToolStripMenuItem_Click;
            // 
            // bitacoraEventosToolStripMenuItem
            // 
            bitacoraEventosToolStripMenuItem.Name = "bitacoraEventosToolStripMenuItem";
            // AirPadel style preview BEGIN
            bitacoraEventosToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            bitacoraEventosToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            bitacoraEventosToolStripMenuItem.Size = new Size(219, 22);
            bitacoraEventosToolStripMenuItem.Text = "Auditar Bitacora de Eventos";
            bitacoraEventosToolStripMenuItem.Click += bitacoraEventosToolStripMenuItem_Click;
            // 
            // gestionarPerfilToolStripMenuItem
            // 
            gestionarPerfilToolStripMenuItem.Name = "gestionarPerfilToolStripMenuItem";
            // AirPadel style preview BEGIN
            gestionarPerfilToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            gestionarPerfilToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            gestionarPerfilToolStripMenuItem.Size = new Size(219, 22);
            gestionarPerfilToolStripMenuItem.Text = "Gestion de Roles y Familias";
            gestionarPerfilToolStripMenuItem.Click += gestionarPerfilToolStripMenuItem_Click;
            // 
            // gestionDeRespaldoToolStripMenuItem
            // 
            gestionDeRespaldoToolStripMenuItem.Name = "gestionDeRespaldoToolStripMenuItem";
            // AirPadel style preview BEGIN
            gestionDeRespaldoToolStripMenuItem.BackColor = Color.FromArgb(24, 70, 138);
            gestionDeRespaldoToolStripMenuItem.ForeColor = Color.White;
            // AirPadel style preview END
            gestionDeRespaldoToolStripMenuItem.Size = new Size(219, 22);
            gestionDeRespaldoToolStripMenuItem.Text = "Gestion de Respaldo";
            gestionDeRespaldoToolStripMenuItem.Click += gestionDeRespaldoToolStripMenuItem_Click;
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblUsuarioSesion, lblEstadoSesion });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            // AirPadel style preview BEGIN
            statusStrip1.BackColor = Color.FromArgb(24, 70, 138);
            statusStrip1.ForeColor = Color.White;
            // AirPadel style preview END
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblUsuarioSesion
            // 
            lblUsuarioSesion.Name = "lblUsuarioSesion";
            // AirPadel style preview BEGIN
            lblUsuarioSesion.ForeColor = Color.White;
            // AirPadel style preview END
            lblUsuarioSesion.Size = new Size(50, 17);
            lblUsuarioSesion.Text = "Usuario:";
            lblUsuarioSesion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEstadoSesion
            // 
            lblEstadoSesion.Name = "lblEstadoSesion";
            // AirPadel style preview BEGIN
            lblEstadoSesion.ForeColor = Color.White;
            // AirPadel style preview END
            lblEstadoSesion.Size = new Size(735, 17);
            lblEstadoSesion.Spring = true;
            lblEstadoSesion.Text = "Sesión activa";
            lblEstadoSesion.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlInicio
            // 
            pnlInicio.Controls.Add(tableLayoutPanel1);
            pnlInicio.Dock = DockStyle.Fill;
            pnlInicio.Location = new Point(0, 24);
            pnlInicio.Name = "pnlInicio";
            // AirPadel style preview BEGIN
            pnlInicio.BackColor = Color.FromArgb(24, 70, 138);
            // AirPadel style preview END
            pnlInicio.Size = new Size(800, 404);
            pnlInicio.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.PowderBlue;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblDescripcionInicio, 0, 2);
            tableLayoutPanel1.Controls.Add(lblTituloInicio, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // AirPadel style preview BEGIN
            tableLayoutPanel1.BackColor = Color.FromArgb(24, 70, 138);
            // AirPadel style preview END
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
            lblDescripcionInicio.Location = new Point(3, 181);
            lblDescripcionInicio.Name = "lblDescripcionInicio";
            // AirPadel style preview BEGIN
            lblDescripcionInicio.ForeColor = Color.White;
            // AirPadel style preview END
            lblDescripcionInicio.Size = new Size(794, 32);
            lblDescripcionInicio.TabIndex = 1;
            lblDescripcionInicio.Text = "Seleccione un módulo desde el menú superior para comenzar.";
            lblDescripcionInicio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTituloInicio
            // 
            lblTituloInicio.Dock = DockStyle.Fill;
            lblTituloInicio.Font = new Font("Segoe UI", 22F);
            lblTituloInicio.Location = new Point(3, 121);
            lblTituloInicio.Name = "lblTituloInicio";
            // AirPadel style preview BEGIN
            lblTituloInicio.ForeColor = Color.White;
            // AirPadel style preview END
            lblTituloInicio.Size = new Size(794, 60);
            lblTituloInicio.TabIndex = 0;
            lblTituloInicio.Text = "Bienvenido a Padelgest";
            lblTituloInicio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(pnlInicio);
            Controls.Add(statusStrip1);
            Controls.Add(menuPrincipal);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuPrincipal;
            Name = "frmMenuPrincipal";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
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
