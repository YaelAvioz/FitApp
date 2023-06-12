using FitAppServer.DTO;

namespace FitAppServer.Helper
{
    public class GradeCalculator
    {
        public static GradeDTO AnalyzeData(DoubleFoodDTO current,  GradeDTO expected)
        {
            GradeDTO finalGrade = new GradeDTO();

            // Calculate the difference between expected and current values
            double calorieDifference = Math.Abs(expected.calories_diff - current.calories);
            double totalFatDifference = Math.Abs(expected.total_fat_diff - current.total_fat);
            double calciumDifference = Math.Abs(expected.calcium_diff - current.calcium);
            double proteinDifference = Math.Abs(expected.protein_diff - current.protein);
            double carbohydrateDifference = Math.Abs(expected.carbohydrate_diff - current.carbohydrate);
            double fiberDifference = Math.Abs(expected.fiber_diff - current.fiber);
            double sugarsDifference = Math.Abs(expected.sugars_diff - current.sugars);
            double fatDifference = Math.Abs(expected.fat_diff - current.fat);

            // Calculate the total difference as a percentage
            double totalDifference = (calorieDifference + totalFatDifference + calciumDifference +
                proteinDifference + carbohydrateDifference + fiberDifference + sugarsDifference +
                fatDifference) / 8;

            // Calculate the grade based on the total difference
            int grade = 10 - (int)(totalDifference / 10);

            // Ensure the grade is within the valid range
            grade = Math.Max(1, Math.Min(10, grade));

            // Write the results
            finalGrade.grade = grade;
            finalGrade.calories_diff = calorieDifference;
            finalGrade.total_fat_diff = totalFatDifference;
            finalGrade.calcium_diff = calciumDifference;
            finalGrade.protein_diff = proteinDifference;
            finalGrade.carbohydrate_diff = carbohydrateDifference;
            finalGrade.fiber_diff = fiberDifference;
            finalGrade.sugars_diff = sugarsDifference;
            finalGrade.fat_diff = fatDifference;

            return finalGrade;
        }

        public static GradeDTO CalculateDailyExpectedNutrition(string gender, int age, double weight, int height)
        {
            GradeDTO nutrition = new GradeDTO();

            // Calculate calories
            if (gender.ToLower() == "male")
            {
                nutrition.calories_diff = 66 + (13.75 * weight) + (5 * height) - (6.75 * age);
            }
            else if (gender.ToLower() == "female")
            {
                nutrition.calories_diff = 655 + (9.56 * weight) + (1.85 * height) - (4.68 * age);
            }
            else
            {
                // Invalid gender, return empty nutrition object
                return nutrition;
            }

            // Calculate other nutritional values based on calories
            nutrition.total_fat_diff = nutrition.calories_diff * 0.3 / 9;
            nutrition.calcium_diff = nutrition.calories_diff * 0.1 / 9;
            nutrition.protein_diff = nutrition.calories_diff * 0.15 / 4;
            nutrition.carbohydrate_diff = nutrition.calories_diff * 0.5 / 4;
            nutrition.fiber_diff = nutrition.calories_diff * 0.25 / 4;
            nutrition.sugars_diff = nutrition.calories_diff * 0.1 / 4;
            nutrition.fat_diff = nutrition.total_fat_diff;

            return nutrition;
        }
    }
}
