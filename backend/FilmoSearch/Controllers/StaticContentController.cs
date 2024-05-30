using Microsoft.AspNetCore.Mvc;
using FilmoSearch.Services.ManageImageService;

namespace FilmoSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticContentController : ControllerBase
    {
        private readonly IManageImageService _manageImageService;

        public StaticContentController(IManageImageService manageImageService)
        {
            _manageImageService = manageImageService;
        }

        [HttpGet("getImg")]
        public async Task<IActionResult> GetImage(string FileName)
        {
            var result = await _manageImageService.GetImage(FileName);

            if (result.Item1 == null)
                return NotFound();

            return File(result.Item1, result.Item2, result.Item2);
        }
    }
}
