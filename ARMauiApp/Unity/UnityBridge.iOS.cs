#if __IOS__
using Foundation;
using IOSBridge;

namespace ARMauiApp.Unity
{
    public class UnityBridge_iOS : INativeUnityBridge
    {
        public void SendContent(string eventName, string eventContent)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UnityBridge.ShowUnityWindow();
                UnityBridge.SendMessage("Bridge", eventName, $"SetAccessories|{eventContent}");
            });
        }
    }

    public static partial class UnityBridge
    {
        public static TaskCompletionSource<bool>? UnityQuitTcs { get; set; }
        static bool _isLoadingPageVisible = false;
        static UnityFramework framework;
        static bool _isInitialising;
        public static bool IsUnityInitialised => framework != null && framework.AppController() != null;


        public class UnityBridge_UnityFrameworkListener : UnityFrameworkListener
        {
            public override void UnityDidUnload(NSNotification notification)
            {
                ShowMainWindow();
            }

            public override void UnityDidQuit(NSNotification notification)
            {
                ShowMainWindow();
            }
        }

        public class UnityBridge_UnityContentReceiver : UnityContentReceiver
        {
            public override void OnReceiveUnityContent(string content)
            {
                var parts = (content ?? "").Split('|', 2, StringSplitOptions.None);
                var eventName = parts.Length > 0 ? parts[0] : string.Empty;
                var eventContent = parts.Length > 1 ? parts[1] : string.Empty;

                OnContentReceived?.Invoke(this, new UnityContentReceivedEventArgs(eventName, eventContent));

                if (eventName == "ShowMainWindow")
                {
                    ReturnToMaui();
                }
            }
        }

        public const string UnityFrameworkPath = "/Frameworks/UnityFramework.framework";

        private static UnityBridge_UnityFrameworkListener unityFrameworkListener = null;
        public static void ShowMainWindow()
        {
            var appDelegate = MauiUIApplicationDelegate.Current;
            appDelegate.Window.MakeKeyAndVisible();
        }

        static void ReturnToMaui()
        {
            try { framework?.Pause(true); } catch { /* ignore */ }
            ShowMainWindow();
        }

        public static void EnsureInitialised()
        {
            if (IsUnityInitialised || _isInitialising) return;

            _isInitialising = true;
            try
            {
                framework = UnityFramework.GetInstance();

                if (framework == null)
                {
                    var ufwPath = NSBundle.MainBundle.BundlePath + "/Frameworks/UnityFramework.framework";
                    using var bundle = NSBundle.FromPath(ufwPath);
                    bundle?.Load();
                    framework = UnityFramework.GetInstance();
                }

                if (framework == null)
                    throw new InvalidOperationException("Cannot get UnityFramework instance.");

                _listener ??= new UnityBridge_UnityFrameworkListener();
                framework.RegisterFrameworkListener(_listener);

                _receiver ??= new UnityBridge_UnityContentReceiver();
                Bridge.RegisterUnityContentReceiver(_receiver);

                if (framework.AppController() == null)
                {
                    framework.RunEmbedded();
                }
            }
            finally
            {
                _isInitialising = false;
            }
        }

        static UnityBridge_UnityFrameworkListener _listener;
        static UnityBridge_UnityContentReceiver _receiver;

        public static void ShowUnityWindow()
        {
            EnsureInitialised();
            try { framework?.Pause(false); } catch { /* ignore */ }
            framework?.ShowUnityWindow();
        }

        // TODO: Tear down unity?

        public static void SendMessage(string goName, string method, string message)
        {
            EnsureInitialised();
            framework?.SendMessageToGOWithName(goName, method, message);
        }

        private static bool _receiverRegistered = false;

        private static void InitialiseUnity()
        {
            if (IsUnityInitialised) return;

            framework = UnityFramework.GetInstance();

            if (framework == null)
            {
                var ufwPath = Foundation.NSBundle.MainBundle.BundlePath + UnityFrameworkPath; // "/Frameworks/UnityFramework.framework"
                var ufwBundle = Foundation.NSBundle.FromPath(ufwPath);
                if (ufwBundle == null || !ufwBundle.Load())
                {
                    System.Diagnostics.Debug.WriteLine($"[UAAL] Failed to load UnityFramework at: {ufwPath}");
                    throw new InvalidOperationException("UnityFramework.framework not found or failed to load.");
                }

                // Sau khi Load(), mới có GetInstance()
                framework = UnityFramework.GetInstance();
                if (framework == null)
                {
                    throw new InvalidOperationException("UnityFramework.GetInstance() returned null after bundle load.");
                }
            }

            var mainBundleId = Foundation.NSBundle.MainBundle.BundleIdentifier ?? "com.company.app";
            framework.SetDataBundleId(mainBundleId);

            var dataBoot = System.IO.Path.Combine(Foundation.NSBundle.MainBundle.BundlePath, "Data", "boot.config");
            var haveData = System.IO.File.Exists(dataBoot);
            System.Diagnostics.Debug.WriteLine($"[UAAL] Data present: {haveData} at {dataBoot}");

            if (unityFrameworkListener == null)
            {
                unityFrameworkListener = new UnityBridge_UnityFrameworkListener();
                framework.RegisterFrameworkListener(unityFrameworkListener);
            }
            if (!_receiverRegistered)
            {
                Bridge.RegisterUnityContentReceiver(new UnityBridge_UnityContentReceiver());
                _receiverRegistered = true;
            }

            // 5) Chạy Unity embedded
            framework.RunEmbedded();
        }
    }
}

#endif