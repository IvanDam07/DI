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

//namespace MarioBrosGrafico
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        private string[,] mushromKingdom = new string[8, 8];
//        private int lifes = 3;
//        private int potion = 0;

//        public MainWindow()
//        {
//            InitializeComponent();
//            InitializeGame();
//        }

//        private void InitializeGame()
//        {
//            // Inicializa el tablero con X y coloca a Mario en la posición inicial
//            Helper.fillBoard(mushromKingdom, "X");
//            Helper.setPosition(mushromKingdom, 0, 0, "M");

//            // Muestra las vidas y las pociones iniciales en los TextBox
//            tBoxLives.Text = lifes.ToString();
//            tBoxPotions.Text = potion.ToString();

//            DisplayBoard(); // Muestra el tablero inicial
//        }

//        // Manejador de evento para btnStart
//        private void btnStart_Click(object sender, RoutedEventArgs e)
//        {
//            // Cambia la pestaña actual a GAME
//            MainTabControl.SelectedIndex = 1;
//        }

//        // Manejador de evento para btnPlay
//        private void btnPlay_Click(object sender, RoutedEventArgs e)
//        {
//            // Llena el tablero con números aleatorios y lo muestra en el GameGrid
//            Helper.updateBoardGame(mushromKingdom); // Llena el tablero sin modificar a Mario
//            DisplayBoard(); // Muestra el tablero actualizado
//        }

//        private void DisplayBoard()
//        {
//            // Limpia el GameGrid antes de agregar elementos nuevos
//            GameGrid.Children.Clear();

//            for (int i = 0; i < mushromKingdom.GetLength(0); i++)
//            {
//                for (int j = 0; j < mushromKingdom.GetLength(1); j++)
//                {
//                    TextBlock cell = new TextBlock
//                    {
//                        Text = mushromKingdom[i, j],
//                        HorizontalAlignment = HorizontalAlignment.Center,
//                        VerticalAlignment = VerticalAlignment.Center
//                    };
//                    Grid.SetRow(cell, i);
//                    Grid.SetColumn(cell, j);
//                    GameGrid.Children.Add(cell);
//                }
//            }
//        }
//    }
//}

using System.Windows;
using System.Windows.Controls;

namespace MarioBrosGrafico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[,] mushromKingdom = new string[8, 8];
        private int lifes = 3; // Vidas iniciales
        private int potion = 0; // Pócima inicial

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Inicializa el tablero con "X" y coloca a Mario en la posición inicial (0,0)
            Helper.fillBoard(mushromKingdom, "X");
            Helper.setPosition(mushromKingdom, 0, 0, "M");

            // Actualiza los valores iniciales de las vidas y pócimas en la interfaz
            UpdateStatsUI();

            // Muestra el tablero inicial en el GameGrid
            DisplayBoard();
        }

        private void UpdateStatsUI()
        {
            // Actualiza las vidas y la cantidad de pócima en los TextBox correspondientes
            tBoxLives.Text = lifes.ToString();
            tBoxPotions.Text = potion.ToString();
        }

        // Manejador de evento para el botón "Start"
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Cambia a la pestaña GAME
            MainTabControl.SelectedIndex = 1;
        }

        // Manejador de evento para el botón "Play"
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            // Genera números aleatorios en el tablero respetando la posición de Mario
            Helper.updateBoardGame(mushromKingdom);
            DisplayBoard(); // Refresca el tablero en la interfaz
        }

        // Manejador para los botones de movimiento
        private void MoveMario(int moveI, int moveJ)
        {
            Helper.move(mushromKingdom, moveI, moveJ, ref lifes, ref potion); // Llama al método para mover a Mario

            // Actualiza las estadísticas en la interfaz
            UpdateStatsUI();

            // Verifica condiciones de victoria o derrota
            if (lifes <= 0)
            {
                MessageBox.Show("¡Has perdido todas las vidas! El juego se reiniciará.", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                InitializeGame(); // Reinicia el juego
            }
            else if (potion >= 5)
            {
                MessageBox.Show("¡Has conseguido suficiente pócima para salvar a Peach! ¡Felicidades!", "Victoria", MessageBoxButton.OK, MessageBoxImage.Information);
                InitializeGame(); // Reinicia el juego
            }

            // Actualiza el tablero en la interfaz
            DisplayBoard();
        }

        private void DisplayBoard()
        {
            // Limpia el GameGrid antes de actualizarlo
            GameGrid.Children.Clear();

            for (int i = 0; i < mushromKingdom.GetLength(0); i++)
            {
                for (int j = 0; j < mushromKingdom.GetLength(1); j++)
                {
                    TextBlock cell = new TextBlock
                    {
                        Text = mushromKingdom[i, j],
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = 16,
                        FontWeight = FontWeights.Bold
                    };
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    GameGrid.Children.Add(cell);
                }
            }
        }

        // Eventos para botones de movimiento (arriba, abajo, izquierda, derecha)
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            MoveMario(-1, 0); // Mover hacia arriba
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            MoveMario(1, 0); // Mover hacia abajo
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            MoveMario(0, -1); // Mover hacia la izquierda
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            MoveMario(0, 1); // Mover hacia la derecha
        }
    }
}
