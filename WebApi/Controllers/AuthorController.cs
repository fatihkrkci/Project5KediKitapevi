using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult AuthorList()
        {
            var values = _authorService.TGetAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateAuthor(Author author)
        {
            _authorService.TInsert(author);
            return Ok("Ekleme Başarılı!");
        }

        [HttpDelete]
        public IActionResult DeleteAuthor(int id)
        {
            _authorService.TDelete(id);
            return Ok("Silme Başarılı!");
        }

        [HttpPut]
        public IActionResult UpdateAuthor(Author author)
        {
            _authorService.TUpdate(author);
            return Ok("Güncelleme Yapıldı!");
        }

        [HttpGet("GetAuthor")]
        public IActionResult GetAuthor(int id)
        {
            var value = _authorService.TGetByID(id);
            return Ok(value);
        }

        [HttpGet("AuthorCount")]
        public IActionResult AuthorCount()
        {
            return Ok(_authorService.TGetAuthorCount());
        }
    }
}
