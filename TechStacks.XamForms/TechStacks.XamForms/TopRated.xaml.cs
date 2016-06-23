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

        public TopRated()
        {
            InitializeComponent();
			TopTechs = new ObservableCollection<TechnologyInfo>();
            foreach (string val in techTypes.Values)
            {
                this.topTechPicker.Items.Add(val);
            }
			topTechListView.ItemsSource = this.TopTechs;
			this.topTechPicker.SelectedIndexChanged += delegate
			{
				TopTechs.Clear();
				//Filter
				foreach (var techInfo in AllTopTechs)
				{
					if (techInfo.Tier == techTypes.Keys.ElementAt(topTechPicker.SelectedIndex).ToString())
					{
						TopTechs.Add(techInfo);
					}
				}
			};
        }


		private void InitWithTopTechs()
		{
			var response = AppUtils.ServiceClient.GetAsync(new AppOverview());
			response.ConfigureAwait(false);
			response.ContinueWith(x =>
			{
				AllTopTechs = x.Result.TopTechnologies;
			});
		}


		public ObservableCollection<TechnologyInfo> TopTechs { get; set; }
		public List<TechnologyInfo> AllTopTechs { get; set; }
    }
}
