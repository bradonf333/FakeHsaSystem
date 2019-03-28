using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HsaSystem.Input
{
  public class TransactionFileReader : ITransactionReader
  {
    private string filePath;

    public TransactionFileReader(string path)
    {
      filePath = path;
    }

    public List<string> Read()
    {
      var fileContents = File.ReadAllLines(filePath);
      return fileContents.ToList();
    }

    public string ReadLine()
    {
      throw new NotImplementedException();
    }
  }

  public interface ITransactionReader
  {
    List<string> Read();
  }
}
