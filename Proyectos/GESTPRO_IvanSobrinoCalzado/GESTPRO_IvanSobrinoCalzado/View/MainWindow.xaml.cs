using GESTPRO_IvanSobrinoCalzado.Control;
using GESTPRO_IvanSobrinoCalzado.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GESTPRO_IvanSobrinoCalzado
{
    public partial class MainWindow : Window
    {
        private List<Proyecto> proyectos;
        private DBBroker dbBroker;
        private List<Usuario> usuarios;
        private List<Empleado> empleados;

        public MainWindow()
        {
            InitializeComponent();

            //dbBroker = DBBroker.ObtenerAgente();
            //CargarProyectosDesdeBD();

            CargarUsuariosDesdeBD();

            Usuario usuario = new Usuario();
            usuario.readUsuarios();

            dgUsuarios.ItemsSource = usuario.getListaUsuarios();
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

        /**--------------------------------------------------------------------------------------
         * 
         * 
         * 
         * USUARIOS
         * 
         * 
         * */

        //private void bAlta_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!bModificar.Content.Equals("Actualizar"))
        //    {

        //        if (MessageBox.Show("Do you want to add this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //        {
        //            string pass = tbPassword.Text;
        //            //Aquí iría encriptamiento
        //            Usuario u = new Usuario(tbUsuario.Text, tbPassword.Text);
        //            u.insert();
        //            u.last();

        //            ((List<Usuario>)dgUsuarios.ItemsSource).Add(u);
        //            dgUsuarios.Items.Refresh();
        //        }
        //    }
        //    else
        //    {
        //        Usuario u = (Usuario)dgUsuarios.SelectedItem;

        //        List<Usuario> listUsuarios = (List<Usuario>)dgUsuarios.ItemsSource;

        //        listUsuarios[dgUsuarios.SelectedIndex].nombre = tbNombre.Text;
        //        listUsuarios[dgUsuarios.SelectedIndex].password = tbPassword.Text;

        //        int idU = u.id;
        //        String nombre = u.nombre;
        //        String password = u.password;

        //        Usuario us = new Usuario(idU, nombre, password);

        //        us.update();

        //        dgUsuarios.Items.Refresh();

        //    }
        //}
        private void bAlta_Click(object sender, RoutedEventArgs e)
        {
            if (bAlta.Tag != null && bAlta.Tag.ToString() == "ActualizarPass")
            {
                // Modo actualización de contraseña
                if (string.IsNullOrWhiteSpace(tbPassword.Text))
                {
                    MessageBox.Show("Introduce una nueva contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Usuario usuarioModificar = (Usuario)dgUsuarios.SelectedItem;

                // Encriptar la contraseña antes de guardarla
                //string nuevaPassword = encriptarMD5(tbPassword.Text);

                //usuarioModificar.Password = nuevaPassword;
                usuarioModificar.Password = tbPassword.Text; // No se encripta
                usuarioModificar.update();  // Actualiza en la base de datos

                dgUsuarios.Items.Refresh(); // Refrescar la tabla de usuarios

                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Restablecer el formulario
                tbUsuario.Text = "";
                tbPassword.Text = "";
                tbUsuario.IsEnabled = true;
                bAlta.Content = "Dar de alta";
                bAlta.Tag = null;
                bEliminarUsuario.IsEnabled = true;

                return;
            }

            // Código original para dar de alta a un usuario nuevo
            if (MessageBox.Show("¿Quieres añadir este usuario?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //string pass = tbPassword.Text;
                //string passEncriptada = encriptarMD5(pass);  // Encriptar antes de insertar

                //Usuario u = new Usuario(tbUsuario.Text, passEncriptada);
                //u.insert();
                //u.last();

                Usuario u = new Usuario(tbUsuario.Text, tbPassword.Text); // No se encripta
                u.insert();
                u.last();

                ((List<Usuario>)dgUsuarios.ItemsSource).Add(u);
                dgUsuarios.Items.Refresh();
            }
        }

        private void bEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you want to remove this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Usuario usuario = (Usuario)dgUsuarios.SelectedItem;

                usuario.delete();
                List<Usuario> lstUsuario = (List<Usuario>)dgUsuarios.ItemsSource;
                lstUsuario.Remove(usuario);
                dgUsuarios.Items.Refresh();
                dgUsuarios.ItemsSource = lstUsuario;
                start();
            } 
        }

        //private void bActualizarPass_Click(object sender, RoutedEventArgs e)
        //{
        //    Usuario usuarioModificar = (Usuario)dgUsuarios.SelectedItem;

        //    tbUsuario.Text = usuarioModificar.Nombre.ToString();
        //    tbPassword.Text = usuarioModificar.Password.ToString();

        //    tbUsuario.IsEnabled = false;
        //    bAlta.IsEnabled = false;
        //    bEliminarUsuario.IsEnabled = false;

        //    bModificar.Content = "Actualizar";
        //}
        private void bActualizarPass_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioModificar = (Usuario)dgUsuarios.SelectedItem;

            if (usuarioModificar == null)
            {
                MessageBox.Show("Selecciona un usuario para actualizar la contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            tbUsuario.Text = usuarioModificar.Nombre;
            tbPassword.Text = ""; // Limpiar el campo de contraseña para que se introduzca una nueva

            tbUsuario.IsEnabled = false;  // Deshabilitar para que no se cambie el usuario
            bAlta.Content = "Actualizar"; // Cambiar el texto del botón para indicar actualización
            bAlta.Tag = "ActualizarPass"; // Marcar el botón con una etiqueta especial
        }

        private void start()
        {
            tbUsuario.Text = "";
            tbPassword.Text = "";

            dgUsuarios.SelectedItems.Clear();
        }

        private string encriptarMD5(string text)
        {
        using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        
        }


        /**
         * 
         * EMPLEADOS
         * 
         */


        private void bAnadirEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNombreEmpleado.Text) ||
                string.IsNullOrWhiteSpace(tbApellidosEmpleado.Text) ||
                string.IsNullOrWhiteSpace(tbCSREmpleado.Text) ||
                cboxRolEmpleado.SelectedItem == null ||
                cboxSeleccionarUsuario.SelectedItem == null)
            {
                MessageBox.Show("Debe completar todos los campos antes de añadir un empleado.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!float.TryParse(tbCSREmpleado.Text, out float csr))
            {
                MessageBox.Show("El campo CSR debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Usuario usuarioSeleccionado = (Usuario)cboxSeleccionarUsuario.SelectedItem;
            ComboBoxItem rolSeleccionado = (ComboBoxItem)cboxRolEmpleado.SelectedItem;
            int idRol = Convert.ToInt32(rolSeleccionado.Tag);

            Empleado nuevoEmpleado = new Empleado(tbNombreEmpleado.Text, tbApellidosEmpleado.Text, csr, usuarioSeleccionado.Id, idRol);
            nuevoEmpleado.insert();

            empleados.Add(nuevoEmpleado);
            dgEmpleados.Items.Refresh();

            MessageBox.Show("Empleado añadido correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpiar los campos
            tbNombreEmpleado.Clear();
            tbApellidosEmpleado.Clear();
            tbCSREmpleado.Clear();
            cboxRolEmpleado.SelectedIndex = -1;
            cboxSeleccionarUsuario.SelectedIndex = -1;
        }

        private void bModificarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleadoSeleccionado = (Empleado)dgEmpleados.SelectedItem;

            if (empleadoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un empleado para modificar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbNombreEmpleado.Text) ||
                string.IsNullOrWhiteSpace(tbApellidosEmpleado.Text) ||
                string.IsNullOrWhiteSpace(tbCSREmpleado.Text) ||
                cboxRolEmpleado.SelectedItem == null ||
                cboxSeleccionarUsuario.SelectedItem == null)
            {
                MessageBox.Show("Debe completar todos los campos antes de modificar un empleado.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!float.TryParse(tbCSREmpleado.Text, out float csr))
            {
                MessageBox.Show("El campo CSR debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Usuario usuarioSeleccionado = (Usuario)cboxSeleccionarUsuario.SelectedItem;
            ComboBoxItem rolSeleccionado = (ComboBoxItem)cboxRolEmpleado.SelectedItem;
            int idRol = Convert.ToInt32(rolSeleccionado.Tag);

            empleadoSeleccionado.Nombre = tbNombreEmpleado.Text;
            empleadoSeleccionado.Apellido = tbApellidosEmpleado.Text;
            empleadoSeleccionado.Csr = csr;
            empleadoSeleccionado.IdUsuario = usuarioSeleccionado.Id;
            empleadoSeleccionado.IdRol = idRol;

            empleadoSeleccionado.update();

            dgEmpleados.Items.Refresh();

            MessageBox.Show("Empleado modificado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpiar los campos
            tbNombreEmpleado.Clear();
            tbApellidosEmpleado.Clear();
            tbCSREmpleado.Clear();
            cboxRolEmpleado.SelectedIndex = -1;
            cboxSeleccionarUsuario.SelectedIndex = -1;
        }

        private void bEliminarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleadoSeleccionado = (Empleado)dgEmpleados.SelectedItem;

            if (empleadoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un empleado para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de que desea eliminar este empleado?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                empleadoSeleccionado.delete();
                empleados.Remove(empleadoSeleccionado);
                dgEmpleados.Items.Refresh();

                MessageBox.Show("Empleado eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void bRegistrarRecargarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsuarioEmpleado.Text) || string.IsNullOrWhiteSpace(tbPasswordEmpleado.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de usuario y contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Usuario nuevoUsuario = new Usuario(tbUsuarioEmpleado.Text, tbPasswordEmpleado.Text);
            nuevoUsuario.insert();

            MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Recargar el ComboBox de usuarios
            CargarUsuariosDesdeBD();

            tbUsuarioEmpleado.Clear();
            tbPasswordEmpleado.Clear();
        }

        private void CargarUsuariosDesdeBD()
        {
            Usuario usuario = new Usuario();
            usuarios = usuario.getListaUsuarios();

            cboxSeleccionarUsuario.ItemsSource = usuarios;
            cboxSeleccionarUsuario.DisplayMemberPath = "Nombre";  // Mostrar nombres de usuario en el ComboBox
            cboxSeleccionarUsuario.SelectedValuePath = "Id";      // Guardar el ID del usuario seleccionado
            cboxSeleccionarUsuario.Items.Refresh();
        }

        private void RealizarBusquedaEmpleado()
        {
            //string searchText = tbBuscarEmpleado.Text.Trim().ToLower();

            //if (string.IsNullOrWhiteSpace(searchText))
            //{
            //    dgEmpleados.ItemsSource = empleados;
            //}
            //else
            //{
            //    var filteredList = proyectos.Where(proyecto =>
            //        proyecto != null &&
            //        (
            //            proyecto.CodigoProyecto.ToLower().Contains(searchText) ||
            //            (proyecto.Nombre?.ToLower().Contains(searchText) ?? false) ||
            //            proyecto.FechaInicio.ToString("dd/MM/yyyy").Contains(searchText) ||
            //            proyecto.FechaFin.ToString("dd/MM/yyyy").Contains(searchText)
            //        )).ToList();

            //    dgProyectos.ItemsSource = filteredList;
            //}

            //dgProyectos.Items.Refresh();
        }
    }
}
