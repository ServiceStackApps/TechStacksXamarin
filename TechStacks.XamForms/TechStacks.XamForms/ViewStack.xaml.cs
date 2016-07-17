using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class ViewStack : ContentPage
    {
        private readonly string stackSlug;
        public ObservableCollection<TechnologyInStack> ListDataSource = new ObservableCollection<TechnologyInStack>();
        private TechStackDetails technologyStack;

        public ViewStack(string stackSlug)
        {
            this.stackSlug = stackSlug;
            InitializeComponent();
            ListView.ItemsSource = ListDataSource;
            ListView.ItemSelected += ListViewOnItemSelected;
            FetchTechnologies().ConfigureAwait(false);
            var isIos = Device.OS.ToString() == "iOS";
            ToolbarItems.Add(new ToolbarItem
            {
                Text = isIos ? "Back" : "Close",
                Command = new Command(() => Navigation.PopModalAsync())
            });
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techSelected = selectedItemChangedEventArgs.SelectedItem as TechnologyInStack;
            Navigation.PushAsync(new ViewTech(techSelected.Slug));
        }

        private async Task FetchTechnologies()
        {
            var result = await AppUtils.ServiceClient.GetAsync(new GetTechnologyStack {Slug = stackSlug });
            technologyStack = result.Result;
            ListDataSource.UpdateDataSource(result.Result.TechnologyChoices);
            Device.BeginInvokeOnMainThread(() =>
            {
                BindingContext = technologyStack;
                ApplyBindings();
            });
        }
    }
}
