using System;
using FubuMVC.Core.Ajax;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Ajax.Tests
{
	[TestFixture]
	public class IntegratedFormSettingsRegistryTester
	{
		private BehaviorChain explicitChain;
		private BehaviorChain defaultChain;
		private BehaviorChain ajaxChain;

		private FormSettings theSettings;
		private IFormRegistry theRegistry;

		[SetUp]
		public void SetUp()
		{
			explicitChain = BehaviorChain.For<IntegratedSettingsEndpoint>(x => x.get_int());
			defaultChain = BehaviorChain.For<IntegratedSettingsEndpoint>(x => x.get_string());
			ajaxChain = BehaviorChain.For<IntegratedSettingsEndpoint>(x => x.get_ajax());

			theSettings = new FormSettings();;
			theSettings.ForChainsMatching(new LambdaChainFilter(x => x.ResourceType() == typeof(int)), FormMode.None);

			theRegistry = theSettings.BuildRegistry();
		}

		[Test]
		public void explicit_setting()
		{
			theRegistry.ModeFor(explicitChain).ShouldEqual(FormMode.None);
		}

		[Test]
		public void default_lofi()
		{
			theRegistry.ModeFor(defaultChain).ShouldEqual(FormMode.LoFi);
		}

		[Test]
		public void default_ajax()
		{
			theRegistry.ModeFor(ajaxChain).ShouldEqual(FormMode.Ajax);
		}

		public class IntegratedSettingsEndpoint
		{
			public int get_int()
			{
				throw new NotImplementedException();
			}

			public string get_string()
			{
				throw new NotImplementedException();
			}

			public AjaxContinuation get_ajax()
			{
				throw new NotImplementedException();
			}
		}
	}
}