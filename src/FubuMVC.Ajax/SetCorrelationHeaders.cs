using FubuMVC.Core;
using FubuMVC.Core.Behaviors;

namespace FubuMVC.Ajax
{
	public class SetCorrelationHeaders : BasicBehavior
	{
		private readonly ICorrelateRequests _correlator;

		public SetCorrelationHeaders(ICorrelateRequests correlator, IActionBehavior inner)
			: base(PartialBehavior.Ignored)
		{
			_correlator = correlator;
			InsideBehavior = inner;
		}

		protected override DoNext performInvoke()
		{
			_correlator.Correlate();
			return DoNext.Continue;
		}
	}
}