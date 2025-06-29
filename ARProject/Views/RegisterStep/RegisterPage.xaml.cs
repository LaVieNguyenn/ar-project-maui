using ARProject.ViewModels;

namespace ARProject.Views.RegisterStep;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vw)
    {
        InitializeComponent();
        BindingContext = vw;
    }
}