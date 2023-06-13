using FitAppServer.DTO;

namespace FitAppServer.Helper
{
    public class GradeCalculator
    {
        public static GradeDTO AnalyzeData(DoubleFoodDTO current,  GradeDTO expected)
        {
            GradeDTO finalGrade = new GradeDTO();

            // Calculate the difference between expected and current values
            finalGrade.calories_diff = expected.calories_diff - current.calories;
            finalGrade.total_fat_diff = expected.total_fat_diff - current.total_fat;
            finalGrade.calcium_diff = expected.calcium_diff - current.calcium;
            finalGrade.protein_diff = expected.protein_diff - current.protein;
            finalGrade.carbohydrate_diff = expected.carbohydrate_diff - current.carbohydrate;
            finalGrade.fiber_diff = expected.fiber_diff - current.fiber;
            finalGrade.sugars_diff = expected.sugars_diff - current.sugars;
            finalGrade.fat_diff =  expected.fat_diff - current.fat;

            double calorieDifference = Math.Abs(finalGrade.calories_diff);
            double totalFatDifference = Math.Abs(finalGrade.total_fat_diff);
            double calciumDifference = Math.Abs(finalGrade.calcium_diff);
            double proteinDifference = Math.Abs(finalGrade.protein_diff);
            double carbohydrateDifference = Math.Abs(finalGrade.carbohydrate_diff);
            double fiberDifference = Math.Abs(finalGrade.fiber_diff);
            double sugarsDifference = Math.Abs(finalGrade.sugars_diff);
            double fatDifference = Math.Abs(finalGrade.fat_diff);

            // Calculate the total difference as a percentage
            double totalDifference = (calorieDifference + totalFatDifference + calciumDifference +
                proteinDifference + carbohydrateDifference + fiberDifference + sugarsDifference +
                fatDifference) / 8;

            // Calculate the grade based on the total difference
            int grade = 10 - (int)(totalDifference / 10);

            // Ensure the grade is within the valid range
            grade = Math.Max(1, Math.Min(10, grade));

            // Write the result
            finalGrade.grade = grade;

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
