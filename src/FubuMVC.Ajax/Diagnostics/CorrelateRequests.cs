using FubuCore;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Ajax.Diagnostics
{
    // TODO -- move this to OWIN middleware?
	/// <summary>
	/// Correlates requests by reading/writing from the X-Correlation-Id header.
	/// </summary>
	public class CorrelateRequests : ICorrelateRequests
	{
		public const string Correlation_Id = "X-Correlation-Id";

		private readonly IHttpRequest _headers;
		private readonly IOutputWriter _writer;

		public CorrelateRequests(IHttpRequest headers, IOutputWriter writer)
		{
			_headers = headers;
			_writer = writer;
		}

		public void Correlate()
		{
		    var header = _headers.GetSingleHeader(Correlation_Id);
		    if (header.IsNotEmpty())
		    {
		        _writer.AppendHeader(Correlation_Id, header);
		    }
		}
	}
}