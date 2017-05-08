
namespace Proton.VendorPortalAPI.App_Start
{
    using Foundation.Core.Cache;
    using Foundation.Core.Data;
    using Foundation.Core.DocumentDb;
    using Foundation.Core.Installation;
    using Foundation.Core.Reliability;
    using Foundation.Core.Serialization;
    using Foundation.Core.Services;
    using Foundation.Core.UnitOfWork;
    using Foundation.StructureMapAdapters.DependencyResolution;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using V1.Models;
    using Foundation.Core.ServiceBus;
    using Microsoft.ServiceBus;
    using Microsoft.ApplicationInsights;
    using Foundation.Core.Logging;
    using Foundation.Core.EF;

    /// <summary>
    /// Register application Ioc
    /// </summary>
    public class IoCConfigExtension
    {
        /// <summary>
        /// Registers application behaviuors.
        /// </summary>
        public static void Register()
        {
            var container = (GlobalConfiguration.Configuration.DependencyResolver as StructureMapWebApiDependencyResolver).Container;
            container.Configure((x) =>
            {
                // configure appInsight
                x.For<TelemetryClient>().Singleton().Use<TelemetryClient>();

                // configure log to use appinsghts
                x.For<ILogger>().Singleton().Use<AppInsightLogger>();

                // configure reliable on-premises services and overriding some behaviour
                x.For<ICircuitBreakerSettings>().Singleton().Use(new CircuitBreakerSettings() { BreakOnNumberOfExceptions = 10 });
                x.For<ICircuitBreaker>().Singleton().Use<RetryCircuitBreaker>();

                // configure access to DocumentDb setting special values for retry behaviour or with default retry
                //x.For<IUnitOfWorkAsync>().Transient().Use<DocumentDbUnitOfWork>().SetProperty((y) => { y.MaxRetryAttemptsOnThrottledRequests = 9; y.MaxRetryWaitTimeInSeconds = 30; });
                x.For<IUnitOfWorkAsync>().Transient().Use<DocumentDbUnitOfWork>();

                // configure access to SqlAzure
                x.For<IUnitOfWork>().Transient().Use<EFUnitOfWork>();
                x.For<IDataContext>().Transient().Use<SUNEntities>();


                // configure service bus retry behaviour with personal or default settings
                //x.For<IBusConfiguration>().Singleton().Use(new BusConfiguration() {  RetryingPolicy=  new RetryExponential(minBackoff: TimeSpan.FromSeconds(0),
                //                                            maxBackoff: TimeSpan.FromSeconds(30),
                //                                            maxRetryCount: 3)
                //});
                x.For<IBusConfiguration>().Singleton().Use(new BusConfiguration());

            });
            
            //set behaviour for User
            var userPolicy = BuildUserPolicy();
            container.Configure((x) =>
            {
                // configure publisher behaviour with default or special settings megabytes needs to be 1024;2048;3072;4096;5120
                //x.For<ITopicConfiguration<Customer>>().Singleton().Use< ExactlyOneProcessingTopicConfiguration<Customer>>().SetProperty((y)=> { y.DuplicateDetectionHistoryTimeWindow = new TimeSpan(0, 2, 30); y.MaxSizeInMegabytes = 3072, y.DefaultMessageTimeToLive = new TimeSpan(0, 10, 0); });
                x.For<ITopicConfiguration<User>>().Singleton().Use(new ExactlyOneProcessingTopicConfiguration<User>() { EnablePartitioning = false });

                //configure publisher manager
                x.For<ITopicManager<User>>().Singleton().Use<TopicManager<User>>();

                // configure manager for DocumentDb for Customer
                x.For<IDocumentManager<User>>().Transient().Use<DocumentDbManager<User>>().SetProperty((y) => y.OfferThroughput = 400);

                // configure behaviour of services for Customer 
                x.For<IWritableRepositoryAsync<User>>().Transient().Use<DocumentDbRepository<User>>();
                x.For<IWritableServiceAsync<User>>().Transient().Use<WritableServiceAsync<User>>();

                // configure serialization policy
                x.For<ISerializationConfigurePolicy<User>>().Singleton().Use(userPolicy);
            });

            //set behaviour for Userhistory
            var userHistoryPolicy = BuildUserHistoryPolicy();
            container.Configure((x) =>
            {
                // configure publisher behaviour with default or special settings megabytes needs to be 1024;2048;3072;4096;5120
                //x.For<ITopicConfiguration<Customer>>().Singleton().Use< ExactlyOneProcessingTopicConfiguration<Customer>>().SetProperty((y)=> { y.DuplicateDetectionHistoryTimeWindow = new TimeSpan(0, 2, 30); y.MaxSizeInMegabytes = 3072, y.DefaultMessageTimeToLive = new TimeSpan(0, 10, 0); });
                x.For<ITopicConfiguration<UserHistory>>().Singleton().Use(new ExactlyOneProcessingTopicConfiguration<UserHistory>() { EnablePartitioning = false });

                //configure publisher manager
                x.For<ITopicManager<UserHistory>>().Singleton().Use<TopicManager<UserHistory>>();

                // configure manager for DocumentDb for Customer
                x.For<IDocumentManager<UserHistory>>().Transient().Use<DocumentDbManager<UserHistory>>().SetProperty((y) => y.OfferThroughput = 400);

                // configure behaviour of services for Customer 
                x.For<IWritableRepositoryAsync<UserHistory>>().Transient().Use<DocumentDbRepository<UserHistory>>();
                x.For<IWritableServiceAsync<UserHistory>>().Transient().Use<WritableServiceAsync<UserHistory>>();

                // configure serialization policy
                x.For<ISerializationConfigurePolicy<UserHistory>>().Singleton().Use(userHistoryPolicy);
            });

            // set behaviuor for VENDOR
            var vendorPolicy = BuildVendorPolicy();
            container.Configure((x) =>
            {
                // configure behaviour of services for Country
                x.For<IWritableRepository<VENDOR>>().Transient().Use<EFRepository<VENDOR>>();
                x.For<IWritableService<VENDOR>>().Transient().Use<WritableService<VENDOR>>();

                // configure serialization policy
                x.For<ISerializationConfigurePolicy<VENDOR>>().Singleton().Use(vendorPolicy);

                x.For<ITopicConfiguration<VENDOR>>().Singleton().Use(new ExactlyOneProcessingTopicConfiguration<VENDOR>() { EnablePartitioning = true });

                //configure publisher manager
                x.For<ITopicManager<VENDOR>>().Singleton().Use<TopicManager<VENDOR>>();

            });

            // set behaviuor for INVOICE
            var invoicePolicy = BuildInvoicePolicy();
            container.Configure((x) =>
            {
                // configure behaviour of services for Country
                x.For<IWritableRepository<INVOICE>>().Transient().Use<EFRepository<INVOICE>>();
                x.For<IWritableService<INVOICE>>().Transient().Use<WritableService<INVOICE>>();

                // configure serialization policy
                x.For<ISerializationConfigurePolicy<INVOICE>>().Singleton().Use(invoicePolicy);

                x.For<ITopicConfiguration<INVOICE>>().Singleton().Use(new ExactlyOneProcessingTopicConfiguration<INVOICE>() { EnablePartitioning = true });

                //configure publisher manager
                x.For<ITopicManager<INVOICE>>().Singleton().Use<TopicManager<INVOICE>>();

            });


            // set behaviuor for INVOICE
            var centreEntityListPolicy = BuildCentreEntityListPolicy();
            container.Configure((x) =>
            {
                // configure behaviour of services for Country
                x.For<IWritableRepository<CENTRE_ENTITY_LIST>>().Transient().Use<EFRepository<CENTRE_ENTITY_LIST>>();
                x.For<IWritableService<CENTRE_ENTITY_LIST>>().Transient().Use<WritableService<CENTRE_ENTITY_LIST>>();

                // configure serialization policy
                x.For<ISerializationConfigurePolicy<CENTRE_ENTITY_LIST>>().Singleton().Use(centreEntityListPolicy);

                x.For<ITopicConfiguration<CENTRE_ENTITY_LIST>>().Singleton().Use(new ExactlyOneProcessingTopicConfiguration<CENTRE_ENTITY_LIST>() { EnablePartitioning = true });

                //configure publisher manager
                x.For<ITopicManager<CENTRE_ENTITY_LIST>>().Singleton().Use<TopicManager<CENTRE_ENTITY_LIST>>();
            });

            // set behaviuor for INVOICE
            var serviceContactPolicy = BuildServiceContactPolicy();
            container.Configure((x) =>
            {
                // configure behaviour of services for Country
                x.For<IWritableRepository<SERVICE_CONTACT>>().Transient().Use<EFRepository<SERVICE_CONTACT>>();
                x.For<IWritableService<SERVICE_CONTACT>>().Transient().Use<WritableService<SERVICE_CONTACT>>();

                // configure serialization policy
                x.For<ISerializationConfigurePolicy<SERVICE_CONTACT>>().Singleton().Use(serviceContactPolicy);

                x.For<ITopicConfiguration<SERVICE_CONTACT>>().Singleton().Use(new ExactlyOneProcessingTopicConfiguration<SERVICE_CONTACT>() { EnablePartitioning = true });

                //configure publisher manager
                x.For<ITopicManager<SERVICE_CONTACT>>().Singleton().Use<TopicManager<SERVICE_CONTACT>>();
            });
        }

