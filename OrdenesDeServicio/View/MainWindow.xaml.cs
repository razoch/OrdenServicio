using OrdenesDeServicio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;

namespace OrdenesDeServicio.View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModelBase ViewModel { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            //ViewModel = new ViewModelBase();
            //DataContext = ViewModel;
            this.DataContext = new OrdenViewModel();
            
        }

        private void ButtonEnviar_Click(object sender, RoutedEventArgs e)
        {
            EnviarOrdenes();
        }

        private Task EnviarOrdenes()
        {
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    string url = "https://jsonplaceholder.typicode.com/posts";
                    var cliente = new HttpClient();
                    Valores datos = new Valores();
                    string contenido = datos.Cargar();

                    HttpContent content = new StringContent(contenido, System.Text.Encoding.UTF8, "application/json");
                    var httpResponse = await cliente.PostAsync(url, content);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string result = await httpResponse.Content.ReadAsStringAsync();
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en método EnviarOrdenes: " + ex.Message);
                }
            });
        }
    }
}
