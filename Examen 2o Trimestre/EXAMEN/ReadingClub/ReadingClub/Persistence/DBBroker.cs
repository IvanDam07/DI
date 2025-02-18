using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingClub.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public class DBBroker
    {
        /// <summary>
        /// The instancia
        /// </summary>
        private static DBBroker _instancia;
        /// <summary>
        /// The conexion
        /// </summary>
        private static MySql.Data.MySqlClient.MySqlConnection conexion;
        /// <summary>
        /// The cadena conexion
        /// </summary>
        private const String cadenaConexion = "server=localhost;database=mydb;uid=root;pwd=toor"; //o toor, no lo sé

        /// <summary>
        /// Prevents a default instance of the <see cref="DBBroker"/> class from being created.
        /// </summary>
        private DBBroker()
        {
            DBBroker.conexion = new MySql.Data.MySqlClient.MySqlConnection(DBBroker.cadenaConexion);
        }

        /// <summary>
        /// Obteners the agente.
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
        /// Leers the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
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
        /// Modificars the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
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
        /// Conectars this instance.
        /// </summary>
        private void conectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Closed)
            {
                DBBroker.conexion.Open();
            }
        }
        /// <summary>
        /// Desconectars this instance.
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
