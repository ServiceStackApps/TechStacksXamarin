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
            FetchDetails();
        }

        private async void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techStack = selectedItemChangedEventArgs.SelectedItem as TechnologyStack;
            await Navigation.PushAsync(new NavigationPage(new ViewStack(techStack.Slug)));
        }

        private async void FetchDetails()
        {
            var resultTask =  await AppUtils.ServiceClient.GetAsync(new GetTechnology() { Slug = techSlug });
            this.TechStacks = resultTask.TechnologyStacks;
            TechStacksDataSource.UpdateDataSource(this.TechStacks);
            this.technology = resultTask.Technology;
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = this.technology;
                this.ApplyBindings();
            });
        }
    }
}
