namespace FubuMVC.Ajax.Diagnostics
{
	/// <summary>
	/// Responsible for correlating requests.
	/// </summary>
	public interface ICorrelateRequests
	{
		void Correlate();
	}
}