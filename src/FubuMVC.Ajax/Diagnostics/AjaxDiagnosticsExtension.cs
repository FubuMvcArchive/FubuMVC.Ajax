using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace FubuMVC.Ajax.Diagnostics
{
	public class AjaxDiagnosticsExtension : IFubuRegistryExtension
	{
		public void Configure(FubuRegistry registry)
		{
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
	}
}