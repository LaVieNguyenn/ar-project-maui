using ARProject.ViewModels;

namespace ARProject.Views;

public partial class MePage : ContentPage
{
    public MePage(MeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

}