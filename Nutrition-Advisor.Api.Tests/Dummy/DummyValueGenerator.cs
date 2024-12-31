using AutoFixture;
using NutritionAdvisor.Domain.Persona;

namespace NutritionAdvisor.Tests.Api.Dummy
{
    public static class DummyValueGenerator
    {
        private static readonly Fixture _fixture;

        static DummyValueGenerator()
        {
            _fixture = new Fixture();
            // Always resolve the same goal
            _fixture.Register(() => Goal.BecomeFit);
        }

        public static T Any<T>() => _fixture.Create<T>();
    }
}
