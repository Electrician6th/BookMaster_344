using BookMaster_34.Models;
using BookMaster_34.View.Windows;
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

namespace BookMaster_34.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BrowseCatalogPage.xaml
    /// </summary>
    public partial class BrowseCatalogPage : Page
    {
        // Создаем локальный список для единоразового вытягивания данных из таблицы БД 
        private  List<Book> _books;

        //Создаем поле для хранения выбранной книги;
        private Book _selectedBook;
        public BrowseCatalogPage()
        {
            InitializeComponent();

            // Заполняем локальный список
            
            _books = App.GetContext().Books.ToList();

           
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchResultsGrid.Visibility = Visibility.Visible;
            string booktitle = BookTitleTb.Text;
            string bookauthors =BookAuthorsTb.Text;
            string booksubjects= BookSubjectsTb.Text;

            
            if (string.IsNullOrWhiteSpace(booktitle)&& string.IsNullOrWhiteSpace(bookauthors) && string.IsNullOrWhiteSpace(booksubjects))
            {
                LoadData(_books);
            }
            else
            {
                List<Book> fillerwedBooks = _books.Where(book => book.Title.Contains(booktitle,StringComparison.OrdinalIgnoreCase) && book.Authors.Contains(bookauthors, StringComparison.OrdinalIgnoreCase) && book.Subject.Contains(booksubjects, StringComparison.OrdinalIgnoreCase)).ToList();

                LoadData(fillerwedBooks);
            }
                
        }

        private void PreviewsPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadData(List <Book> booksList)
        {
            BookAuthorLv.ItemsSource = booksList;
        }

        private void BookAuthorLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBook = (Book)BookAuthorLv.SelectedItem;
            BookDotailsGrid.DataContext = _books;

            if (_selectedBook == null)
            {
                BookDotailsGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                BookDotailsGrid.Visibility=Visibility.Visible;
            }
        }

        private void BookAuthorsDetails_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBook != null)
            {
                BookAuthorsDetailsWindow BookAuthorsDetailsWindow = new BookAuthorsDetailsWindow(_selectedBook.BookAuthors);
                BookAuthorsDetailsWindow.ShowDialog();
            }
               
        }
    }
}
