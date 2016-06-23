using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public class MainForm : TabbedPage
    {
        public MainForm()
        {
            var navigationPage = new NavigationPage(new TopRated());
            navigationPage.Icon = "schedule.png";
            navigationPage.Title = "Schedule";

            //Children.Add(new TodayPageCS());
            //Children.Add(navigationPage);
            //Children.Add(new SettingsPage());
        }
    }
}
