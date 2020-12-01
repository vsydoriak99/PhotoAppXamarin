using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoApp.Services;
using PhotoApp.Views;

namespace PhotoApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<PhotoDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
