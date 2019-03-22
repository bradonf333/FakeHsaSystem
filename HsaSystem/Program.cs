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

      var hsa = new Hsa(writer, reader);
      hsa.CreateUser();
    }
  }
}
