using FubuMVC.Core;
using FubuMVC.StructureMap;
using Serenity;

namespace FubuMVC.Ajax.StoryTeller
{
	public class AjaxSystem : FubuMvcSystem<AjaxApplication>
	{
		
	}

	public class AjaxApplication : IApplicationSource
	{
		public FubuApplication BuildApplication()
		{
			return FubuApplication
				.DefaultPolicies()
				.StructureMapObjectFactory();
		}
	}

	public class StandardIn { }
	public class StandardOut { }

	public class AjaxIn
	{
		public string Message { get; set; }
	}

	public class IgnoredModel {}
}