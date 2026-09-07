using System.Drawing;

namespace UI
{
    public static class EstiloVisual
    {
        private static readonly Color Fondo = Color.FromArgb(14, 43, 92);
        private static readonly Color Superficie = Color.FromArgb(24, 70, 138);
        private static readonly Color SuperficieHover = Color.FromArgb(31, 83, 156);
        private static readonly Color Primario = Color.FromArgb(214, 246, 36);
        private static readonly Color PrimarioHover = Color.FromArgb(192, 224, 29);
        private static readonly Color TextoClaro = Color.White;
        private static readonly Color TextoOscuro = Color.FromArgb(18, 18, 18);
        private static readonly Color Borde = Color.FromArgb(64, 103, 166);
        private static readonly Color CajaTexto = Color.White;
        private static readonly Color CajaSoloLectura = Color.FromArgb(236, 241, 248);
        private static readonly Color FilaAlterna = Color.FromArgb(244, 247, 251);
        private static readonly Color Seleccion = Color.FromArgb(211, 230, 248);

        public static void Aplicar(Form form)
        {
            form.Font = new Font("Segoe UI", 9.5F);
            form.BackColor = Fondo;
            form.ForeColor = TextoClaro;
            AplicarControles(form.Controls);
        }

        private static void AplicarControles(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                switch (control)
                {
                    case GroupBox grupo:
                        grupo.BackColor = Superficie;
                        grupo.ForeColor = TextoClaro;
                        grupo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                        break;
                    case Button boton:
                        EstilizarBoton(boton);
                        break;
                    case DataGridView grilla:
                        EstilizarGrilla(grilla);
                        break;
                    case TextBox caja:
                        caja.BorderStyle = BorderStyle.FixedSingle;
                        caja.BackColor = caja.ReadOnly ? CajaSoloLectura : CajaTexto;
                        caja.ForeColor = TextoOscuro;
                        break;
                    case ComboBox combo:
                        combo.FlatStyle = FlatStyle.Flat;
                        combo.BackColor = CajaTexto;
                        combo.ForeColor = TextoOscuro;
                        break;
                    case DateTimePicker fecha:
                        fecha.CalendarForeColor = TextoOscuro;
                        fecha.CalendarMonthBackground = CajaTexto;
                        fecha.CalendarTitleBackColor = Superficie;
                        fecha.CalendarTitleForeColor = TextoClaro;
                        break;
                    case NumericUpDown numero:
                        numero.BackColor = CajaTexto;
                        numero.ForeColor = TextoOscuro;
                        break;
                    case CheckedListBox lista:
                        lista.BackColor = CajaTexto;
                        lista.ForeColor = TextoOscuro;
                        lista.BorderStyle = BorderStyle.FixedSingle;
                        break;
                    case TreeView arbol:
                        arbol.BackColor = CajaTexto;
                        arbol.ForeColor = TextoOscuro;
                        arbol.BorderStyle = BorderStyle.FixedSingle;
                        break;
                    case CheckBox check:
                        check.ForeColor = TextoClaro;
                        break;
                    case RadioButton radio:
                        radio.ForeColor = TextoClaro;
                        break;
                    case MenuStrip menu:
                        menu.BackColor = Superficie;
                        menu.ForeColor = TextoClaro;
                        menu.Renderer = CrearMenuRenderer();
                        EstilizarItemsMenu(menu.Items);
                        break;
                    case StatusStrip status:
                        status.BackColor = Superficie;
                        status.ForeColor = TextoClaro;
                        break;
                    case TableLayoutPanel tabla:
                        tabla.BackColor = Superficie;
                        break;
                    case Panel panel:
                        panel.BackColor = Superficie;
                        break;
                    case Label etiqueta:
                        etiqueta.ForeColor =etiqueta.Name is "lblTitulo" or "lblMensaje"? Primario: TextoClaro;
                        if (etiqueta.Name == "lblTitulo")
                            etiqueta.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
                        break;
                }

                if (control.HasChildren)
                    AplicarControles(control.Controls);
            }
        }

        public static ToolStripRenderer CrearMenuRenderer()
        {
            return new AirPadelMenuRenderer();
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
                        ? TextoOscuro
                        : TextoClaro;

                base.OnRenderItemText(e);
            }
        }

        private sealed class AirPadelColorTable : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Superficie;
            public override Color MenuStripGradientEnd => Superficie;

            public override Color MenuItemSelected => Primario;
            public override Color MenuItemSelectedGradientBegin => Primario;
            public override Color MenuItemSelectedGradientEnd => Primario;

            public override Color MenuItemPressedGradientBegin => Primario;
            public override Color MenuItemPressedGradientMiddle => Primario;
            public override Color MenuItemPressedGradientEnd => Primario;

            public override Color ToolStripDropDownBackground => Superficie;

            public override Color ImageMarginGradientBegin => Superficie;
            public override Color ImageMarginGradientMiddle => Superficie;
            public override Color ImageMarginGradientEnd => Superficie;

            public override Color MenuBorder => Borde;
            public override Color MenuItemBorder => Primario;
        }

        private static void EstilizarItemsMenu(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.BackColor = Superficie;
                item.ForeColor = TextoClaro;

                if (item is ToolStripMenuItem menu && menu.DropDownItems.Count > 0)
                    EstilizarItemsMenu(menu.DropDownItems);
            }
        }

        private static void EstilizarBoton(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.Cursor = Cursors.Hand;
            boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            boton.UseVisualStyleBackColor = false;

            if (EsBotonPrincipal(boton.Name))
            {
                boton.FlatAppearance.BorderSize = 0;
                boton.BackColor = Primario;
                boton.ForeColor = TextoOscuro;
                boton.FlatAppearance.MouseOverBackColor = PrimarioHover;
                boton.FlatAppearance.MouseDownBackColor = PrimarioHover;
            }
            else
            {
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = Borde;
                boton.BackColor = Superficie;
                boton.ForeColor = TextoClaro;
                boton.FlatAppearance.MouseOverBackColor = SuperficieHover;
                boton.FlatAppearance.MouseDownBackColor = SuperficieHover;
            }
        }

        private static bool EsBotonPrincipal(string nombre)
        {
            return nombre is
                "btnBuscar" or "btnSeleccionar" or "btnContinuar" or
                "btnCobrarReserva" or "btnRegistrarReserva" or "btnRegistrar" or
                "btnGuardar" or "btnAceptar" or "btnConfirmar" or "btnIngresar" or
                "btnImprimir" or "btnRealizarBackup" or "btnRestaurar";
        }

        private static void EstilizarGrilla(DataGridView grilla)
        {
            grilla.BorderStyle = BorderStyle.None;
            grilla.BackgroundColor = CajaTexto;
            grilla.GridColor = Borde;
            grilla.EnableHeadersVisualStyles = false;
            grilla.RowHeadersVisible = false;
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersHeight = 36;
            grilla.RowTemplate.Height = 30;

            grilla.ColumnHeadersDefaultCellStyle.BackColor = Primario;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = TextoOscuro;
            grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Primario;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = TextoOscuro;

            grilla.DefaultCellStyle.BackColor = CajaTexto;
            grilla.DefaultCellStyle.ForeColor = TextoOscuro;
            grilla.DefaultCellStyle.SelectionBackColor = Seleccion;
            grilla.DefaultCellStyle.SelectionForeColor = TextoOscuro;
            grilla.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
        }
    }
}
