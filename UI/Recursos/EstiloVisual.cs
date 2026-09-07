using System.Drawing;

namespace UI.Recursos
{
    public static class EstiloVisual
    {
        // Paleta inspirada en la identidad visual enviada:
        // azul profundo + azul medio + lima neón + blanco.
        private static readonly Color Fondo =
            Color.FromArgb(14, 43, 92);

        private static readonly Color Superficie =
            Color.FromArgb(24, 70, 138);

        private static readonly Color SuperficieHover =
            Color.FromArgb(31, 83, 156);

        private static readonly Color Primario =
            Color.FromArgb(214, 246, 36);

        private static readonly Color PrimarioHover =
            Color.FromArgb(192, 224, 29);

        private static readonly Color TextoClaro =
            Color.White;

        private static readonly Color TextoOscuro =
            Color.FromArgb(18, 18, 18);

        private static readonly Color Borde =
            Color.FromArgb(64, 103, 166);

        private static readonly Color CajaTexto =
            Color.White;

        private static readonly Color CajaSoloLectura =
            Color.FromArgb(236, 241, 248);

        private static readonly Color FilaAlterna =
            Color.FromArgb(244, 247, 251);

        private static readonly Color Seleccion =
            Color.FromArgb(211, 230, 248);

        public static void Aplicar(Form form)
        {
            form.Font = new Font("Segoe UI", 9.5F);
            form.BackColor = Fondo;
            form.ForeColor = TextoClaro;

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
                        EstilizarCombo(combo);
                        break;

                    case Label etiqueta:
                        EstilizarEtiqueta(etiqueta);
                        break;

                    case DateTimePicker fecha:
                        EstilizarFecha(fecha);
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
            grupo.ForeColor = TextoClaro;
            grupo.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold);
        }

        private static void EstilizarEtiqueta(Label etiqueta)
        {
            etiqueta.ForeColor = TextoClaro;

            if (etiqueta.Name.Equals(
                "lblTitulo",
                StringComparison.OrdinalIgnoreCase))
            {
                etiqueta.Font = new Font(
                    "Segoe UI",
                    18F,
                    FontStyle.Bold);

                etiqueta.ForeColor = Primario;
            }

            if (etiqueta.Name.Equals(
                "lblMensaje",
                StringComparison.OrdinalIgnoreCase))
            {
                etiqueta.ForeColor = Primario;
            }
        }

        private static void EstilizarCajaTexto(TextBox caja)
        {
            caja.BorderStyle = BorderStyle.FixedSingle;
            caja.BackColor =
                caja.ReadOnly
                    ? CajaSoloLectura
                    : CajaTexto;

            caja.ForeColor = TextoOscuro;
        }

        private static void EstilizarCombo(ComboBox combo)
        {
            combo.FlatStyle = FlatStyle.Flat;
            combo.BackColor = CajaTexto;
            combo.ForeColor = TextoOscuro;
        }

        private static void EstilizarFecha(DateTimePicker fecha)
        {
            fecha.CalendarForeColor = TextoOscuro;
            fecha.CalendarMonthBackground = CajaTexto;
            fecha.CalendarTitleBackColor = Superficie;
            fecha.CalendarTitleForeColor = TextoClaro;
            fecha.CalendarTrailingForeColor =
                Color.FromArgb(110, 120, 135);
        }

        private static void EstilizarBoton(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Cursor = Cursors.Hand;
            boton.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold);

            if (EsBotonPrincipal(boton.Name))
            {
                boton.BackColor = Primario;
                boton.ForeColor = TextoOscuro;
                boton.FlatAppearance.MouseOverBackColor =
                    PrimarioHover;
                boton.FlatAppearance.MouseDownBackColor =
                    PrimarioHover;
            }
            else
            {
                boton.BackColor = Superficie;
                boton.ForeColor = TextoClaro;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = Borde;
                boton.FlatAppearance.MouseOverBackColor =
                    SuperficieHover;
                boton.FlatAppearance.MouseDownBackColor =
                    SuperficieHover;
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
            grilla.BackgroundColor = CajaTexto;
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
                TextoOscuro;
            grilla.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold);
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Primario;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                TextoOscuro;

            grilla.DefaultCellStyle.BackColor = CajaTexto;
            grilla.DefaultCellStyle.ForeColor = TextoOscuro;
            grilla.DefaultCellStyle.SelectionBackColor =
                Seleccion;
            grilla.DefaultCellStyle.SelectionForeColor =
                TextoOscuro;
            grilla.DefaultCellStyle.Padding =
                new Padding(4, 2, 4, 2);

            grilla.AlternatingRowsDefaultCellStyle.BackColor =
                FilaAlterna;
        }
    }
}
