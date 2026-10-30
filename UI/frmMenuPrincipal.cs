using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Servicios;

namespace UI
{
    public partial class frmMenuPrincipal : Form, IObservadorIdioma
    {
        private readonly UsuarioBLL _usuarioBLL = new UsuarioBLL();

        public frmMenuPrincipal()
        {
            InitializeComponent();
            // Traducir antes de que el form se cargue.
            ActualizarIdioma();
            // El menu se muestra detras del Login, pero no se puede usar sin autenticar.
            menuPrincipal.Enabled = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SolicitarInicioSesion();
        }

        private void SolicitarInicioSesion()
        {
            menuPrincipal.Enabled = false;
            using (frmLogin login = new frmLogin())
            {
                if (login.ShowDialog(this) != DialogResult.OK)
                {
                    Close();
                    return;
                }
            }

            CargarDatosSesion();
            AplicarPermisos();
            menuPrincipal.Enabled = true;
        }


        // Suscripcion al Observer de idioma y carga de datos de sesion al cargar el form.
        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            // Suscripcion al Observer de idioma.
            SM.Instancia.Suscribir(this);
            CargarDatosSesion();
            AplicarPermisos();
        }

   
        
        /// Muestra exclusivamente las opciones para las que el usuario tiene permiso.
        /// Los menus padre se ocultan cuando no hay opciones visibles.
        private void AplicarPermisos()
        {
            var sm = SM.Instancia;

            // Calcular permisos antes de asignar Visible: la visibilidad efectiva
            // de un item hijo depende tambien de la visibilidad de su menu padre.
            bool buffet = sm.TienePermiso("BUF_VENDER");
            bool reservas = sm.TienePermiso("RES_CREAR");
            bool usuarios = sm.TienePermiso("USR_LISTAR") ||
                sm.TienePermiso("USR_CREAR") || sm.TienePermiso("USR_MODIFICAR");
            bool bitacora = sm.TienePermiso("BIT_AUDITAR");
            bool perfiles = sm.TienePermiso("ROL_GESTIONAR") ||
                sm.TienePermiso("FAM_GESTIONAR");
            bool respaldos = sm.TienePermiso("BAK_GESTIONAR");

            mnuSesion.Visible = true;
            reLoginToolStripMenuItem.Visible = sm.TienePermiso("SES_RELOGIN");
            cambiarClaveToolStripMenuItem.Visible = sm.TienePermiso("SES_CAMBIAR_CLAVE");
            cambiarIdiomaToolStripMenuItem.Visible = sm.TienePermiso("SES_CAMBIAR_IDIOMA");
            cerrarSesionToolStripMenuItem.Visible = true;

            vendedorBuffetToolStripMenuItem.Visible = buffet;
            generarCarritoToolStripMenuItem.Visible = buffet;
            gestionarProductosToolStripMenuItem.Visible = buffet;

            RecepcionistaToolStripMenuItem.Visible = reservas;
            generarReservaToolStripMenuItem.Visible = reservas;

            encargadoDeCanchasToolStripMenuItem.Visible = false;
            dueñoToolStripMenuItem.Visible = false;

            mnuAdministrador.Visible = usuarios || bitacora || perfiles || respaldos;
            usuariosToolStripMenuItem.Visible = usuarios;
            bitacoraEventosToolStripMenuItem.Visible = bitacora;
            gestionarPerfilToolStripMenuItem.Visible = perfiles;
            gestionDeRespaldoToolStripMenuItem.Visible = respaldos;
        }


        private void frmMenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

 
        /// Implementacion del Observer: el SM nos llama cuando cambia el idioma. Recorre todos los textos del form y aplica las traducciones.
        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            this.Text = t.Traducir("frmMenuPrincipal.Title");

            // Menu Usuario
            mnuSesion.Text = t.Traducir("frmMenuPrincipal.MenuUsuario");
            reLoginToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuReLogin");
            cambiarClaveToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuCambiarClave");
            cambiarIdiomaToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuCambiarIdioma");
            cerrarSesionToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuCerrarSesion");

