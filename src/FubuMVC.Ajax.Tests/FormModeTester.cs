using System;
using FubuCore;
using FubuMVC.Core.Ajax;
using FubuMVC.Core.Assets;
using FubuMVC.Core.Http;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Querying;
using FubuMVC.Core.UI.Forms;
using FubuMVC.Core.Urls;
using FubuTestingSupport;
using HtmlTags;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Ajax.Tests
{
    [TestFixture]
    public class FormModeTester
    {
        private BehaviorGraph theGraph;
        private IAssetRequirements theRequirements;
        private IFormRegistry theRegistry;

        [SetUp]
        public void SetUp()
        {
            theRequirements = MockRepository.GenerateStub<IAssetRequirements>();
            theGraph = BehaviorGraph.BuildFrom(x => x.Actions.IncludeType<FormModeEndpoint>());

            theRegistry = theGraph.Settings.Get<FormSettings>().BuildRegistry();
        }

        private FormRequest requestFor<T>() where T : class, new()
        {
            var services = new InMemoryServiceLocator();
            services.Add<IChainResolver>(new ChainResolutionCache(new TypeResolver(), theGraph));
            services.Add(theRequirements);
            services.Add<IChainUrlResolver>(new ChainUrlResolver(new StandInCurrentHttpRequest()));

            var request = new FormRequest(new ChainSearch {Type = typeof (T)}, new T());
            request.Attach(services);
            request.ReplaceTag(new FormTag("test"));

            return request;
        }

        [Test]
        public void ajax_mode()
        {
            theRegistry.ModeFor(requestFor<AjaxTarget>().Chain).ShouldEqual(FormMode.Ajax);
        }

        [Test]
        public void lofi_mode()
        {
            theRegistry.ModeFor(requestFor<LoFiTarget>().Chain).ShouldEqual(FormMode.LoFi);
        }

        [Test]
        public void none()
        {
            theRegistry.ModeFor(requestFor<NoneTarget>().Chain).ShouldEqual(FormMode.None);
        }

        [Test]
        public void ignored()
        {
            theRegistry.ModeFor(requestFor<IgnoredTarget>().Chain).ShouldEqual(FormMode.None);
        }

        [Test]
        public void no_modification_to_tag()
        {
            formForMode(FormMode.None)
                .ToString().ShouldEqual("<form method=\"post\" action=\"test\">");
        }

        [Test]
        public void modify_the_tag_for_ajax()
        {
            formForMode(FormMode.Ajax)
                .ToString()
                .ShouldEqual("<form method=\"post\" action=\"test\" data-form-mode=\"ajax\" class=\"activated-form\">");
        }

        [Test]
        public void modify_the_tag_for_lofi()
        {
            formForMode(FormMode.LoFi)
                .ToString()
                .ShouldEqual("<form method=\"post\" action=\"test\" data-form-mode=\"lofi\" class=\"activated-form\">");
        }

        private HtmlTag formForMode(FormMode mode)
        {
            var form = new FormTag("test");
            mode.Modify(form);

            return form;
        }
    }

    public class LoFiTarget
    {
    }

    public class AjaxTarget
    {
    }

    [NoFormMode]
    public class NoneTarget
    {
    }

    public class IgnoredTarget
    {
    }

    public class FormModeEndpoint
    {
        public NoneTarget post_none(NoneTarget target)
        {
            throw new NotImplementedException();
        }

        [NoFormMode]
        public IgnoredTarget post_ignored(IgnoredTarget target)
        {
            throw new NotImplementedException();
        }

        public LoFiTarget post_lofi(LoFiTarget target)
        {
            throw new NotImplementedException();
        }

        public AjaxContinuation post_ajax(AjaxTarget target)
        {
            throw new NotImplementedException();
        }
    }
}