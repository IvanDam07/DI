using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContactosBBDD.persistence
{
    public class DBBroker
    {
        //ATRIBUTOS

        private static DBBroker _instancia;
        private static MySql.Data.MySqlClient.MySqlConnection conexion;
        private const String cadenaConexion = "server=localhost;database=bbddTPV;uid=root;pwd=mysql";

        //-------------------------------------------------------------------------------------------------------------
        //CONSTRUCTOR

        private DBBroker()
        {
            DBBroker.conexion = new MySql.Data.MySqlClient.MySqlConnection(DBBroker.cadenaConexion);
        }

        //-------------------------------------------------------------------------------------------------------------
        //MÉTODOS

        /// <summary>
        ///     Obetenemos la instancia del DBBrocker
        /// </summary>
        /// <returns></returns>
        public static DBBroker obtenerAgente()
        {
            if (DBBroker._instancia == null)
            {
                DBBroker._instancia = new DBBroker();
            }
            return DBBroker._instancia;
        }


        /// <summary>
        ///     Lee un producto de la bbdd
        /// </summary>
        /// <param name="sql">
        ///     Sentencia sql de tipo select
        /// </param>
        /// <returns></returns>
        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<object>();
            List<Object> fila;
            int i;
            MySql.Data.MySqlClient.MySqlDataReader reader;
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, DBBroker.conexion);

            conectar();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                fila = new List<object>();
                for (i = 0; i <= reader.FieldCount - 1; i++)
                {
                    fila.Add(reader[i].ToString());

                }
                resultado.Add(fila);
            }
            desconectar();
            return resultado;
        }


        /// <summary>
        ///     Cualquier modificacion en la bbdd insert y delete
        /// </summary>
        /// <param name="sql">
        ///     Sentencia sql que se va ha llevar a cabo
        /// </param>
        /// <returns>
        ///     Devuelve el numero de filas que se han modificado
        /// </returns>
        public int modificar(String sql)
        {
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, DBBroker.conexion);
            int resultado;
            conectar();
            resultado = com.ExecuteNonQuery();
            desconectar();
            return resultado;
        }


        /// <summary>
        ///     Conectar a la bbdd
        /// </summary>
        private void conectar()
        {

            if (DBBroker.conexion.State == System.Data.ConnectionState.Closed)
            {
                DBBroker.conexion.Open();
            }

        }


        /// <summary>
        ///     Desconectar de la bbdd
        /// </summary>
        private void desconectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Open)
            {
                DBBroker.conexion.Close();
            }
        }
    }
}
