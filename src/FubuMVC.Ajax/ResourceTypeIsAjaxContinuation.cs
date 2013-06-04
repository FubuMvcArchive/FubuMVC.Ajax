using FubuCore;
using FubuCore.Descriptions;
using FubuMVC.Core.Ajax;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Ajax
{
	public class ResourceTypeIsAjaxContinuation : IChainFilter, DescribesItself
	{
		public bool Matches(BehaviorChain chain)
		{
			return chain.ResourceType().CanBeCastTo<AjaxContinuation>();
		}

		public void Describe(Description description)
		{
			description.Title = "Resource Type is " + typeof(AjaxContinuation).Name;
		}
	}
}