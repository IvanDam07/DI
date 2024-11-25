using GESTPRO_IvanSobrinoCalzado.Control;
using GESTPRO_IvanSobrinoCalzado.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GESTPRO_IvanSobrinoCalzado
{
    public partial class MainWindow : Window
    {
        private List<Proyecto> proyectos;
        private DBBroker dbBroker;

        public MainWindow()
        {
            InitializeComponent();

            dbBroker = DBBroker.ObtenerAgente();
            CargarProyectosDesdeBD();
        }

        private void CargarProyectosDesdeBD()
        {
            proyectos = ObtenerProyectosDeBD();
            dgProyectos.ItemsSource = proyectos;
            dgProyectos.Items.Refresh();
        }

        private List<Proyecto> ObtenerProyectosDeBD()
        {
            string query = "SELECT CODIGOPROY, NOMBREPROY, FECHAINICIO, FECHAFIN FROM proyecto";
            var datos = dbBroker.Leer(query);
            var listaProyectos = new List<Proyecto>();

            foreach (var fila in datos)
            {
                listaProyectos.Add(new Proyecto(
                    fila[0].ToString(),
                    fila[1].ToString(),
                    Convert.ToDateTime(fila[2]),
                    Convert.ToDateTime(fila[3])
                ));
            }

            return listaProyectos;
        }

        private void bProyectos_Click(object sender, RoutedEventArgs e)
        {
            tiProyecto.IsSelected = true;
        }

        private void bEstadistica_Click(object sender, RoutedEventArgs e)
        {
            tiEstadisticas.IsSelected = true;
        }

        private void bEmpleados_Click(object sender, RoutedEventArgs e)
        {
            tiEmpleados.IsSelected = true;
        }

        private void bUsuarios_Click(object sender, RoutedEventArgs e)
        {
            tiUsuarios.IsSelected = true;
        }

        private void bEconomica_Click(object sender, RoutedEventArgs e)
        {
            tiGEconomica.IsSelected = true;
        }

        private void bAnadir_Click(object sender, RoutedEventArgs e)
        {
            if (proyectos.Any(p => p.CodigoProyecto == tbCodigo.Text))
            {
                MessageBox.Show("Ya existe un proyecto con este código.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!DateTime.TryParse(dpFechaInicio.Text, out DateTime fechaInicio) ||
                !DateTime.TryParse(dpFechaFin.Text, out DateTime fechaFin))
            {
                MessageBox.Show("Introduce fechas válidas (formato: DD/MM/YYYY).", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string query = $@"
                INSERT INTO proyecto (CODIGOPROY, NOMBREPROY, DESCPROY, FECHAINICIO, FECHAFIN)
                VALUES (
                    '{tbCodigo.Text}',
                    '{tbNombre.Text}',
                    'Descripción del {tbNombre.Text}',
                    '{fechaInicio:yyyy-MM-dd}',
                    '{fechaFin:yyyy-MM-dd}'
                )";

            int filasAfectadas = dbBroker.Modificar(query);

            if (filasAfectadas > 0)
            {
                proyectos.Add(new Proyecto(tbCodigo.Text, tbNombre.Text, fechaInicio, fechaFin));
                dgProyectos.Items.Refresh();

                tbCodigo.Clear();
                tbNombre.Clear();
                dpFechaInicio.SelectedDate = null;
                dpFechaFin.SelectedDate = null;

                MessageBox.Show("Proyecto añadido exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error al añadir el proyecto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bModificar_Click(object sender, RoutedEventArgs e)
        {
            var proyectoSeleccionado = dgProyectos.SelectedItem as Proyecto;

            if (proyectoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un proyecto para modificar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!DateTime.TryParse(dpFechaInicio.Text, out DateTime fechaInicio) ||
                !DateTime.TryParse(dpFechaFin.Text, out DateTime fechaFin))
            {
                MessageBox.Show("Introduce fechas válidas (formato: DD/MM/YYYY).", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string query = $@"
                UPDATE proyecto 
                SET NOMBREPROY = '{tbNombre.Text}', FECHAINICIO = '{fechaInicio:yyyy-MM-dd}', FECHAFIN = '{fechaFin:yyyy-MM-dd}' 
                WHERE CODIGOPROY = '{proyectoSeleccionado.CodigoProyecto}'";

            int filasAfectadas = dbBroker.Modificar(query);

            if (filasAfectadas > 0)
            {
                proyectoSeleccionado.Nombre = tbNombre.Text;
                proyectoSeleccionado.FechaInicio = fechaInicio;
                proyectoSeleccionado.FechaFin = fechaFin;

                dgProyectos.Items.Refresh();

                tbCodigo.Clear();
                tbNombre.Clear();
                dpFechaInicio.SelectedDate = null;
                dpFechaFin.SelectedDate = null;

                MessageBox.Show("Proyecto modificado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error al modificar el proyecto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bEliminar_Click(object sender, RoutedEventArgs e)
        {
            var proyectoSeleccionado = dgProyectos.SelectedItem as Proyecto;

            if (proyectoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un proyecto para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string query = $@"DELETE FROM proyecto WHERE CODIGOPROY = '{proyectoSeleccionado.CodigoProyecto}'";

            int filasAfectadas = dbBroker.Modificar(query);

            if (filasAfectadas > 0)
            {
                proyectos.Remove(proyectoSeleccionado);
                dgProyectos.Items.Refresh();
                MessageBox.Show("Proyecto eliminado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error al eliminar el proyecto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var proyectoSeleccionado = dgProyectos.SelectedItem as Proyecto;

            if (proyectoSeleccionado != null)
            {
                tbCodigo.Text = proyectoSeleccionado.CodigoProyecto;
                tbNombre.Text = proyectoSeleccionado.Nombre;
                dpFechaInicio.Text = proyectoSeleccionado.FechaInicio.ToString("dd/MM/yyyy");
                dpFechaFin.Text = proyectoSeleccionado.FechaFin.ToString("dd/MM/yyyy");
            }
        }

        private void tbBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            RealizarBusqueda();
        }

        private void tbBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RealizarBusqueda();
            }
        }

        private void RealizarBusqueda()
        {
            string searchText = tbBuscador.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                dgProyectos.ItemsSource = proyectos;
            }
            else
            {
                var filteredList = proyectos.Where(proyecto =>
                    proyecto != null &&
                    (
                        proyecto.CodigoProyecto.ToLower().Contains(searchText) ||
                        (proyecto.Nombre?.ToLower().Contains(searchText) ?? false) ||
                        proyecto.FechaInicio.ToString("dd/MM/yyyy").Contains(searchText) ||
                        proyecto.FechaFin.ToString("dd/MM/yyyy").Contains(searchText)
                    )).ToList();

                dgProyectos.ItemsSource = filteredList;
            }

            dgProyectos.Items.Refresh();
        }




        private List<Proyecto> GenerarProyectosAleatorios(int cantidad)
        {
            string[] empresas = { "SAP", "Sage", "Odoo", "Acumatica" };
            var proyectos = new List<Proyecto>();
            var random = new Random();
            int year = DateTime.Now.Year % 100; // Últimos dos dígitos del año

            for (int i = 1; i <= cantidad; i++)
            {
                string empresa = empresas[random.Next(empresas.Length)];
                string codigoBase = $"COD{empresa}{i}{year}";
                // Si la longitud supera 16, recortamos; si no, la dejamos tal cual
                string codigo = codigoBase.Length > 16 ? codigoBase.Substring(0, 16) : codigoBase;
                string nombre = $"Proyecto-{i}".Substring(0, Math.Min(32, $"Proyecto-{i}".Length)); // Máximo 32 caracteres
                DateTime fechaInicio = DateTime.Now.AddDays(random.Next(-30, 0)); // Fecha en el pasado
                DateTime fechaFin = fechaInicio.AddDays(random.Next(10, 90));   // Fecha futura

                proyectos.Add(new Proyecto(codigo, nombre, fechaInicio, fechaFin));
            }

            return proyectos;
        }



        private void bInsertar_Click(object sender, RoutedEventArgs e)
        {
            var proyectosAleatorios = GenerarProyectosAleatorios(20);
            int proyectosInsertados = 0;

            foreach (var proyecto in proyectosAleatorios)
            {
                string query = $@"
            INSERT INTO proyecto (CODIGOPROY, NOMBREPROY, DESCPROY, FECHAINICIO, FECHAFIN)
            VALUES (
                '{proyecto.CodigoProyecto}',
                '{proyecto.Nombre}',
                'Descripción del {proyecto.Nombre}',
                '{proyecto.FechaInicio:yyyy-MM-dd}',
                '{proyecto.FechaFin:yyyy-MM-dd}'
            )";

                int filasAfectadas = dbBroker.Modificar(query);

                if (filasAfectadas > 0)
                {
                    proyectosInsertados++;
                }
            }

            MessageBox.Show($"{proyectosInsertados} proyectos insertados exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Actualiza la lista de proyectos en el DataGrid
            CargarProyectosDesdeBD();
        }

    }
}
