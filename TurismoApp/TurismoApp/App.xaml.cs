using System;
using TurismoApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurismoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SitioView())
            {
                BarTextColor = Color.FromHex("#f9aa33")
            };
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
