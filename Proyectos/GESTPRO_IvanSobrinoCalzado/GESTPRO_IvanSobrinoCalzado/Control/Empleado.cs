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
    internal class Empleado
    {
        public int id;
        public string nombre;
        public string apellido;
        public float csr;
        public int idUsuario;
        public int idRol;
        public EmpleadoPersistence ep;

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

        public Empleado()
        {
            ep = new EmpleadoPersistence();

            this.id = ep.lastId(this);
        }

        public int Id {  get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public float Csr { get => csr; set => csr = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int IdRol {  get => idRol; set => idRol = value; }

        public void readEmpleados()
        {
            ep.readEmpleados();
        }

        public List<Empleado> getListaEmpleados()
        {
            return ep.empleadoList;
        }

        public void insert()
        {
            ep.insertEmpleado(this);
        }

        public void delete()
        {
            ep.deleteEmpleado(this);
        }

        public void update()
        {
            ep.modifyEmpleado(this);
        }
        
    }
}
