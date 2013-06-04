using System;
using FubuCore;
using HtmlTags;

namespace FubuMVC.Ajax
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class NoFormModeAttribute : Attribute
	{
	}

	public class FormMode
	{
		public static readonly FormMode None = new FormMode("None", false);
		public static readonly FormMode LoFi = new FormMode("LoFi");
		public static readonly FormMode Ajax = new FormMode("Ajax");

		private readonly string _value;
		private readonly bool _modify;

		public FormMode(string value, bool modify = true)
		{
			_value = value;
			_modify = modify;
		}

		public string Value { get { return _value; } }

		public virtual void Modify(HtmlTag form)
		{
			if (!_modify) return;

			form.Data("form-mode", _value.ToLower());
			form.AddClass("activated-form");
		}

		public override string ToString()
		{
			return "Form Mode: {0}".ToFormat(_value);
		}
	}
}