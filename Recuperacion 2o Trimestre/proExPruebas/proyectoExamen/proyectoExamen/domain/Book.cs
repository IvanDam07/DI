using proyectoExamen.persistence.manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace proyectoExamen.domain
{
    internal class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int GenreId { get; set; }
        public int PublicationYear { get; set; }

        public BookManager bm { get; set; }


        public Book()
        {
            bm = new BookManager();
        }

        public Book(int id, string title, string author, int genreId, int publicationYear)
        {
            this.Id = id;
            this.Title = title;
            this.Author = author;
            this.GenreId = genreId;
            this.PublicationYear = publicationYear;

            bm = new BookManager();
        }

        public Book (string title, string author, int genreId, int publicationYear)
        {
            bm = new BookManager ();

            this.Id = bm.lastId();
            this.Title = title;
            this.Author = author;
            this.GenreId = genreId;
            this.PublicationYear = publicationYear;

        }


        public List<Book> readBooks()
        {
            return bm.readBooks();
        }

        public void insert()
        {
            bm.insertBook(this);
        }

        public void delete()
        {
            bm.deleteBook(this);
        }

        public void update()
        {
            bm.updateBook(this);
        }

        public string getGenderName(int idGen)
        {
            return bm.getGenderName(idGen);
        }

    }
}
