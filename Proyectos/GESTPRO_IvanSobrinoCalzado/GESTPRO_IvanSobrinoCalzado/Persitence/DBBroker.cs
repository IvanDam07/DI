using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GESTPRO_IvanSobrinoCalzado.Control;
using MySql.Data.MySqlClient;

namespace GESTPRO_IvanSobrinoCalzado.Persitence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySqlConnection conexion;
        private const string cadenaConexion = "server=localhost;database=proyectoempleado;uid=root;pwd=root"; //toor en clase

        private DBBroker()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        public static DBBroker ObtenerAgente()
        {
            if (DBBroker._instancia == null)
            {
                DBBroker._instancia = new DBBroker();
            }
            return DBBroker._instancia;
        }

        public List<List<object>> Leer(string sql)
        {
            List<List<object>> resultado = new List<List<object>>();

            try
            {
                Conectar();

                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<object> fila = new List<object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fila.Add(reader[i]);
                        }
                        resultado.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer los datos: {ex.Message}");
            }
            finally
            {
                Desconectar();
            }

            return resultado;
        }

        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<object>();
            List<Object> fila;
            int i;
            MySql.Data.MySqlClient.MySqlDataReader reader;
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, DBBroker.conexion);
            Conectar();
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
            Desconectar();
            return resultado;
        }
        public int Modificar(string sql)
        {
            int filasAfectadas = 0;

            try
            {
                Conectar();

                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar los datos: {ex.Message}");
            }
            finally
            {
                Desconectar();
            }

            return filasAfectadas;
        }

        private void Conectar()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    conexion.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
                }
            }
        }

        private void Desconectar()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al desconectar de la base de datos: {ex.Message}");
                }
            }
        }

        public void Dispose()
        {
            if (conexion != null)
            {
                conexion.Dispose();
                conexion = null;
            }
        }

        public MySqlDataReader Consultar(string query)
        {
            try
            {
                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open(); // Abre la conexión si está cerrada
                }

                using (MySqlCommand command = new MySqlCommand(query, conexion))
                {
                    return command.ExecuteReader(); // Devuelve un MySqlDataReader con los resultados
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al ejecutar la consulta: {ex.Message}");
            }
        }
        public int modifier(String sql)
        {
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, DBBroker.conexion);
            int resultado;
            Conectar();
            resultado = com.ExecuteNonQuery();
            Desconectar();
            return resultado;
        }
    }
}