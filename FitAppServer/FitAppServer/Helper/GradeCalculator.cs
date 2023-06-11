using FitAppServer.DTO;

namespace FitAppServer.Helper
{
    public class GradeCalculator
    {
        public static GradeDTO AnalyzeData(DoubleFoodDTO current,  GradeDTO expected)
        {
            GradeDTO finalGrade = new GradeDTO();

            // Calculate the difference between expected and current values
            double calorieDifference = Math.Abs(expected.calories - current.calories);
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
            finalGrade.calories = calorieDifference;
            finalGrade.total_fat_diff = totalFatDifference;
            finalGrade.calcium_diff = calciumDifference;
            finalGrade.protein_diff = proteinDifference;
            finalGrade.carbohydrate_diff = carbohydrateDifference;
            finalGrade.fiber_diff = fiberDifference;
            finalGrade.sugars_diff = sugarsDifference;
            finalGrade.fat_diff = fatDifference;

            return finalGrade;
        }
    }
}
