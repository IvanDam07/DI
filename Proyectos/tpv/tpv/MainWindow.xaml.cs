using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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

namespace tpv
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

        private void CargarProductosEnStackPanel()
        {
            //Conexión a la BBDD
            string connectionString = "Micadenadeconexion";
            string query = "SELECT Nombre FROM Productos";

            List<string> productos = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(reader.GetString(0)); // Añade cada nombre de producto a la lista
                            }
                        }
                    }
                }

                //Crear botones dinámicamente
                foreach(string producto in productos)
                {
                    Button boton = new Button
                    {
                        Content = producto,
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                    };

                    boton.Click += Boton_Click; //Agrega un evento al click
                    spCategorias.Children.Add(boton); //Añade botones al StackPanel
                } 

            } catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}");
            }
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            if (sender  is Button boton)
            {
                MessageBox.Show($"Hiciste clic en el producto: {boton.Content}"); //Aquí cambiar y que salga una pantalla nueva con los productos
            }
        }
    }
}
