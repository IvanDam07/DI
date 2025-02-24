using proyectoExamen.domain;
using proyectoExamen.persistence.manages;
using proyectoExamen.reports;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace proyectoExamen
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
  {

        /// <summary>
        /// The bm
        /// </summary>
        private BookManager bm;
        /// <summary>
        /// The lm
        /// </summary>
        private LoanManager lm;
        /// <summary>
        /// The mm
        /// </summary>
        private MemberManager mm;
        /// <summary>
        /// The gm
        /// </summary>
        private GenderManager gm;

        /// <summary>
        /// The books list
        /// </summary>
        List<Book> booksList;
        /// <summary>
        /// The loan list
        /// </summary>
        List<Loan> loanList;
        /// <summary>
        /// The members list
        /// </summary>
        List<Member> membersList;
        /// <summary>
        /// The genders list
        /// </summary>
        List<Gender> gendersList;

        //tabla para el reporte
        /// <summary>
        /// The table books by gender
        /// </summary>
        DataTable tableBooksByGender;
        /// <summary>
        /// The table monthly report
        /// </summary>
        DataTable tableMonthlyReport;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            bm = new BookManager();
            lm = new LoanManager();
            mm = new MemberManager();
            gm = new GenderManager();
            

            booksList = bm.readBooks();
            loanList = lm.readLoans();
            membersList = mm.readMembers();
            gendersList = gm.readGenders();

            dgMembers.ItemsSource = membersList;
            dgBooks.ItemsSource = booksList;

            cmbGenders.ItemsSource = gendersList;

            //cargamos elementos del combobox
            cmbBoxElegirInforme.Items.Add("Books by genre");
            cmbBoxElegirInforme.Items.Add("Monthly report");
        }


        //PARA LOS REPORTES
        /// <summary>
        /// Handles the Click event of the btnCargarReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCargarReport_Click(object sender, RoutedEventArgs e)
        {

            if(cmbBoxElegirInforme.SelectedItem.Equals("Books by genre"))
            {
                //declaro la tabla
                tableBooksByGender = new DataTable("books");

                //creo las columnas
                tableBooksByGender.Columns.Add("book");
                tableBooksByGender.Columns.Add("gender");

                //añado las filas al informe
                for (int i = 0; i < booksList.Count; i++)
                {
                    DataRow row = tableBooksByGender.NewRow();

                    row["book"] = booksList[i].Title;
                    row["gender"] = booksList[i].getGenderName(booksList[i].GenreId);

                    tableBooksByGender.Rows.Add(row);
                }

                //creo una instancia para el report
                booksByGender booksReport = new booksByGender();
                booksReport.Database.Tables["books"].SetDataSource(tableBooksByGender);

                reportViewer.ViewerCore.ReportSource = booksReport;

            } else //ponemos solo else, podriamos poner comprobacion del otro elemento del cmbbox
            {
                tableMonthlyReport = new DataTable("monthlyData");

                tableMonthlyReport.Columns.Add("Month");
                tableMonthlyReport.Columns.Add("LoanDate");
                tableMonthlyReport.Columns.Add("ReturnDate");
                tableMonthlyReport.Columns.Add("MemberName");
                tableMonthlyReport.Columns.Add("BookName");

                for (int i = 0; i < loanList.Count; i++)
                {
                    DataRow row = tableMonthlyReport.NewRow();

                    row["Month"] = loanList[i].LoanDate.Month;
                    row["LoanDate"] = loanList[i].LoanDate.ToShortDateString();
                    row["ReturnDate"] = loanList[i].ReturnDate.ToShortDateString();
                    row["MemberName"] = loanList[i].getMemberName(loanList[i].MemberId);
                    row["BookName"] = loanList[i].getBookName(loanList[i].BookId);

                    tableMonthlyReport.Rows.Add(row);
                }

                monthlyReport monthlyReport = new monthlyReport();
                monthlyReport.Database.Tables["monthlyData"].SetDataSource(tableMonthlyReport);

                reportViewer.ViewerCore.ReportSource = monthlyReport;
                

            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the dgMembers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dgMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgMembers.SelectedItems.Count > 0)
            {
                Member m = (Member)dgMembers.SelectedItems[0];
                txtNameMember.Text = m.Name;
                txtEmailMember.Text = m.Email;
                txtTelephoneMember.Text = m.PhoneNumber;
                dpDateBirth.SelectedDate = m.DateBirth;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnInsertMember control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInsertMember_Click(object sender, RoutedEventArgs e)
        {
            if(dgMembers.SelectedItems.Count == 0)
            {
                if (MessageBox.Show("Do you want to add this member?", "Confirmtion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Member m = new Member(txtNameMember.Text, dpDateBirth.SelectedDate.Value, txtEmailMember.Text, txtTelephoneMember.Text);
                    m.insert();

                    ((List<Member>)dgMembers.ItemsSource).Add(m);
                    dgMembers.Items.Refresh();

                    clean();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteMember control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (dgMembers.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to remove this member?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Member m = (Member)dgMembers.SelectedItems[0];

                        m.delete();
                        List<Member> lstMembers = (List<Member>)dgMembers.ItemsSource;

                        lstMembers.Remove(m);

                        dgMembers.Items.Refresh();
                        dgMembers.ItemsSource = lstMembers;
                        clean();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, este miembro esta en la tabla historica de intercambios y no puede ser borrado");
                    }
                }
            }
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        public void clean()
        {
            txtNameMember.Text = "";
            txtEmailMember.Text = "";
            txtTelephoneMember.Text = "";
            dpDateBirth.SelectedDate = DateTime.Today;

            txtAuthorBook.Text = "";
            txtTitleBook.Text = "";
            txtYear.Text = "";
            cmbGenders.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the btnModMember control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnModMember_Click(object sender, RoutedEventArgs e)
        {
            if(dgMembers.SelectedItems.Count > 0)
            {
                Member m = (Member)dgMembers.SelectedItem;

                List<Member> lstMember = (List<Member>)dgMembers.ItemsSource;

                lstMember[dgMembers.SelectedIndex].Name = txtNameMember.Text;
                lstMember[dgMembers.SelectedIndex].DateBirth = dpDateBirth.SelectedDate.Value;
                lstMember[dgMembers.SelectedIndex].Email = txtEmailMember.Text;
                lstMember[dgMembers.SelectedIndex].PhoneNumber = txtTelephoneMember.Text;

                int idm = m.Id;
                String Name = m.Name;
                DateTime dateTime = m.DateBirth;
                String Email = m.Email;
                String PhoneNumber = m.PhoneNumber;

                Member mem = new Member(idm, Name, dateTime, Email, PhoneNumber);

                mem.update();
                
                dgMembers.Items.Refresh();

                clean();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnReturn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnInsertBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInsertBook_Click(object sender, RoutedEventArgs e)
        {
            if(dgBooks.SelectedItems.Count == 0)
            {
                if (MessageBox.Show("Do you want to add this book?", "Confirmtion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    Book b = new Book(txtTitleBook.Text, txtAuthorBook.Text, (cmbGenders.SelectedIndex + 1),Convert.ToInt32(txtYear.Text));
                    b.insert();

                    ((List<Book>)dgBooks.ItemsSource).Add(b);
                    dgBooks.Items.Refresh();

                    clean();


                }

            }
        }

        /// <summary>
        /// Handles the Click event of the btnModBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnModBook_Click(object sender, RoutedEventArgs e)
        {
            if(dgBooks.SelectedItems.Count > 0)
            {
                Book b = (Book)dgBooks.SelectedItem;

                List<Book> lstBooks = (List<Book>)dgBooks.ItemsSource;

                lstBooks[dgBooks.SelectedIndex].Title = txtTitleBook.Text;
                lstBooks[dgBooks.SelectedIndex].Author = txtAuthorBook.Text;
                lstBooks[dgBooks.SelectedIndex].GenreId = cmbGenders.SelectedIndex + 1;
                lstBooks[dgBooks.SelectedIndex].PublicationYear = Convert.ToInt32(txtYear.Text);

                int idb = b.Id;
                String Title = b.Title;
                String Author = b.Author;
                int GenreId = b.GenreId;
                int PublicationYear = b.PublicationYear;

                Book book = new Book(idb, Title, Author, GenreId, PublicationYear);

                book.update();

                dgMembers.Items.Refresh();

                clean();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (dgBooks.SelectedItems.Count > 0)
            {

                if (MessageBox.Show("Do you want to remove this book?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    try
                    {
                        Book b = (Book)dgBooks.SelectedItems[0];

                        b.delete();
                        List<Book> lstBooks = (List<Book>)dgBooks.ItemsSource;

                        lstBooks.Remove(b);

                        dgBooks.Items.Refresh();
                        dgBooks.ItemsSource = lstBooks;
                        clean();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, este libro está en un intercambio y no se puede borrar");
                    }

                    
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLoan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnLoan_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectionChanged event of the dgBooks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBooks.SelectedItems.Count > 0)
            {
                Book b = (Book)dgBooks.SelectedItems[0];
                txtTitleBook.Text = b.Title;
                txtAuthorBook.Text = b.Author;
                cmbGenders.SelectedIndex = b.GenreId;
                txtYear.Text = b.PublicationYear.ToString();
            }
        }
    }
}
