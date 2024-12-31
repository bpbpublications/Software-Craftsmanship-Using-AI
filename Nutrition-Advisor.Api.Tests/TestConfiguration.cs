using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionAdvisor.Api.Tests
{
    public static class TestConfiguration
    {
        // read the test configuration from appsettings.json
        public static IConfigurationRoot Configuration { get; }

        static TestConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        // Get BaseUri
        public static string BaseUri => Configuration["BaseUri"];
    }
}
