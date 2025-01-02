using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Dtos;

namespace WebUI.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeaturesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> FeatureList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7015/api/Feature");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
