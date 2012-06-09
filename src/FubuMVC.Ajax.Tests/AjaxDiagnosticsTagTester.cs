using FubuCore;
using FubuCore.Reflection;
using FubuMVC.Ajax.Diagnostics;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Ajax.Tests
{
	[TestFixture]
	public class AjaxDiagnosticsTagTester
	{
		private AjaxDiagnosticsTag theTag;

		[SetUp]
		public void SetUp()
		{
			theTag = new AjaxDiagnosticsTag();
		}

		[Test]
		public void creates_input_for_pending_requests()
		{
			var accessor = ReflectionHelper.GetAccessor<AjaxDiagnostics>(x => x.PendingRequests);
			theTag.Children[0].ToString().ShouldEqual("<input type=\"hidden\" id=\"{0}\" value=\"0\" />".ToFormat(accessor.Name));
		}

		[Test]
		public void creates_input_for_errors()
		{
			var accessor = ReflectionHelper.GetAccessor<AjaxDiagnostics>(x => x.Errors);
			theTag.Children[1].ToString().ShouldEqual("<input type=\"hidden\" id=\"{0}\" />".ToFormat(accessor.Name));
		}
	}
}