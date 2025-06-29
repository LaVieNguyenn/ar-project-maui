using ARProject.ViewModels;

namespace ARProject.Views.RegisterStep;

public partial class RegisterStep1 : ContentPage
{
	public RegisterStep1(RegisterStep1ViewModel vw)
	{
		InitializeComponent();
        BindingContext = vw;

	}
}