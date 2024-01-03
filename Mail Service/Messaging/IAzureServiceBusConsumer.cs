namespace Mail_Service.Messaging
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();

        Task Stop();

    }
}
