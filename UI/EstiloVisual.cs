using System.Drawing;

namespace UI
{
    public static class EstiloVisual
    {
        private static readonly Color Fondo =
            Color.FromArgb(245, 247, 250);

        private static readonly Color Superficie =
            Color.White;

        private static readonly Color Primario =
            Color.FromArgb(37, 99, 165);

        private static readonly Color PrimarioHover =
            Color.FromArgb(31, 83, 139);

        private static readonly Color Secundario =
            Color.FromArgb(232, 236, 241);

        private static readonly Color Texto =
            Color.FromArgb(35, 42, 52);

        private static readonly Color Borde =
            Color.FromArgb(215, 220, 226);

        private static readonly Color FilaAlterna =
            Color.FromArgb(248, 249, 251);

        private static readonly Color Seleccion =
            Color.FromArgb(214, 228, 244);

        public static void Aplicar(Form form)
        {
            form.Font = new Font("Segoe UI", 9.5F);
            form.BackColor = Fondo;

            AplicarControles(form.Controls);
        }

        private static void AplicarControles(
            Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                switch (control)
                {
                    case GroupBox grupo:
                        EstilizarGrupo(grupo);
                        break;

                    case Button boton:
                        EstilizarBoton(boton);
                        break;

                    case DataGridView grilla:
                        EstilizarGrilla(grilla);
                        break;

                    case TextBox caja:
                        EstilizarCajaTexto(caja);
                        break;

                    case ComboBox combo:
                        combo.FlatStyle = FlatStyle.Flat;
                        combo.BackColor = Superficie;
                        combo.ForeColor = Texto;
                        break;

                    case Label etiqueta:
                        EstilizarEtiqueta(etiqueta);
                        break;
                }

                if (control.HasChildren)
                {
                    AplicarControles(control.Controls);
                }
            }
        }

        private static void EstilizarGrupo(GroupBox grupo)
        {
            grupo.BackColor = Superficie;
            grupo.ForeColor = Texto;
            grupo.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold);
        }

        private static void EstilizarEtiqueta(Label etiqueta)
        {
            etiqueta.ForeColor = Texto;

            if (etiqueta.Name.Equals(
                "lblTitulo",
                StringComparison.OrdinalIgnoreCase))
            {
                etiqueta.Font = new Font(
                    "Segoe UI",
                    18F,
                    FontStyle.Bold);
            }
        }

        private static void EstilizarCajaTexto(TextBox caja)
        {
            caja.BorderStyle = BorderStyle.FixedSingle;
            caja.BackColor =
                caja.ReadOnly
                    ? Color.FromArgb(247, 248, 250)
                    : Superficie;

            caja.ForeColor = Texto;
        }

        private static void EstilizarBoton(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Cursor = Cursors.Hand;
            boton.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Regular);

            if (EsBotonPrincipal(boton.Name))
            {
                boton.BackColor = Primario;
                boton.ForeColor = Color.White;
                boton.FlatAppearance.MouseOverBackColor =
                    PrimarioHover;
                boton.FlatAppearance.MouseDownBackColor =
                    PrimarioHover;
            }
            else
            {
                boton.BackColor = Secundario;
                boton.ForeColor = Texto;
                boton.FlatAppearance.MouseOverBackColor =
                    Color.FromArgb(220, 225, 231);
                boton.FlatAppearance.MouseDownBackColor =
                    Color.FromArgb(210, 216, 223);
            }
        }

        private static bool EsBotonPrincipal(string nombre)
        {
            return nombre is
                "btnBuscar" or
                "btnSeleccionar" or
                "btnContinuar" or
                "btnCobrarReserva" or
                "btnRegistrarReserva" or
                "btnImprimir";
        }

        private static void EstilizarGrilla(DataGridView grilla)
        {
            grilla.BorderStyle = BorderStyle.None;
            grilla.BackgroundColor = Superficie;
            grilla.GridColor = Borde;
            grilla.EnableHeadersVisualStyles = false;
            grilla.RowHeadersVisible = false;
            grilla.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;
            grilla.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersHeight = 36;
            grilla.RowTemplate.Height = 30;

            grilla.ColumnHeadersDefaultCellStyle.BackColor =
                Primario;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            grilla.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold);
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Primario;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            grilla.DefaultCellStyle.BackColor = Superficie;
            grilla.DefaultCellStyle.ForeColor = Texto;
            grilla.DefaultCellStyle.SelectionBackColor =
                Seleccion;
            grilla.DefaultCellStyle.SelectionForeColor =
                Texto;
            grilla.DefaultCellStyle.Padding =
                new Padding(4, 2, 4, 2);

            grilla.AlternatingRowsDefaultCellStyle.BackColor =
                FilaAlterna;
        }
    }
}
