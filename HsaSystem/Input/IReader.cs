namespace HsaSystem.Input
{
  public interface IReader
    {
        /// <summary>
        /// Reads a single Character.
        /// </summary>
        char ReadChar();

        /// <summary>
        /// Reads an entire line.
        /// </summary>
        string ReadLine();
    }
}
