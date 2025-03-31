using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Base_datos
{
    public class Accesodatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public Accesodatos()
        {

            conexion = new SqlConnection("Server=.\\SQLEXPRESS;Database=CATALOGO_DB;Integrated Security=True;");
            comando = new SqlCommand();
        }
        public void Setearconsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void Ejecutarlectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }
        public void Cerrarconexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
        public void Setearparametros(string consulta,Object valor)
        {
            comando.Parameters.AddWithValue(consulta,valor);
        }
        public void ejecutarAccion()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open ();
                lector = comando.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}
