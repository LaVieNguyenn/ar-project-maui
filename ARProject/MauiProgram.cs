using ARProject.Helpers;
using ARProject.Services;
using ARProject.Services.ProductServices;
using ARProject.Services.UserServices;
using ARProject.ViewModels;
using ARProject.Views;
using ARProject.Views.RegisterStep;
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

            //Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            //

            //Login Page
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            //

            //Register Page
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterStep1ViewModel>();
            builder.Services.AddTransient<RegisterStep1>();
            builder.Services.AddTransient<RegisterStep2ViewModel>();
            builder.Services.AddTransient<RegisterStep2>();
            //

            //Me Page
            builder.Services.AddTransient<MeViewModel>();
            builder.Services.AddTransient<MePage>();
            //

            //Edit Profile
            builder.Services.AddTransient<EditProfilePage>();
            builder.Services.AddTransient<EditProfileViewModel>();

            return builder.Build();
        }
    }
}
