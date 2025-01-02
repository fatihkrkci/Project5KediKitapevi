using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public IActionResult FeatureList()
        {
            var values = _featureService.TGetAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateFeature(Feature feature)
        {
            _featureService.TInsert(feature);
            return Ok("Ekleme Başarılı!");
        }

        [HttpDelete]
        public IActionResult DeleteFeature(int id)
        {
            _featureService.TDelete(id);
            return Ok("Silme Başarılı!");
        }

        [HttpPut]
        public IActionResult UpdateFeature(Feature feature)
        {
            _featureService.TUpdate(feature);
            return Ok("Güncelleme Yapıldı!");
        }

        [HttpGet("GetFeature")]
        public IActionResult GetFeature(int id)
        {
            var value = _featureService.TGetByID(id);
            return Ok(value);
        }
    }
}
