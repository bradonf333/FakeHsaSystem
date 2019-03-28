using System.IO;
using System.Reflection;
using HsaSystem.Input;
using HsaSystem.Models;
using HsaSystem.Output;

namespace HsaSystem
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var writer = new ConsoleWriter();
      var reader = new ConsoleReader();
      var askUser = new AskUser(writer, reader);
      var file = Path.Combine(Directory.GetCurrentDirectory(), @"ScenarioFile.txt");
      var transactionReader = new TransactionFileReader(file);

      var hsa = new Hsa(writer, reader, askUser);
      hsa.CreateUser();
      hsa.CreateTransactions(transactionReader);
    }
  }
}
