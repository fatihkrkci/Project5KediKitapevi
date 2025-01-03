using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using WebUI.Dtos;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> BookList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7015/api/Book");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:7015/api/Category");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

            List<SelectListItem> categories = (from x in values
                                            select new SelectListItem
                                            {
                                                Text = x.CategoryName,
                                                Value = x.CategoryId.ToString()
                                            }).ToList();
            ViewBag.Categories = categories;

            var responseMessageAuthor = await client.GetAsync("https://localhost:7015/api/Author");
            var jsonDataAuthor = await responseMessageAuthor.Content.ReadAsStringAsync();
            var valuesAuthor = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(jsonDataAuthor);
            List<SelectListItem> authors = (from x in valuesAuthor
                                               select new SelectListItem
                                               {
                                                   Text = x.FirstName + " " + x.LastName,
                                                   Value = x.AuthorId.ToString()
                                               }).ToList();
            ViewBag.Authors = authors;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7015/api/Book", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Redirect("/Admin/Book/BookList");
            }

            var errorContent = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine("API Error: " + errorContent);
            TempData["ErrorMessage"] = errorContent;
            return View();
        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7015/api/Book?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Redirect("/Admin/Book/BookList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBook(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:7015/api/Book/GetBook?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<UpdateBookDto>(jsonData);

                var responseMessageCategory = await client.GetAsync("https://localhost:7015/api/Category");
                var jsonDataCategory = await responseMessageCategory.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonDataCategory);

                List<SelectListItem> categoryList = categories.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString(),
                    Selected = x.CategoryId == book.CategoryId
                }).ToList();

                ViewBag.Categories = categoryList;

                var responseMessageAuthor = await client.GetAsync("https://localhost:7015/api/Author");
                var jsonDataAuthor = await responseMessageAuthor.Content.ReadAsStringAsync();
                var authors = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(jsonDataAuthor);

                List<SelectListItem> authorList = authors.Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.AuthorId.ToString(),
                    Selected = x.AuthorId == book.AuthorId
                }).ToList();

                ViewBag.Authors = authorList;

                return View(book);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(updateBookDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7015/api/Book", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Redirect("/Admin/Book/BookList");
            }

            var errorContent = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine("API Error: " + errorContent);
            TempData["ErrorMessage"] = errorContent;
            return View();
        }
    }
}