            // Menus principales
            RecepcionistaToolStripMenuItem.Text =t.Traducir("frmMenuPrincipal.MenuRecepcionista");
            generarReservaToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuGenerarReserva");
            encargadoDeCanchasToolStripMenuItem.Text =t.Traducir("frmMenuPrincipal.MenuEncargadoCanchas");
            dueñoToolStripMenuItem.Text =t.Traducir("frmMenuPrincipal.MenuDueño");
            vendedorBuffetToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuVendedorBuffet");
            generarCarritoToolStripMenuItem.Text = t.Traducir("frmGenerarCarrito.Menu");
            gestionarProductosToolStripMenuItem.Text = t.Traducir("frmGestionProductos.Menu");

            // Menu Administrador
            mnuAdministrador.Text = t.Traducir("frmMenuPrincipal.MenuAdministrador");
            usuariosToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuUsuarios");
            bitacoraEventosToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuBitacora");
            gestionarPerfilToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuGestionPerfiles");
            gestionDeRespaldoToolStripMenuItem.Text = t.Traducir("frmMenuPrincipal.MenuGestionRespaldo");
            // Panel central
            lblTituloInicio.Text = t.Traducir("frmMenuPrincipal.Titulo");
            lblDescripcionInicio.Text = t.Traducir("frmMenuPrincipal.Descripcion");

            // Status bar (depende de la sesion)
            CargarDatosSesion();
        }

        // Carga los datos de la sesion en la barra de estado. Si no hay sesion activa, muestra "Sin Sesion".
        private void CargarDatosSesion()
        {
            var t = Traductor.Instancia;

            if (SM.Instancia.HaySesionActiva())
            {
                Usuario usuario = SM.Instancia.UsuarioActual;

                lblUsuarioSesion.Text = t.Traducir("frmMenuPrincipal.UsuarioLabel") + " " + usuario.NombreUsuario;
                lblEstadoSesion.Text = t.Traducir("frmMenuPrincipal.SesionActiva");
            }
            else
            {
                lblUsuarioSesion.Text = t.Traducir("frmMenuPrincipal.UsuarioLabelSinSesion");
                lblEstadoSesion.Text = t.Traducir("frmMenuPrincipal.SinSesion");
            }
        }


        /// Evento del menu "Cerrar Sesion": pregunta al usuario si desea cerrar la sesion, y si confirma, llama al BLL para cerrar la sesion y cierra el form.
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = Traductor.Instancia;

            DialogResult respuesta = MessageBox.Show(t.Traducir("frmMenuPrincipal.ConfirmarCerrarSesion"), t.Traducir("frmMenuPrincipal.CerrarSesionTitulo"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    _usuarioBLL.Logout();
                    CargarDatosSesion();
                    SolicitarInicioSesion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "PadelGest", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        #region "MENUs"
        private void reLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin formSesion = new frmLogin(true);
            formSesion.ShowDialog();
        }

        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCambiarClave cambiarClave = new frmCambiarClave();
            cambiarClave.ShowDialog();
        }

        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCambiarIdioma cambiarIdioma = new frmCambiarIdioma();
            cambiarIdioma.ShowDialog();
        }

        private void generarCarritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmGenerarFactura formCarrito = new frmGenerarFactura();
            formCarrito.ShowDialog(this);
        }

        private void gestionarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmGestionProductos gestion = new frmGestionProductos();
            gestion.ShowDialog(this);
        }

        private void generarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmGenerarReserva formGenerarReserva = new frmGenerarReserva();
            formGenerarReserva.ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionDeUsuario formAdministrador = new frmGestionDeUsuario();
            formAdministrador.ShowDialog();
        }

        private void bitacoraEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuditarEventos formAuditarEventos = new frmAuditarEventos();
            formAuditarEventos.ShowDialog();
        }

        private void gestionarPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionarRolesYFamilias formGestionar = new frmGestionarRolesYFamilias();
            formGestionar.ShowDialog();
        }

        private void gestionDeRespaldoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionarRespaldo formGestionarRespaldo = new frmGestionarRespaldo();
            formGestionarRespaldo.ShowDialog();
        }
        #endregion

    }
}
