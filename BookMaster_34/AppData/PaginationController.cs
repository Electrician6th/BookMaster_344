using BookMaster_34.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMaster_34.AppData
{

    /// <summary>
    /// Обеспечивает навигацию между книгами.
    /// </summary>
    public class PaginationController
    {
        /// <summary>
        /// Список книг.
        /// </summary>
        private List<Book> _books = new();

        /// <summary>
        /// Количество книг на странице.
        /// </summary>
        private const int PAGE_SIZE = 50;

        /// <summary>
        /// Порядковый номер страницы.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int TotatPages { get; set; }

        /// <summary>
        /// Количество книг.
        /// </summary>
        public int BooksCount => _books.Count;

        /// <summary>
        /// Определеяет возможен ли переход назад на предыдущую страницу.
        /// </summary>
        public bool CanGoPrevious => CurrentPage > 1;

        /// <summary>
        /// Определеяет возможен ли переход назад на следующую страницу.
        /// </summary>
        public bool CanGoNext => CurrentPage < TotatPages;

        public void Load(List<Book> books)
        {
            _books = books ?? new List<Book>();// если коллекция "books" пустая(null), то в коллекцию "_books" инициализируется новую коллецкию
        
            TotatPages = BooksCount == 0 ? 1 : (int)Math.Ceiling(BooksCount / (double)PAGE_SIZE);     // => Считаем кол-во

            CurrentPage=1; // => Устанавливаем номер страницы по умолчанию
        }
        public void GoToPage(int page)
        {
            CurrentPage = Math.Clamp(page, 1, TotatPages);
        }

        public  List<Book> GetCurrentPage()
        {
            return _books.Skip((CurrentPage - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
        }
    }
}
