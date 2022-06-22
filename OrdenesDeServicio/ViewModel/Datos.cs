using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text;
using System.Windows;

namespace OrdenesDeServicio.ViewModel
{
    public class Datos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public int estatusId { get; set; }
        public string estatus { get; set; }
    }
    public class Valores
    {
        public List<Datos> datos { get; set; }
        public string Cargar()
        {
            string resultado = String.Empty;
            try
            {
                string query = "EXEC spRegresaDatosJSON";
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Conexion))
                {
                    if (conn != null)
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            SqlDataReader IdDato = null;

                            IdDato = cmd.ExecuteReader();
                            if (IdDato.Read())
                            {
                                resultado = IdDato[0].ToString();
                            }
                            IdDato.Close();
                        }
                    }
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error método Carga: " + ex.Message);
            }
            return resultado;
        }
    }
   
    public class Basededatos<T>
    {
        public List<T> valores = new List<T>();
        
    }
}
