using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Ajax.Tests
{
	[TestFixture]
	public class FormModePolicyTester
	{
		private FormMode theMode;
		private IChainFilter theFilter;
		private BehaviorChain theChain;
		private FormModePolicy thePolicy;

		[SetUp]
		public void SetUp()
		{
			theFilter = MockRepository.GenerateStub<IChainFilter>();
			theMode = FormMode.Ajax;
			theChain = new BehaviorChain();

			thePolicy = new FormModePolicy(theFilter, theMode);
		}

		[Test]
		public void matches()
		{
			theFilter.Stub(x => x.Matches(theChain)).Return(true);
			thePolicy.Matches(theChain).ShouldBeTrue();
		}

		[Test]
		public void matches_negative()
		{
			theFilter.Stub(x => x.Matches(theChain)).Return(false);
			thePolicy.Matches(theChain).ShouldBeFalse();
		}

		[Test]
		public void gets_the_mode()
		{
			thePolicy.ModeFor(theChain).ShouldEqual(theMode);
		}
	}
}