using CommunityToolkit.Mvvm.ComponentModel;

namespace ARProject.ViewModels
{
    public abstract partial class BaseViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        bool isLoading;
        public bool IsNotLoading => !IsLoading;
    }
}
