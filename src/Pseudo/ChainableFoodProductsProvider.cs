using NutritionAdvisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NutritionAdvisor.Pseudo
{
    public class FoodProvidersDirector
    {
        public IChainableFoodProductsProvider ChainInTheProviderSequence(params IFoodProductsProvider[] providers)
        {
            var chainables = providers.Select(p => new ChainableFoodProductsProvider(p));

            for (i = 0; i < chainables.count - 1; i++)
            {
                chainables[i].SetNext(chainables[i + 1];
            }

            return chainables.First();
        }

        public static async Task Demo()
        {
            var food = new List<string> { "apple", "banana", "orange" };
            var director = new FoodProvidersDirector();
            var provider = director.ChainInTheProviderSequence(mostImportantProvider, secondInImportanceProvider, leastImportantProvider);
            // Will first call mostImportantProvider,
            // if food missing then secondInImportanceProvider,
            // if food still missing then leastImportantProvider.
            var products = await provider.GetFoodProductsAsync(food);
        }
    }

    public interface IChainableFoodProductsProvider: IFoodProductsProvider
    {
        void SetNext(IChainableFoodProductsProvider provider);
    }

    public class ChainableFoodProductsProvider : IChainableFoodProductsProvider
    {
        private readonly IChainableFoodProductsProvider _currentProvider;
        private IChainableFoodProductsProvider _nextProvider;

        public ChainableFoodProductsProvider(IChainableFoodProductsProvider currentProvider)
        {
            _currentProvider = currentProvider;
        }

        public void SetNext(IChainableFoodProductsProvider provider)
        {
            _nextProvider = provider;
        }

        public async Task<Dictionary<string, FoodProperties>> GetFoodProductsAsync(IEnumerable<string> food)
        {
            var foodPropertiesFound = await _currentProvider.GetFoodProductsAsync(food);
            var missingFood = foodPropertiesFound.Keys.Except(food);
            if (missingFood.Any() && _nextProvider != null)
            {
                // Moving to the next part in the chain
                var remaining = await _nextProvider.GetFoodProductsAsync(missingFood);
                foodPropertiesFound.Add(remaining);
            }

            var stillMissingFood = foodPropertiesFound.Keys.Except(food).Any();
            if (stillMissingFood)
            {
                throw new Exception("Food not found");
            }

            return foodProperties;
        }
    }
}
