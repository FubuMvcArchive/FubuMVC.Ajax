using System;
using System.Linq.Expressions;
using FubuCore.Reflection;
using HtmlTags;

namespace FubuMVC.Ajax.Diagnostics
{
	public class AjaxDiagnosticsTag : HtmlTag
	{
		public const string DIAGNOSTICS_ID = "ajax-diagnostics";

		public AjaxDiagnosticsTag()
			: base("div")
		{
			Id(DIAGNOSTICS_ID);
			addInput(x => x.PendingRequests).Attr("value", "0");
			addInput(x => x.Errors).Attr("value", "");
		}

		private HtmlTag addInput(Expression<Func<AjaxDiagnostics, object>> expression)
		{
			var accessor = expression.ToAccessor();
			return Add("input").Attr("type", "hidden").Id(accessor.Name);
		}
	}
}