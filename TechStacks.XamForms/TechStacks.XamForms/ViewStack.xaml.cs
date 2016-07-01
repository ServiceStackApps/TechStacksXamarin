using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStacks.ServiceModel;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class ViewStack : ContentPage
    {
        private readonly string stackSlug;
        private List<TechnologyInStack> technologiesInStack;
        private TechnologyStackBase technologyStack;

        public ViewStack(string stackSlug)
        {
            this.stackSlug = stackSlug;
            technologiesInStack = new List<TechnologyInStack>();
            InitializeComponent();
            FetchTechnologies();
            this.ListView.ItemsSource = this.technologiesInStack;
            this.ListView.ItemSelected += ListViewOnItemSelected;
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techSelected = selectedItemChangedEventArgs.SelectedItem as TechnologyInStack;
            Navigation.PushAsync(new NavigationPage(new ViewTech(techSelected.Slug)));
        }

        private async void FetchTechnologies()
        {
            var result = await AppUtils.ServiceClient.GetAsync(new GetTechnologyStack {Slug = this.stackSlug });
            this.technologiesInStack = result.Result.TechnologyChoices;
            this.technologyStack = result.Result;
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = this.technologyStack;
                this.ApplyBindings();
            });
        }
    }
}
