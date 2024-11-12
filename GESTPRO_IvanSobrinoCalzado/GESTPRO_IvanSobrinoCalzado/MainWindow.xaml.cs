using GESTPRO_IvanSobrinoCalzado.Control;
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

namespace GESTPRO_IvanSobrinoCalzado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Proyecto> proyectos;

        public MainWindow()
        {
            InitializeComponent();

            proyectos = new List<Proyecto>();

            dgProyectos.ItemsSource = proyectos;            
        }

        private void bAnadir_Click(object sender, RoutedEventArgs e)
        {
            proyectos.Add(new Control.Proyecto(tbCodigo.Text, tbNombre.Text, tbFechaInicio.Text, tbFechaFin.Text));
            dgProyectos.Items.Refresh();
        }

        private void bModificar_Click(object sender, RoutedEventArgs e)
        {
            if(proyectos.Where(c => c.CodigoProyecto == tbCodigo.Text && c.Nombre  == tbNombre.Text).ToList().Any())
            {
                proyectos.Where(c => c.CodigoProyecto == tbCodigo.Text && c.Nombre == tbNombre.Text).ToList().ForEach(c =>
                {
                    c.CodigoProyecto = tbCodigo.Text;
                    c.Nombre = tbNombre.Text;
                    c.FechaInicio = tbFechaInicio.Text;
                    c.FechaFin = tbFechaFin.Text;
                });

                dgProyectos.Items.Refresh();
                tbCodigo.Clear();
                tbNombre.Clear();
                tbFechaInicio.Clear();
                tbFechaFin.Clear();
                bModificar.IsEnabled = true;
                bEliminar.IsEnabled = true;
            }
            else
            {
                
            }
        }

        private void bEliminar_Click(object sender, RoutedEventArgs e)
        {
            proyectos.Remove((Proyecto)dgProyectos.SelectedItem);
            dgProyectos.Items.Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bProyectos_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
