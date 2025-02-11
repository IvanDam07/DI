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
    internal class UsuarioPersistence
    {
        /// <summary>
        /// Gets or sets the usuario list.
        /// </summary>
        /// <value>
        /// The usuario list.
        /// </value>
        public List<Usuario> usuarioList { get; set; }
        /// <summary>
        /// The identifier us
        /// </summary>
        int idUs;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsuarioPersistence"/> class.
        /// </summary>
        public UsuarioPersistence()
        {
            usuarioList = new List<Usuario>();
        }

        /// <summary>
        /// Reads the usuarios.
        /// </summary>
        public void readUsuarios()
        {
            Usuario u = null;

            List<Object> listUsuarios;

            listUsuarios = DBBroker.ObtenerAgente().leer("select * from usuario");

            foreach (List<Object> lusuariosAux in  listUsuarios)
            {
                u = new Usuario();

                u.Id = Convert.ToInt32(lusuariosAux[0]);
                u.Nombre = lusuariosAux[1].ToString();
                u.Password = lusuariosAux[2].ToString();

                usuarioList.Add(u);
            }
        }

        /// <summary>
        /// Inserts the usuarios.
        /// </summary>
        /// <param name="u">The u.</param>
        public void insertUsuarios(Usuario u)
        {
            DBBroker broker = DBBroker.ObtenerAgente();

            broker.Modificar("Insert into usuario (NOMBREUSUARIO,PASSUSUARIO) values" +
                "('" + u.Nombre + "', '" + u.Password + "')");
        }

        /// <summary>
        /// Deletes the usuarios.
        /// </summary>
        /// <param name="u">The u.</param>
        public void deleteUsuarios(Usuario u)
        {
            DBBroker broker = DBBroker.ObtenerAgente();

            broker.Modificar("Delete from usuario where IDUSUARIO = " + u.id);
        }

        //public void modifyUsuario(Usuario u)
        //{
        //    DBBroker broker = DBBroker.ObtenerAgente();

        //    broker.modifier("Update usuario set NOMBREUSUARIO = " + u.nombre + ", PASSUSUARIO = " + u.password + " where IDUSUARIO = " + u.id);
        //}
        /// <summary>
        /// Modifies the usuario.
        /// </summary>
        /// <param name="u">The u.</param>
        public void modifyUsuario(Usuario u)
        {
            DBBroker broker = DBBroker.ObtenerAgente();

            string query = $"UPDATE usuario SET NOMBREUSUARIO = '{u.Nombre}', PASSUSUARIO = '{u.Password}' WHERE IDUSUARIO = {u.Id}";

            broker.Modificar(query);
        }

        /// <summary>
        /// Lasts the identifier.
        /// </summary>
        /// <param name="u">The u.</param>
        /// <returns></returns>
        public int lastId(Usuario u)
        {
            List<Object> lUsuarios;
            lUsuarios = DBBroker.ObtenerAgente().leer("select COALESCE(MAX(IDUSUARIO), 0) from usuario");

            foreach(List<Object> lusuarioAux in lUsuarios)
            {
                idUs = Convert.ToInt32(lusuarioAux[0]) + 1;
            }

            return idUs;
        }
    }
}
