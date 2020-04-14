using Microsoft.Extensions.DependencyInjection;

namespace AutoMapper.Extensions.Microsoft.DependencyInjection.Tests
{
    using System;
    using System.Reflection;
    using Shouldly;
    using Xunit;

    public class AssemblyResolutionWithMultipleAddTests
    {
        private static readonly IServiceProvider _provider;

        static AssemblyResolutionWithMultipleAddTests()
        {
            _provider = BuildServiceProvider();
        }

        private static ServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(Source).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(TestApp.Source).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(Dest).GetTypeInfo().Assembly);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        [Fact]
        public void ShouldResolveConfiguration()
        {
            _provider.GetService<IConfigurationProvider>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldConfigureProfiles()
        {
            _provider.GetService<IConfigurationProvider>().GetAllTypeMaps().Length.ShouldBe(5);
        }

        [Fact]
        public void ShouldResolveMapper()
        {
            _provider.GetService<IMapper>().ShouldNotBeNull();
        }

        [Fact]
        public void CanRegisterTwiceWithoutProblems()
        {
            new Action(() => BuildServiceProvider()).ShouldNotThrow();
        }
    }
}