using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using ExampleMVCnoDatabase.Domain;
using ExampleMVCnoDatabase.Persistence;

namespace ExampleMVCnoDatabase.Persistence.Manages
{
    internal class PeopleManage
    {
        public List<People> listPeople { get; set; }

        public PeopleManage()
        {
            listPeople=new List<People>();
        }

        public void readPeople()
        {

            /*listPeople.Add(new People(1, "Neil",42));
            listPeople.Add(new People(2, "Jimi", 20));
            listPeople.Add(new People(3, "Valverde", 19));*/

            People p = null;
            List<Object> lpeople;
            lpeople = DBBroker.obtenerAgente().leer("select * from people order by id");
            foreach (List<Object> aux in lpeople)
            {
                p = new People(Int32.Parse(aux[0].ToString()));
                p.name = aux[1].ToString();
                p.age = Int32.Parse(aux[2].ToString());
                this.listPeople.Add(p);
            }

            //Same code using LINQ
            // List<Object> lpeople;
            // lpeople = DBBroker.obtenerAgente().leer("select * from people");
            // this.listPeople = lpeople.Cast<List<Object>>().Select(aux => new People(Int32.Parse(aux[0].ToString()))
            //{
            //    name = aux[1].ToString(),
            //    age = Int32.Parse(aux[2].ToString())
            //}).ToList().OrderBy(x => x.Id).ToList();
        }
        public void insertPeople (People p)
        {
            DBBroker dBBroker=DBBroker.obtenerAgente();
            dBBroker.modificar("Insert into people (name,age) values ('" + p.name + "',"+ p.age +")");
            MessageBox.Show("Insert into people (name,age) values ('" + p.name + "'," + p.age + ")");
        }

        public void lastId(People p)
        { 
            List<Object> lpeople;
            lpeople = DBBroker.obtenerAgente().leer("select MAX(id) FROM people");

            if (lpeople.Any())
            {
                List<Object> aux = (List<Object>)lpeople.First();
                p.Id = Convert.ToInt32(aux[0]);
            }
        }

        public void deletePeople(People p)
        {
            DBBroker dBBroker = DBBroker.obtenerAgente();
            dBBroker.modificar("Delete from people where id="+ p.Id);
        }
    }
}
