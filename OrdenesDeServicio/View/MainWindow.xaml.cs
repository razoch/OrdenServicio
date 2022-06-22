using OrdenesDeServicio.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net.Http;

namespace OrdenesDeServicio.View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new OrdenViewModel();
        }

        private void ButtonEnviar_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridOrdenes.Items.Count >= 1)
                EnviarOrdenes();
            else
                MessageBox.Show("No hay datos para realizar el envío.");
        }

        private Task EnviarOrdenes()
        {
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    string url = "https://my-json-server.typicode.com/razoch/ordenServicio/Post";
                    var cliente = new HttpClient();
                    Valores datos = new Valores();
                    string contenido = datos.Cargar();

                    HttpContent content = new StringContent(contenido, System.Text.Encoding.UTF8, "application/json");
                    var httpResponse = await cliente.PostAsync(url, content);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string result = await httpResponse.Content.ReadAsStringAsync();
                        //write string to file
                        System.IO.File.WriteAllText(@"C:\shared\jsonFile.txt", result);
                        //Limpia la Base de datos
                        datos.LimpiaRegistros();
                        
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en método EnviarOrdenes: " + ex.Message);
                }
                finally
                {
                    MessageBox.Show("Las órdenes fueron guardadas con éxito");
                    DataGridOrdenes.Items.Refresh();
                }
            });
        }
    }
}
