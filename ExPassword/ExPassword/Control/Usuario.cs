using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExPassword.Control
{
    class Usuario
    {
        public string Login { get; set; }
        public string Password { get; set; }

        //Constructor
        public Usuario(string login, string password)
        {
            Login = login;
            Password = password;
        }

        //Sobrecargamos el método ToString
        public override string ToString()
        {
            return Login + " " + Password;
        }
    }
}
