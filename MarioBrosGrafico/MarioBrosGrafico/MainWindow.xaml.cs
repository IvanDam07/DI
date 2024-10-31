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

namespace MarioBrosGrafico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[,] mushromKingdom = new string[8, 8];
        private int lifes = 3;
        private int potion = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Inicializa el tablero con X y coloca a Mario en la posición inicial
            Helper.fillBoard(mushromKingdom, "X");
            Helper.setPosition(mushromKingdom, 0, 0, "M");

            // Muestra las vidas y las pociones iniciales en los TextBox
            tBoxLives.Text = lifes.ToString();
            tBoxPotions.Text = potion.ToString();
        }

        // Manejador de evento para btnStart
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Cambia la pestaña actual a GAME
            ((TabControl)this.Content).SelectedIndex = 1;
        }

        // Manejador de evento para btnPlay
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            // Llena el tablero con números aleatorios y lo muestra en el GameGrid
            Helper.fillBoardGame(mushromKingdom);
            DisplayBoard();
        }

        private void DisplayBoard()
        {
            // Limpia el GameGrid antes de agregar elementos nuevos
            GameGrid.Children.Clear();

            for (int i = 0; i < mushromKingdom.GetLength(0); i++)
            {
                for (int j = 0; j < mushromKingdom.GetLength(1); j++)
                {
                    TextBlock cell = new TextBlock
                    {
                        Text = mushromKingdom[i, j],
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    GameGrid.Children.Add(cell);
                }
            }
        }
    }
}