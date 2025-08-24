using ARMauiApp.ViewModels;

namespace ARMauiApp.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    private void OpenBirthPicker(object sender, TappedEventArgs e)  => BirthPicker.Focus();
private void OpenGenderPicker(object sender, TappedEventArgs e) => GenderPicker.Focus();

}
