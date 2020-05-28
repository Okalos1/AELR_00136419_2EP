using System;
using System.Collections.Generic;
using System.Data;

namespace HugoApp
{
    public class ProductoDAO
    {
        public static List<Productos> getLista()
        {
            string sql = "select * from PRODUCT";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Productos> lista = new List<Productos>();
            foreach (DataRow fila in dt.Rows)
            {
                Productos u = new Productos();
                u.idproduct = fila[0].ToString();
                u.idbusiness = fila[1].ToString();
                u.name = fila[2].ToString();
                
                lista.Add(u);
            }
            return lista;    
        }


    }
}