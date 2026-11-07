using BE;
using BLL;
using Servicios;
using UI.Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class frmReportePredictivo : Form, IObservadorIdioma
    {
        private readonly ReportePredictivoBLL _reporteBLL = new ReportePredictivoBLL();
        private List<ReportePredictivoBE> _datos = new List<ReportePredictivoBE>();
        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        public frmReportePredictivo()
        {
            InitializeComponent();
            AyudaContextual.Configurar(this);
            ActualizarIdioma();
            Load += frmReportePredictivo_Load;
            FormClosed += frmReportePredictivo_FormClosed;
        }

        private void frmReportePredictivo_Load(object? sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            CargarReporte();
        }

        private void frmReportePredictivo_FormClosed(object? sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            Text = T("frmReportePredictivo.Title");
            lblTitulo.Text = T("frmReportePredictivo.Titulo");
            lblPeriodo.Text = string.Format(T("frmReportePredictivo.Periodo"),
                DateTime.Today.AddDays(-28).ToShortDateString(), DateTime.Today.AddDays(-1).ToShortDateString());
            lblNota.Text = T("frmReportePredictivo.Nota");
            btnActualizar.Text = T("frmReportePredictivo.Actualizar");
            btnVolver.Text = T("frmReportePredictivo.Volver");
            string[] columnas = {
                "Producto", "Categoria", "Ventas", "Promedio", "Estimacion", "Stock", "Minimo", "Reposicion"
            };
            foreach (string columna in columnas)
                dgvReporte.Columns["col" + columna]!.HeaderText = T("frmReportePredictivo.Col" + columna);
            MostrarResumen();
            pnlGrafico.Invalidate();
        }

        private void MostrarResumen()
        {
            lblResumen.Text = string.Format(T("frmReportePredictivo.Resumen"),
                _datos.Count, _datos.Count(x => x.ReposicionSugerida > 0),
                _datos.Sum(x => (long)x.Estimadas7Dias));
        }

        private void CargarReporte()
        {
            try
            {
                _datos = _reporteBLL.ObtenerPronostico();
                dgvReporte.DataSource = null;
                dgvReporte.DataSource = _datos;
                dgvReporte.ClearSelection();
                MostrarResumen();
                pnlGrafico.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void btnActualizar_Click(object? sender, EventArgs e) => CargarReporte();
        private void btnVolver_Click(object? sender, EventArgs e) => Close();

        // Grafico de barras sencillo sin paquetes externos: ocho productos
        // con mayor demanda estimada durante los proximos siete dias.
        private void pnlGrafico_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(24, 70, 138));
            using Font titulo = new Font("Segoe UI", 10F, FontStyle.Bold);
            using Font texto = new Font("Segoe UI", 8F);
            using SolidBrush blanco = new SolidBrush(Color.White);
            using SolidBrush verde = new SolidBrush(Color.FromArgb(214, 246, 36));

            g.DrawString(T("frmReportePredictivo.Grafico"), titulo, blanco, 12, 8);
            List<ReportePredictivoBE> productos = _datos
                .OrderByDescending(x => x.Estimadas7Dias)
                .ThenBy(x => x.Producto)
                .Take(8).ToList();
            if (productos.Count == 0)
            {
                g.DrawString(T("frmReportePredictivo.SinDatos"), texto, blanco, 15, 46);
                return;
            }

            int maximo = Math.Max(1, productos.Max(x => x.Estimadas7Dias));
            float ancho = Math.Max(80, pnlGrafico.ClientSize.Width - 300);
            float alto = Math.Min(17f, 130f / productos.Count);
            for (int i = 0; i < productos.Count; i++)
            {
                ReportePredictivoBE p = productos[i];
                float y = 37 + i * 17;
                string nombre = p.Producto.Length > 25 ? p.Producto.Substring(0, 22) + "..." : p.Producto;
                g.DrawString(nombre, texto, blanco, 12, y);
                float largo = ancho * p.Estimadas7Dias / maximo;
                if (largo > 0) g.FillRectangle(verde, 210, y + 2, largo, alto - 2);
                g.DrawString(p.Estimadas7Dias.ToString(), texto, blanco, 220 + largo, y);
            }
        }
    }
}