        private static ISerializationConfigurePolicy<User> BuildUserPolicy()
        {
            var infoExclusions = new Dictionary<Type, string[]>();
            infoExclusions.Add(typeof(User), new[] { "password" });

            var normalExclusions = new Dictionary<Type, string[]>();
            normalExclusions.Add(typeof(User), new[] { "password" });

            var userPolicy = new SerializationConfigurationPolicy<User>()
                        .WithResolverFor(SerializationMode.Info, infoExclusions)
                        .WithResolverFor(SerializationMode.Extended, infoExclusions);

            return userPolicy;
        }

        private static ISerializationConfigurePolicy<UserHistory> BuildUserHistoryPolicy()
        {
            var userPolicy = new SerializationConfigurationPolicy<UserHistory>();
            
            return userPolicy;
        }

        private static ISerializationConfigurePolicy<VENDOR> BuildVendorPolicy()
        {
           var vendorPolicy = new SerializationConfigurationPolicy<VENDOR>();

            return vendorPolicy;
        }

        private static ISerializationConfigurePolicy<INVOICE> BuildInvoicePolicy()
        {
            var infoExclusions = new Dictionary<Type, string[]>();
            infoExclusions.Add(typeof(INVOICE), new[] { "buCode", "centreCode", "allocation", "journalType", "journalSource", "vendorCode" });

            var normalExclusions = new Dictionary<Type, string[]>();
            normalExclusions.Add(typeof(INVOICE), new[] { "buCode", "centreCode", "allocation", "journalType", "journalSource","vendorCode" });

            var vendorPolicy = new SerializationConfigurationPolicy<INVOICE>()
                        .WithResolverFor(SerializationMode.Info, infoExclusions)
                        .WithResolverFor(SerializationMode.Normal, infoExclusions);
            return vendorPolicy;
        }

        private static ISerializationConfigurePolicy<CENTRE_ENTITY_LIST> BuildCentreEntityListPolicy()
        {
            var centreEntityListPolicy = new SerializationConfigurationPolicy<CENTRE_ENTITY_LIST>();
            return centreEntityListPolicy;
        }
        private static ISerializationConfigurePolicy<SERVICE_CONTACT> BuildServiceContactPolicy()
        {
            var serviceContactPolicy = new SerializationConfigurationPolicy<SERVICE_CONTACT>();
            return serviceContactPolicy;
        }

    }
}