using Exercise3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Exercise3
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Player player;

        public MainWindow()
        {
            InitializeComponent();

            player = new Player();

            //cbLevel.SelectedIndex = 0;

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player(tbNickname.Text, dpDate.DisplayDate, int.Parse(cbLevel.Text), int.Parse(tbScore.Text));

            player.insertarNuevoPlayer();

            limpiarCampos();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void limpiarCampos()
        {
            tbNickname.Text = "";
            dpDate = null;
            cbLevel.Text = "";
            tbScore.Text = "";
        }
    }
}
