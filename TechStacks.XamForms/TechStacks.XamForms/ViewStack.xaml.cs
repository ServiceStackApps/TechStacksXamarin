using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStacks.ServiceModel;
using TechStacks.ServiceModel.Types;
using Xamarin.Forms;

namespace TechStacks.XamForms
{
    public partial class ViewStack : ContentPage
    {
        private readonly TechnologyStack techStack;
        private List<TechnologyInStack> technologiesInStack;

        public ViewStack(TechnologyStack techStack)
        {
            technologiesInStack = new List<TechnologyInStack>();
            this.techStack = techStack;
            InitializeComponent();
            FetchTechnologies();
            this.BindingContext = this.techStack;
            this.ListView.ItemsSource = this.technologiesInStack;
        }

        private void FetchTechnologies()
        {
            var resultTask = AppUtils.ServiceClient.GetAsync(new GetTechnologyStack {Slug = this.techStack.Slug});
            resultTask.ConfigureAwait(false);
            resultTask.ContinueWith(x =>
            {
                this.technologiesInStack = x.Result.Result.TechnologyChoices;
            });
        }
    }
}
