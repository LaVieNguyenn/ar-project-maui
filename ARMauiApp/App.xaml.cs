using ARMauiApp.Helpers;
using ARMauiApp.Services;

namespace ARMauiApp;

public partial class App : Application
{
    public App(AppShell shell)
    {
        InitializeComponent();
        MainPage = shell;
    }

    protected override async void OnStart()
    {
        base.OnStart();
        
        // Điều hướng đến splash page ngay lập tức
        await Shell.Current.GoToAsync("//splash");
    }
}
