using System;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    /// <summary>
    /// 
    /// </summary>
    public class Proyecto
    {
        /// <summary>
        /// Gets or sets the codigo proyecto.
        /// </summary>
        /// <value>
        /// The codigo proyecto.
        /// </value>
        public string CodigoProyecto { get; set; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string Nombre { get; set; }
        /// <summary>
        /// Gets or sets the fecha inicio.
        /// </summary>
        /// <value>
        /// The fecha inicio.
        /// </value>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Gets or sets the fecha fin.
        /// </summary>
        /// <value>
        /// The fecha fin.
        /// </value>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Proyecto"/> class.
        /// </summary>
        /// <param name="codigoProyecto">The codigo proyecto.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="fechaInicio">The fecha inicio.</param>
        /// <param name="fechaFin">The fecha fin.</param>
        public Proyecto(string codigoProyecto, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            CodigoProyecto = codigoProyecto;
            Nombre = nombre;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}