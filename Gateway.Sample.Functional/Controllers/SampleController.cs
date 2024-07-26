using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Sample.Functional.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            var result = "";
            try
            {
                await HandleRequest(id, HttpContext.Request);
                result = await ListenToMessageBroker(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return NotFound(result);
            }

        }

        private static async Task HandleRequest(int id, HttpRequest request)
        {
            var theMsg = new Msg(id, await request.Body.ReadAsStringAsync(), request.Path.ToString());
            BrokerSimulator.PutMsgIntoQueue(theMsg);
        }

        private static async Task<string> ListenToMessageBroker(int id)
        {

            Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            while (!BrokerSimulator.Queue.ContainsKey(id))
            {
            }
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
            var result = BrokerSimulator.Queue[id].RequestBody;
            BrokerSimulator.Queue.Remove(id);
            return result;
        }


    }

    public class Msg(int id, string requestBody, string requestRouteValues)
    {
        public int Id {  set; get; } = id;
        public string RequestBody { set;  get; } = requestBody;
        public string RequestRouteValues = requestRouteValues;
    }

    public static class RequestExtensions
    {
        public static async Task<string> ReadAsStringAsync(this Stream requestBody, bool leaveOpen = false)
        {
            using StreamReader reader = new(requestBody, leaveOpen: leaveOpen);
            var bodyAsString = await reader.ReadToEndAsync();
            return bodyAsString;
        }
    }
}
