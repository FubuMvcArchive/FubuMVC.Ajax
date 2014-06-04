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

            mode.Modify(request.CurrentTag);
        }
    }
}