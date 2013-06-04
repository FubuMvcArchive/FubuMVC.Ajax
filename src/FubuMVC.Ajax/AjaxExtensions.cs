using FubuMVC.Core;
using FubuMVC.Core.UI;

namespace FubuMVC.Ajax
{
	public class AjaxExtensions : IFubuRegistryExtension
	{
		public void Configure(FubuRegistry registry)
		{
			registry.Import<HtmlConventionRegistry>(x =>
			{
				x.Forms.Add(new FormModifier());
			});
		}
	}
}