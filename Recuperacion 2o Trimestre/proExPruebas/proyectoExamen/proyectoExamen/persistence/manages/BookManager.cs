using proyectoExamen.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace proyectoExamen.persistence.manages
{
    /// <summary>
    /// 
    /// </summary>
    internal class BookManager
    {

        /// <summary>
        /// Gets or sets the books list.
        /// </summary>
        /// <value>
        /// The books list.
        /// </value>
        public List<Book> booksList {  get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookManager"/> class.
        /// </summary>
        public BookManager() 
        {
            booksList = new List<Book>();
        }


        /// <summary>
        /// Reads the books.
        /// </summary>
        /// <returns></returns>
        public List<Book> readBooks()
        {

            Book book = null;
            List<object> auxList;
            auxList = DBBroker.obtenerAgente().leer("SELECT * FROM Book");

            foreach (List<object> row in auxList)
            {
                book = new Book();

                book.Id = Convert.ToInt32(row[0]);
                book.Title = row[1].ToString();
                book.Author = row[2].ToString();
                book.PublicationYear = Convert.ToInt32(row[3]);
                book.GenreId = Convert.ToInt32(row[4]);

                booksList.Add(book);
            }
            return booksList;
        }

        /// <summary>
        /// Lasts the identifier.
        /// </summary>
        /// <returns></returns>
        public int lastId()
        {

            int lastId = 0;

            List<object> result = DBBroker.obtenerAgente().leer("SELECT COALESCE(MAX(ID),0) FROM Book");

            foreach (List<object> row in result)
            {
                lastId = Convert.ToInt32(row[0]) + 1;
            }

            return lastId;
        }

        /// <summary>
        /// Inserts the book.
        /// </summary>
        /// <param name="book">The book.</param>
        public void insertBook(Book book)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string query = $"INSERT INTO Book (title, author, publicationYear, Gender_ID) VALUES ('{book.Title}','{book.Author}',{book.PublicationYear},{book.GenreId})";
            broker.modifier(query);
            MessageBox.Show($"Libro insertado: {book.Title}");
        }

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="book">The book.</param>
        public void deleteBook(Book book)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            String query = $"DELETE FROM Book WHERE ID = {book.Id}";
            broker.modifier(query);
            MessageBox.Show($"Libro eliminado: {book.Title}");
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        public void updateBook(Book book)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            String query = $"UPDATE Book SET Title = '{book.Title}', Author = '{book.Author}', PublicationYear = {book.PublicationYear} " +
                $",Gender_ID = {book.GenreId}";
            broker.modifier(query);
        }

        /// <summary>
        /// Gets the name of the gender.
        /// </summary>
        /// <param name="idGender">The identifier gender.</param>
        /// <returns></returns>
        public String getGenderName(int idGender)
        {
            String name = "";

            DBBroker broker = DBBroker.obtenerAgente();
            List<Object> result = DBBroker.obtenerAgente().leer($"SELECT name FROM Gender WHERE ID = {idGender}");

            foreach(List<object> row in result)
            {
                name = row[0].ToString();
            }

            return name;
        }


    }
}
