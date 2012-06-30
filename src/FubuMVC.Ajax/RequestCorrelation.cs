using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace FubuMVC.Ajax
{
	public class RequestCorrelation : IFubuRegistryExtension
	{
		public void Configure(FubuRegistry registry)
		{
			registry.Services(x => x.SetServiceIfNone<ICorrelateRequests, CorrelateRequests>());
			registry.Configure(graph => graph
				.Behaviors
				.Each(chain => chain.Prepend(new SetCorrelationHeadersNode())));
		}
	}
}