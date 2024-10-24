using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TablaUsuarios
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Persona> personaList;
        public MainWindow()
        {
            InitializeComponent();

            personaList = new List<Persona>();
            
            dgPersonas.ItemsSource = personaList;
        }
        
        private void bAgregar_Click(object sender, RoutedEventArgs e)
        {
            personaList.Add(new Persona(tNombre.Text, tApellidos.Text, int.Parse(tEdad.Text)));
            dgPersonas.Items.Refresh();
        }

        private void bModificar_Click(Object sender, RoutedEventArgs e) //mirar esto bien porque no funciona como debería
        {
            if (personaList.Where(p => p.Nombre == tNombre.Text && p.Apellidos == tApellidos.Text).ToList().Any())
            {
                //Same code using LINQ expresion over the list
                personaList.Where(p => p.Nombre == tNombre.Text && p.Apellidos == tApellidos.Text).ToList().ForEach(p =>
                {
                    p.Nombre = tNombre.Text;
                    p.Apellidos = tApellidos.Text;
                    p.Edad = int.Parse(tEdad.Text);
                });

                dgPersonas.Items.Refresh();
                tEdad.Clear();
                tNombre.Clear();
                tApellidos.Clear();
                bModificar.IsEnabled = true;
                bEliminar.IsEnabled = true;
                bAgregar.Content = "Agregar Personas";
            }          
            else
            {
                if (personaList.Where(p => p.Nombre.Equals(tNombre.Text) &&
                                        p.Apellidos.Equals(tApellidos.Text)).ToList().Any() == false)
                    personaList.Add(new Persona(tNombre.Text, tApellidos.Text, int.Parse(tEdad.Text)));
                else
                    MessageBox.Show("La persona ya existe en la lista de persoan. No se añade de nuevo.");
                dgPersonas.Items.Refresh();
                tEdad.Clear();
                tNombre.Clear();
                tApellidos.Clear();
            }

        }

            private void bEliminar_Click( object sender, RoutedEventArgs e)
        {
            personaList.Remove((Persona)dgPersonas.SelectedItem);
            dgPersonas.Items.Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}