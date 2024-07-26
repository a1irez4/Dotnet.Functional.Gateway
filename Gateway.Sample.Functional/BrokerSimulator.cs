using Gateway.Sample.Functional.Controllers;

namespace Gateway.Sample.Functional
{
    public class BrokerSimulator
    {
        public static Dictionary<int, Msg> Queue = new Dictionary<int, Msg>();

        public static async Task PutMsgIntoQueue(Msg theMsg)
        {
            theMsg.RequestBody = "{Processed Successfully}";
            await Task.Delay(300);
            Queue.Add(theMsg.Id, theMsg);
        }
    }



}
