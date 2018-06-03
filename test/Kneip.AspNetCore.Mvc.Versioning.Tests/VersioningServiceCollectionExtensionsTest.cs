// Copyright (c) KNEIP Communication S.A.. All rights reserved.

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;

namespace Kneip.AspNetCore.Mvc.Versioning
{
    /// <summary>
    /// Tests the <see cref="VersioningServiceCollectionExtensions"/> class.
    /// </summary>
    [TestClass]
    public class VersioningServiceCollectionExtensionsTest
    {
        /// <summary>
        /// Tests the <see cref="VersioningServiceCollectionExtensions.AddConfiguredApiVersioning"/> method.
        /// </summary>
        [TestMethod]
        public void AddConfiguredApiVersioning()
        {
            IServiceCollection services = new ServiceCollection();

            Assert.ThrowsException<ArgumentNullException>(
                () => VersioningServiceCollectionExtensions.AddConfiguredApiVersioning(null, null, null))
                .Assert(exception => Assert.AreEqual("services", exception.ParamName));

            Assert.ThrowsException<ArgumentNullException>(
                () => VersioningServiceCollectionExtensions.AddConfiguredApiVersioning(services, null, null))
                .Assert(exception => Assert.AreEqual("title", exception.ParamName));

            services.Clear();
            services.AddConfiguredApiVersioning("unit-test");
            Assert.AreNotEqual(0, services.Count, "The services collection doesn't contain any service");
            Assert.IsTrue(services.Any(s => s.ServiceType == typeof(IApiVersionDescriptionProvider)));
            Assert.IsTrue(services.Any(i => i.ServiceType == typeof(ISwaggerProvider)));
        }
    }
}
