using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Ajax.Diagnostics
{
	public class SetCorrelationHeadersNode : Wrapper
	{
		public SetCorrelationHeadersNode()
			: base(typeof(SetCorrelationHeaders))
		{
		}
	}
}