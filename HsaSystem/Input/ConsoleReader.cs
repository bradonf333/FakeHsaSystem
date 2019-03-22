using System;

namespace HsaSystem.Input
{
  public class ConsoleReader : IReader
  {
    public char ReadChar()
    {
      return Console.ReadKey().KeyChar;
    }

    public string ReadLine()
    {
      return Console.ReadLine();
    }
  }
}