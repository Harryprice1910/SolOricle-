using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SolOricle.App.ViewModels
{
    public class ReflectionPageViewModel
    {
        private string _prompt = string.Empty;
        private string _insightText = string.Empty;

        public string Prompt
        {
            get => _prompt;
            set => _prompt = value;
        }

        public string InsightText
        {
            get => _insightText;
            set => _insightText = value;
        }

        public ICommand ProcessPromptCommand { get; }

        public ReflectionPageViewModel()
        {
            ProcessPromptCommand = new Command(async () => await ProcessPromptAsync());
        }

        private async Task ProcessPromptAsync()
        {
            if (string.IsNullOrWhiteSpace(Prompt))
                return;

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
                };

                using var client = new HttpClient(handler);

                var url = "https://10.0.2.2:7161/journal";

                var request = new
                {
                    title = "AI Prompt",
                    content = Prompt
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                InsightText = $" Saved!\n{responseBody}";

                // Navigate to InsightPage
                await Shell.Current.GoToAsync(nameof(SolOricle.App.views.InsightPage));
            }
            catch (Exception ex)
            {
                InsightText = $" ERROR: {ex.Message}";
            }
        }
    }
}