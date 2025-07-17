using ARProject.ViewModels;

namespace ARProject.Views
{
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage(EditProfileViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
