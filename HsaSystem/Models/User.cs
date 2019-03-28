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

    private readonly IAskUser _askUser;

    private readonly int minLevel;
    private readonly int maxLevel;

    public User()
    {
      
    }

    public User(IAskUser askUser)
    {
      _askUser = askUser;

      minLevel = 1;
      maxLevel = 5;
    }

    /// <summary>
    /// Sets all necessary info needed to create a User.
    /// </summary>
    /// <returns></returns>
    public User Build()
    {
      Age = _askUser.ForNumber(Messages.Age());
      ActivityLevel = _askUser.ForRatingBetween(minLevel, maxLevel, Messages.ActivityLevel(minLevel, maxLevel));
      NutritionLevel = _askUser.ForRatingBetween(minLevel, maxLevel, Messages.NutritionLevel(minLevel, maxLevel));
      IsMarried = _askUser.YesOrNo(Messages.Married());
      NumberOfCoveredDependents = _askUser.ForNumber(Messages.CoveredDependents());
      SavingsDedicationLevel = _askUser.ForRatingBetween(minLevel, maxLevel, Messages.SavingsDedicationLevel(minLevel, maxLevel));
      EmployerBenefitsLevel = _askUser.ForRatingBetween(minLevel, maxLevel, Messages.EmployerBenefitsRating(minLevel, maxLevel));

      return this;
    }

  }
}