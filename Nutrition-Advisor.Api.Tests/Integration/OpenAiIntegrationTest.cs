using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NutritionAdvisor.Api.Bootstrap;
using Rystem.OpenAi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NutritionAdvisor.Api.Tests.Integration
{
    public class OpenAiIntegrationTest
    {
        [Fact]
        public async Task OpenAiApi_DoesNotThrow()
        {
            var services = new ServiceCollection();
            
            // create configuration from string
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<OpenAiIntegrationTest>()
                .Build();

            services.AddOpenAiApiServices(configuration);
            var serviceProvider = services.BuildServiceProvider();

            var openAiFactory = serviceProvider.GetRequiredService<IOpenAiFactory>();
            var openAi = openAiFactory.Create();

            var apiCall = async () => await openAi.Chat
                .RequestWithUserMessage("Hi")
                .ExecuteAsync();

            await apiCall.Should().NotThrowAsync();
        }
    }
}
