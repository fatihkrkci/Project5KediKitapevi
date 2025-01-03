using EntityLayer.Concrete;

namespace WebUI.Dtos
{
    public class CreateBookDto
    {
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public int PageCount { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
