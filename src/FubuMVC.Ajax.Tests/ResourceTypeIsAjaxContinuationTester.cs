using System;
using FubuMVC.Core.Ajax;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Ajax.Tests
{
	[TestFixture]
	public class ResourceTypeIsAjaxContinuationTester
	{
		[Test]
		public void matches()
		{
			var chain = BehaviorChain.For<FilterEndpoint>(x => x.get_ajax());
			new ResourceTypeIsAjaxContinuation().Matches(chain).ShouldBeTrue();
		}

		[Test]
		public void matches_negative()
		{
			var chain = BehaviorChain.For<FilterEndpoint>(x => x.get_string());
			new ResourceTypeIsAjaxContinuation().Matches(chain).ShouldBeFalse();

			chain = BehaviorChain.For<FilterEndpoint>(x => x.get_void());
			new ResourceTypeIsAjaxContinuation().Matches(chain).ShouldBeFalse();
		}


		 public class FilterEndpoint
		 {
			 public string get_string()
			 {
				 throw new NotImplementedException();
			 }

			 public void get_void()
			 {
				 
			 }

			 public AjaxContinuation get_ajax()
			 {
				 throw new NotImplementedException();
			 }
		 }
	}
}