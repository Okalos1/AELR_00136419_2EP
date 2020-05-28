using System;
using System.Collections.Generic;
using System.Data;

namespace HugoApp
{
    public class UsuarioDAO
    {
        
        public static List<Usuario> getLista()
        {
            string sql = "select * from APPUSER";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Usuario> lista = new List<Usuario>();
            foreach (DataRow fila in dt.Rows)
            {
                Usuario u = new Usuario();
                u.iduser = fila[0].ToString();
                u.fullname = fila[1].ToString();
                u.username = fila[2].ToString();
                u.password = fila[3].ToString();
                u.userType= Convert.ToBoolean(fila[4].ToString());

                lista.Add(u);
            }
            return lista;    
        }

        public static void actualizarContra(string usuario, string nuevaContra)
        {
            string sql = String.Format(
                "update APPUSER set password='{0}' where username='{1}';",
                nuevaContra, usuario);
            
            Conexion.realizarAccion(sql);
        }
    }
    
}
