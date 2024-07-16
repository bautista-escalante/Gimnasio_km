using System;
using System.Data;
using System.Data.SqlClient;

namespace BaseDatos
{
    public class AccesoDatos
    {
        private static AccesoDatos? instancia;
        private SqlConnection conexion;
        private string cadenaConexion = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Cliente;Integrated Security=True";

        private AccesoDatos()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public static AccesoDatos ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new AccesoDatos();
            }
            return instancia;
        }

        public SqlCommand PrepararConsulta(string sql)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.CommandType = CommandType.Text;
                return comando;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void AbrirConexion()
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
                throw;
            }
        }
    }
}
