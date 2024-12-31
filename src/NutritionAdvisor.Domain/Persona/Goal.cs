namespace NutritionAdvisor.Domain.Persona
{
    public class Goal
    {
        private const string LeanProteins = "Lean proteins (chicken, fish, tofu)";
        private const string Vegetables = "Vegetables (especially leafy greens)";
        private const string OatsAndQuinoa = "Oats and quinoa";
        private const string LowFatDairy = "Low-fat dairy or dairy alternatives";
        private const string ComplexCarbohydrates = "Complex carbohydrates (brown rice, pasta)";
        private const string HealthyFats = "Healthy fats (avocado, nuts)";
        private const string WholeGrains = "Whole grains (brown rice, quinoa)";
        private const string FruitsAndVegetables = "Fruits and vegetables";
        private const string NutsAndSeeds = "Nuts and seeds";

        public static readonly Goal LoseWeight = new("Lose Weight", -500,
            [ LeanProteins, Vegetables, OatsAndQuinoa, LowFatDairy ], minProteinPerKg: 1.2f);

        public static readonly Goal GainWeight = new("Gain Weight", 250,
            [ LeanProteins, ComplexCarbohydrates, HealthyFats, LowFatDairy ], minProteinPerKg: 0.8f);

        public static readonly Goal BecomeFit = new("Become Fit", 0,
            [ LeanProteins, WholeGrains, FruitsAndVegetables, NutsAndSeeds ],
        minProteinPerKg: 1.6f);

        public string Name { get; }
        public int RecommendedKcalAdjustment { get; }
        public IEnumerable<string> FoodRecommendations { get; }
        public float MinProteinPerKg { get; }

        private Goal(string name, int recommendedKcalAdjustment, IEnumerable<string> foodRecommendations, float minProteinPerKg)
        {
            Name = name;
            RecommendedKcalAdjustment = recommendedKcalAdjustment;
            FoodRecommendations = foodRecommendations;
            MinProteinPerKg = minProteinPerKg;
        }
    }
}
