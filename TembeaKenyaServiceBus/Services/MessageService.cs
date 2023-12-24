using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using TembeaKenyaServiceBus.Services.IService;

namespace TembeaKenyaServiceBus.Services
{
    public class MessageService : IMessage
    {

        //primary connection key
        private readonly string connectionStr = "Endpoint=sb://tembeakenyaservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yRjWQd0E514aSMhIANAg4I57/SX7oxChR+ASbI4JgBU=";
        ServiceBusClient client;
        ServiceBusSender sender;
        ServiceBusMessage theMessage;
        public async Task publishMessage(object message, string Tpoci_Queue_Name)
        {
            //details from entered user send to Service bus;
       client = new ServiceBusClient(connectionStr);
       sender = client.CreateSender(Tpoci_Queue_Name);
       //serialize the message we are sending to the queue 
       var messageSent = JsonConvert.SerializeObject(message);
            theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageSent))
            {
                //UNIQUE IDENTIFIER for message
                CorrelationId = Guid.NewGuid().ToString(),
            };
            //send message
            await sender.SendMessageAsync(theMessage);
            //free resources
            await sender.DisposeAsync();
        }
    }
}
