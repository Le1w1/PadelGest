using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI.Servicios
{
    /// <summary>
    /// Evita que los formularios de tamaño fijo queden fuera del area util
    /// en notebooks, resoluciones bajas o con escalado de Windows.
    /// Conserva las medidas originales en pantallas grandes.
    /// </summary>
    internal static class AjustePantalla
    {
        public static void HabilitarDesplazamiento(Form formulario)
        {
            Size contenidoOriginal = formulario.ClientSize;
            formulario.AutoScroll = true;
            formulario.AutoScrollMinSize = contenidoOriginal;

            // Load ocurre antes de mostrar el dialogo. La pantalla del formulario
            // ya es conocida y su barra de tareas se excluye con WorkingArea.
            formulario.Load += (_, _) =>
            {
                Rectangle areaUtil = Screen.FromControl(formulario).WorkingArea;

                int bordeHorizontal = formulario.Width - formulario.ClientSize.Width;
                int bordeVertical = formulario.Height - formulario.ClientSize.Height;

                int anchoDisponible = Math.Max(200, areaUtil.Width - bordeHorizontal - 16);
                int altoDisponible = Math.Max(200, areaUtil.Height - bordeVertical - 16);

                int ancho = Math.Min(contenidoOriginal.Width, anchoDisponible);
                int alto = Math.Min(contenidoOriginal.Height, altoDisponible);

                if (formulario.ClientSize.Width != ancho || formulario.ClientSize.Height != alto)
                {
                    formulario.ClientSize = new Size(ancho, alto);

                    // Recentrar despues de ajustar: CenterParent puede haber
                    // calculado una posicion parcialmente fuera de la pantalla.
                    formulario.Location = new Point(
                        areaUtil.Left + Math.Max(0, (areaUtil.Width - formulario.Width) / 2),
                        areaUtil.Top + Math.Max(0, (areaUtil.Height - formulario.Height) / 2));
                }
            };
        }
    }
}
