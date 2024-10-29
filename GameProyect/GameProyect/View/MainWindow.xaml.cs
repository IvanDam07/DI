using GameProyect.Domain;
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

namespace GameProyect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Character character1 = new Character("Fermin", 0);
            Character character2 = new Character("Pedro", 25);
            Character character3 = new Character("Leo", 50);

            cboPlayer.Items.Add(character1.name);
            cboPlayer.Items.Add(character2.name);
            cboPlayer.Items.Add(character3.name);

            btnNew.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;

            imgWand.IsEnabled = false;
            imgLightning.IsEnabled = false;
            imgBrain.IsEnabled = false;
        }
        private void cboPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnNew.IsEnabled = true;
            //switch (cboPlayer.SelectedIndex)
            //{
            //    case 0:
            //        lblAvaible.Content = "Avaiable Points: " + HT.points;
            //        break;
            //    case 1:
            //        lblAvaible.Content = ""
            //}
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //// Solicitar el nombre del personaje al usuario
            //string name = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the Character:", "New Character");

            //// Verificar que el nombre no esté vacío o nulo
            //if (string.IsNullOrWhiteSpace(name))
            //{
            //    MessageBox.Show("Name cannot be empty.");
            //    return;
            //}

            //// Generar un id para el nuevo Character (puedes cambiar la lógica si quieres)
            //int id = cboPlayer.Items.Count + 1;

            //// Asignar puntos iniciales (puedes cambiar la lógica si quieres)
            //int points = 0;

            //// Crear el nuevo Character
            //Character newCharacter = new Character(name, id, points);

            //// Añadir el Character al ComboBox
            //cboPlayer.Items.Add(newCharacter);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}