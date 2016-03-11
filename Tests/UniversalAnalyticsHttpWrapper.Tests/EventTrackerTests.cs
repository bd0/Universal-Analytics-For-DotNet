﻿using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAnalyticsHttpWrapper;
using UniversalAnalyticsHttpWrapper.Exceptions;

namespace UniversalAnalyticsHttpWrapper.Tests
{
    [TestFixture]
    public class EventTrackerTests
    {
        private EventTracker eventTracker;
        private IUniversalAnalyticsEvent analyticsEvent;
        private IPostDataBuilder postDataBuilderMock;
        private IGoogleDataSender googleDataSenderMock;

        [SetUp]
        public void SetUp()
        {
            postDataBuilderMock = MockRepository.GenerateMock<IPostDataBuilder>();
            googleDataSenderMock = MockRepository.GenerateMock<IGoogleDataSender>();
            eventTracker = new EventTracker(postDataBuilderMock, googleDataSenderMock);

            analyticsEvent = MockRepository.GenerateMock<IUniversalAnalyticsEvent>();
        }

        [Test]
        public void ItSendsTheDataToGoogle()
        {
            string expectedPostData = "some amazing string that matches what google requires";
            
            postDataBuilderMock.Expect(mock => mock.BuildPostDataString(Arg<string>.Is.Anything, Arg<UniversalAnalyticsEvent>.Is.Anything))
                .Return(expectedPostData);

            eventTracker.TrackEvent(analyticsEvent);

            googleDataSenderMock.AssertWasCalled(mock => mock.SendData(EventTracker.GOOGLE_COLLECTION_URI, expectedPostData));
        }
    }
}
