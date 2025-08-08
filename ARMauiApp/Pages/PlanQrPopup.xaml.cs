using ARMauiApp.ViewModels;

namespace ARMauiApp.Pages
{
    public partial class PlanQrPopup : CommunityToolkit.Maui.Views.Popup
    {
        public PlanQrPopup()
        {
            InitializeComponent();
        }
        public PlanQrPopup(PlanQrPopupViewModel vm) : this()
        {
            BindingContext = vm;
        }
    }
}
