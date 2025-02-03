using AgendaContactosBBDD.domain;
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

namespace AgendaContactosBBDD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Contacto> listaContactos; 
        private Contacto contacto;

        public MainWindow()
        {
            InitializeComponent();
            listaContactos = new List<Contacto>();
            contacto = new Contacto();

            //Meto en listaContactos los contactos existentes en la bbdd cuando se ejecuta
            listaContactos = contacto.genListaContactos();
            
            //Le digo al dataGrid que muestre la lista de contactos
            dgContactos.ItemsSource = listaContactos;
        }


        /// <summary>
        ///     Agrega un nuevo contacto en la bbdd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Contacto contacto = new Contacto(tbNombreContacto.Text, int.Parse(tbTelefonoContacto.Text) );
            
            contacto.insertarNuevoContacto();

            listaContactos.Add( contacto );

            dgContactos.Items.Refresh();

            limpiarCampos();
        }



        /// <summary>
        ///     Borra un contacto de la bbdd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Contacto c = (Contacto) dgContactos.SelectedItem;
            c.eliminar();
            List<Contacto> newList = (List<Contacto>)dgContactos.ItemsSource;
            newList.Remove(c);
            dgContactos.Items.Refresh();
            dgContactos.ItemsSource = newList;

            limpiarCampos();
        }


        /// <summary>
        ///     Modifica un contacto de la bbdd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Contacto c = (Contacto) dgContactos.SelectedItem;
            List<Contacto> newList = (List<Contacto>)dgContactos.ItemsSource;
            newList.Remove(c);

            Contacto contacto = new Contacto(tbNombreContacto.Text, int.Parse(tbTelefonoContacto.Text));
            contacto.Id = c.Id;

            newList.Add(contacto);
            contacto.modificarContacto();

            dgContactos.Items.Refresh();
            dgContactos.ItemsSource = newList;

            limpiarCampos();

        }


        /// <summary>
        ///     Limpia los textbox cuando terminas de realizar una operacion
        /// </summary>
        private void limpiarCampos()
        {
            tbNombreContacto.Text = "";
            tbTelefonoContacto.Text = "";
        }
    }
}