using QuanLiSach.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLiSach.Models
{
    public class MockBookRepository : IBookRepository
    {
        private readonly List<Book> _books;
        public MockBookRepository()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Ông Già Và Biển Cả", Description = "Description 1", Author = "Author 1", Publisher = "Publisher 1", Year = 2020, Cover =  "onggiavabienca.jpg", Price = 10000},
                new Book { Id = 2, Title = "Đi Tìm Lẽ Sống", Description = "Description 2", Author = "Author 2", Publisher = "Publisher 2", Year = 2021, Cover = "ditimlesong.jpg", Price = 10000 },
                new Book { Id = 3, Title = "Hoàng Tử Bé", Description = "Description 3", Author = "Author 3", Publisher = "Publisher 3", Year = 2022, Cover = "hoangtube.jpg", Price = 10000 },
                new Book { Id = 4, Title = "Tư Duy Nhanh và Chậm", Description = "Description 3", Author = "Author 3", Publisher = "Publisher 3", Year = 2022, Cover = "tuduynhanhvacham.jpg", Price = 10000 }
            };
        }

        // get all
        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        //get by id
        public Book GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        //add book
        public void Add(Book book)
        {
            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
        }

        // delete book
        public void Delete(int id)
        {
           var book = _books.FirstOrDefault(b => b.Id == id);
            
            if (book != null)
            {   
                _books.Remove(book); 
            }
            
        }

        // update book
        public void Update(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Description = book.Description;
                existingBook.Author = book.Author;
                existingBook.Publisher = book.Publisher;
                existingBook.Year = book.Year;
                existingBook.Cover = book.Cover;
                existingBook.Price = book.Price;
            }
        }
    }
}
