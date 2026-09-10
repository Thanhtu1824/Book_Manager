namespace QuanLiSach.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string Cover { get; set; }
        public int Price { get; set; } = 0;
    }
}
