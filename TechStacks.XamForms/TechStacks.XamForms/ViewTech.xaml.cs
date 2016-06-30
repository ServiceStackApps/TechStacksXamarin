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
        private readonly TechnologyInfo techInfo;
        public List<TechnologyStack> TechStacks = new List<TechnologyStack>();
        public ObservableCollection<TechnologyStack> TechStacksDataSource = new ObservableCollection<TechnologyStack>();
        private Technology technology;

        public ViewTech(TechnologyInfo techInfo)
        {
            this.techInfo = techInfo;
            InitializeComponent();
            this.BindingContext = this.techInfo;
            this.ListView.ItemsSource = TechStacksDataSource;
            FetchDetails();
            this.ListView.ItemSelected += ListViewOnItemSelected;
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techStack = selectedItemChangedEventArgs.SelectedItem as TechnologyStack;
            Navigation.PushAsync(new ViewStack(techStack));
        }

        private void FetchDetails()
        {
            var resultTask = AppUtils.ServiceClient.GetAsync(new GetTechnology() { Slug = this.techInfo.Slug });
            resultTask.ConfigureAwait(false);
            resultTask.ContinueWith(x =>
            {
                this.TechStacks = x.Result.TechnologyStacks;
                TechStacksDataSource.UpdateDataSource(this.TechStacks);
                this.technology = x.Result.Technology;
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.BindingContext = this.technology;
                    this.ApplyBindings(this.techInfo);
                });
            });
        }
    }
}
