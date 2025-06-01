using ARProject.BLL.Services.UserService;
using ARProject.DAL.Helper;
using ARProject.DAL.UnitOfWork.Authentication;
using ARProject.DAL.UnitOfWork.Storage;
using ARProject.ViewModels;
using ARProject.Views;
using Microsoft.Extensions.Logging;

namespace ARProject
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //Services
            builder.Services.AddSingleton<IAuthUnitOfWork>(_ =>
                new AuthUnitOfWork(DbNameHelper.DbConnectionString.AUTH));
            builder.Services.AddSingleton<IStorageUnitOfWork>(_ =>
                new StorageUnitOfWork("mongodb://localhost:27017/"));
            //mongodb://localhost:27017/
            //mongodb://10.0.2.2:27017/
            builder.Services.AddSingleton<IUserService, UserService>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            return builder.Build();
        }
    }
}
