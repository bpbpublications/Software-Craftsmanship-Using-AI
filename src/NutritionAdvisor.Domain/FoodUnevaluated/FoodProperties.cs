namespace NutritionAdvisor.Domain.FoodUnevaluated
{
    public class FoodProperties
    {
        public string Name { get; set; }
        public float Kcal { get; set; }
        public float Protein { get; set; }
        public float Carbohydrates { get; set; }
        public float Fat { get; set; }
        public float Sugar { get; set; }

        public bool IsHealthy()
        {
            return
                Kcal < 200 &&
                Protein > 3 &&
                Carbohydrates < 20 &&
                Fat < 5 &&
                Sugar < 5;
        }
    }
}