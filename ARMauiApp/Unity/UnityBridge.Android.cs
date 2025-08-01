#if __ANDROID__

using Android.Content;
using Android.Util;
using ARMauiApp.Pages;
using ARMauiApp.Platforms.Android;
using System;
namespace ARMauiApp.Unity
{
	public static partial class UnityBridge
	{
#if __ANDROID__
        public static TaskCompletionSource<bool>? UnityQuitTcs { get; set; }
        private static bool _isLoadingPageVisible = false;
        protected class UnityContentReceiver : Com.Unity3d.Player.BaseUnityContentReceiver
		{

            protected async override void OnReceivedUnityContent(string content)
			{
				var split = content?.Split('|') ?? new string[0];
				var eventName = split.Length > 0 ? split[0] : "";
				var eventContent = split.Length > 1 ? split[1] : "";

				Android.Util.Log.Debug("UnityBridgeTest", $"OnReceivedUnityContent: {eventName}|{eventContent}");

				OnContentReceived?.Invoke(this, new UnityContentReceivedEventArgs(eventName, eventContent));

                if (eventName == "ShowMainWindow")
                {
                    UnityQuitTcs = new TaskCompletionSource<bool>();
                    if (_isLoadingPageVisible) return; // Tránh double loading
                    _isLoadingPageVisible = true;

                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        try
                        {
                            await Shell.Current.Navigation.PushModalAsync(new LoadingPage());
                            await UnityQuitTcs.Task;
                            await Task.Delay(100);
                            await Shell.Current.Navigation.PopModalAsync();
                        }
                        catch (Exception ex)
                        {
                            Log.Error("UnityBridge", "LoadingPage Exception: " + ex);
                        }
                        finally
                        {
                            _isLoadingPageVisible = false;
                        }
                        HardRestartApp();
                    });
                }
            }
		}
#endif

		public static void ShowMainWindow()
		{
			var intent = new Android.Content.Intent(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, typeof(MainActivity));
			intent.AddFlags(Android.Content.ActivityFlags.ReorderToFront | Android.Content.ActivityFlags.SingleTop);

			Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivity(intent);
		}

		public static void ShowUnityWindow()
		{
			var intent = new Android.Content.Intent(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, typeof(UnityActivity));
			intent.AddFlags(Android.Content.ActivityFlags.ReorderToFront);

			Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivity(intent);
		}

        public static void HardRestartApp()
        {
#if ANDROID
            var context = Android.App.Application.Context;
            var packageManager = context.PackageManager;
            Intent intent = packageManager.GetLaunchIntentForPackage(context.PackageName);
            ComponentName componentName = intent.Component;

            Intent mainIntent = Intent.MakeRestartActivityTask(componentName);
            mainIntent.PutExtra("navigate_to", "cart");
            mainIntent.SetPackage(context.PackageName);
            context.StartActivity(mainIntent);

            Java.Lang.Runtime.GetRuntime().Exit(0);
#endif
        }
    }
}
#endif