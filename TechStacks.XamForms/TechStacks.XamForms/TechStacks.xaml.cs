using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ServiceStack;
using TechStacks.ServiceModel;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class TechStacks : ContentPage
    {
        public TechStacks()
        {
            TechStackDataSource = new ObservableCollection<TechnologyStack>();
            TechStacksData = new List<TechnologyStack>();
            InitializeComponent();
            SearchBarTechStacks.TextChanged += (sender, args) => { Search(); };
            TechStacksListView.ItemsSource = TechStackDataSource;
            TechStacksListView.ItemSelected += TechStacksListViewOnItemSelected;
            InitData();
        }

        private void TechStacksListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techInfo = selectedItemChangedEventArgs.SelectedItem as TechnologyInfo;
            Navigation.PushAsync(new ViewTech(techInfo));
        }

        private void Search()
        {
            var responseTask = AppUtils.ServiceClient.
                GetAsync<QueryResponse<TechnologyStack>>("/techstacks/search?NameContains=" + SearchBarTechStacks.Text);
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
            var responseTask = AppUtils.ServiceClient.GetAsync<QueryResponse<TechnologyStack>>("/techstacks/search?NameContains=");
            responseTask.ConfigureAwait(false);
            responseTask.ContinueWith(x =>
            {
                TechStacksData = x.Result.Results;
                UpdateData();
            });
        }

        public ObservableCollection<TechnologyStack> TechStackDataSource { get; set; }
        public List<TechnologyStack> TechStacksData { get; set; }
    }
}
