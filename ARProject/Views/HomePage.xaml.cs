using System;
using ARProject.ViewModels;
using Microsoft.Maui.Controls;

namespace ARProject.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel();
        }
    }
}
