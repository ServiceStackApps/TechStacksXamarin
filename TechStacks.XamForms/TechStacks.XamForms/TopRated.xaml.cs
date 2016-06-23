using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            foreach (string val in techTypes.Values)
            {
                this.topTechPicker.Items.Add(val);
            }
        }
    }
}
