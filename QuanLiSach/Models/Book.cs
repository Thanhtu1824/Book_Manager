using System.ComponentModel;

namespace QuanLiSach.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Cover { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public List<BookImage>? Images { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
