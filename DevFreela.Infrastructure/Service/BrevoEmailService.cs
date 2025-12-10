using DevFreela.Infrastructure.Email;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Service
{
    public class BrevoEmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly BrevoEmailSettings _settings;

        public BrevoEmailService(
            HttpClient httpClient,
            IOptions<BrevoEmailSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;

            // Base address da Brevo
            _httpClient.BaseAddress = new Uri("https://api.brevo.com/v3/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", _settings.ApiKey);
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        }

        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var payload = new
            {
                sender = new
                {
                    name = _settings.SenderName,
                    email = _settings.SenderEmail
                },
                to = new[]
                {
                    new { email = to }
                },
                subject = subject,
                htmlContent = htmlBody
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("smtp/email", content);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                // Aqui você pode logar, lançar exception específica, etc.
                throw new ApplicationException(
                    $"Erro ao enviar e-mail via Brevo. Status: {response.StatusCode}. Body: {body}");
            }
        }
    }
}
