using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestpro_MateoMolina.persistence
{
    internal class DBBroker
    {
        private static DBBroker _instancia;
        private static MySql.Data.MySqlClient.MySqlConnection conexion;
        private const String cadenaConexion = "server=127.0.0.1;database=gestpro;uid=root;pwd=root";
        public DBBroker()
        {
            DBBroker.conexion = new MySql.Data.MySqlClient.MySqlConnection(cadenaConexion);
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
        /// Creates a connection with th DB and execute the String SQL parameter sentence
        /// </summary>
        /// <param name="sql"></param>
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
                fila = new List<Object>();
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
        /// Update in DB using the SQL input parameter
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int modifier(String sql)
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
        public void conectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Closed)
            {
                DBBroker.conexion.Open();
            }
        }
        /// <summary>
        /// Disconects the DB only if connected 
        /// </summary>
        public void desconectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Open)
            {
                DBBroker.conexion.Close();
            }
        }
    }
}
