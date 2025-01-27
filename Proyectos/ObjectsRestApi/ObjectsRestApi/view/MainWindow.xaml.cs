using ObjectsRestApi.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObjectsRestApi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            //URL de la API
            string apiURL = "https://api.restful-api.dev/objects";
            Api api;
            try
            {
                ResultsListBox.Items.Clear();
                ResultsListBox.Items.Add("Cargando datos...");

                //Realizar la solicitud HTTP
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    //Leer y deserializar los datos JSON
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var objects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Api>>(jsonResponse);

                    //Limpiar y cargar datos en el ListBox
                    ResultsListBox.Items.Clear();
                    foreach (var obj in objects)
                    {
                        ResultsListBox.Items.Add($"ID: {obj.Id}, Name: {obj.Name}");
                    }
                }
                else
                {
                    ResultsListBox.Items.Clear();
                    ResultsListBox.Items.Add($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                ResultsListBox.Items.Clear();
                ResultsListBox.Items.Add($"Error al obtener datos: {ex.Message}");
            }
        }
    }
}