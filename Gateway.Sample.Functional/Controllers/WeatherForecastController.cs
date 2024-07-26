using Microsoft.AspNetCore.Mvc;

namespace Gateway.Sample.Functional.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Test()
        {

            return Ok();
        }
    }
}
