using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Ajax.Diagnostics
{
	/// <summary>
	/// Correlates requests by reading/writing from the X-Correlation-Id header.
	/// </summary>
	public class CorrelateRequests : ICorrelateRequests
	{
		public const string Correlation_Id = "X-Correlation-Id";

		private readonly IRequestHeaders _headers;
		private readonly IOutputWriter _writer;

		public CorrelateRequests(IRequestHeaders headers, IOutputWriter writer)
		{
			_headers = headers;
			_writer = writer;
		}

		public void Correlate()
		{
			_headers.Value<string>(Correlation_Id, id => _writer.AppendHeader(Correlation_Id, id));
		}
	}
}