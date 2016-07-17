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

        public Technologies()
        {
            ListDataSource = new ObservableCollection<Technology>();
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "title_logo.png");
            SearchBarTechnologies.TextChanged += (sender, args) => { Search().ConfigureAwait(false); };
            TechnologiesListView.ItemsSource = ListDataSource;
            TechnologiesListView.ItemSelected += TechnologiesListViewOnItemSelected;
            InitData().ConfigureAwait(false);
        }

        private void TechnologiesListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var technology = selectedItemChangedEventArgs.SelectedItem as Technology;
            Navigation.PushModalAsync(new NavigationPage(new ViewTech(technology.Slug)));
        }

        private async Task Search()
        {
            var response = await AppUtils.ServiceClient.
                GetAsync(new FindTechnologies { NameContains = SearchBarTechnologies.Text });
            ListDataSource.UpdateDataSource(response.Results);
        }

        private async Task InitData()
        {
            var response = await AppUtils.ServiceClient.GetAsync(new GetAllTechnologies());
            ListDataSource.UpdateDataSource(response.Results);
        }
    }
}
