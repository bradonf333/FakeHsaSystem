using HsaSystem.Input;
using HsaSystem.Output;

namespace HsaSystem.Models
{
  public class Hsa
  {
    public User User { get; set; }
    public double Balance { get; set; }

    private readonly IWriter _writer;
    private readonly IReader _reader;

    public Hsa(IWriter writer, IReader reader)
    {
      _writer = writer;
      _reader = reader;
    }

    public void CreateTransaction()
    {
      
    }

    public void CreateUser()
    {
      var user = new User(_writer, _reader);
      user = user.Build();
    }
  }
}
