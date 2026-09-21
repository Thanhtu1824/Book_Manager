using QuanLiSach.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiSach.Repositories
{
    public class MockBookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        public MockBookRepository()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Ông Già Và Biển Cả", Description = "Description 1", Author = "Author 1", Publisher = "Publisher 1", Year = 2020, Cover = "onggiavabienca.jpg", Price = 10000 },
                new Book { Id = 2, Title = "Đi Tìm Lẽ Sống", Description = "Description 2", Author = "Author 2", Publisher = "Publisher 2", Year = 2021, Cover = "ditimlesong.jpg", Price = 10000 },
                new Book { Id = 3, Title = "Hoàng Tử Bé", Description = "Description 3", Author = "Author 3", Publisher = "Publisher 3", Year = 2022, Cover = "hoangtube.jpg", Price = 10000 },
                new Book { Id = 4, Title = "Tư Duy Nhanh và Chậm", Description = "Description 3", Author = "Author 3", Publisher = "Publisher 3", Year = 2022, Cover = "tuduynhanhvacham.jpg", Price = 10000 }
            };
        }

        // 1. Get All Async
        public Task<IEnumerable<Book>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Book>>(_books);
        }

        // 2. Get By Id Async
        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return await Task.FromResult(book);
        }

        // 3. Add Async
        public Task AddAsync(Book book)
        {
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
            return Task.CompletedTask;
        }

        // 4. Update Async
        public Task UpdateAsync(Book book)
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
                existingBook.CategoryId = book.CategoryId;
            }
            return Task.CompletedTask;
        }

        // 5. Sửa tên thành DeleteAsync để khớp với Interface
        public Task DeleteAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return Task.CompletedTask;
        }
    }
}