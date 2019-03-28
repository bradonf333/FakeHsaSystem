using System.Collections.Generic;
using HsaSystem.Input;
using HsaSystem.Output;

namespace HsaSystem.Models
{
  public class Hsa
  {
    public User User { get; set; }
    public double Balance { get; set; }
    public List<HsaTransaction> Transactions { get; set; }

    private readonly IWriter _writer;
    private readonly IReader _reader;
    private readonly IAskUser _askUser;
    private ITransactionReader _transactionReader;

    private const int ContributionMultiplier = 150;

    public Hsa(IWriter writer, IReader reader, IAskUser askUser)
    {
      _writer = writer;
      _reader = reader;
      _askUser = askUser;
      Transactions = new List<HsaTransaction>();
    }

    public void CalculateBalance(User user)
    {
      Balance += CalculateContributionAmount(user.SavingsDedicationLevel, user.IsMarried);
      Balance += CalculateContributionAmount(user.EmployerBenefitsLevel, user.IsMarried);
    }

    private double CalculateContributionAmount(int dedicationLevel, bool marriedMultiplier)
    {
      var contribution = dedicationLevel * ContributionMultiplier;

      if (marriedMultiplier)
      {
        contribution *= 2;
      }

      return contribution;
    }

    public void CreateTransactions(ITransactionReader transactionReader)
    {
      _transactionReader = transactionReader;
      var allTransactions = _transactionReader.Read();

      foreach (var transaction in allTransactions)
      {
        var fields = transaction.Split(',');
        Transactions.Add(new HsaTransaction
        {
          Year = fields[0],
          Month = fields[1],
          EventLikelihoodScore = fields[2],
          Amount = double.Parse(fields[3]),
          Description = fields[4]
        });
      }
    }

    public void CreateUser()
    {
      var user = new User(_askUser);
      user = user.Build();
    }
  }
}
