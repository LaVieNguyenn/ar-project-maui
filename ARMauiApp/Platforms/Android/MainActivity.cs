using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;

namespace ARMauiApp.Platforms.Android;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
    }

    protected override void OnPause()
    {
        Log.Info(GetType().Name, nameof(OnPause) + "|" + GetHashCode());
        base.OnPause();
    }

    protected override void OnResume()
    {
        Log.Info(GetType().Name, nameof(OnResume) + "|" + GetHashCode());
        base.OnResume();
    }

    protected override void OnStart()
    {
        Log.Info(GetType().Name, nameof(OnStart) + "|" + GetHashCode());
        base.OnStart();
    }

    protected override void OnStop()
    {
        Log.Info(GetType().Name, nameof(OnStop) + "|" + GetHashCode());
        base.OnStop();
    }

    protected override void OnDestroy()
    {
        Log.Info(GetType().Name, nameof(OnDestroy) + "|" + GetHashCode());
        base.OnDestroy();
    }
}
