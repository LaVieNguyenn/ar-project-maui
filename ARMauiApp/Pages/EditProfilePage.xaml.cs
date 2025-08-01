using ARMauiApp.ViewModels;

namespace ARMauiApp.Pages;

public partial class EditProfilePage : ContentPage
{
    public EditProfilePage(AccountViewModel accountViewModel)
    {
        InitializeComponent();
        BindingContext = accountViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is AccountViewModel viewModel)
        {
            if (viewModel.LoadUserCommand.CanExecute(null))
            {
                viewModel.LoadUserCommand.Execute(null);
            }
        }
    }
}
