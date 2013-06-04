using System;
using System.Collections.Generic;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Ajax
{
	public class FormSettings
	{
		private readonly IList<FormModePolicy> _policies = new List<FormModePolicy>();

		public void ForChainsMatching<T>(FormMode mode)
			where T : IChainFilter, new()
		{
			ForChainsMatching(new T(), mode);
		}

		public void ForChainsMatching(IChainFilter filter, FormMode mode)
		{
			addPolicy(new FormModePolicy(filter, mode));
		}

		public void ForInputType<T>(FormMode mode)
		{
			ForChainsMatching(new InputTypeIs<T>(), mode);
		}

		public void ForInputTypesMatching(Func<Type, bool> filter, FormMode mode)
		{
			var chainFilter = new LambdaChainFilter(chain => chain.InputType() != null && filter(chain.InputType()));
			ForChainsMatching(chainFilter, mode);
		}

		protected void addPolicy(FormModePolicy policy)
		{
			_policies.Add(policy);
		}

		public IFormRegistry BuildRegistry()
		{
			return new FormRegistry(_policies);
		}
	}
}