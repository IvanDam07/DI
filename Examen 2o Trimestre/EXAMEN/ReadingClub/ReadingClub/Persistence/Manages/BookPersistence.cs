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
    internal class BookPersistence
    {
        /// <summary>
        /// Gets or sets the book list.
        /// </summary>
        /// <value>
        /// The book list.
        /// </value>
        public List<Book> bookList { get; set; }
        /// <summary>
        /// The identifier book
        /// </summary>
        int idBook;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookPersistence"/> class.
        /// </summary>
        public BookPersistence()
        {
            bookList = new List<Book>();
        }

        /// <summary>
        /// Reads the books.
        /// </summary>
        public void readBooks()
        {
            Book b = null;

            List<Object> listBooks;

            listBooks = DBBroker.obtenerAgente().leer("select * from Book");

            foreach (List<Object> book in listBooks)
            {
                b = new Book();

                b.Id = Convert.ToInt32(book[0]);
                b.Title = book[1].ToString();
                b.Author = book[2].ToString();
                b.GenreId = Convert.ToInt32(book[3]);
                b.PublicationYear = Convert.ToDateTime(book[4]);

                bookList.Add(b);
            }
        }

        /// <summary>
        /// Inserts the books.
        /// </summary>
        /// <param name="b">The b.</param>
        public void insertBooks(Book b)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            broker.modificar("Insert into Book (Title, Author, GenreId, PublicationYear) values " +
                "('" + b.Title + "', '" + b.Author + "', '" + b.GenreId + "', '" + b.PublicationYear.ToString("yyyy-MM-dd") + "')");
        }

        /// <summary>
        /// Deletes the books.
        /// </summary>
        /// <param name="b">The b.</param>
        public void deleteBooks(Book b)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            broker.modificar("Delete from Book where ID = " + b.id);
        }

        /// <summary>
        /// Modifies the book.
        /// </summary>
        /// <param name="b">The b.</param>
        public void modifyBook(Book b)
        {
            DBBroker broker = DBBroker.obtenerAgente();

            string query = $"UPDATE Book SET Title = '{b.Title}', Author = '{b.Author}', GenreID = '{b.GenreId}', PublicationYear = '{b.PublicationYear}";

            broker.modificar(query);
        }

        /// <summary>
        /// Lasts the identifier.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public int lastId(Book b)
        {
            List<Object> lBooks;

            lBooks = DBBroker.obtenerAgente().leer("select COALESCE((ID), 0) from Book");

            foreach (List<Object> lBook in lBooks)
            {
                idBook = Convert.ToInt32(lBook[0]) + 1;
            }

            return idBook;
        }
    }
}
