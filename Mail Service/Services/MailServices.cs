using Mail_Service.Dto;
using MimeKit;
using MailKit.Net.Smtp;

namespace Mail_Service.Services
{
    public class MailServices
    {
        private readonly string _email;
        private readonly string _password;
        private readonly IConfiguration _configuration;

        public MailServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _email = _configuration.GetValue<string>("GmailPass:email");
            _password = _configuration.GetValue<string>("GmailPass:password");
        }
        public async Task sendEmail(UserMessageDto user,string message)
        {
            MimeMessage message1 = new MimeMessage();
            message1.From.Add(new MailboxAddress("Tembea Kenya", _email));
            message1.To.Add(new MailboxAddress(user.Name, user.Email));
            message1.Subject = "Welcome to Tembea Kenya best Tour Adventure Company";
            var body = new TextPart("html")
            {
                Text = message.ToString()
            };
            message1.Body = body;

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(_email, _password);
            await client.SendAsync(message1);
            await client.DisconnectAsync(true);
        }
    }
}
