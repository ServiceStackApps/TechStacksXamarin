using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using TechStacks.ServiceModel;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class ViewTech : ContentPage
    {
        private readonly string techSlug;
        public List<TechnologyStack> TechStacks = new List<TechnologyStack>();
        public ObservableCollection<TechnologyStack> TechStacksDataSource = new ObservableCollection<TechnologyStack>();
        private Technology technology;

        public ViewTech(string techSlug)
        {
            this.techSlug = techSlug;
            InitializeComponent();
            this.ListView.ItemsSource = TechStacksDataSource;
            this.ListView.ItemSelected += ListViewOnItemSelected;
            FetchDetails().ConfigureAwait(false);
            bool isIos = Device.OS.ToString() == "iOS";
            this.ToolbarItems.Add(new ToolbarItem
            {
                Text = isIos ? "Back" : "Close",
                Command = new Command(() => Navigation.PopModalAsync())
            });
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techStack = selectedItemChangedEventArgs.SelectedItem as TechnologyStack;
            this.Navigation.PushAsync(new ViewStack(techStack.Slug));
        }

        private async Task FetchDetails()
        {
            var response = await AppUtils.ServiceClient.GetAsync(new GetTechnology() {Slug = techSlug});
            this.TechStacks = response.TechnologyStacks;
            TechStacksDataSource.UpdateDataSource(this.TechStacks);
            this.technology = response.Technology;
            if (this.TechStacks.Count == 0)
                usedInLabel.IsVisible = false;
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = this.technology;
                this.StackLayout.BindingContext = this.technology;
                this.ApplyBindings();
            });
        }
    }
}
