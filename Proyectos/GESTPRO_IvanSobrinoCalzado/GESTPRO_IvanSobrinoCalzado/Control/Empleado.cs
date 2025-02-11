using GESTPRO_IvanSobrinoCalzado.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    /// <summary>
    /// 
    /// </summary>
    internal class Empleado
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int id;
        /// <summary>
        /// The nombre
        /// </summary>
        public string nombre;
        /// <summary>
        /// The apellido
        /// </summary>
        public string apellido;
        /// <summary>
        /// The CSR
        /// </summary>
        public float csr;
        /// <summary>
        /// The identifier usuario
        /// </summary>
        public int idUsuario;
        /// <summary>
        /// The identifier rol
        /// </summary>
        public int idRol;
        /// <summary>
        /// The ep
        /// </summary>
        public EmpleadoPersistence ep;

        /// <summary>
        /// Initializes a new instance of the <see cref="Empleado"/> class.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        /// <param name="apellido">The apellido.</param>
        /// <param name="csr">The CSR.</param>
        /// <param name="idUsuario">The identifier usuario.</param>
        /// <param name="idRol">The identifier rol.</param>
        public Empleado(string nombre, string apellido, float csr, int idUsuario, int idRol)
        {
            ep = new EmpleadoPersistence();

            this.id = ep.lastId(this);
            this.nombre = nombre;
            this.apellido = apellido;
            this.csr = csr;
            this.idUsuario = idUsuario;
            this.idRol = idRol;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Empleado"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="apellido">The apellido.</param>
        /// <param name="csr">The CSR.</param>
        /// <param name="idUsuario">The identifier usuario.</param>
        /// <param name="idRol">The identifier rol.</param>
        /// <param name="ep">The ep.</param>
        public Empleado(int id, string nombre, string apellido, float csr, int idUsuario, int idRol, EmpleadoPersistence ep)
        {
            ep = new EmpleadoPersistence();

            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.csr = csr;        
            this.idUsuario= idUsuario;
            this.idRol = idRol;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Empleado"/> class.
        /// </summary>
        public Empleado()
        {
            ep = new EmpleadoPersistence();

            this.id = ep.lastId(this);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id {  get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string Nombre { get => nombre; set => nombre = value; }
        /// <summary>
        /// Gets or sets the apellido.
        /// </summary>
        /// <value>
        /// The apellido.
        /// </value>
        public string Apellido { get => apellido; set => apellido = value; }
        /// <summary>
        /// Gets or sets the CSR.
        /// </summary>
        /// <value>
        /// The CSR.
        /// </value>
        public float Csr { get => csr; set => csr = value; }
        /// <summary>
        /// Gets or sets the identifier usuario.
        /// </summary>
        /// <value>
        /// The identifier usuario.
        /// </value>
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        /// <summary>
        /// Gets or sets the identifier rol.
        /// </summary>
        /// <value>
        /// The identifier rol.
        /// </value>
        public int IdRol {  get => idRol; set => idRol = value; }

        /// <summary>
        /// Reads the empleados.
        /// </summary>
        public void readEmpleados()
        {
            ep.readEmpleados();
        }

        /// <summary>
        /// Gets the lista empleados.
        /// </summary>
        /// <returns></returns>
        public List<Empleado> getListaEmpleados()
        {
            return ep.empleadoList;
        }

        /// <summary>
        /// Inserts this instance.
        /// </summary>
        public void insert()
        {
            ep.insertEmpleado(this);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void delete()
        {
            ep.deleteEmpleado(this);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void update()
        {
            ep.modifyEmpleado(this);
        }
        
    }
}
