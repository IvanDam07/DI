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

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}