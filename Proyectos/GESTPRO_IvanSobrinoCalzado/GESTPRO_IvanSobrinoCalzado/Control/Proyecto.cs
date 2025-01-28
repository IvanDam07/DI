using System;

namespace GESTPRO_IvanSobrinoCalzado.Control
{
    public class Proyecto
    {
        public string CodigoProyecto { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public Proyecto(string codigoProyecto, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            CodigoProyecto = codigoProyecto;
            Nombre = nombre;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}

/**
 * using Gestpro_MateoMolina.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Gestpro_MateoMolina.domain
{
    internal class Proyecto
    {
        public int id;
        public String codigo;
        public String nombre;
        public DateTime fecIncio;
        public DateTime fecFin;
        public proyectoPersistence pm;

        public Proyecto(string codigo, string nombre, DateTime fecIncio, DateTime fecFin)
        {

            pm = new proyectoPersistence();

            this.id = pm.lastId(this);
            this.codigo = codigo;
            this.nombre = nombre;
            this.fecIncio = fecIncio;
            this.fecFin = fecFin;

        }

        public Proyecto(int id, string codigo, string nombre, DateTime fecIni, DateTime fecFin)
        {
            pm = new proyectoPersistence();

            this.id = id;
            this.codigo = codigo;
            this.nombre = nombre;
            this.fecIncio = fecIni;
            this.fecFin = fecFin;

        }

        public Proyecto() 
        {
            
            pm = new proyectoPersistence();
            this.id = pm.lastId(this);
        }


        public int Id { get => id; set => id = value; }
        public String Codigo { get => codigo; set => codigo = value; }
        public String Nombre { get => nombre; set => nombre = value; }
        public DateTime FecInicio { get => fecIncio; set => fecIncio = value; }
        public DateTime FecFin { get => fecFin; set => fecFin = value; }
        public proyectoPersistence PM { get => pm; set => pm = value; }


        public void readProyectos()
        {
            pm.readProyectos();
        }

        public List<Proyecto> getListaProyectos()
        {
            return pm.proyectoList;
        }

        public void insert()
        {
            pm.insertProyectos(this);
        }

        public void delete()
        {
            pm.deleteProyectos(this);
        }

        public void update()
        {
            pm.modifyProyecto(this);
        }

        public void last()
        {
            pm.lastId(this);
        }




        public override string ToString()
        {
            return nombre;
        }

    }
}
*/

/**
 * proyectoPersistence
 * 
 * using Gestpro_MateoMolina.domain;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gestpro_MateoMolina.persistence
{
    internal class proyectoPersistence
    {

        public List<Proyecto> proyectoList { get; set; }
        int idDev;

        public proyectoPersistence()
        {
            proyectoList = new List<Proyecto>();
        }

        public void readProyectos()
        {

            Proyecto p = null;

            List<Object> listProyectos;

            listProyectos = DBBroker.obtenerAgente().leer("select * from PROYECTO_GESTPRO");

            foreach (List<Object> lproyectosAux in listProyectos)
            {

                p = new Proyecto();

                p.Id = Convert.ToInt32(lproyectosAux[0]);
                p.Codigo = lproyectosAux[1].ToString();
                p.Nombre = lproyectosAux[2].ToString();
                p.FecInicio = Convert.ToDateTime(lproyectosAux[3]);
                p.FecFin = Convert.ToDateTime(lproyectosAux[4]);

            }

        }


        public void insertProyectos(Proyecto p)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            broker.modifier("Insert into proyecto_gestpro (CodigoProyecto,NombreProyecto,FechaInicio,FechaFin) values" +
                "('" + p.Codigo + "', '" + p.Nombre + "','" + p.FecInicio.ToString("yyyy-MM-dd") + "','" + p.FecFin.ToString("yyyy-MM-dd") + "')");
        }


        public void deleteProyectos(Proyecto p)
        {

            DBBroker broker = DBBroker.obtenerAgente();

            broker.modifier("Delete from proyecto_gestpro where idProyecto = " + p.id);

        }


        public void modifyProyecto(Proyecto p)
        {

            DBBroker broker = DBBroker.obtenerAgente();

            broker.modifier("Update proyecto_gestpro set CodigoProyecto = " + p.codigo + ", NombreProyecto = " + p.nombre + ", FechaInicio = " + p.fecIncio.ToString("yyyy-MM-dd")
                + ", FechaFin = " + p.fecFin.ToString("yyyy-MM-dd") + " where idProyecto = " + p.id);

        }

        public int lastId(Proyecto p)
        {
            List<Object> lproyectos;
            lproyectos = DBBroker.obtenerAgente().leer("select COALESCE(MAX(IdProyecto), 0) from proyecto_gestpro");

            foreach (List<Object> lproyectosAux in lproyectos)
            {

                idDev = Convert.ToInt32(lproyectosAux[0]) + 1;

            }
            return idDev;
        }

    }
}
*/

/**
 * using System;
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
*/