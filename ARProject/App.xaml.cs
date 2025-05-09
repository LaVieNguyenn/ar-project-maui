using ARProject.Views;

namespace ARProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = new AppShell();

            shell.GoToAsync($"//{nameof(LoginPage)}");

            return new Window(shell);
        }
    }
}