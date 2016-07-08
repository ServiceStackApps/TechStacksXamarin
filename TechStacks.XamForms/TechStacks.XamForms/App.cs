using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public class App : Application
    {
        public App()
        {

            Current.Resources = new ResourceDictionary();
            Current.Resources.Add("MyNavigationBackgroundColor", Color.FromRgb(0, 149, 245));
            var navigationStyle = new Style(typeof(NavigationPage));
            var barTextColorSetter = new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White };
            var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.FromRgb(0, 149, 245) };

            navigationStyle.Setters.Add(barTextColorSetter);
            navigationStyle.Setters.Add(barBackgroundColorSetter);

            Current.Resources.Add(navigationStyle);
            // The root page of your application
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
