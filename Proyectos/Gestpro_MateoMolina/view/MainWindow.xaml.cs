using Gestpro_MateoMolina.domain;
using Gestpro_MateoMolina.persistence;
using Org.BouncyCastle.Asn1.X509.SigI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Gestpro_MateoMolina
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Proyecto> lstProyectos;
        private List<String> empresas;
        Random r = new Random();
        int contador = 0;

        public MainWindow()
        {
            InitializeComponent();

            Proyecto proyecto = new Proyecto();
            proyecto.readProyectos();

            dgDatos.ItemsSource = proyecto.getListaProyectos();

            //creo la lista de empresas y la relleno
            empresas = new List<String>();

            empresas.Add("Allegro");
            empresas.Add("Velneo");
            empresas.Add("SAP");
            empresas.Add("Naturgy");
            empresas.Add("Santander");
            empresas.Add("MutuaMadrileña");          
        }

        private void btnProyectos_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void start()
        {
            txtCodigoProyecto.Text = "";
            txtNombre.Text = "";
            dateFin.Text = "";
            dateInicio.Text = "";

            dgDatos.SelectedItems.Clear();
        }


        private void dgDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(dgDatos.SelectedItems.Count > 0)
            {

                Proyecto proyecto = (Proyecto) dgDatos.SelectedItems[0];
                txtCodigoProyecto.Text = proyecto.Codigo;
                txtNombre.Text = proyecto.Nombre;
                dateInicio.SelectedDate = proyecto.fecIncio;
                dateFin.SelectedDate = proyecto.fecFin;

            }

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (!btnModificar.Content.Equals("Actualizar"))
            {

                if (MessageBox.Show("Do you want to add this proyect?", "Confirmtion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Proyecto p = new Proyecto(txtCodigoProyecto.Text, txtNombre.Text, dateInicio.SelectedDate.Value, dateFin.SelectedDate.Value);
                    p.insert();
                    p.last();

                    ((List<Proyecto>)dgDatos.ItemsSource).Add(p);
                    dgDatos.Items.Refresh();
                }

            } 
            else
            {

                Proyecto pro = (Proyecto)dgDatos.SelectedItem;

                List<Proyecto> listProyecto = (List<Proyecto>) dgDatos.ItemsSource;

                listProyecto[dgDatos.SelectedIndex].codigo = txtCodigoProyecto.Text;
                listProyecto[dgDatos.SelectedIndex].nombre = txtNombre.Text;
                listProyecto[dgDatos.SelectedIndex].fecIncio = dateInicio.SelectedDate.Value;
                listProyecto[dgDatos.SelectedIndex].fecFin = dateFin.SelectedDate.Value;

                int idP = pro.id;
                String codPro = pro.codigo;
                String nombre = pro.nombre;
                DateTime fecIni = pro.fecIncio;
                DateTime fecFin = pro.fecFin;

                Proyecto p = new Proyecto(idP,nombre, codPro, fecIni, fecFin);

                p.update();

                dgDatos.Items.Refresh();

            }
                

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            Proyecto proyectoModificar = (Proyecto)dgDatos.SelectedItem;

            txtCodigoProyecto.Text = proyectoModificar.Codigo.ToString();
            txtNombre.Text = proyectoModificar.Nombre.ToString();
            dateInicio.SelectedDate = proyectoModificar.FecInicio;
            dateFin.SelectedDate = proyectoModificar.FecFin;

            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;

            btnAgregar.Content = "Actualizar";

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if(MessageBox.Show("Do you wat to remove this project?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Proyecto proyecto = (Proyecto) dgDatos.SelectedItem;

                proyecto.delete();
                List<Proyecto> lstProyecto = (List<Proyecto>)dgDatos.ItemsSource;
                lstProyecto.Remove(proyecto);
                dgDatos.Items.Refresh();
                dgDatos.ItemsSource = lstProyecto;
                start();
            }

        }

        private void tboxBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RealizarBusqueda();
            }
        }

        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            RealizarBusqueda();
        }

        private void RealizarBusqueda()
        {
            string searchText = txtBusqueda.Text.Trim().ToLower();

            if (lstProyectos == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                dgDatos.ItemsSource = lstProyectos;
            }
            else
            {
                //NO METO PARA BUSCAR LAS FECHAS
                var filteredList = lstProyectos.Where(proyecto =>
                    proyecto != null && (
                    proyecto.Codigo.ToString().ToLower().Contains(searchText) ||
                    (proyecto.Nombre != null && proyecto.Nombre.ToLower().Contains(searchText))
                    )
                ).ToList();

                dgDatos.ItemsSource = filteredList;
            }

            dgDatos.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
            int empresa = 0;
            List<Proyecto> proyectos = new List<Proyecto>();


            for (int i = 0; i < 20; i++)
            {
                Proyecto p = new Proyecto();

                empresa = r.Next(0, 5);
                contador = contador + 1;

                p.codigo = "MTR" + contador + empresas[empresa] + DateTime.Now.Year.ToString();
                p.nombre = empresas[empresa];
                p.fecIncio = DateTime.Now;
                p.fecFin = DateTime.Now;

                p.insert();
                p.last();

                
                proyectos.Add(p);
                

            }

            foreach (Proyecto pro in proyectos) {
                ((List<Proyecto>)dgDatos.ItemsSource).Add(pro);
                dgDatos.Items.Refresh();

            }
            
        }

        
    }
}
