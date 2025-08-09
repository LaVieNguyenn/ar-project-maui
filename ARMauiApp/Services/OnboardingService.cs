using Microsoft.Maui.Storage;

namespace ARMauiApp.Services;

public interface IOnboardingService
{
    bool IsFirstRun();
    void CompleteOnboarding();
}

public class OnboardingService : IOnboardingService
{
    private const string FirstRunKey = "app_first_run_completed";

    public bool IsFirstRun()
    {
        // Default true: if key not set, it's first run
        var completed = Preferences.Get(FirstRunKey, false);
        return !completed;
    }

    public void CompleteOnboarding()
    {
        Preferences.Set(FirstRunKey, true);
    }
}
