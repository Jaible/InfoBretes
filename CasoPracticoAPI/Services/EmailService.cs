using SendGrid.Helpers.Mail;
using SendGrid;

namespace InfoBretesAPI.Services
{
    public class EmailService
    {
        public async Task SendEmail(string toEmail, string username, string nombre)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("jaiblemd@gmail.com", "InfoBretes");
            var to = new EmailAddress(toEmail, username);

            var dynamicId = "d-bea9b34c86554d038ccaa14bd2f4aec1";
            var data = new
            {
                user = username,
                email = toEmail,
                titulo = nombre,
            };

            var msg = MailHelper.CreateSingleTemplateEmail(from, to, dynamicId, data);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
