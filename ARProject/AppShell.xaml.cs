﻿using ARProject.Views;

namespace ARProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
