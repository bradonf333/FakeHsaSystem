using System;
using System.Collections.Generic;
using System.Text;
using HsaSystem.Input;
using HsaSystem.Output;

namespace HsaSystem.Models
{
  public class User
  {
    public int Age { get; private set; }
    public int ActivityLevel { get; private set; }
    public int NutritionLevel { get; private set; }
    public bool IsMarried { get; private set; }
    public int NumberOfCoveredDependents { get; private set; }
    public int SavingsDedicationLevel { get; private set; }
    public int EmployerBenefitsLevel { get; private set; }

    private readonly IWriter _writer;
    private readonly IReader _reader;

    private readonly int minLevel;
    private readonly int maxLevel;

    public User(IWriter writer, IReader reader)
    {
      _writer = writer;
      _reader = reader;

      minLevel = 1;
      maxLevel = 5;
    }

    /// <summary>
    /// Sets all necessary info needed to create a User.
    /// </summary>
    /// <returns></returns>
    public User Build()
    {
      Age = AskUserForNumber(Messages.Age());
      ActivityLevel = AskUserForRatingBetween(minLevel, maxLevel, Messages.ActivityLevel(minLevel, maxLevel));
      NutritionLevel = AskUserForRatingBetween(minLevel, maxLevel, Messages.NutritionLevel(minLevel, maxLevel));
      IsMarried = AskUserYesOrNo(Messages.Married());
      NumberOfCoveredDependents = AskUserForNumber(Messages.CoveredDependents());
      SavingsDedicationLevel = AskUserForRatingBetween(minLevel, maxLevel, Messages.SavingsDedicationLevel(minLevel, maxLevel));
      EmployerBenefitsLevel = AskUserForRatingBetween(minLevel, maxLevel, Messages.EmployerBenefitsRating(minLevel, maxLevel));

      return this;
    }

    public bool AskUserYesOrNo(string message)
    {
      while (true)
      {
        _writer.WriteMessage(message);
        var userInput = _reader.ReadLine().ToLowerInvariant();

        if (userInput.Equals("yes"))
        {
          return true;
        }

        if (userInput.Equals("no"))
        {
          return false;
        }

        _writer.WriteMessage(Messages.ValidYesOrNo());
      }
    }

    /// <summary>
    /// Sets the Level and validates the rating is correct.
    /// </summary>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public int AskUserForRatingBetween(int min, int max, string message)
    {
      while (true)
      {
        _writer.WriteMessage(message);
        var userInput = _reader.ReadLine();

        if (int.TryParse(userInput, out int parsedInt))
        {
          if (parsedInt >= min && parsedInt <= max)
          {
            return parsedInt;
          }

          _writer.WriteMessage(Messages.ValidRangeMessage(min, max));
        }
        else
        {
          _writer.WriteMessage(Messages.ValidNumberError());
        }
      }
    }

    #region PrivateMethods

    /// <summary>
    /// Pass in a prompt and ask user for a number.
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public int AskUserForNumber(string prompt)
    {
      while (true)
      {
        _writer.WriteMessage(prompt);
        var userInput = _reader.ReadLine();
        if (int.TryParse(userInput, out int parsedInt))
        {
          return parsedInt;
        }
        else
        {
          _writer.WriteMessage(Messages.ValidNumberError());
        }
      }
    }

    #endregion
  }
}