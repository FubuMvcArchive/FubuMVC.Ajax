using System;
using System.Collections.Generic;
using FubuMVC.Core.Http;

namespace FubuMVC.Ajax.Tests
{
	public class StubRequestHeaders : IRequestHeaders
	{
		public readonly IDictionary<string, object> Values = new Dictionary<string, object>();

		public void Value<T>(string header, Action<T> callback)
		{
			if (Values.ContainsKey(header))
			{
				callback((T)Values[header]);
			}
		}

		public T BindToHeaders<T>()
		{
			throw new NotImplementedException();
		}

		public bool HasHeader(string header)
		{
			throw new NotImplementedException();
		}

		public bool IsAjaxRequest()
		{
			throw new NotImplementedException();
		}
	}
}