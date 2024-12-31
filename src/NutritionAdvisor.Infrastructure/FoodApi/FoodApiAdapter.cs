using NutritionAdvisor.Domain.FoodUnevaluated;

namespace NutritionAdvisor.Infrastructure.FoodApi
{

    // Placeholder implementation for demonstration purposes
    public class FoodApiAdapter : IFoodApiAdapter
    {
        private readonly Dictionary<string, Recipe> recipeDatabase;
        private readonly Dictionary<string, FoodProperties> foodDatabase;

        public FoodApiAdapter()
        {
            // Initialize with some dummy data
            recipeDatabase = new Dictionary<string, Recipe>
            {
                {
                    "Smoothie",
                    new Recipe
                    { Name = "Smoothie",
                        Ingredients =
                        [
                            new() { Name = "Apple", AmountG = 100 },
                            new() { Name = "Banana", AmountG = 150 }
                        ]
                    }
                },
                {
                    "French Fries With Gyros",
                    new Recipe
                    { Name = "French Fries With Gyros",
                        Ingredients =
                        [
                            new() { Name = "French Fries", AmountG = 50 },
                            new() { Name = "Gyros", AmountG = 100 }
                        ]
                    }
                }
                // Add more recipes as needed
            };

            foodDatabase = new Dictionary<string, FoodProperties>
            {
                { "Apple", new FoodProperties { Name = "Apple", Kcal = 52, Protein = 0.3f, Carbohydrates = 14, Fat = 0.2f, Sugar = 10.4f } },
                { "Banana", new FoodProperties { Name = "Banana", Kcal = 105, Protein = 1.3f, Carbohydrates = 27, Fat = 0.3f, Sugar = 14.4f } },
                { "Chocolate", new FoodProperties { Name = "Chocolate", Kcal = 546, Protein = 5.3f, Carbohydrates = 58, Fat = 32.4f, Sugar = 52.2f } },
                { "Chicken Breast", new FoodProperties { Name = "Chicken Breast", Kcal = 165, Protein = 31, Carbohydrates = 0, Fat = 3.6f, Sugar = 0 } },
                { "Broccoli", new FoodProperties { Name = "Broccoli", Kcal = 34, Protein = 2.8f, Carbohydrates = 7, Fat = 0.4f, Sugar = 1.7f } },
                { "Brown Rice", new FoodProperties { Name = "Brown Rice", Kcal = 111, Protein = 2.6f, Carbohydrates = 23, Fat = 0.9f, Sugar = 0.4f } },
                { "Gyros", new FoodProperties { Name = "Gyros", Kcal = 470, Protein = 20, Carbohydrates = 25, Fat = 30, Sugar = 0 } },
                { "Pasta", new FoodProperties { Name = "Pasta", Kcal = 131, Protein = 5.5f, Carbohydrates = 25, Fat = 1.1f, Sugar = 0.7f } },
                { "Tofu", new FoodProperties { Name = "Tofu", Kcal = 144, Protein = 15, Carbohydrates = 3.5f, Fat = 8, Sugar = 0.7f } },
                { "Avocado", new FoodProperties { Name = "Avocado", Kcal = 160, Protein = 2, Carbohydrates = 9, Fat = 15, Sugar = 0.7f } },
                { "Almonds", new FoodProperties { Name = "Almonds", Kcal = 576, Protein = 21, Carbohydrates = 22, Fat = 49, Sugar = 4.2f } },
                { "Quinoa", new FoodProperties { Name = "Quinoa", Kcal = 120, Protein = 4.1f, Carbohydrates = 21, Fat = 1.9f, Sugar = 0.9f } },
                { "Milk", new FoodProperties { Name = "Milk", Kcal = 42, Protein = 3.4f, Carbohydrates = 5, Fat = 1.2f, Sugar = 5 } },
                { "Yogurt", new FoodProperties { Name = "Yogurt", Kcal = 59, Protein = 10, Carbohydrates = 3.6f, Fat = 0.4f, Sugar = 3.6f } },
                { "Spinach", new FoodProperties { Name = "Spinach", Kcal = 23, Protein = 2.9f, Carbohydrates = 3.6f, Fat = 0.4f, Sugar = 0.4f } },
                { "Salmon", new FoodProperties { Name = "Salmon", Kcal = 208, Protein = 20, Carbohydrates = 0, Fat = 13,}},
                { "French Fries", new FoodProperties { Name = "French Fries", Kcal = 312, Protein = 3.4f, Carbohydrates = 41, Fat = 15, Sugar = 0.6f } },
                { "Pizza", new FoodProperties { Name = "Pizza", Kcal = 266, Protein = 12, Carbohydrates = 31, Fat = 10, Sugar = 2.5f } },
                { "Coca Cola", new FoodProperties { Name = "Coca Cola", Kcal = 42, Protein = 0, Carbohydrates = 11, Fat = 0, Sugar = 11 } },
                { "Beer", new FoodProperties { Name = "Beer", Kcal = 43, Protein = 0.5f, Carbohydrates = 3.6f, Fat = 0, Sugar = 0.3f } },
                { "Wine", new FoodProperties { Name = "Wine", Kcal = 83, Protein = 0.1f, Carbohydrates = 2.6f, Fat = 0, Sugar = 0.6f } },
                { "Vodka", new FoodProperties { Name = "Vodka", Kcal = 231, Protein = 0, Carbohydrates = 0, Fat = 0, Sugar = 0 } },
                { "Whiskey", new FoodProperties { Name = "Whiskey", Kcal = 250, Protein = 0, Carbohydrates = 0, Fat = 0, Sugar = 0 } },
                { "Tequila", new FoodProperties { Name = "Tequila", Kcal = 250, Protein = 0, Carbohydrates = 0, Fat = 0, Sugar = 0 } },
                { "Rum", new FoodProperties { Name = "Rum", Kcal = 231, Protein = 0, Carbohydrates = 0, Fat = 0, Sugar = 0 } },
                { "Gin", new FoodProperties { Name = "Gin", Kcal = 263, Protein = 0, Carbohydrates = 0, Fat = 0, Sugar = 0 } },
                { "Cognac", new FoodProperties { Name = "Cognac", Kcal = 250, Protein = 0}}
            };
        }

        public Task<Recipe> GetRecipeAsync(string foodItem)
        {
            // Placeholder implementation, you should replace this with actual logic
            if (recipeDatabase.TryGetValue(foodItem, out var recipe))
            {
                return Task.FromResult(recipe);
            }
            else
            {
                return Task.FromResult<Recipe>(null);
            }
        }

        public Task<IEnumerable<string>> GetIngredientsAsync(string compositeFood)
        {
            if (recipeDatabase.TryGetValue(compositeFood, out var recipe))
            {
                return Task.FromResult<IEnumerable<string>>(recipe.Ingredients.Select(i => i.Name));
            }
            else
            {
                throw new InvalidOperationException($"Recipe '{compositeFood}' not found in the database.");
            }
        }

        public Task<FoodProperties> GetFoodPropertyAsync(string foodItem)
        {
            if (foodDatabase.TryGetValue(foodItem, out var foodProperties))
            {
                return Task.FromResult(foodProperties);
            }
            else
            {
                throw new InvalidOperationException($"Food item '{foodItem}' not found in the database.");
            }
        }
    }
}
