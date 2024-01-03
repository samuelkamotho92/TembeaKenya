
using Azure.Messaging.ServiceBus;
using Mail_Service.Dto;
using Mail_Service.Services;
using Newtonsoft.Json;
using System.Text;

namespace Mail_Service.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceBusProcessor _emailProccessor;
        private readonly string _connString;
        private readonly string _queueValue;
        private readonly MailServices _emailService;
             
        public AzureServiceBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connString = _configuration.GetValue<string>("AzureConnectionString");
            _queueValue = _configuration.GetValue<string>("ServiceBus:queueName");
            _emailService = new MailServices(configuration);
            var client = new ServiceBusClient(_connString);
            //processor
            _emailProccessor = client.CreateProcessor(_queueValue);
        }
        public async Task Start()
        {
            _emailProccessor.ProcessMessageAsync += OnRegisterUser;
            _emailProccessor.ProcessErrorAsync += ErrorHandler;
            //start procesing
            await _emailProccessor.StartProcessingAsync();
        }
        private  Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            //send the notification there is an error to admin
            return Task.CompletedTask;
        }
        private async Task OnRegisterUser(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;
            Console.WriteLine($"Check message {message.Body}");
            //DESERIALIZE to usermessage struct from string
            var body = Encoding.UTF8.GetString(message.Body);
            var user = JsonConvert.DeserializeObject<UserMessageDto>(body);
            //use the details the email that we want to set
            Console.WriteLine(user);
            try
            {
                //send email
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://www.tsavonationalparkkenya.com/wp-content/uploads/2020/04/Tsavo-National-Park.jpg\" width=\"500\" height=\"300\">");
                stringBuilder.Append($"<h1> Welcome  {user.Name} to  Tembea Kenya</h1>");
                stringBuilder.Append("<h4>Thanks for Registering </h4>");
                stringBuilder.Append("<p>Adventure the beauty that is in kenya</p>");
                await _emailService.sendEmail(user, stringBuilder.ToString());
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);

            }
        }

        public async Task Stop()
        {
            //stop the processing.
            await _emailProccessor.StopProcessingAsync();
            //clean up
            await _emailProccessor.DisposeAsync();
        }
    }
}
