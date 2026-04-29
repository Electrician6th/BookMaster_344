using BookMaster_34.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMaster_34.AppData
{
    public class PaginationController
    {
        private List<Book> _books = new();
        private const int PAGE_SIZE = 50;

        public int CurrentPage { get; set; }

        public int TotatPages { get; set; }

        public int BooksCount => _books.Count;

        public bool CanGoPrevious => CurrentPage > 1;
        public bool CanGoNext => CurrentPage < TotatPages;

    }
}
