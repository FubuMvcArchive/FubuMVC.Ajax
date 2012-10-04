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
            registry.Services(x => x.SetServiceIfNone<ICorrelateRequests, CorrelateRequests>());
            registry.Policies.Add<CorrelationPolicy>();
        }

        [ConfigurationType(ConfigurationType.Instrumentation)]
        public class CorrelationPolicy : IConfigurationAction
        {
            public void Configure(BehaviorGraph graph)
            {
                graph
                    .Behaviors
                    .Each(chain => chain.Prepend(new SetCorrelationHeadersNode()));
            }
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