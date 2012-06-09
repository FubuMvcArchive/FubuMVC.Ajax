using FubuMVC.Core.Behaviors;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Ajax.Tests
{
	[TestFixture]
	public class when_settings_correlation_headers
	{
		private SetCorrelationHeaders theBehavior;
		private IActionBehavior theNextBehavior;
		private ICorrelateRequests theCorrelator;

		[SetUp]
		public void SetUp()
		{
			theCorrelator = MockRepository.GenerateStub<ICorrelateRequests>();
			theNextBehavior = MockRepository.GenerateStub<IActionBehavior>();

			theBehavior = new SetCorrelationHeaders(theCorrelator, theNextBehavior);
			theBehavior.Invoke();
		}

		[Test]
		public void correlates_the_requests()
		{
			theCorrelator.AssertWasCalled(x => x.Correlate());
		}

		[Test]
		public void invokes_the_next_behavior()
		{
			theNextBehavior.AssertWasCalled(x => x.Invoke());
		}
	}
}