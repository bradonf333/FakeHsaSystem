namespace HsaSystem.Models
{
  public class HsaTransaction
  {
    public string Year { get; set; }
    public string Month { get; set; }
    public string EventLikelihoodScore { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
  }
}
