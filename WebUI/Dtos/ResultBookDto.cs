using EntityLayer.Concrete;

namespace WebUI.Dtos
{
    public class ResultBookDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public int PageCount { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
