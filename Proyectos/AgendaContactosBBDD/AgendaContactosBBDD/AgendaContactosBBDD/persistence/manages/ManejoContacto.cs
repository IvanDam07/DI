using AgendaContactosBBDD.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContactosBBDD.persistence.manages
{
    class ManejoContacto
    {
        //ATRIBUTOS
        DBBroker dbbroker;
        List<Contacto> listaContacto;
        int lastId;


//---------------------------------------------------------------------------------------------
        //CONSTRUCTOR
        public ManejoContacto()
        {
            dbbroker = DBBroker.obtenerAgente();
            
            listaContacto = new List<Contacto>();
        }

//---------------------------------------------------------------------------------------------
        //MÉTODOS

        /// <summary>
        ///     Obtienes el ultimo id de la bbdd y le añades 1
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns>
        ///     Ultimo id de la bbdd + 1
        /// </returns>
        public int getLastId(Contacto contacto)
        {
            List<Object> listaAux;
            listaAux = DBBroker.obtenerAgente().leer("SELECT COALESCE(MAX(id), 0) FROM bbddAgendaContactos.Contactos");

            foreach (List<Object> aux in listaAux)
            {
                lastId = Convert.ToInt32(aux[0]) + 1;
            }
            return lastId;
        }


        /// <summary>
        ///     Insertas un nuevo contacto
        /// </summary>
        /// <param name="contacto">
        ///     Contacto que vas a insertar en la bbdd
        /// </param>
        public void insetarNuevoContacto(Contacto contacto)
        {
            dbbroker.modificar("INSERT INTO bbddAgendaContactos.Contactos (id, nombre, telefono) VALUES ( "+contacto.Id+", '"+contacto.Nombre+"', "+contacto.Telefono+")");
        }


        /// <summary>
        ///     Lees todos los contactos de la bbdd
        /// </summary>
        /// <returns>
        ///     Lista rellena de todos los contactos de la bbdd
        /// </returns>
        public List<Contacto> leerContactos()
        {
            List<Object> listaAux = DBBroker.obtenerAgente().leer("SELECT * FROM bbddAgendaContactos.Contactos");

            foreach (List<Object> aux in listaAux)
            {
                Contacto contacto = new Contacto();
                contacto = new Contacto(Convert.ToInt32(aux[0]), aux[1].ToString(), Convert.ToInt32(aux[2]));
                listaContacto.Add(contacto);
            }
            return listaContacto;
        }


        /// <summary>
        ///     Elimina un contacto de la bbdd
        /// </summary>
        /// <param name="contacto">
        ///     Contacto que vas a eliminar de la bbdd
        /// </param>
        public void eliminarContacto(Contacto contacto)
        {
            DBBroker.obtenerAgente().modificar("DELETE FROM bbddAgendaContactos.Contactos WHERE id = " + contacto.Id + ";");
        }


        /// <summary>
        ///     Modificas un contacto de la bbdd
        /// </summary>
        /// <param name="contacto">
        ///     Contacto que vas a modificar de la bbdd
        /// </param>
        public void modificarContacto(Contacto contacto)
        {
            DBBroker.obtenerAgente().modificar("UPDATE bbddAgendaContactos.Contactos SET nombre = '" + contacto.Nombre + "', telefono = " + contacto.Telefono + ";");
        }

    }
}
