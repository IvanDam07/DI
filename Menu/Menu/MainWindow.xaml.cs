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

namespace Menu
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

        private void Max_Click(object sender, RoutedEventArgs e)
        {
            // Maximiza la ventana si no está maximizada; de lo contrario, restaura el tamaño original.
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                menuMax.IsChecked = false; // Desmarcar
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                menuMax.IsChecked = true;
                menuMin.IsChecked = false; // Desmarcar "Min"
            }
        }

        private void Min_Click (object sender, RoutedEventArgs e)
        {
            // Minimiza la ventana
            this.WindowState = WindowState.Minimized;
            menuMin.IsChecked = true;
            menuMax.IsChecked = false; // Desmarcar "Max"
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // Cierra la ventana
            this.Close();
        }
    }
}