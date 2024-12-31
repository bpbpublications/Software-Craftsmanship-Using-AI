using NutritionAdvisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionAdvisor.Pseudo
{
    public static class FoodProductsProviderExtensions
    {
        public static IFoodProductsProvider WithCache(this IFoodProductsProvider provider)
            => new FoodProductsProviderWithCache(provider);

        public static void Test()
        {
            FoodProductsProvider provider = new FoodProductsProvider();

            provider.WithCache();
        }
    }

    public class FoodProductsProviderWithCache : IFoodProductsProvider
    {
        private readonly ICache _cache;
        private readonly IFoodProductsProvider _foodProductsProvider;

        public FoodProductsProviderWithCache(..., IFoodProductsProvider foodProductsProvider)
        {
            _foodProductsProvider = foodProductsProvider;
        }

        public Task<Dictionary<string, FoodProperties>> GetFoodProductsAsync(IEnumerable<string> items)
        {
            // New behavior - check the cache first
            var leftToRetrieve = items.Copy();
            var products = new List<FoodProperties>();
            foreach (item in items)
            {
                if (cache.Contains(item){
                    leftToRetrieve.Remove(item);
                    products.Add(cache.GetItem(item);
                }
            }
            
            // Calling the original provider
            var productsLeft = provider.GetFoodProducts(leftToRetrieve);

            products = products.Union(productsLeft);

            return products;
        }
    }

    internal interface ICache
    {
    }
}
