using FubuMVC.Core.Assets;
using FubuMVC.Core.UI.Forms;
using HtmlTags.Conventions;

namespace FubuMVC.Ajax
{
    public class FormModifier : ITagModifier<FormRequest>
    {
        public bool Matches(FormRequest token)
        {
            return true;
        }

        public void Modify(FormRequest request)
        {
            var registry = request.Services.GetInstance<FormSettings>().BuildRegistry();
            var mode = registry.ModeFor(request.Chain);

            writeScriptRequirements(request);
            mode.Modify(request.CurrentTag);
        }

        private void writeScriptRequirements(FormRequest request)
        {
            request.Services.GetInstance<IAssetRequirements>().Require("FormActivator.js");
        }
    }
}