﻿using GESTPRO_IvanSobrinoCalzado.Control;
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

        public MainWindow()
        {
            InitializeComponent();

            proyectos = new List<Proyecto>();

            dgProyectos.ItemsSource = proyectos;
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

            proyectos.Add(new Proyecto(tbCodigo.Text, tbNombre.Text, fechaInicio, fechaFin));
            dgProyectos.Items.Refresh();

            tbCodigo.Clear();
            tbNombre.Clear();
            dpFechaInicio.SelectedDate = null;
            dpFechaFin.SelectedDate = null;
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

            proyectoSeleccionado.CodigoProyecto = tbCodigo.Text;
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

        private void bEliminar_Click(object sender, RoutedEventArgs e)
        {
            var proyectoSeleccionado = dgProyectos.SelectedItem as Proyecto;

            if (proyectoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un proyecto para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            proyectos.Remove(proyectoSeleccionado);
            dgProyectos.Items.Refresh();
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
    }
}
