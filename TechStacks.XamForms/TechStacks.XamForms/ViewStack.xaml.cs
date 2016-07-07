using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<TechnologyInStack> TechnologiesInStack;
        public ObservableCollection<TechnologyInStack> TechnologiesInStackDataSource = new ObservableCollection<TechnologyInStack>();
        private TechStackDetails technologyStack;

        public ViewStack(string stackSlug)
        {
            this.stackSlug = stackSlug;
            TechnologiesInStack = new List<TechnologyInStack>();
            InitializeComponent();
            this.ListView.ItemsSource = this.TechnologiesInStackDataSource;
            this.ListView.ItemSelected += ListViewOnItemSelected;
            FetchTechnologies().ConfigureAwait(false);
            bool isIos = Device.OS.ToString() == "iOS";
            this.ToolbarItems.Add(new ToolbarItem
            {
                Text = isIos ? "Back" : "Close",
                Command = new Command(() => Navigation.PopModalAsync())
            });
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techSelected = selectedItemChangedEventArgs.SelectedItem as TechnologyInStack;
            this.Navigation.PushAsync(new ViewTech(techSelected.Slug));
        }

        private async Task FetchTechnologies()
        {
            var result = await AppUtils.ServiceClient.GetAsync(new GetTechnologyStack {Slug = this.stackSlug });
            this.TechnologiesInStack = result.Result.TechnologyChoices;
            this.technologyStack = result.Result;
            this.TechnologiesInStackDataSource.UpdateDataSource(this.TechnologiesInStack);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = this.technologyStack;
                this.ApplyBindings();
            });
        }
    }
}
