using Microsoft.Extensions.Logging;
using SolOricle.App.Services;


namespace SolOricle.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {





            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });






            builder.Services.AddSingleton(sp =>
            {
                // CHANGE THIS depending on target (Windows vs Android emulator)
                var baseUrl = "var baseUrl = \"https://10.0.2.2:7161/\";\r\n";

                return new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };
            });

            builder.Services.AddSingleton<JournalApiClient>();



            builder.Services.AddSingleton<MainPage>();






#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
