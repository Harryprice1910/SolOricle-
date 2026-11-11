using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace SolOricle.App.ViewModels
{
    public class ExamplePromptPageViewModel : INotifyPropertyChanged
    {
        private string _prompt = string.Empty;

        public string Prompt
        {
            get => _prompt;
            set
            {
                _prompt = value;
                OnPropertyChanged();
            }
        }

        public ICommand GenerateCommand { get; }

        public ExamplePromptPageViewModel()
        {
            GenerateCommand = new Command(async () => await OnGenerate());

            GenerateCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//insight"));
        }

        public async Task OnGenerate()
        {
             Prompt = " BUTTON CLICKED";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        (message, cert, chain, errors) => true
                };

                var client = new HttpClient(handler);

                var url = "https://10.0.2.2:7161/journal";

                var request = new
                {
                    title = "AI Prompt",
                    content = Prompt
                };

                var json = System.Text.Json.JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                var responseBody = await response.Content.ReadAsStringAsync();

                Prompt = $"✅ Saved!\n{responseBody}";
            }
            catch (Exception ex)
            {
                Prompt = $"❌ ERROR: {ex.Message}";
            }






            /* try
             {
                 var client = new HttpClient();

                
                 var url = "https://10.0.2.2:5001/journal"; // Android emulator → localhost

                 var request = new
                 {
                     title = "AI Prompt",
                     content = Prompt
                 };

                 var json = JsonSerializer.Serialize(request);
                 var content = new StringContent(json, Encoding.UTF8, "application/json");

                 var response = await client.PostAsync(url, content);

                 if (response.IsSuccessStatusCode)
                 {
                     var result = await response.Content.ReadAsStringAsync();

                     // Show response (temporary)
                     Prompt = $" Sent!\n{result}";
                 }
                 else
                 {
                     Prompt = $"❌ Error: {response.StatusCode}";
                 }
             }
             catch (Exception ex)
             {
                 Prompt = $" Exception: {ex.Message}";
             }*/
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
  


    }
}