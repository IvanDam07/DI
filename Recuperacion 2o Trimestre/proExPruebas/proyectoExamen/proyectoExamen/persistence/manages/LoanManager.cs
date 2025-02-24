using proyectoExamen.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace proyectoExamen.persistence.manages
{
    internal class LoanManager
    {

        public List<Loan> loansList {  get; set; }

        public LoanManager() 
        {
            loansList = new List<Loan>();
        }

        public List<Loan> readLoans()
        {
            Loan loan = null;
            List<object> auxList;
            auxList = DBBroker.obtenerAgente().leer("SELECT * FROM Loan");

            foreach (List<Object> row in auxList)
            {
                loan = new Loan();

                loan.MemberId = Convert.ToInt32(row[0]);
                loan.BookId = Convert.ToInt32(row[1]);
                loan.LoanDate = Convert.ToDateTime(row[2]);
                loan.ReturnDate = Convert.ToDateTime(row[3]);

                loansList.Add(loan);
            }
            return loansList;
        }

        public void insertLoan(Loan loan)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string query = $"INSERT INTO Loan (Member_ID, Book_ID, loanDate, returnDate) VALUES ({loan.MemberId},{loan.BookId},'{loan.LoanDate}'," +
                $"'{loan.ReturnDate}'";
            broker.modifier(query);
            MessageBox.Show($"Prestamo insertado");
        }

        public String getBookName(int idBook)
        {
            String name = "";

            DBBroker broker = DBBroker.obtenerAgente();
            List<Object> result = DBBroker.obtenerAgente().leer($"SELECT title FROM Book WHERE ID = {idBook}");

            foreach (List<object> row in result)
            {
                name = row[0].ToString();
            }

            return name;
        }

        public String getMemberName(int idMember)
        {
            String name = "";

            DBBroker broker = DBBroker.obtenerAgente();
            List<Object> result = DBBroker.obtenerAgente().leer($"SELECT name FROM Member WHERE ID = {idMember}");

            foreach (List<object> row in result)
            {
                name = row[0].ToString();
            }

            return name;
        }


    }
}
