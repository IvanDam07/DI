using ReadingClub.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingClub.Persistence.Manages
{
    /// <summary>
    /// 
    /// </summary>
    internal class MemberPersistence
    {

        /// <summary>
        /// Gets or sets the member list.
        /// </summary>
        /// <value>
        /// The member list.
        /// </value>
        public List<Member> memberList { get; set; }
        /// <summary>
        /// The identifier memory
        /// </summary>
        int idMem;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberPersistence"/> class.
        /// </summary>
        public MemberPersistence()
        {
            memberList = new List<Member>();
        }

        /// <summary>
        /// Reads the members.
        /// </summary>
        public void readMembers()
        {
            Member m = null;

            List<Object> listMembers;

            listMembers = DBBroker.obtenerAgente().leer("select * from member");

            foreach (List<Object> lmemberAux in listMembers)
            {
                m = new Member();

                m.Id = Convert.ToInt32(lmemberAux[0]);
                m.Name = lmemberAux[1].ToString();
                m.DateBirth = Convert.ToDateTime(lmemberAux[2]);
                m.Email = lmemberAux[3].ToString();
                m.Telephone = lmemberAux[4].ToString();

                memberList.Add(m);
            }
        }

        /// <summary>
        /// Inserts the members.
        /// </summary>
        /// <param name="m">The m.</param>
        public void insertMembers(Member m)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            broker.modificar("Insert into member (Name,DateBirth,Email,Telephone) values " +
                "('" + m.Name + "', '" + m.DateBirth.ToString("yyyy-MM-dd") + "', '" + m.Email + "', '" + m.Telephone + "')");
        }

        /// <summary>
        /// Deletes the members.
        /// </summary>
        /// <param name="m">The m.</param>
        public void deleteMembers(Member m)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            broker.modificar("Delete from member where ID = " + m.id);
        }

        /// <summary>
        /// Modifies the member.
        /// </summary>
        /// <param name="m">The m.</param>
        public void modifyMember(Member m)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            broker.modificar("Update member set Name = " + m.Name + ", DateBirth = " + m.DateBirth.ToString("yyyy-MM-dd") + ", Email = " + m.Email + ", Telephone = " + m.Telephone);            
        }

        /// <summary>
        /// Lasts the identifier.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        public int lastId(Member m)
        {
            List<Object> lMembers;
            lMembers = DBBroker.obtenerAgente().leer("select COALESCE((ID), 0) from member");

            foreach (List<Object> lMember in lMembers)
            {
                idMem = Convert.ToInt32(lMember[0]) + 1;
            }

            return idMem;
        }
    }
}
