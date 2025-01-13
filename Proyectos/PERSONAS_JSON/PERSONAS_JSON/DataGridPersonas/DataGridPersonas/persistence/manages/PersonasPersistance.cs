using DataGridPersonas;
using Newtonsoft.Json;

//using DataGridPersonas.persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

internal class PersonasPersistance
{
   // private DataTable table { get; set; }
    public List<Persona> PersonaList { get; set; }
    private string path;
    //public int idReturn;
    public PersonasPersistance()
    {
        //table = new DataTable();
        PersonaList = new List<Persona>();
        path = "example.json";
    }

    /// <summary>
    /// Basically those are the people that we will show on the screen
    /// </summary>
    public void readPeople()
    {
        //Read the file content
        string jsonContent = File.ReadAllText(path);

        //Deserialize the JSON content
        RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonContent);

        //Validate that the deserialized object is not null
        if (rootObject == null || rootObject.PersonaList == null)
        {
            MessageBox.Show("The JSON does not have a valid format or is empty.");
            throw new InvalidOperationException("The JSON does not have a valid format or is empty.");
        }

        Persona p = null;
        List<Persona> lpersona = rootObject.PersonaList.OrderBy(persona => persona.id).ToList();
        //lpersona = DBBroker.obtenerAgente().leer("select * from people");
        foreach (Persona aux in lpersona)
        {
            p = new Persona();
            p.id = aux.id;
            p.nombre = aux.nombre;
            p.edad = aux.edad;
            this.PersonaList.Add(p);
        }
    }

    public void insertPeople(Persona p) {
        //DBBroker broker = DBBroker.obtenerAgente();
        //broker.modifier("Insert into people (name,age) values ('"+ p.nombre +"',"+p.edad+")");
        
        //string jsonContent = File.ReadAllText(path);
        //RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonContent);

        PersonaList.Add(p);

        RootObject rootObject = new RootObject { PersonaList = this.PersonaList };

        string updatedJsonContent = JsonConvert.SerializeObject(
            rootObject, Formatting.Indented);

        File.WriteAllText(path, updatedJsonContent);

        if(p == null) 
            throw new ArgumentNullException(nameof(p), "The person Object cannot be null");

        PersonaList.Add(p);
        var rootObjecy = new RootObject { PersonaList = PersonaList };

        string jsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);

        File.WriteAllText(path, jsonContent);
    }


    //public int lastId(Persona p) {
    //    List<Object> lpeople;
    //    //lpeople = DBBroker.obtenerAgente().leer("select COALESCE(MAX(idPeople),0) from people");

    //    foreach (List<Object> aux in lpeople) { 
    //        //Recupera la ultima id y le suma 1
    //        idReturn = Convert.ToInt32(aux[0])+1;
    //    }

    //    return idReturn;
    //}

    public void deletePeople(Persona p) {
        //DBBroker broker = DBBroker.obtenerAgente();
       // broker.modifier("Delete from people where idPeople = " + p.id);
    }

    public void modifyPeople(Persona p)
    {
        //DBBroker broker = DBBroker.obtenerAgente();
        //broker.modifier("Update people set age = " + p.edad + ", name= '" + p.nombre + "' where idPeople = " + p.id);
    }

    class RootObject
    {
        [JsonProperty("people")]
        public List<Persona> PersonaList { get; set; }
    }

}
