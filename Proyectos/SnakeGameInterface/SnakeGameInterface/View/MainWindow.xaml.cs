using SnakeGameInterface.Domain;
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

namespace SnakeGameInterface
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string[,] tablero = new string[5, 5];
        private int ratones;
        private int tamSerpiente;
        private int numParedes;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void InitializaGame()
        {
            Helper.fillBoardDefault(tablero, " ");
            Helper.fillBoardSnake(tablero, "S", tamSerpiente);
            Helper.fillBoard(tablero, "R", ratones);
            Helper.fillBoard(tablero, "P", numParedes);

            DisplayBoard();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            ratones = int.Parse(tBoxRatones.Text);
            tamSerpiente = int.Parse(tBoxSerpiente.Text);
            numParedes = int.Parse(tBoxParedes.Text);

            InitializaGame();

            MainTabControl.SelectedIndex = 1;
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DisplayBoard()
        {
            GameGrid.Children.Clear();

            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    TextBlock cell = new TextBlock
                    {
                        Text = tablero[i, j],
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
    }
}
