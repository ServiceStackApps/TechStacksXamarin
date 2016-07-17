using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ServiceStack;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class TechStacks : ContentPage
    {
        public ObservableCollection<TechnologyStack> ListDataSource { get; set; }

        public TechStacks()
        {
            ListDataSource = new ObservableCollection<TechnologyStack>();
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "title_logo.png");
            SearchBarTechStacks.TextChanged += (sender, args) => { Search().ConfigureAwait(false); };
            TechStacksListView.ItemsSource = ListDataSource;
            TechStacksListView.ItemSelected += TechStacksListViewOnItemSelected;
            InitData().ConfigureAwait(false);
        }

        private void TechStacksListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var technologyStack = selectedItemChangedEventArgs.SelectedItem as TechnologyStack;
            Navigation.PushModalAsync(new NavigationPage(new ViewStack(technologyStack.Slug)));
        }

        private async Task Search()
        {
            var response = await AppUtils.ServiceClient.
                GetAsync(new FindTechStacks { NameContains = SearchBarTechStacks.Text });
            ListDataSource.UpdateDataSource(response.Results);
        }

        private async Task InitData()
        {
            var response = await AppUtils.ServiceClient.GetAsync(new GetAllTechnologyStacks());
            ListDataSource.UpdateDataSource(response.Results);
        }
    }
}
