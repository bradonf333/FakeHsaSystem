using HsaSystem.Input;
using HsaSystem.Output;

namespace HsaSystem.Models
{
  public class AskUser : IAskUser
  {
    private readonly IWriter writer;
    private readonly IReader reader;

    public AskUser(IWriter writer, IReader reader)
    {
      this.writer = writer;
      this.reader = reader;
    }

    public int ForNumber(string prompt)
    {
      while (true)
      {
        writer.WriteMessage(prompt);
        var userInput = reader.ReadLine();
        if (int.TryParse(userInput, out int parsedInt))
        {
          return parsedInt;
        }
        else
        {
          writer.WriteMessage(Messages.ValidNumberError());
        }
      }
    }

    public int ForRatingBetween(int min, int max, string prompt)
    {
      while (true)
      {
        writer.WriteMessage(prompt);
        var userInput = reader.ReadLine();

        if (int.TryParse(userInput, out int parsedInt))
        {
          if (parsedInt >= min && parsedInt <= max)
          {
            return parsedInt;
          }

          writer.WriteMessage(Messages.ValidRangeMessage(min, max));
        }
        else
        {
          writer.WriteMessage(Messages.ValidNumberError());
        }
      }
    }

    public bool YesOrNo(string prompt)
    {
      while (true)
      {
        writer.WriteMessage(prompt);
        var userInput = reader.ReadLine().ToLowerInvariant();

        if (userInput.Equals("yes"))
        {
          return true;
        }

        if (userInput.Equals("no"))
        {
          return false;
        }

        this.writer.WriteMessage(Messages.ValidYesOrNo());
      }
    }

   
  }
}