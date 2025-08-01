namespace ARMauiApp.Helpers
{
    public static class ServiceHelper
    {
        public static T? GetService<T>()
        {
            var services = GetServiceProvider();
            return services != null ? services.GetService<T>() : default;
        }

        private static IServiceProvider? GetServiceProvider()
        {
#if WINDOWS10_0_17763_0_OR_GREATER
            return MauiWinUIApplication.Current?.Services;
#elif ANDROID
            return IPlatformApplication.Current?.Services;
#elif IOS || MACCATALYST
            return MauiUIApplicationDelegate.Current?.Services;
#else
            return null;
#endif
        }
    }
}
