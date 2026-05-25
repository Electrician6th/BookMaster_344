using BookMaster_34.AppData;
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

        // Создаем контроллер пагинации
        private readonly PaginationController _paginationController = new(); 

        //Создаем поле для хранения выбранной книги;
        private Book _selectedBook;
        public BrowseCatalogPage()
        {
            InitializeComponent();

            // Загружаем в контреллер пагинации список книг 
            _paginationController.Load(App.GetContext().Books.ToList());

            // Обновляем интерфейс
            RefreshUI();


        }

        

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchResultsGrid.Visibility = Visibility.Visible;
            string booktitle = BookTitleTb.Text;
            string bookauthors =BookAuthorsTb.Text;
            string booksubjects= BookSubjectsTb.Text;

            
            if (string.IsNullOrWhiteSpace(booktitle)&& string.IsNullOrWhiteSpace(bookauthors) && string.IsNullOrWhiteSpace(booksubjects))
            {
                RefreshUI();
            }
            else
            {

              

                RefreshUI();
            }
                
        }

        private void PreviewsPageBtn_Click(object sender, RoutedEventArgs e)
        {
            _paginationController.GoToPage(_paginationController.CurrentPage - 1);
            RefreshUI();
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

        private void CurrentPageTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse (CurrentPageTb.Text, out int page))
            {
                _paginationController.CurrentPage = page;
                RefreshUI();
            }
        }

        private void NextPageBtn_Click(object sender, RoutedEventArgs e)
        {
            _paginationController.GoToPage(_paginationController.CurrentPage + 1);
            RefreshUI();
        }
    
        public void RefreshUI()
        {
            BookAuthorLv.ItemsSource = _paginationController.GetCurrentPage();
            TotalBooksTbl.Text = $"Найдено {_paginationController.BooksCount} книг";
            TotalBooksTbl.Text = $"из {_paginationController.TotatPages}";
             CurrentPageTb.Text = _paginationController.CurrentPage.ToString();

            PreviewsPageBtn.IsEnabled = _paginationController.CanGoPrevious;
            NextPageBtn.IsEnabled = _paginationController.CanGoNext;
        }
    }
}
