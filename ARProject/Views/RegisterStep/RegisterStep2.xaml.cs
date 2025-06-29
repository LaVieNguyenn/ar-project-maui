using ARProject.ViewModels;

namespace ARProject.Views.RegisterStep;

public partial class RegisterStep2 : ContentPage
{
	public RegisterStep2(RegisterStep2ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;   
	}
}