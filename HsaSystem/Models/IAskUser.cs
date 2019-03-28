namespace HsaSystem.Models
{
  public interface IAskUser
  {
    int ForRatingBetween(int minLevel, int maxLevel, string prompt);
    bool YesOrNo(string prompt);
    int ForNumber(string prompt);
  }
}