using FubuMVC.Ajax.Diagnostics;
using FubuMVC.Core.Runtime;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Ajax.Tests.Diagnostics
{
	[TestFixture]
	public class CorrelateRequestsIntegratedTester
	{
		[Test]
		public void appends_the_header()
		{
			var id = "123";
			var writer = MockRepository.GenerateMock<IOutputWriter>();
			var headers = new StubRequestHeaders();

			headers.Values.Add(CorrelateRequests.Correlation_Id, id);


			new CorrelateRequests(headers, writer).Correlate();

			writer.AssertWasCalled(w => w.AppendHeader(CorrelateRequests.Correlation_Id, id));
		}
	}
}