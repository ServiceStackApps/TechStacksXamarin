using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ServiceStack;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class Technologies : ContentPage
    {
        public Technologies()
        {
            TechStackDataSource = new ObservableCollection<Technology>();
            TechStacksData = new List<Technology>();
            InitializeComponent();
            SearchBarTechnologies.TextChanged += (sender, args) => { Search(); };
            TechnologiesListView.ItemsSource = TechStackDataSource;
            TechnologiesListView.ItemSelected += TechnologiesListViewOnItemSelected;
            InitData();
        }

        private void TechnologiesListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var technology = selectedItemChangedEventArgs.SelectedItem as Technology;
            Navigation.PushModalAsync(new ViewTech(technology.Slug));
        }

        private void Search()
        {
            var responseTask = AppUtils.ServiceClient.
                GetAsync<QueryResponse<Technology>>("/technology/search?NameContains=" + SearchBarTechnologies.Text);
            responseTask.ConfigureAwait(false);
            responseTask.ContinueWith(x =>
            {
                TechStacksData = x.Result.Results;
                UpdateData();
            });
        }

        private void UpdateData()
        {
            TechStackDataSource.Clear();
            foreach (var techInfo in TechStacksData)
            {
                TechStackDataSource.Add(techInfo);
            }
        }

        private void InitData()
        {
            var responseTask = AppUtils.ServiceClient.GetAsync<QueryResponse<Technology>>("/technology/search?NameContains=");
            responseTask.ConfigureAwait(false);
            responseTask.ContinueWith(x =>
            {
                TechStacksData = x.Result.Results;
                UpdateData();
            });
        }

        public ObservableCollection<Technology> TechStackDataSource { get; set; }
        public List<Technology> TechStacksData { get; set; }
    }
}
