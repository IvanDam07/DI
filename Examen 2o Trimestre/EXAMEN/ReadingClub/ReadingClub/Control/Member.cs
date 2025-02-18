using ReadingClub.Persistence.Manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingClub.Control
{
    /// <summary>
    /// 
    /// </summary>
    internal class Member
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int id;
        /// <summary>
        /// The name
        /// </summary>
        public string name;
        /// <summary>
        /// The date birth
        /// </summary>
        public DateTime dateBirth;
        /// <summary>
        /// The email
        /// </summary>
        public string email;
        /// <summary>
        /// The telephone
        /// </summary>
        public string telephone;

        /// <summary>
        /// The mp
        /// </summary>
        public MemberPersistence mp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="dateBirth">The date birth.</param>
        /// <param name="email">The email.</param>
        /// <param name="telephone">The telephone.</param>
        public Member(int id, string name, DateTime dateBirth, string email, string telephone)
        {
            mp = new MemberPersistence();

            this.id = id;
            this.name = name;
            this.dateBirth = dateBirth;
            this.email = email;
            this.telephone = telephone;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dateBirth">The date birth.</param>
        /// <param name="email">The email.</param>
        /// <param name="telephone">The telephone.</param>
        public Member(string name, DateTime dateBirth, string email, string telephone)
        {
            mp = new MemberPersistence();

            this.id = mp.lastId(this);
            this.name = name;
            this.dateBirth = dateBirth;
            this.email = email;
            this.telephone = telephone;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class.
        /// </summary>
        public Member()
        {
            mp = new MemberPersistence();

            this.id = mp.lastId(this);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Gets or sets the date birth.
        /// </summary>
        /// <value>
        /// The date birth.
        /// </value>
        public DateTime DateBirth { get => dateBirth; set => dateBirth = value; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get => email; set => email = value; }
        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        /// <value>
        /// The telephone.
        /// </value>
        public string Telephone { get => telephone; set => telephone = value; }

        /// <summary>
        /// Reads the members.
        /// </summary>
        public void readMembers()
        {
            mp.readMembers();
        }

        /// <summary>
        /// Gets the lista members.
        /// </summary>
        /// <returns></returns>
        public List<Member> getListaMembers()
        {
            return mp.memberList;
        }

        /// <summary>
        /// Inserts this instance.
        /// </summary>
        public void insert()
        {
            mp.insertMembers(this);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void delete()
        {
            mp.deleteMembers(this);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void update()
        {
            mp.modifyMember(this);
        }

        /// <summary>
        /// Lasts this instance.
        /// </summary>
        public void last()
        {
            mp.lastId(this);
        }
    }
}
