using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using TechStacks.ServiceModel;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class TopRated : ContentPage
    {
        static Dictionary<TechnologyTier, string> techTypes = new Dictionary<TechnologyTier, string>
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
			TopTechsDataSource = new ObservableCollection<TechnologyInfo>();
			TopTechsData = new List<TechnologyInfo>();
            foreach (string val in techTypes.Values)
            {
                this.topTechPicker.Items.Add(val);
            }
			topTechListView.ItemsSource = this.TopTechsDataSource;
			this.topTechPicker.SelectedIndexChanged += TopTechPickerOnSelectedIndexChanged;
            this.topTechListView.ItemSelected += TopTechListViewOnItemSelected;
            InitData();
        }

        private void TopTechListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var techInfo = selectedItemChangedEventArgs.SelectedItem as TechnologyInfo;
            Navigation.PushModalAsync(new NavigationPage(new ViewTech(techInfo)));
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
                    
                if (techInfo.Tier == techTypes.Keys.ElementAt(topTechPicker.SelectedIndex).ToString())
                    TopTechsDataSource.Add(techInfo);
            }
        }


        private void InitData()
		{
			var response = AppUtils.ServiceClient.GetAsync(new AppOverview());
			response.ConfigureAwait(false);
			response.ContinueWith(x =>
			{
				TopTechsData = x.Result.TopTechnologies;
                UpdateData();
            });
		}
    }
}
