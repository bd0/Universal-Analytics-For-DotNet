﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalAnalyticsHttpWrapper
{
    /// <summary>
    /// An object which represents a Universal Analytics Event that will be pushed via the Measurement Protocol. Expects
    /// an App Setting for 'UniversalAnalytics.TrackingId' in the config. 
    /// See https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#events
    /// for more details on the primary fields for an event.
    /// </summary>
    /// <exception cref="UniversalAnalyticsHttpWrapper.Exceptions.ConfigEntryMissingException">Thrown when
    /// one of the required config attributes are missing.</exception>
    public class UniversalAnalyticsEvent : UniversalAnalyticsHttpWrapper.IUniversalAnalyticsEvent
    {
        internal const string EXCEPTION_MESSAGE_PARAMETER_CANNOT_BE_NULL_OR_WHITESPACE = "{0} cannot be null or whitespace";

        private readonly string trackingId;
        private readonly string anonymousClientId;
        private readonly string eventCategory;
        private readonly string eventAction;
        private readonly string eventLabel;
        private readonly string eventValue;

        /// <param name="trackingId">Required. The universal analytics tracking id for the property 
        /// that events will be logged to. If you don't want to pass this every time, set the UniversalAnalytics.TrackingId 
        /// app setting and use the UniversalAnalyticsEventFactory to get instances of this class.
        /// See https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#tid for details.</param>
        /// <param name="anonymousClientId">Required. Anonymous client id for the event. 
        /// See https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#cid for details.</param>
        /// <param name="eventCategory">Required. The event category for the event. 
        /// See https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#ec for details.</param>
        /// <param name="eventAction">Required. The event action for the event. 
        /// See https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#ea for details.</param>
        /// <param name="eventLabel">Optional. The event label for the event.
        /// See https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#el for details.</param>
        /// <param name="eventValue">Optional. The event value for the event.
        /// See https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters#ev for details.</param>
        /// <exception cref="UniversalAnalyticsHttpWrapper.Exceptions.ConfigEntryMissingException">Thrown when
        /// one of the required config attributes are missing.</exception>
        /// <exception cref="System.ArgumentException">Thrown when one of the required fields are null or whitespace.</exception>
        /// <exception cref="System.Web.HttpException">Thrown when the HttpRequest that's posted to Google returns something
        /// other than a 200 OK response.</exception>
        public UniversalAnalyticsEvent(
            string trackingId,
            string anonymousClientId,
            string eventCategory,
            string eventAction,
            string eventLabel,
            string eventValue = null)
        {
            this.trackingId = trackingId;
            this.anonymousClientId = anonymousClientId;
            this.eventCategory = eventCategory;
            this.eventAction = eventAction;
            this.eventLabel = eventLabel;
            this.eventValue = eventValue;

			this.AdditionalParameters = new Dictionary<string, string>();

			ValidateRequiredFields();
        }

        /// <summary>
        /// Gets the tracking id for the Universal Analytics property
        /// </summary>
        public string TrackingId
        {
            get { return this.trackingId; }
        }

        /// <summary>
        /// Gets the anonymousClientId that was passed in when the object was constructed.
        /// </summary>
        public string AnonymousClientId
        {
            get { return this.anonymousClientId; }
        }

        /// <summary>
        /// Gets the eventCategory that was passed in when the object was constructed.
        /// </summary>
        public string EventCategory
        {
            get { return this.eventCategory; }
        }

        /// <summary>
        /// Gets the eventAction that was passed in when the object was constructed.
        /// </summary>
        public string EventAction
        {
            get { return this.eventAction; }
        }

        /// <summary>
        /// Gets the eventLabel that was passed in when the object was constructed.
        /// </summary>
        public string EventLabel
        {
            get { return this.eventLabel; }
        }

        /// <summary>
        /// Gets the eventValue that was passed in when the object was constructed.
        /// </summary>
        public string EventValue
        {
            get { return this.eventValue; }
        }

		/// <summary>
		/// Allows setting additional parameters for the request.
		/// Use the Key as the parameter name and the Value as the 
		/// parameter value.
		/// </summary>
		public IDictionary<string, string> AdditionalParameters { get; private set; }


		private void ValidateRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(this.trackingId))
            {
                throw new ArgumentException(
                    string.Format(EXCEPTION_MESSAGE_PARAMETER_CANNOT_BE_NULL_OR_WHITESPACE,
                                  "analyticsEvent.TrackingId"));
            }

            if (string.IsNullOrWhiteSpace(this.anonymousClientId))
            {
                throw new ArgumentException(
                    string.Format(EXCEPTION_MESSAGE_PARAMETER_CANNOT_BE_NULL_OR_WHITESPACE,
                                  "analyticsEvent.AnonymousClientId"));
            }

            if (string.IsNullOrWhiteSpace(this.eventCategory))
            {
                throw new ArgumentException(
                    string.Format(EXCEPTION_MESSAGE_PARAMETER_CANNOT_BE_NULL_OR_WHITESPACE,
                                  "analyticsEvent.EventCategory"));
            }

            if (string.IsNullOrWhiteSpace(this.eventAction))
            {
                throw new ArgumentException(
                    string.Format(EXCEPTION_MESSAGE_PARAMETER_CANNOT_BE_NULL_OR_WHITESPACE,
                                  "analyticsEvent.EventAction"));
            }
        }
    }
}