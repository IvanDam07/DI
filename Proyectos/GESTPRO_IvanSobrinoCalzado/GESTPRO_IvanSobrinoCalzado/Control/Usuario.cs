using GESTPRO_IvanSobrinoCalzado.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    internal class Usuario
    {
        public int id;
        public string nombre;
        public string password;
        public UsuarioPersistence up;

        public Usuario(string nombre, string password)
        {
            up = new UsuarioPersistence();

            this.id = up.lastId(this);
            this.nombre = nombre;
            this.password = password;
        }

        public Usuario(int id, string nombre, string password)
        {
            up = new UsuarioPersistence();

            this.id = id;
            this.nombre = nombre;
            this.password = password;
        }

        public Usuario()
        {
            up = new UsuarioPersistence();

            this.id = up.lastId(this);
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Password { get => password; set => password = value; }

        public void readUsuarios()
        {
            up.readUsuarios();
        }

        public List<Usuario> getListaUsuarios()
        {            
            return up.usuarioList;
        }

        public void insert()
        {
            up.insertUsuarios(this);            
        }

        public void delete()
        {
            up.deleteUsuarios(this);
        }

        public void update()
        {
            up.modifyUsuario(this);
        }

        public void last()
        {
            up.lastId(this);
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}
