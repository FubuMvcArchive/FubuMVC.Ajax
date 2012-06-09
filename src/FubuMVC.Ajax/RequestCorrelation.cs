using System.Collections.Generic;
using FubuMVC.Core.Registration;

namespace FubuMVC.Ajax
{
	public class RequestCorrelation : IConfigurationAction
	{
		public void Configure(BehaviorGraph graph)
		{
			graph
				.Behaviors
				.Each(chain => chain.Prepend(new SetCorrelationHeadersNode()));
		}
	}
}