using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStacks.ServiceModel;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class TechStacks : ContentPage
    {
        public ICommand SearchCommand { get; private set; }

        public TechStacks()
        {
            TechStackDataSource = new ObservableCollection<TechnologyStack>();
            TechStacksData = new List<TechnologyStack>();
            SearchCommand = new Command(Search);
            InitializeComponent();
            SearchBarTechStacks.SearchCommand = SearchCommand;
            //SearchBarTechStacks.TextChanged += (sender, args) => { Search(); };
            TechStacksListView.ItemsSource = TechStackDataSource;
            InitData();
        }

        private void Search()
        {
            var responseTask = AppUtils.ServiceClient.GetAsync(new FindTechStacks
            {
                Meta = new Dictionary<string, string>
                {
                    {"NameContains", this.SearchBarTechStacks.Text}
                }
            });
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
            var responseTask = AppUtils.ServiceClient.GetAsync(new FindTechStacks());
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
