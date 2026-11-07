using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ReportePredictivoDAL
    {
        private readonly DAO_AccesoDatos _acceso = new DAO_AccesoDatos();

        // Incluye productos sin ventas para detectar faltantes de stock.
        // Se cuentan exclusivamente facturas pagadas y emitidas dentro del periodo.
        public List<ReportePredictivoBE> ObtenerVentas(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT p.CodigoProducto, p.Nombre, p.Categoria,
                       p.StockDisponible, p.StockMinimo, p.PrecioUnitario,
                       COALESCE(v.Unidades, 0) AS Unidades
                FROM dbo.Producto p
                LEFT JOIN (
                    SELECT d.CodigoProducto, SUM(CONVERT(bigint, d.Cantidad)) AS Unidades
                    FROM dbo.DetalleFactura d
                    INNER JOIN dbo.FacturaBuffet f ON f.IdFactura = d.IdFactura
                    WHERE f.Estado = N'Pagada'
                      AND f.FechaHoraEmision >= @Desde
                      AND f.FechaHoraEmision < @Hasta
                    GROUP BY d.CodigoProducto
                ) v ON v.CodigoProducto = p.CodigoProducto
                WHERE p.Activo = 1
                ORDER BY p.Nombre, p.CodigoProducto;";

            List<ReportePredictivoBE> resultado = new List<ReportePredictivoBE>();
            using SqlConnection conexion = _acceso.ObtenerConexion();
            using SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@Desde", SqlDbType.DateTime2).Value = desde;
            comando.Parameters.Add("@Hasta", SqlDbType.DateTime2).Value = hasta;
            conexion.Open();
            using SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                long unidades = Convert.ToInt64(lector["Unidades"]);
                resultado.Add(new ReportePredictivoBE
                {
                    CodigoProducto = Convert.ToInt32(lector["CodigoProducto"]),
                    Producto = Convert.ToString(lector["Nombre"]) ?? string.Empty,
                    Categoria = Convert.ToString(lector["Categoria"]) ?? string.Empty,
                    Vendidas28Dias = unidades > int.MaxValue ? int.MaxValue : (int)unidades,
                    StockDisponible = Convert.ToInt32(lector["StockDisponible"]),
                    StockMinimo = Convert.ToInt32(lector["StockMinimo"]),
                    PrecioActual = Convert.ToDecimal(lector["PrecioUnitario"])
                });
            }
            return resultado;
        }
    }
}
