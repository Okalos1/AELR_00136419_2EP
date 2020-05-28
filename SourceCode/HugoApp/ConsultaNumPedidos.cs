using System;
using System.Collections.Generic;
using System.Data;

namespace HugoApp
{
    public class ConsultaNumPedidos
    {
        public static List<NumPedidos> getLista()
        {
            string sql = $"SELECT b.name AS \"Negocio\", sum(cp.cant) AS \"Total pedidos\" " +
                         $"FROM BUSINESS b, (SELECT p.idBusiness, p.name, count(ap.idProduct) " +
                         $"AS \"cant\" FROM PRODUCT p, APPORDER ap WHERE p.idProduct = ap.idProduct " +
                         $"GROUP BY p.idProduct ORDER BY p.name ASC) AS cp WHERE b.idBusiness = " +
                         $"cp.idBusiness GROUP BY b.idBusiness;";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<NumPedidos> lista = new List<NumPedidos>();
            foreach (DataRow fila in dt.Rows)
            {
                NumPedidos u = new NumPedidos();
                u.NombNegocio = fila[0].ToString();
                u.CantPedidos = Convert.ToInt32(fila[1].ToString());
                
                
                lista.Add(u);
            }
            return lista;    
        }
    }
}