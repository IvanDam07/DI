using GESTPRO_IvanSobrinoCalzado.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    /// <summary>
    /// 
    /// </summary>
    internal class Usuario
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
        /// The password
        /// </summary>
        public string password;
        /// <summary>
        /// Up
        /// </summary>
        public UsuarioPersistence up;

        /// <summary>
        /// Initializes a new instance of the <see cref="Usuario"/> class.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        /// <param name="password">The password.</param>
        public Usuario(string nombre, string password)
        {
            up = new UsuarioPersistence();

            this.id = up.lastId(this);
            this.nombre = nombre;
            this.password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Usuario"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="password">The password.</param>
        public Usuario(int id, string nombre, string password)
        {
            up = new UsuarioPersistence();

            this.id = id;
            this.nombre = nombre;
            this.password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Usuario"/> class.
        /// </summary>
        public Usuario()
        {
            up = new UsuarioPersistence();

            this.id = up.lastId(this);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string Nombre { get => nombre; set => nombre = value; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get => password; set => password = value; }

        /// <summary>
        /// Reads the usuarios.
        /// </summary>
        public void readUsuarios()
        {
            up.readUsuarios();
        }

        /// <summary>
        /// Gets the lista usuarios.
        /// </summary>
        /// <returns></returns>
        public List<Usuario> getListaUsuarios()
        {            
            return up.usuarioList;
        }

        /// <summary>
        /// Inserts this instance.
        /// </summary>
        public void insert()
        {
            up.insertUsuarios(this);            
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void delete()
        {
            up.deleteUsuarios(this);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void update()
        {
            up.modifyUsuario(this);
        }

        /// <summary>
        /// Lasts this instance.
        /// </summary>
        public void last()
        {
            up.lastId(this);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return nombre;
        }
    }
}
