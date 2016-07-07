using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ServiceStack;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class TechStacks : ContentPage
    {
        public ObservableCollection<TechnologyStack> ListDataSource { get; set; }
        public List<TechnologyStack> TechStacksData { get; set; }

        public TechStacks()
        {
            ListDataSource = new ObservableCollection<TechnologyStack>();
            TechStacksData = new List<TechnologyStack>();
            InitializeComponent();
            SearchBarTechStacks.TextChanged += (sender, args) => { Search(); };
            TechStacksListView.ItemsSource = ListDataSource;
            TechStacksListView.ItemSelected += TechStacksListViewOnItemSelected;
            InitData().ConfigureAwait(false);
        }

        private void TechStacksListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var technologyStack = selectedItemChangedEventArgs.SelectedItem as TechnologyStack;
            Navigation.PushModalAsync(new NavigationPage(new ViewStack(technologyStack.Slug)));
        }

        private void Search()
        {
            var responseTask = AppUtils.ServiceClient.
                GetAsync<QueryResponse<TechnologyStack>>("/techstacks/search?NameContains=" + SearchBarTechStacks.Text);
            responseTask.ConfigureAwait(false);
            responseTask.ContinueWith(x =>
            {
                TechStacksData = x.Result.Results;
                ListDataSource.UpdateDataSource(TechStacksData);
            });
        }

        private async Task InitData()
        {
            var response = await AppUtils.ServiceClient.GetAsync<QueryResponse<TechnologyStack>>("/techstacks/search?NameContains=");
            TechStacksData = response.Results;
            ListDataSource.UpdateDataSource(TechStacksData);
        }
    }
}
