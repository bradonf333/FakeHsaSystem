using System;
using HsaSystem.Input;
using HsaSystem.Output;

namespace HsaSystem.Models
{
  public class User
  {
    public int Age { get; private set; }
    public int ActivityLevel { get; private set; }
    public int NutritionLevel { get; private set; }
    public bool Married { get; private set; }
    public int NumberOfCoveredDependents { get; private set; }
    public int SavingsDedicationLevel { get; private set; }
    public int EmployerBenefitsLevel { get; private set; }

    private readonly IWriter _writer;
    private readonly IReader _reader;

    public User(IWriter writer, IReader reader)
    {
      _writer = writer;
      _reader = reader;
    }

    /// <summary>
    /// Sets all necessary info needed to create a User.
    /// </summary>
    /// <returns></returns>
    public User Build()
    {
      SetUsersAge();
      SetLevelAndType(1, 5, LevelType.Activity);
      SetLevelAndType(1, 5, LevelType.Nutrition);
      IsMarried();

      return this;
    }

    private void IsMarried()
    {
      var validInput = false;

      while (!validInput)
      {
        var userMarried = AskForMarried();
        if (userMarried.Equals("yes"))
        {
          Married = true;
          validInput = true;
        }
        else if (userMarried.Equals("no"))
        {
          Married = false;
          validInput = true;
        }
        else
        {
          _writer.WriteMessage("\nPlease enter Yes or No.\n");
        }
      }
    }

    /// <summary>
    /// Ensures the input is a valid number and then sets the Users Age.
    /// </summary>
    public void SetUsersAge()
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
    /// Sets the Level and validates the rating is correct.
    /// </summary>
    /// <param name="min">Minimum number.</param>
    /// <param name="max">Maximum number.</param>
    /// <param name="levelType">Type of Level to Set and Rate.</param>
    public void SetLevelAndType(int min, int max, LevelType levelType)
    {
      var validLevelAndType = false;

      while (!validLevelAndType)
      {
        var level = AskForRating(levelType.ToString(), min, max);

        if (int.TryParse(level, out int parsedInt))
        {
          if (parsedInt >= min && parsedInt <= max)
          {
            SetLevel(levelType, parsedInt);
            validLevelAndType = true;
          }
          else
          {
            _writer.WriteMessage($"\nPlease enter a number between {min} and {max}.");
          }
        }
        else
        {
          _writer.WriteMessage("\nPlease enter a valid number.\n");
        }
      }
    }

    /// <summary>
    /// Sets the Users Level depending on which type it is.
    /// </summary>
    /// <param name="levelType"></param>
    /// <param name="level"></param>
    private void SetLevel(LevelType levelType, int level)
    {
      switch (levelType)
      {
        case LevelType.Activity:
          ActivityLevel = level;
          break;
        case LevelType.Nutrition:
          NutritionLevel = level;
          break;
      }
    }

    /// <summary>
    /// Display a message to prompt the User to enter their Age.
    /// </summary>
    /// <returns>The Users age as a string</returns>
    private string AskForAge()
    {
      _writer.WriteMessage("What is your age?");
      return _reader.ReadLine();
    }

    /// <summary>
    /// Display a message to prompt the User to enter a rating using the provided min and max.
    /// </summary>
    /// <param name="itemToRate">
    /// Item you are rating.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The Users input as a string.
    /// </returns>
    private string AskForRating(string itemToRate, int min, int max)
    {
      _writer.WriteMessage($"\nPlease Rate your {itemToRate} Level from {min} to {max}.");
      _writer.WriteMessage($"{min} being the lowest and {max} being the highest.");
      return _reader.ReadLine();
    }

    /// <summary>
    /// Ask if user is married.
    /// </summary>
    /// <returns>Returns the users input in all lower case</returns>
    private string AskForMarried()
    {
      _writer.WriteMessage("\nAre you Married? Yes or No");
      return _reader.ReadLine().ToLowerInvariant();
    }
  }


  /// <summary>
  /// Different Levels a User can have.
  /// </summary>
  public enum LevelType
  {
    Activity,
    Nutrition
  }
}