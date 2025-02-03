using AgendaContactosBBDD.persistence.manages;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContactosBBDD.domain
{
    class Contacto
    {
        //ATRIBUTOS
        private static int nextId = 1;
        private int id;
        private string nombre;
        private int telefono;
        private List<Contacto> listaContactos;
        public ManejoContacto mc;

//------------------------------------------------------------------------------------------
        //CONSTRUCTORES

        public Contacto(string nombre, int telefono)
        {
            mc = new ManejoContacto();

            this.id = mc.getLastId(this);
            this.nombre = nombre;
            this.telefono = telefono;
        }

        public Contacto(int id, string nombre, int telefono)
        {
            mc = new ManejoContacto();

            this.id = id;
            this.nombre = nombre;
            this.telefono= telefono;
        }

        public Contacto()
        {   
            mc = new ManejoContacto();

            this.id = mc.getLastId(this);
        }

//------------------------------------------------------------------------------------------
        //MÉTODOS

        /// <summary>
        ///     Insertas un contacto nuevo
        /// </summary>
        public void insertarNuevoContacto()
        {
            mc.insetarNuevoContacto(this);
        }

        
        /// <summary>
        ///     Genera una lista de Contactos ya existentes en la bbdd
        /// </summary>
        /// <returns>
        ///     Lista de Contactos que estén el la bbdd
        /// </returns>
        public List<Contacto> genListaContactos()
        {
            
            listaContactos = mc.leerContactos();

            if (listaContactos.Any())
            {
                nextId = listaContactos.Max(c => c.Id) + 1;
            }

            return listaContactos;

        }


        /// <summary>
        ///     Eliminas un contacto de la bbdd
        /// </summary>
        public void eliminar()
        {
            mc.eliminarContacto(this);

            ResetIdCounter();
        }

        
        /// <summary>
        ///     Como el id es autoincremental para q al eliminar un contacto el id se ponga 1 y haga una carga nueva del último id
        /// </summary>
        public static void ResetIdCounter()
        {
            nextId = 1;
        }


        /// <summary>
        ///     Modificas un contacto de la bbdd
        /// </summary>
        public void modificarContacto()
        {
            mc.modificarContacto(this);
        }
//------------------------------------------------------------------------------------------
        //GETTERS Y SETTERS
        public int Id { get => id; set => id = value;  }
        public string Nombre { get=> nombre; set=> nombre = value; }
        public int Telefono { get=> telefono; set=> telefono = value; }
    }
}
