using FubuMVC.Core.Ajax;
using FubuMVC.Core.Assets;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.UI;
using HtmlTags;

namespace FubuMVC.Ajax.StoryTeller
{
	public class AjaxSampleEndpoints
	{
		private readonly FubuHtmlDocument<StandardIn> _standardPage;
		private readonly FubuHtmlDocument<IgnoredModel> _ignoredPage;
		private readonly FubuHtmlDocument<AjaxIn> _ajaxPage;

		public AjaxSampleEndpoints(FubuHtmlDocument<StandardIn> standardPage, FubuHtmlDocument<IgnoredModel> ignoredPage, FubuHtmlDocument<AjaxIn> ajaxPage)
		{
			_standardPage = standardPage;
			_ignoredPage = ignoredPage;
			_ajaxPage = ajaxPage;
		}


		public FubuHtmlDocument<StandardIn> get_standard(StandardIn input)
		{
			_standardPage.Add(new HtmlTag("h1").Text("Standard Form"));
			_standardPage.Add(createStandardForm());
			_standardPage.Asset("recorder.js");
			_standardPage.Add(_standardPage.WriteScriptTags());
			return _standardPage;
		}

		public FubuContinuation post_standard(StandardIn input)
		{
			return FubuContinuation.RedirectTo(input, "GET");
		}

		public FubuHtmlDocument<AjaxIn> get_ajax(AjaxIn input)
		{
			_ajaxPage.Add(new HtmlTag("h1").Text("Ajax Form"));
			_ajaxPage.Add(createAjaxForm());
			_ajaxPage.Asset("recorder.js");
			_ajaxPage.Add(_ajaxPage.WriteScriptTags());
			return _ajaxPage;
		}

		public AjaxContinuation post_ajax(AjaxIn input)
		{
			var continuation = AjaxContinuation.Successful();
			continuation.Message = input.Message;

			return continuation;
		}

		public FubuHtmlDocument<IgnoredModel> get_ignored(IgnoredModel input)
		{
			_ignoredPage.Add(new HtmlTag("h1").Text("Ignored Form"));
			_ignoredPage.Add(createIgnoredForm());
			_ignoredPage.Asset("recorder.js");
			_ignoredPage.Add(_ignoredPage.WriteScriptTags());
			return _ignoredPage; 
		}

		[NoFormMode]
		public AjaxContinuation post_ignored(IgnoredModel input)
		{
			return AjaxContinuation.Successful();
		}

		private HtmlTag createStandardForm()
		{
			var form = _standardPage.FormFor<StandardIn>();

			form.Append(new HtmlTag("input").Attr("type", "submit").Attr("value", "Submit").Id("Submit"));
			form.Id("StandardIn");

			return form;
		}

		private HtmlTag createAjaxForm()
		{
			var form = _ajaxPage.FormFor<AjaxIn>();

			form.Append(_ajaxPage.Edit(x => x.Message));

			form.Append(new HtmlTag("input").Attr("type", "submit").Attr("value", "Submit").Id("Submit"));
			form.Id("AjaxIn");

			return form;
		}

		private HtmlTag createIgnoredForm()
		{
			var form = _ignoredPage.FormFor<IgnoredModel>();

			form.Append(new HtmlTag("input").Attr("type", "submit").Attr("value", "Submit").Id("Submit"));
			form.Id("IgnoredModel");

			return form;
		}
	}
}