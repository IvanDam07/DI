using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    class Proyecto
    {
        private string codigoProyecto;
        private string nombre;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        // Constructor 
        public Proyecto(string codigoProyecto, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            this.codigoProyecto = codigoProyecto;
            this.nombre = nombre;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
        }

        public string CodigoProyecto { get => codigoProyecto; set => codigoProyecto = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
    }
}
