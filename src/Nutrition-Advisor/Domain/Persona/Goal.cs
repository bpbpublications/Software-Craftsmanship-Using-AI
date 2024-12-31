namespace Nutrition_Advisor.Domain.Person
{
    public class Goal
    {
        public static readonly Goal LoseWeight = new("Lose Weight", -500, new string[]
        {
            "Lean proteins (chicken, fish, tofu)",
            "Vegetables (especially leafy greens)",
            "Oats and quinoa",
            "Low-fat dairy or dairy alternatives"
        }, minProteinPerKg: 1.2f);

        public static readonly Goal GainWeight = new("Gain Weight", 250, new string[]
        {
            "Lean proteins (chicken, fish, tofu)",
            "Complex carbohydrates (brown rice, pasta)",
            "Healthy fats (avocado, nuts)",
            "Low-fat dairy or dairy alternatives"
        }, minProteinPerKg: 0.8f);

        public static readonly Goal BecomeFit = new("Become Fit", 0, new string[]
        {
            "Lean proteins (chicken, fish, tofu)",
            "Whole grains (brown rice, quinoa)",
            "Fruits and vegetables",
            "Nuts and seeds"
        }, minProteinPerKg: 1.6f);

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
