using Microsoft.AspNetCore.Mvc;

namespace Gateway.Sample.Functional.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        public static Dictionary<int, HttpResponse> Responses = new Dictionary<int, HttpResponse>();

        [HttpPost]
        public async Task Post(int id)
        {
            var result = "";

            try
            {
                await Console.Out.WriteLineAsync("Do some biz logic");

                Responses.Add(id, Response);

                await Console.Out.WriteLineAsync("try part");

                result = await DoProcessing(id, HttpContext.Response);


            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"{e}");
            }

        }

        private async Task<string> DoProcessing(int id, HttpResponse response)
        {
            while (Responses.ContainsKey(id) == true)
            {
                //await Console.Out.WriteLineAsync("Listening .....");
            }
            return "My result";
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await SendResponse(id);
            return Ok("ok");
        }

        private async Task SendResponse(int id)
        {
            await Responses[id].WriteAsync($"Response {id} Okey shod");
            Responses.Remove(id);

        }
    }
}