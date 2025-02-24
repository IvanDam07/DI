using proyectoExamen.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace proyectoExamen.persistence.manages
{
    internal class MemberManager
    {

        public List<Member> membersList { get; set; }

        public MemberManager()
        {
            membersList = new List<Member>();
        }


        public List<Member> readMembers()
        {

            Member member = null;
            List<object> auxList;
            auxList = DBBroker.obtenerAgente().leer("SELECT * FROM Member");

            foreach (List<object> row in auxList)
            {
                member = new Member();

                member.Id = Convert.ToInt32(row[0]);
                member.Name = row[1].ToString();
                member.DateBirth = Convert.ToDateTime(row[2]);
                member.Email = row[3].ToString();
                member.PhoneNumber = row[4].ToString();

                membersList.Add(member);
            }
            return membersList;
        }

        public int lastId()
        {

            int lastId = 0;

            List<object> result = DBBroker.obtenerAgente().leer("SELECT COALESCE(MAX(ID),0) FROM Member");

            foreach (List<object> row in result)
            {
                lastId = Convert.ToInt32(row[0]) + 1;
            }

            return lastId;
        }

        public void insertMember(Member member)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string query = $"INSERT INTO Member (name, datebirth, email, telephone) VALUES ('{member.Name}','{member.DateBirth.ToString("yyyy-MM-dd")}'" +
                $",'{member.Email}','{member.PhoneNumber}')";
            broker.modifier(query);
            MessageBox.Show($"Miembro insertado: {member.Name}");
        }

        public void deleteMember(Member member)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            String query = $"DELETE FROM Member WHERE ID = {member.Id}";
            broker.modifier(query);
            MessageBox.Show($"Miembro eliminado: {member.Name}");
        }

        public void updateMember(Member member)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            String query = $"UPDATE Member SET name = '{member.Name}', datebirth = '{member.DateBirth.ToString("yyyy-MM-dd")}', email = '{member.Email}' " +
                $",telephone = '{member.PhoneNumber}'";
            broker.modifier(query);
        }

    }
}
