using NutritionAdvisor.Domain.Persona;

public interface IRecommendedDailyIntakeCalculator
{
    float MaxSugar(Gender gender);
    float MaxFat(float recommendedKcalIntake);
    float MaxCarbs(float recommendedKcalIntake);
    float MinProtein(float personWeight, float minProteinPerKg);
}

public class RecommendedDailyIntakeCalculator : IRecommendedDailyIntakeCalculator
{
    public float MaxSugar(Gender gender)
    {
        const float maxSugarMale = 38;
        const float maxSugarFemale = 25;
        return gender == Gender.Male ? maxSugarMale : maxSugarFemale;
    }

    public float MaxFat(float recommendedKcalIntake)
    {
        const float maxFatOfTotalKcal = 0.25f;
        return recommendedKcalIntake * maxFatOfTotalKcal;
    }

    public float MaxCarbs(float recommendedKcalIntake)
    {
        const float maxCarbsOfTotalKcal = 0.5f;
        return recommendedKcalIntake * maxCarbsOfTotalKcal;
    }

    public float MinProtein(float personWeight, float minProteinPerKg)
    {
        return personWeight * minProteinPerKg;
    }
}
