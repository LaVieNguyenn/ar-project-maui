using ARProject.Helpers;
using ARProject.Services;
using ARProject.Services.UserServices;
using ARProject.ViewModels;
using ARProject.Views;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace ARProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri(ConstData.Api.BASEURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentUI");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            return builder.Build();
        }
    }
}
