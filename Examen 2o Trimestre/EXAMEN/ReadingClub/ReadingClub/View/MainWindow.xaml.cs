using Google.Protobuf.Collections;
using ReadingClub.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadingClub
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The LST members
        /// </summary>
        private List<Member> lstMembers;
        private List<Book> lstBooks;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Member member = new Member();
            member.readMembers();

            Book book = new Book();
            book.readBooks();
                  
            dgMembers.ItemsSource = member.getListaMembers(); //debe estar vacía
            dgBooks.ItemsSource = book.getListaBooks();
        }

        /**
         * 
         * 
         * MEMBERS
         * 
         */
        /// <summary>
        /// Handles the SelectionChanged event of the dgMembers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dgMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgMembers.SelectedItems.Count > 0)
            {
                Member member = (Member) dgMembers.SelectedItems[0];
                tbNameMember.Text = member.Name;
                dpDateBirthMember.SelectedDate = member.DateBirth;
                tbEmailMember.Text = member.Email;
                tbTelephoneMember.Text = member.Telephone;
            }
        }

        /// <summary>
        /// Handles the Click event of the bAnadirMember control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void bAnadirMember_Click(object sender, RoutedEventArgs e)
        {
            if (!bModificarMember.Content.Equals("Actualizar"))
            {
                if(MessageBox.Show("Do you want to add this member?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Member m = new Member(tbNameMember.Text, dpDateBirthMember.SelectedDate.Value, tbEmailMember.Text, tbTelephoneMember.Text);
                    m.insert();
                    m.last();

                    ((List<Member>)dgMembers.ItemsSource).Add(m);
                    dgMembers.Items.Refresh();
                }
            } else
            {
                Member mem = (Member)dgMembers.SelectedItem;

                List<Member> listMember = (List<Member>) dgMembers.ItemsSource;

                listMember[dgMembers.SelectedIndex].name = tbNameMember.Text;
                listMember[dgMembers.SelectedIndex].dateBirth = dpDateBirthMember.SelectedDate.Value;
                listMember[dgMembers.SelectedIndex].email = tbEmailMember.Text;
                listMember[dgMembers.SelectedIndex].telephone = tbTelephoneMember.Text;

                int idM = mem.id;
                String nameM = mem.name;
                DateTime dateBirthM = mem.dateBirth;
                String emailM = mem.email;
                String telephoneM = mem.telephone;

                Member m = new Member(idM, nameM, dateBirthM, emailM, telephoneM);

                m.update();

                dgMembers.Items.Refresh();
            }
        }

        /// <summary>
        /// Handles the Click event of the bModificarMember control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void bModificarMember_Click(object sender, RoutedEventArgs e)
        {
            Member modifyMember = (Member)dgMembers.SelectedItem;

            tbNameMember.Text = modifyMember.Name.ToString();
            dpDateBirthMember.SelectedDate = modifyMember.dateBirth;
            tbEmailMember.Text = modifyMember.email;
            tbTelephoneMember.Text = modifyMember.telephone;

            bModificarMember.IsEnabled = false;
            bEliminarMember.IsEnabled = false;

            bAnadirMember.Content = "Actualizar";
        }

        /// <summary>
        /// Handles the Click event of the bEliminarMember control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void bEliminarMember_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you want to remove this member?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Member member = (Member)dgMembers.SelectedItem;

                member.delete();
                List<Member> lstMember = (List<Member>)dgMembers.ItemsSource;
                lstMember.Remove(member);
                dgMembers.Items.Refresh();
                dgMembers.ItemsSource = lstMember;
                startMember();
            }
        }

        /// <summary>
        /// Starts the member.
        /// </summary>
        private void startMember()
        {
            tbNameMember.Text = "";
            dpDateBirthMember.Text = "";
            tbEmailMember.Text = "";
            tbTelephoneMember.Text = "";

            dgMembers.SelectedItems.Clear();
        }

        /**
         * 
         * BOOKS
         * 
         */
        /// <summary>
        /// Handles the SelectionChanged event of the dgBooks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBooks.SelectedItems.Count > 0)
            {
                Book book = (Book)dgBooks.SelectedItems[0];
                tbTitleBook.Text = book.Title;
                tbGenreID.Text =book.genreId.ToString();
                tbAuthorBook.Text = book.Author;
                dpPublicationYear.SelectedDate = book.PublicationYear;
            }
        }
        /// <summary>
        /// Handles the Click event of the bAnadirBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void bAnadirBook_Click(object sender, RoutedEventArgs e)
        {
            if (!bModificarBook.Content.Equals("Actualizar"))
            {
                if (MessageBox.Show("Do you want to add this book?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Book b = new Book(tbTitleBook.Text, tbAuthorBook.Text, 1, dpPublicationYear.SelectedDate.Value);
                    b.insert();
                    b.last();

                    ((List<Book>)dgBooks.ItemsSource).Add(b);
                    dgBooks.Items.Refresh();
                }
            }
            else
            {
                Book bo = (Book)dgBooks.SelectedItem;

                List<Book> listBook = (List<Book>)dgBooks.ItemsSource;

                listBook[dgBooks.SelectedIndex].title = tbTitleBook.Text;
                listBook[dgBooks.SelectedIndex].author = tbAuthorBook.Text;
                listBook[dgBooks.SelectedIndex].genreId = 1;
                listBook[dgBooks.SelectedIndex].publicationYear = dpPublicationYear.SelectedDate.Value;

                int idb = bo.id;
                String titleB = bo.title;
                String authorB = bo.author;
                int genreIDB = 1;
                DateTime publicationYearB = bo.publicationYear;

                Book b = new Book(idb, titleB, authorB, genreIDB, publicationYearB);

                b.update();

                dgBooks.Items.Refresh();
            }
        }

        /// <summary>
        /// Handles the Click event of the bModificarBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void bModificarBook_Click(object sender, RoutedEventArgs e)
        {
            Book modifyBook = (Book)dgBooks.SelectedItem;

            tbTitleBook.Text = modifyBook.Title.ToString();
            tbAuthorBook.Text = modifyBook.Author.ToString();
            tbGenreID.Text = modifyBook.GenreId.ToString();
            dpPublicationYear.SelectedDate = modifyBook.PublicationYear;

            bModificarBook.IsEnabled = false;
            bEliminarBook.IsEnabled = false;

            bAnadirBook.Content = "Actualizar";
        }

        /// <summary>
        /// Handles the Click event of the bEliminarBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void bEliminarBook_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to remove this book?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Book book = (Book)dgBooks.SelectedItem;

                book.delete();
                List<Book> lstBook = (List<Book>)dgBooks.ItemsSource;
                lstBook.Remove(book);
                dgBooks.Items.Refresh();
                dgBooks.ItemsSource = lstBook;
                startBook();
            }
        }

        /// <summary>
        /// Starts the book.
        /// </summary>
        private void startBook()
        {
            tbTitleBook.Text = "";
            tbAuthorBook.Text = "";
            tbGenreID.Text = "";
            dpPublicationYear.Text = "";

            dgBooks.SelectedItems.Clear();
        }
    }
}
