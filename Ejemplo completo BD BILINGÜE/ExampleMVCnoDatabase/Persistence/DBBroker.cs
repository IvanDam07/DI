using System;
using System.Collections.Generic;

namespace ExampleMVCnoDatabase.Persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySql.Data.MySqlClient.MySqlConnection conexion;
        private const String cadenaConexion = "server=localhost;database=people;uid=root;pwd=mysql";

        private DBBroker()
        {
            DBBroker.conexion = new MySql.Data.MySqlClient.MySqlConnection(DBBroker.cadenaConexion);

        }

        public static DBBroker obtenerAgente()
        {
            if (DBBroker._instancia == null)
            {
                DBBroker._instancia = new DBBroker();
            }
            return DBBroker._instancia;
        }

        /// <summary>
        /// Create a connection with the DB and execute the String SQL parameter sentence
        /// </summary>
        /// <param name="sql">SQL to be execute in DB for the reading</param>
        /// <returns></returns>
        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<Object>();
            MySql.Data.MySqlClient.MySqlDataReader reader;
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, DBBroker.conexion);

            conectar();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                List<Object> fila = new List<object>();

                for (int i = 0; i <= reader.FieldCount - 1; i++)
                {
                    fila.Add(reader[i].ToString());
                }
                resultado.Add(fila);
            }
            desconectar();
            return resultado;
        }

        /// <summary>
        /// Update in DB using the SQL input parameter
        /// </summary>
        /// <param name="sql">Update Sentence to be Execute in the DB</param>
        /// <returns></returns>
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
        /// Connect to the DB only if disconnected
        /// </summary>
        private void conectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Closed)
            {
                DBBroker.conexion.Open();
            }
        }

        /// <summary>
        /// Disconect de DB only if connected
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
