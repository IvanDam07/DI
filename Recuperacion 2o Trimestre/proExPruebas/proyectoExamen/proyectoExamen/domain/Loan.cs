using proyectoExamen.persistence.manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoExamen.domain
{
    internal class Loan
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public LoanManager lm;


        public Loan()
        {
            lm = new LoanManager();
        }

        public Loan(int bookId, int memberId, DateTime loanDate, DateTime returnDate)
        {
            BookId = bookId;
            MemberId = memberId;
            LoanDate = loanDate;
            ReturnDate = returnDate;

            lm = new LoanManager();
        }

        public List<Loan> readLoans()
        {
            return lm.readLoans();
        }

        public void insert()
        {
            lm.insertLoan(this);
        }

        public String getMemberName(int id)
        {
            return lm.getMemberName(id);
        }

        public String getBookName(int id)
        {
            return lm.getBookName(id);
        }

    }
}
