using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Serenity.Fixtures;
using StoryTeller;
using StoryTeller.Engine;

namespace FubuMVC.Ajax.StoryTeller.Fixtures
{
	public abstract class FormFixture<T> : ScreenFixture<T>
		where T : class, new()
	{
		protected FormFixture()
		{
			Title = typeof (T).Name + " Screen";

			EditableElementsForAllImmediateProperties();
		}

		protected override void beforeRunning()
		{
			Navigation.NavigateTo(new T());
		}

		[FormatAs("Submit the form")]
		public void Submit()
		{
			Driver.FindElement(By.Id("Submit")).Click();
		}

		[FormatAs("The form should be activated")]
		public bool TheFormIsActivated()
		{
			return isActivated;
		}

		[FormatAs("The form should not be activated")]
		public bool TheFormIsNotActivated()
		{
			return !isActivated;
		}

		private bool isActivated
		{
			get { return Driver.FindElement(By.Id(typeof(T).Name)).HasClass("activated-form"); }
		}

		[FormatAs("No messages were recorded")]
		public bool NoMessages()
		{
			return !messages().Any();
		}

		public IGrammar VerifyTheMessages()
		{
			return VerifySetOf(messages)
				.Titled("Verify the messages")
				.MatchOn(x => x.Message);
		}

		private IEnumerable<RecordedMessage> messages()
		{
			return Driver.InjectJavascript<IEnumerable<object>>("return Recorder.allMessages()")
				.Cast<string>()
				.Select(x => new RecordedMessage { Message = x});
		}

		public class RecordedMessage
		{
			public string Message { get; set; }
		}
	}
}