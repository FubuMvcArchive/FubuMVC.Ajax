using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Ajax
{
	public class FormModePolicy
	{
		private readonly IChainFilter _filter;
		private readonly FormMode _mode;

		public FormModePolicy(IChainFilter filter, FormMode mode)
		{
			_filter = filter;
			_mode = mode;
		}

		public bool Matches(BehaviorChain chain)
		{
			return _filter.Matches(chain);
		}

		public FormMode ModeFor(BehaviorChain chain)
		{
			return _mode;
		}

		public static FormModePolicy Default()
		{
			return new FormModePolicy(null, FormMode.LoFi);
		}

		public static FormModePolicy Ajax()
		{
			var filter = new ResourceTypeIsAjaxContinuation();
			return new FormModePolicy(filter, FormMode.Ajax);
		}
	}
}