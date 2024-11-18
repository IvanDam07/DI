using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGridPersonas.persistence;
using proyectoPersonas.domain;

namespace proyectoPersonas.persistence.manages
{
  internal class ManejoPersonas
  {
     // constantes & variables =>
    private DataTable dataTable;
    private List<Persona> listaPersonas;
    int lastId;

     //————————————————————————————————————————————————————————————————————————————————————————————————————————————————————
     // constructores =>
    public ManejoPersonas()
    {
      dataTable = new DataTable();
      listaPersonas = new List<Persona>();
    }

    //————————————————————————————————————————————————————————————————————————————————————————————————————————————————————
    // metodos publicos =>
      // metodo | leerPersonas | lee todas las personas de la base de datos y las almacena en una lista que devuelve ==>
    public List<Persona> leerPersonas ()
    {
      Persona persona = new Persona();
      List<Object> listaAux = DBBroker.obtenerAgente().leer("SELECT * FROM mydb.persona;");
      foreach (List<Object> aux in listaAux)
      {
        persona = new Persona(aux[1].ToString(), aux[2].ToString(), Convert.ToInt32(aux[3]));
        listaPersonas.Add(persona);
      }

      return listaPersonas;
    }

      // metodo | genListaPersonas | genera y devuelve una lista de personas ==>
    public void insertarPersona(Persona inputPersona)
    {
      DBBroker dbBroker = DBBroker.obtenerAgente();
      dbBroker.modificar("Insert into mydb.persona (idpersona,nombre,apellido,edad) values (" +
        inputPersona.Id + ",'" +
        inputPersona.Nombre + "','" +
        inputPersona.Apellido + "'," +
        inputPersona.Edad + ");");
    }

      // metodo | getLastId | obtiene el ultimo id y le agrega 1 ==>
    public int getLastId (Persona inputPersona)
    {
      List<Object> listaAux;
      listaAux = DBBroker.obtenerAgente().leer("select MAX(idpersona) FROM mydb.persona");

      foreach (List<Object > aux in listaAux)
      {
        lastId = Convert.ToInt32(aux[0]) + 1;
      }

      return lastId;
    }

      // metodo | deletePersona | elimina una persona ==>
    public void deletePersona (Persona inputPersona)
    {
      DBBroker dBBroker = DBBroker.obtenerAgente();
      dBBroker.modificar("DELETE FROM mydb.persona WHERE idpersona = " + inputPersona.Id + ";");
    }

      // metodo | modificarPersona | modifica una persona ==>
    public void modificarPersona (Persona inputPersona)
    {
      DBBroker dbBroker = DBBroker.obtenerAgente();
      dbBroker.modificar("UPDATE mydb.persona SET nombre = '" + inputPersona.Nombre + "', apellido = '" + inputPersona.Apellido + "', edad = " + inputPersona.Edad.ToString() + " WHERE idpersona = " + inputPersona.Id + ";");
    }
  }
}
