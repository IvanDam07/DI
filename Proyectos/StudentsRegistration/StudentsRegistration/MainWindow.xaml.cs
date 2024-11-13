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

namespace StudentsRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bRegistrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(tbNombre.Text + " se ha registrado.");
        }

        private void bSalir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}