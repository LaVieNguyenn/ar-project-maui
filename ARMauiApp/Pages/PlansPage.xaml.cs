using ARMauiApp.ViewModels;

namespace ARMauiApp.Pages
{
    public partial class PlansPage : ContentPage
    {
        private readonly PlanListViewModel _viewModel;

        public PlansPage(PlanListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize();
        }
    }

}
