using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using ARMauiApp.Unity;
using Com.Unity3d.Player;
using System;

namespace ARMauiApp;

[Activity(Label = "UnityActivity",
          MainLauncher = false,
          ConfigurationChanges = ConfigChanges.Mcc | ConfigChanges.Mnc | ConfigChanges.Locale | ConfigChanges.Touchscreen | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.Navigation | ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.UiMode | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.FontScale | ConfigChanges.LayoutDirection | ConfigChanges.Density,
          ResizeableActivity = false,
          LaunchMode = LaunchMode.SingleTop)]
[MetaData(name: "unityplayer.UnityActivity", Value = "true")]
[MetaData(name: "notch_support", Value = "true")]
public class UnityActivity : Activity, IUnityPlayerLifecycleEvents, INativeUnityBridge
{
    private UnityPlayerForActivityOrService player;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => {
            Android.Util.Log.Error("UNHANDLED_EX", args.Exception.StackTrace);
        };

        AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
            var exObj = args.ExceptionObject as Exception;
            Android.Util.Log.Error("DOMAIN_EX", exObj?.StackTrace ?? exObj?.Message);
        };

        System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (sender, args) => {
            Android.Util.Log.Error("TASK_EX", args.Exception.ToString());
        };
        RequestWindowFeature(WindowFeatures.NoTitle);
        base.OnCreate(savedInstanceState);
        Android.Util.Log.Error("AFTER OnCrreate()", "START");
        AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => {
            Android.Util.Log.Error("UNHANDLED_EX", args.Exception.StackTrace);
            // nếu muốn ngăn crash ngay để xem log, args.Handled = true;
        };

        AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
            var exObj = args.ExceptionObject as Exception;
            Android.Util.Log.Error("DOMAIN_EX", exObj?.StackTrace ?? exObj?.Message);
        };

        System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (sender, args) => {
            Android.Util.Log.Error("TASK_EX", args.Exception.ToString());
        };

        player = new UnityPlayerForActivityOrService(this, this);
        this.SetContentView(player.FrameLayout);
        player.FrameLayout.RequestFocus();

        var accessoryName = Intent.GetStringExtra("accessoryName");
        if (!string.IsNullOrEmpty(accessoryName))
        {
            UnityPlayer.UnitySendMessage("Bridge", "ReceiveContent", $"SetAccessories|{accessoryName}");
        }
    }

    public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
    {
        base.OnConfigurationChanged(newConfig);
        player.ConfigurationChanged(newConfig);
    }

    public override void OnWindowFocusChanged(bool hasFocus)
    {
        base.OnWindowFocusChanged(hasFocus);
        player.WindowFocusChanged(hasFocus);
    }

    protected override void OnNewIntent(Intent intent)
    {
        Android.Util.Log.Info(GetType().Name, nameof(OnNewIntent) + "|" + GetHashCode() + "|" + "Intent=" + intent.Action + "," + intent.Flags);
        Intent = intent;
        player.NewIntent(intent);
    }

    protected override void OnStop()
    {
        Android.Util.Log.Info(GetType().Name, nameof(OnStop) + "|" + GetHashCode() + "|");
        base.OnStop();
        Android.Util.Log.Info(GetType().Name, "UnityPlayer.Pause");
        player.Pause();
    }

    protected override void OnPause()
    {
        Android.Util.Log.Info(GetType().Name, nameof(OnPause) + "|" + GetHashCode() + "|");
        base.OnPause();
        Android.Util.Log.Info(GetType().Name, "UnityPlayer.Pause");
        player.Pause();
    }

    protected override void OnStart()
    {
        base.OnStart();
        player.Resume();
    }

    protected override void OnDestroy()
    {
        Android.Util.Log.Info(GetType().Name, "OnDestroy: START cleanup");
        base.OnDestroy();

        player?.Dispose();
        player = null;
        SignalUnityQuit();
        Unity.UnityBridge.RegisterNativeBridge(null);

        Android.Util.Log.Info(GetType().Name, "OnDestroy: END cleanup");
    }


    // TODO: Input events etc?

    public void OnUnityPlayerQuitted()
    {
        Android.Util.Log.Info(GetType().Name, nameof(OnUnityPlayerQuitted) + "|" + GetHashCode() + "|");
        SignalUnityQuit();
        Finish();
    }

    public void OnUnityPlayerUnloaded()
    {
        Android.Util.Log.Info(GetType().Name, nameof(OnUnityPlayerUnloaded) + "|" + GetHashCode() + "|");
        MoveTaskToBack(true);
    }

    public void SendContent(string eventName, string eventContent)
    {
        var content = eventName + "|" + (eventContent ?? string.Empty);
        UnityPlayer.UnitySendMessage("Bridge", "ReceiveContent", content);
    }

    private void SignalUnityQuit()
    {
        UnityBridge.UnityQuitTcs?.TrySetResult(true);
    }
}
