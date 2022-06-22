using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using OrdenesDeServicio.ViewModel;
using System.Threading.Tasks;

namespace OrdenesDeServicio.ViewModel
{
    class Utilerias
    {
        public static StringBuilder EjecutarQuery(string query)
        {
            var resultado = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Conexion))
            {                
                if (conn != null)
                {
                    conn.Open();
                    List<Datos> result = new List<Datos>();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader IdDato = null;

                        IdDato = cmd.ExecuteReader();
                        if (IdDato.Read())
                        {
                            resultado.Append(IdDato.GetValue(0).ToString());
                        }
                        IdDato.Close();                        
                    }
                }
            }
            return resultado;
        }
    }
}
