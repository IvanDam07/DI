//using DataGridPersonas.persistence;
using Google.Protobuf;
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


namespace DataGridPersonas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Persona persona = new Persona();
                persona.readP();
                dgvPersonas.ItemsSource = persona.getListPersona();
                bntEliminar.IsEnabled = true;
                bntNew.IsEnabled = true;
                txtNombre.Text = "";
                txtEdad.Text = "";
            }
            catch (InvalidJsonException ex) {
                Console.WriteLine("Error during JSON reading: "+ex.Message);
            }
        }
        //aqui galta algo
        private void dgvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (dgvPersonas.SelectedItems.Count>0) {
                bntNew.IsEnabled = true;
                bntEliminar.IsEnabled=true;
                Persona persona = (Persona) dgvPersonas.SelectedItems[0];
                txtNombre.Text = persona.nombre;
                txtEdad.Text = persona.edad.ToString();
            }
        }

        private void start() {
            txtNombre.Text = "";
            txtEdad.Text = "";

            bntEliminar.IsEnabled = false;
            bntNew.IsEnabled=false;
            btnModify.IsEnabled = false;
            dgvPersonas.SelectedItems.Clear();

        }

        private void bntNew_Click(object sender, RoutedEventArgs e)
        {
            start();
        }
        
        private void bntEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to remove this person?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes) { 
                Persona persona = (Persona) dgvPersonas.SelectedItem;

                persona.delete();
                List<Persona> lst = (List<Persona>)dgvPersonas.ItemsSource;
                lst.Remove(persona);
                dgvPersonas.Items.Refresh();
                dgvPersonas.ItemsSource = lst;
                start();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!btnModify.Content.Equals("Modifying"))
            {
                if (MessageBox.Show("Do you want to add this person?", "Confirmtion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Persona p = new Persona(txtNombre.Text, Int32.Parse(txtEdad.Text));
                    p.insert();
                    p.last();
                    ((List<Persona>)dgvPersonas.ItemsSource).Add(p);
                    dgvPersonas.Items.Refresh();
                }
            }
            else {
                if (MessageBox.Show("Do you want to add this modification?", "Confirmtion", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    //Cojo lo primero el id del registro que he seleccionado
                    //Creo una persona con ese id, para poder guardarlo
                    Persona pselected = (Persona)dgvPersonas.SelectedItem;

                    List<Persona> listPeople = (List<Persona>)dgvPersonas.ItemsSource;
                    listPeople[dgvPersonas.SelectedIndex].nombre =txtNombre.Text;
                    listPeople[dgvPersonas.SelectedIndex].edad = int.Parse(txtEdad.Text);
                    int idP = pselected.id;
                    string nombreP = txtNombre.Text;
                    int edadP = int.Parse(txtEdad.Text);

                    //Creo una persona con el id que he recogido al seleccionarlo en el datagrid y las modificaciones que se le hayan hecho
                    Persona p = new Persona(idP,nombreP, edadP);

                    p.update();

                    dgvPersonas.Items.Refresh();

                    bntEliminar.IsEnabled = true;
                    btnModify.IsEnabled = true;
                    bntNew.IsEnabled = true;
                }
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            bntEliminar.IsEnabled = false;
            btnModify.IsEnabled = false;
            bntNew.IsEnabled = false;
            btnModify.Content = "Modifying";
        }
    }
}
