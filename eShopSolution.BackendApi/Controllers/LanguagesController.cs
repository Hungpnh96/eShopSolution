using eShopSolution.Application.System.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        //Service
        private readonly ILanguageService _LanguageService;

        public LanguagesController(ILanguageService LanguageService)
        {
            _LanguageService = LanguageService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var language = await _LanguageService.GetAll();
            return Ok(language);
        }
    }
}
