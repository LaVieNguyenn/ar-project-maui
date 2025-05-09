using ARProject.ViewModels;

namespace ARProject.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vw)
	{
		InitializeComponent();
        BindingContext = vw;
	}
}