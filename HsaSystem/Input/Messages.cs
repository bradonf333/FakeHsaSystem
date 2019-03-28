using System.Text;

namespace HsaSystem.Input
{
  public static class Messages
  {
    public static string Age()
    {
      return "\nWhat is your Age?";
    }

    public static string ActivityLevel(int min, int max)
    {
      return BuildRatingMessage("\nPlease rate your Activity Level", min, max);
    }

    public static string NutritionLevel(int min, int max)
    {
      return BuildRatingMessage("\nPlease rate your Nutrition Level", min, max);
    }

    public static string Married()
    {
      return "\nAre you Married? Yes or No";
    }

    public static string CoveredDependents()
    {
      return "\nHow many covered dependents do you have?";
    }

    public static string SavingsDedicationLevel(int min, int max)
    {
      return BuildRatingMessage("\nHow dedicated to savings are you", min, max);
    }

    public static string EmployerBenefitsRating(int min, int max)
    {
      return BuildRatingMessage("\nPlease rate your Employers Benefits", min, max);
    }

    public static string ValidNumberError()
    {
      return "\nPlease enter a valid number";
    }

    public static string ValidYesOrNo()
    {
      return "\nPlease enter Yes or No.";
    }

    public static string ValidRangeMessage(int min, int max)
    {
      return $"\nPlease enter a number between {min} and {max}.";
    }


    private static string BuildRatingMessage(string preMessage, int min, int max)
    {
      var sb = new StringBuilder();
      sb.Append($"{preMessage} from {min} to {max}?\n");
      sb.Append($"{min} being the lowest and {max} being the highest");
      return sb.ToString();
    }
  }
}
