using HtmlTags;

namespace FubuMVC.Ajax.Diagnostics
{
	public class AjaxDiagnosticsPartial
	{
		public HtmlTag Build(AjaxDiagnostics diagnostics)
		{
			return new AjaxDiagnosticsTag();
		}
	}
}