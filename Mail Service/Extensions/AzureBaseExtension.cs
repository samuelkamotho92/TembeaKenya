using Mail_Service.Messaging;

namespace Mail_Service.Extensions
{
    public static class AzureBaseExtension
    {
        public static IAzureServiceBusConsumer azureServiceBusConsumer { get; set; }
        public static IApplicationBuilder useAzure(this IApplicationBuilder app)
        {
            azureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostLifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostLifeTime.ApplicationStarted.Register(OnAppStart);
            hostLifeTime.ApplicationStopping.Register(OnAppStopping);
            return app;
        }

        private static void OnAppStart()
        {
            azureServiceBusConsumer.Start();
        }

        private static void OnAppStopping()
        {
            azureServiceBusConsumer.Stop();
        }
    }
}
