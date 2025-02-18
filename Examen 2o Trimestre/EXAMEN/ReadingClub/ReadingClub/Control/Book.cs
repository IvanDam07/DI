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
    internal class Book
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int id;
        /// <summary>
        /// The title
        /// </summary>
        public string title;
        /// <summary>
        /// The author
        /// </summary>
        public string author;
        /// <summary>
        /// The genre identifier
        /// </summary>
        public int genreId;
        /// <summary>
        /// The publication year
        /// </summary>
        public DateTime publicationYear;

        /// <summary>
        /// The bp
        /// </summary>
        public BookPersistence bp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="author">The author.</param>
        /// <param name="genreId">The genre identifier.</param>
        /// <param name="publicationYear">The publication year.</param>
        public Book(int id, string title, string author, int genreId, DateTime publicationYear)
        {
            bp = new BookPersistence();

            this.id = id;
            this.title = title;
            this.author = author;   
            this.genreId = genreId;
            this.publicationYear = publicationYear;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="author">The author.</param>
        /// <param name="genreId">The genre identifier.</param>
        /// <param name="publicationYear">The publication year.</param>
        public Book(string title, string author, int genreId, DateTime publicationYear)
        {
            bp = new BookPersistence();

            this.id = bp.lastId(this);
            this.title = title;
            this.author = author;
            this.genreId = genreId;
            this.publicationYear = publicationYear;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        public Book()
        {
            bp = new BookPersistence();

            this.id = bp.lastId(this);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get => title; set => title = value; }
        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get => author; set => author = value; }
        /// <summary>
        /// Gets or sets the genre identifier.
        /// </summary>
        /// <value>
        /// The genre identifier.
        /// </value>
        public int GenreId { get => genreId; set => genreId = value; }
        /// <summary>
        /// Gets or sets the publication year.
        /// </summary>
        /// <value>
        /// The publication year.
        /// </value>
        public DateTime PublicationYear { get => publicationYear; set => publicationYear = value; }

        /// <summary>
        /// Reads the books.
        /// </summary>
        public void readBooks()
        {
            bp.readBooks();
        }

        /// <summary>
        /// Gets the lista books.
        /// </summary>
        /// <returns></returns>
        public List<Book> getListaBooks()
        {
            return bp.bookList;
        }

        /// <summary>
        /// Inserts this instance.
        /// </summary>
        public void insert()
        {
            bp.insertBooks(this);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void delete()
        {
            bp.deleteBooks(this);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void update()
        {
            bp.modifyBook(this);
        }

        /// <summary>
        /// Lasts this instance.
        /// </summary>
        public void last()
        {
            bp.lastId(this);
        }
    }
}
