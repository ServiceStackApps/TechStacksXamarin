using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class TopRated : ContentPage
    {
        static readonly Dictionary<TechnologyTier, string> TechTypes = new Dictionary<TechnologyTier, string>
        {
            {TechnologyTier.ProgrammingLanguage, "Programming Languages" },
            {TechnologyTier.Client, "Client Libraries" },
            {TechnologyTier.Http, "HTTP Server Technologies" },
            {TechnologyTier.Server, "Server Libraries" },
            {TechnologyTier.Data, "Databases and NoSQL Datastores" },
            {TechnologyTier.SoftwareInfrastructure, "Server Software" },
            {TechnologyTier.OperatingSystem, "Operating Systems" },
            {TechnologyTier.HardwareInfrastructure, "Cloud/Hardware Ingrastructure" },
            {TechnologyTier.ThirdPartyServices, "3rd Party API/Services" }
        };


        public ObservableCollection<TechnologyInfo> TopTechsDataSource { get; set; }
        public List<TechnologyInfo> TopTechsData { get; set; }

        public TopRated()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "title_logo.png");
			TopTechsDataSource = new ObservableCollection<TechnologyInfo>();
			TopTechsData = new List<TechnologyInfo>();
            foreach (string val in TechTypes.Values)
            {
                topTechPicker.Items.Add(val);
            }
			topTechListView.ItemsSource = TopTechsDataSource;
			topTechPicker.SelectedIndexChanged += TopTechPickerOnSelectedIndexChanged;
            topTechListView.ItemSelected += TopTechListViewOnItemSelected;
            InitData().ConfigureAwait(false);
        }

        private void TopTechListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techInfo = selectedItemChangedEventArgs.SelectedItem as TechnologyInfo;
            Navigation.PushModalAsync(new NavigationPage(new ViewTech(techInfo.Slug)));
        }

        private void TopTechPickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            TopTechsDataSource.Clear();
            //Filter
            foreach (var techInfo in TopTechsData)
            {
                if (topTechPicker.SelectedIndex == -1)
                {
                    TopTechsDataSource.Add(techInfo);
                    continue;
                }
                    
                if (techInfo.Tier == TechTypes.Keys.ElementAt(topTechPicker.SelectedIndex).ToString())
                    TopTechsDataSource.Add(techInfo);
            }
        }

        private async Task InitData()
		{
			var response = await AppUtils.ServiceClient.GetAsync(new AppOverview());
            TopTechsData = response.TopTechnologies;
            UpdateData();
        }
    }
}
