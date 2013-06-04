using System.Collections.Generic;
using System.Linq;
using FubuCore.Reflection;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Ajax
{
	public interface IFormRegistry
	{
		FormMode ModeFor(BehaviorChain chain);
	}

	public class FormRegistry : IFormRegistry
	{
		private readonly IList<FormModePolicy> _policies = new List<FormModePolicy>();

		public FormRegistry(IEnumerable<FormModePolicy> policies)
		{
			_policies.Add(FormModePolicy.Ajax());
			_policies.AddRange(policies);
		}

		public FormMode ModeFor(BehaviorChain chain)
		{
			var call = chain.FirstCall();
			if (call == null || call.HasAttribute<NoFormModeAttribute>())
			{
				return FormMode.None;
			}

			if (call.HasInput && call.InputType().HasAttribute<NoFormModeAttribute>())
			{
				return FormMode.None;
			}

			var policy = policyFor(chain) ?? FormModePolicy.Default();
			return policy.ModeFor(chain);
		}

		private FormModePolicy policyFor(BehaviorChain chain)
		{
			return _policies.LastOrDefault(x => x.Matches(chain));
		}
	}
}