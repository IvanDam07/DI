using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    class Proyecto
    {

        private String codigoProyecto;
        private String nombre;
        private String fechaInicio;
        private String fechaFin;        

        //Constructor 
        public Proyecto (String codigoProyecto, String nombre, String fechaInicio, String fechaFin)
        {
            this.codigoProyecto = codigoProyecto;
            this.nombre = nombre;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
        }

        public string CodigoProyecto { get => codigoProyecto; set => codigoProyecto = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public string FechaFin { get => fechaFin; set => fechaFin = value; }
    }
}
