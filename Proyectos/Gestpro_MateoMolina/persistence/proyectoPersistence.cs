using Gestpro_MateoMolina.domain;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gestpro_MateoMolina.persistence
{
    internal class proyectoPersistence
    {

        public List<Proyecto> proyectoList { get; set; }
        int idDev;

        public proyectoPersistence()
        {
            proyectoList = new List<Proyecto>();
        }

        public void readProyectos()
        {

            Proyecto p = null;

            List<Object> listProyectos;

            listProyectos = DBBroker.obtenerAgente().leer("select * from PROYECTO_GESTPRO");

            foreach (List<Object> lproyectosAux in listProyectos)
            {

                p = new Proyecto();

                p.Id = Convert.ToInt32(lproyectosAux[0]);
                p.Codigo = lproyectosAux[1].ToString();
                p.Nombre = lproyectosAux[2].ToString();
                p.FecInicio = Convert.ToDateTime(lproyectosAux[3]);
                p.FecFin = Convert.ToDateTime(lproyectosAux[4]);

            }

        }


        public void insertProyectos(Proyecto p)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            broker.modifier("Insert into proyecto_gestpro (CodigoProyecto,NombreProyecto,FechaInicio,FechaFin) values" +
                "('" + p.Codigo + "', '" + p.Nombre + "','" + p.FecInicio.ToString("yyyy-MM-dd") + "','" + p.FecFin.ToString("yyyy-MM-dd") + "')");
        }


        public void deleteProyectos(Proyecto p)
        {

            DBBroker broker = DBBroker.obtenerAgente();

            broker.modifier("Delete from proyecto_gestpro where idProyecto = " + p.id);

        }


        public void modifyProyecto(Proyecto p)
        {

            DBBroker broker = DBBroker.obtenerAgente();

            broker.modifier("Update proyecto_gestpro set CodigoProyecto = " + p.codigo + ", NombreProyecto = " + p.nombre + ", FechaInicio = " + p.fecIncio.ToString("yyyy-MM-dd")
                + ", FechaFin = " + p.fecFin.ToString("yyyy-MM-dd") + " where idProyecto = " + p.id);

        }

        public int lastId(Proyecto p)
        {
            List<Object> lproyectos;
            lproyectos = DBBroker.obtenerAgente().leer("select COALESCE(MAX(IdProyecto), 0) from proyecto_gestpro");

            foreach (List<Object> lproyectosAux in lproyectos)
            {

                idDev = Convert.ToInt32(lproyectosAux[0]) + 1;

            }
            return idDev;
        }

    }
}
