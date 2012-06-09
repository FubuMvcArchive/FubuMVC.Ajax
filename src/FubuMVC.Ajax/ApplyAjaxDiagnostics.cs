using System.Collections.Generic;
using FubuMVC.Ajax.Diagnostics;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Ajax
{
	public class ApplyAjaxDiagnostics : IFubuRegistryExtension
	{
		public void Configure(FubuRegistry registry)
		{
			registry.Actions.FindWith<AjaxDiagnosticsSource>();
		}

		public class AjaxDiagnosticsSource : IActionSource
		{
			public IEnumerable<ActionCall> FindActions(TypePool types)
			{
				yield return ActionCall.For<AjaxDiagnosticsPartial>(x => x.Build(null));
			}
		}
	}
}