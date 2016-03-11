using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAnalyticsHttpWrapper.Exceptions;

namespace UniversalAnalyticsHttpWrapper.Tests
{
    [TestFixture]
    public class UniversalAnalyticsEventFactoryTests
    {
        UniversalAnalyticsEventFactory factory;
        private string measurementProtocolVersion = "measurementProtocolVersion";
        private string trackingId = "tracking id";
        private string anonymousClientId = "anonymous client id";
        private string eventCategory = "event category";
        private string eventAction = "event action";
        private string eventLabel = "event label";
        private string eventValue = "500";

        [SetUp]
        public void SetUp()
        {
             factory = new UniversalAnalyticsEventFactory(this.trackingId);
        }

        [Test]
        public void ItReturnsANewUniversalAnalyticsEvent()
        {
            IUniversalAnalyticsEvent analyticsEvent = factory.MakeUniversalAnalyticsEvent(
                anonymousClientId,
                eventCategory,
                eventAction,
                eventLabel,
                eventValue);

            //generally prefer to have a separate test for each case but this will do just fine.
            Assert.AreEqual(trackingId, analyticsEvent.TrackingId);
            Assert.AreEqual(anonymousClientId, analyticsEvent.AnonymousClientId);
            Assert.AreEqual(eventCategory, analyticsEvent.EventCategory);
            Assert.AreEqual(eventAction, analyticsEvent.EventAction);
            Assert.AreEqual(eventLabel, analyticsEvent.EventLabel);
            Assert.AreEqual(eventValue, analyticsEvent.EventValue);
        }

    }
}
