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
using proyectoPersonas.domain;

namespace proyectoPersonas
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private List<Persona> listPersonas;
    private Persona persona;

    public MainWindow()
    {
      InitializeComponent();
      listPersonas = new List<Persona>();
      persona = new Persona();

      listPersonas = persona.genListaPersonas();
      dgvPersonas.ItemsSource = listPersonas;
      start();
    }

    private void start()
    {
      txtNombre.Text = "";
      txtApellido.Text = "";
      txtEdad.Text = "";

      dgvPersonas.SelectedItems.Clear();
      btnAgregar.IsEnabled = true;
      btnModificar.IsEnabled = false;
      btnEliminar.IsEnabled = false;
    }

    private void dgvPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (dgvPersonas.SelectedItems.Count > 0)
      {
        Persona persona = (Persona)dgvPersonas.SelectedItems[0];
        txtNombre.Text = persona.Nombre;
        txtApellido.Text = persona.Apellido;
        txtEdad.Text = persona.Edad.ToString();
        btnAgregar.IsEnabled = true;
        btnModificar.IsEnabled = true;
        btnEliminar.IsEnabled = true;
      }
    }

    private void btnAgregar_Click(object sender, RoutedEventArgs e)
    {
      persona = new Persona(txtNombre.Text, txtApellido.Text, int.Parse(txtEdad.Text));
      persona.insertar();
      listPersonas.Add(persona);
      dgvPersonas.Items.Refresh();
    }

    private void btnModificar_Click(object sender, RoutedEventArgs e)
    {
      Persona persona = (Persona)dgvPersonas.SelectedItem;
      List<Persona> newList = (List<Persona>)dgvPersonas.ItemsSource;
      newList.Remove(persona);

      Persona newPersona = new Persona(txtNombre.Text, txtApellido.Text, int.Parse(txtEdad.Text));
      newPersona.Id = persona.Id;

      newList.Add(newPersona);
      persona.modificar();

      dgvPersonas.Items.Refresh();
      dgvPersonas.ItemsSource = newList;
      start();
    }

    private void btnEliminar_Click(object sender, RoutedEventArgs e)
    {
      if (MessageBox.Show("¿ Quiere eliminar esta persona?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
      {
        Persona persona = (Persona)dgvPersonas.SelectedItem;
        persona.eliminar();
        List<Persona> newList = (List<Persona>)dgvPersonas.ItemsSource;
        newList.Remove(persona);
        dgvPersonas.Items.Refresh();
        dgvPersonas.ItemsSource = newList;
        start();
      }
    }
  }
}