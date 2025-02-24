using proyectoExamen.domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace proyectoExamen.persistence.manages
{
    internal class GenderManager
    {

        public List<Gender> gendersList {  get; set; } 

        public GenderManager()
        {
            gendersList = new List<Gender>();
        }

        public List<Gender> readGenders()
        {
            Gender gender = null;
            List<object> auxList;
            auxList = DBBroker.obtenerAgente().leer("SELECT * FROM Gender");

            foreach (List<object> row in auxList)
            {
                gender = new Gender();

                gender.Id = Convert.ToInt32(row[0]);
                gender.Name = row[1].ToString();

                gendersList.Add(gender);
            }
            return gendersList;
        }

        public int lastId()
        {

            int lastId = 0;

            List<object> result = DBBroker.obtenerAgente().leer("SELECT COALESCE(MAX(ID),0) FROM Gender");

            foreach (List<object> row in result)
            {
                lastId = Convert.ToInt32(row[0]) + 1;
            }

            return lastId;
        }

        public void insertGender(Gender gender)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string query = $"INSERT INTO Gender (name) VALUES ('{gender.Name}')";
            broker.modifier(query);
            MessageBox.Show($"Genero insertado: {gender.Name}");
        }

        public void deleteGender(Gender gender)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            String query = $"DELETE FROM Gender WHERE ID = {gender.Id}";
            broker.modifier(query);
            MessageBox.Show($"Genero eliminado: {gender.Name}");
        }

        public void updateGender(Gender gender)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            String query = $"UPDATE Gender SET name = '{gender.Name}'";
            broker.modifier(query);
        }
    }
}
