using System;
using System.Collections.Generic;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Ajax.Tests
{
	[TestFixture]
	public class FormRegistryTester
	{
		private FormModePolicy p1;
		private FormModePolicy p2;
		private BehaviorChain theChain;
		private IList<FormModePolicy> thePolicies;

		[SetUp]
		public void SetUp()
		{
			theChain = BehaviorChain.For<FormRegistryEndpoint>(x => x.get_test());

			var theFilter = MockRepository.GenerateStub<IChainFilter>();
			theFilter.Stub(x => x.Matches(theChain)).IgnoreArguments().Return(true);

			p1 = new FormModePolicy(theFilter, FormMode.None);
			p2 = new FormModePolicy(theFilter, FormMode.Ajax);

			thePolicies = new List<FormModePolicy>();
		}

		private FormRegistry theRegistry { get { return new FormRegistry(thePolicies); }}

		private FormMode modeFor(params FormModePolicy[] policies)
		{
			thePolicies.AddRange(policies);
			return theRegistry.ModeFor(theChain);
		}

		[Test]
		public void uses_the_last_matching_policy()
		{
			modeFor(p1, p2).ShouldEqual(p2.ModeFor(theChain));
		}

		[Test]
		public void uses_the_only_matching_policy()
		{
			modeFor(p1).ShouldEqual(p1.ModeFor(theChain));
		}
		
		[Test]
		public void uses_the_default_policy_when_none_match()
		{
			modeFor().ShouldEqual(FormModePolicy.Default().ModeFor(theChain));
		}

		[Test]
		public void ignores_chains_with_the_no_form_mode_attribute()
		{
			var chain = BehaviorChain.For<FormRegistryEndpoint>(x => x.get_ignored());
			
			thePolicies.Add(p1);
			thePolicies.Add(p2);

			theRegistry.ModeFor(chain).ShouldEqual(FormMode.None);
		}

		public class FormRegistryEndpoint
		{
			public string get_test()
			{
				throw new NotImplementedException();
			}

			[NoFormMode]
			public string get_ignored()
			{
				throw new NotImplementedException();
			}
		}
	}
}