using HsaSystem.Input;
using HsaSystem.Output;

namespace HsaSystem.Models
{
  public class User
  {
    private int Age { get; set; }
    private int ActivityLevel { get; set; }
    private int NutritionLevel { get; set; }
    private bool Married { get; set; }
    private int NumberOfCoveredDependents { get; set; }
    private int SavingsDedicationLevel { get; set; }
    private int EmployerBenefitsLevel { get; set; }

    private readonly IWriter _writer;
    private readonly IReader _reader;

    public User(IWriter writer, IReader reader)
    {
      _writer = writer;
      _reader = reader;

      SetUsersAge();
      RateActivityLevel();
    }

    /// <summary>
    /// Ensures the input is a valid number and then sets the Users Age.
    /// </summary>
    private void SetUsersAge()
    {
      var validAge = false;

      while (!validAge)
      {
        var age = AskForAge();
        if (int.TryParse(age, out int parsedInt))
        {
          Age = parsedInt;
          validAge = true;
        }
        else
        {
          _writer.WriteMessage("\nPlease enter a valid number.\n");
        }
      }
    }

    /// <summary>
    /// Ensures the input is valid and then sets the ActivityLevel Rating
    /// </summary>
    private void RateActivityLevel()
    {
      var validActivityRange = false;

      while (!validActivityRange)
      {
        var activityLevel = AskForActivityLevel();
        if (int.TryParse(activityLevel, out int parsedInt))
        {
          if (parsedInt >= 1 && parsedInt <= 5)
          {
            ActivityLevel = parsedInt;
            validActivityRange = true;
          }
          else
          {
            _writer.WriteMessage("Please enter a number between 1 and 5.");
          }
        }
        else
        {
          _writer.WriteMessage("\nPlease enter a valid number.\n");
        }
      }
    }

    private string AskForAge()
    {
      _writer.WriteMessage("What is your age?");
      return _reader.ReadLine();
    }

    private string AskForActivityLevel()
    {
      _writer.WriteMessage("Please Rate your Activity Level from 1 to 5.");
      _writer.WriteMessage("1 Being the lowest and 5 being the highest.");
      return _reader.ReadLine();
    }
  }
}