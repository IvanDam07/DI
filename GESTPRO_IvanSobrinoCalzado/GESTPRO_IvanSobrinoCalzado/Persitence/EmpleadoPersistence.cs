using GESTPRO_IvanSobrinoCalzado.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESTPRO_IvanSobrinoCalzado.Persitence
{
    /// <summary>
    /// 
    /// </summary>
    internal class EmpleadoPersistence
    {
        /// <summary>
        /// Gets or sets the empleado list.
        /// </summary>
        /// <value>
        /// The empleado list.
        /// </value>
        public List<Empleado> empleadoList {  get; set; }
        /// <summary>
        /// The identifier em
        /// </summary>
        int idEm;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmpleadoPersistence"/> class.
        /// </summary>
        public EmpleadoPersistence()
        {
            empleadoList = new List<Empleado>();
        }

        /// <summary>
        /// Reads the empleados.
        /// </summary>
        public void readEmpleados()
        {
            Empleado e = null;

            List<Object> listEmpleados;

            listEmpleados = DBBroker.ObtenerAgente().leer("select * from empleado");

            foreach (List<Object> lempleadosAux in  listEmpleados)
            {

                e = new Empleado();

                e.Id = Convert.ToInt32(lempleadosAux[0]);
                e.Nombre = lempleadosAux[1].ToString();
                e.Apellido = lempleadosAux[2].ToString();
                //poner rol(?)
                e.Csr =(float) Convert.ToDouble(lempleadosAux[3]);

                empleadoList.Add(e);
            }
        }

        /// <summary>
        /// Inserts the empleado.
        /// </summary>
        /// <param name="e">The e.</param>
        public void insertEmpleado(Empleado e)
        {
            DBBroker broker = DBBroker.ObtenerAgente();

            broker.Modificar("Insert into empleado (NOMBREEMP, APELLIDOSEMP, CSR, IDUSUARIO, IDROL) values" +
                "('" + e.Nombre + "', '" + e.Apellido + "', '" + e.Csr + "', '" + 1 + "', '" + 1 + "')");
            //LOS UNO HAY QUE CAMBIARLOS POR LO CORRESPONDIENTE
            //IDROL = ROL SELECCIONADO DEL COMBOBOX
            //IDUSUARIO = no lo sé
        }

        /// <summary>
        /// Deletes the empleado.
        /// </summary>
        /// <param name="e">The e.</param>
        public void deleteEmpleado(Empleado e)
        {
            DBBroker broker = DBBroker.ObtenerAgente();

            broker.Modificar("Delete from empleado where IDEMPLEADO = " + e.id);
        }

        /// <summary>
        /// Modifies the empleado.
        /// </summary>
        /// <param name="e">The e.</param>
        public void modifyEmpleado(Empleado e)
        {
            DBBroker broker = DBBroker.ObtenerAgente();

            broker.modifier("Update empleado set NOMBREEMP = " + e.nombre + ", APELLIDOSEMP = " + e.apellido + ", CSR = " + e.csr + ", IDUSUARIO = " + e.idUsuario + ", IDROL = " + e.idRol);
        }

        /// <summary>
        /// Lasts the identifier.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public int lastId(Empleado e)
        {
            List<Object> lEmpleados;
            lEmpleados = DBBroker.ObtenerAgente().leer("select COALESCE(MAX(IDEMPLEADO), 0) from empleado");

            foreach(List<Object> lempleadoAux in lEmpleados)
            {
                idEm = Convert.ToInt32(lempleadoAux[0]) + 1;
            }

            return idEm;
        }
    }
}
