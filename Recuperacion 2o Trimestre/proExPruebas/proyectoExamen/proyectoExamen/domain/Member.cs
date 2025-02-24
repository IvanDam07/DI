using proyectoExamen.persistence.manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoExamen.domain
{
    internal class Member
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public MemberManager mm { get; set; }

        public Member() 
        {
            mm = new MemberManager();
        }

        public Member(int id, string name, DateTime dateBirth, string email, string phone)
        {
            this.Id = id;
            this.Name = name;
            this.DateBirth = dateBirth;
            this.Email = email;
            this.PhoneNumber = phone;

            mm = new MemberManager();
        }

        public Member(string name, DateTime dateBirth, string email, string phone)
        {
            mm = new MemberManager();

            this.Id = mm.lastId();

            this.Name = name;
            this.DateBirth = dateBirth;
            this.Email = email;
            this.PhoneNumber = phone;
        }

        public List<Member> readMembers()
        {
            return mm.readMembers();
        }

        public void insert()
        {
            mm.insertMember(this);
        }

        public void delete()
        {
            mm.deleteMember(this);
        }

        public void update()
        {
            mm.updateMember(this);
        }


    }
}
