using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ServiceStack;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class Technologies : ContentPage
    {
        public ObservableCollection<Technology> ListDataSource { get; set; }
        public List<Technology> TechsData { get; set; }

        public Technologies()
        {
            ListDataSource = new ObservableCollection<Technology>();
            TechsData = new List<Technology>();
            InitializeComponent();
            SearchBarTechnologies.TextChanged += (sender, args) => { Search(); };
            TechnologiesListView.ItemsSource = ListDataSource;
            TechnologiesListView.ItemSelected += TechnologiesListViewOnItemSelected;
            InitData().ConfigureAwait(false);
        }

        private void TechnologiesListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var technology = selectedItemChangedEventArgs.SelectedItem as Technology;
            Navigation.PushModalAsync(new NavigationPage(new ViewTech(technology.Slug)));
        }

        private void Search()
        {
            var responseTask = AppUtils.ServiceClient.
                GetAsync<QueryResponse<Technology>>("/technology/search?NameContains=" + SearchBarTechnologies.Text);
            responseTask.ConfigureAwait(false);
            responseTask.ContinueWith(x =>
            {
                TechsData = x.Result.Results;
                ListDataSource.UpdateDataSource(TechsData);
            });
        }

        private async Task InitData()
        {
            var response = await AppUtils.ServiceClient.GetAsync<QueryResponse<Technology>>("/technology/search?NameContains=");
            TechsData = response.Results;
            ListDataSource.UpdateDataSource(TechsData);
        }
    }
}
