using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BaseDatos;
using Microsoft.Data.SqlClient;

namespace entidades
{
    public class Cliente
    {
        public int id;
        public string nombre;
        public string apellido;
        public string dni;
        public string numTelefono;
        public DateTime fechaIngreso;
        public DateTime fechaVencimiento;
        public string rutaFoto;

        public Cliente(string nombre, string apellido, string dni, string numTelefono, DateTime fechaIngreso, string foto)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.numTelefono = numTelefono;
            this.fechaIngreso = fechaIngreso;
            this.fechaVencimiento = AsignarFechaVencimiento();
            this.rutaFoto = foto;
        }
        public Cliente(string nombre, string apellido, string dni, string numTelefono, DateTime fechaIngreso, DateTime fechaVencimiento, string foto)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.numTelefono = numTelefono;
            this.fechaIngreso = fechaIngreso;
            this.fechaVencimiento = fechaVencimiento;
            this.rutaFoto = foto;
        }
        public static DateTime AsignarFechaVencimiento()
        {
            DateTime date = DateTime.Now;
            DateTime fechaVencimiento = date.AddDays(30);
            return fechaVencimiento;
        }

        public void Registrar()
        {
            int filasAfectadas = 0;
            AccesoDatos conexion = AccesoDatos.ObtenerInstancia();
            try
            {
                conexion.AbrirConexion();
                System.Data.SqlClient.SqlCommand comando = conexion.PrepararConsulta(
                "INSERT INTO cliente (nombre, apellido, dni, numTelefono, fechaIngreso, fechaVencimiento, rutaFoto) " +
                "VALUES (@nombre, @apellido, @dni, @numTelefono, @fechaIngreso, @fechaVencimiento, @rutaFoto)");
                comando.Parameters.AddWithValue("@nombre", this.nombre);
                comando.Parameters.AddWithValue("@apellido", this.apellido);
                comando.Parameters.AddWithValue("@dni", this.dni);
                comando.Parameters.AddWithValue("@numTelefono", this.numTelefono);
                comando.Parameters.AddWithValue("@fechaIngreso", this.fechaIngreso);
                comando.Parameters.AddWithValue("@fechaVencimiento", this.fechaVencimiento);
                comando.Parameters.AddWithValue("@rutaFoto", this.rutaFoto);

                filasAfectadas = comando.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            if (filasAfectadas == 0)
            {
                throw new Exception("error no se pudo ingresar a este usuario");
            }
        }
        public void EliminarCliente(string dni)
        {
            try{
                
            }catch(Exception e)
            {
                throw e;
            }

        }
        public void ActualizarMembresia()
        {

        }
        public void ConsularDeudores(string fecha)
        {

        }
        public void GuardarFoto()
        {
            // guardar en una carpeta y retornar la ruta
        }
        public void MostrarFoto(string nombre)
        {
            // encontrar la ruta 
        }
        public static Cliente? BuscarCliente(string dni)
        {
            Cliente cliente = null;
            try
            {
                AccesoDatos bd = AccesoDatos.ObtenerInstancia();
                bd.AbrirConexion();

                string consulta = "SELECT * FROM Cliente WHERE dni = @dni";
                System.Data.SqlClient.SqlCommand comando = bd.PrepararConsulta(consulta);
                comando.Parameters.AddWithValue("@dni", dni);

                using (System.Data.SqlClient.SqlDataReader dR = comando.ExecuteReader())
                {
                    if (!dR.HasRows)
                    {
                        throw new Exception("El cliente con DNI " + dni + " no existe.");
                    }
                    while (dR.Read())
                    {
                        cliente = new Cliente(dR["nombre"].ToString(), dR["apellido"].ToString(), dR["dni"].ToString(),
                        dR["numTelefono"].ToString(), Convert.ToDateTime(dR["fechaIngreso"]), Convert.ToDateTime(dR["fechaVencimiento"]),
                        dR["foto"].ToString());
                    }
                }

                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw e;
            }
            return cliente;
        }
    }
}
