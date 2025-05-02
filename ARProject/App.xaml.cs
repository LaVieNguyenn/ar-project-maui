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
            //var shell = new AppShell();

            ////Check authentication

            //bool isAuthenticated = Preferences.ContainsKey("auth_token");

            //if (isAuthenticated)
            //  {
            //    shell.GoToAsync($"//{nameof(LoginPage)}");
            //}

            //return new Window(shell);
            return new Window(new LoginPage());

        }
    }
}