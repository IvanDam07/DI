using System;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    public class Proyecto
    {
        public string CodigoProyecto { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public Proyecto(string codigoProyecto, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            CodigoProyecto = codigoProyecto;
            Nombre = nombre;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
