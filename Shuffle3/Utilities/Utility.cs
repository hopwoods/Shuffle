namespace Shuffle.Utilities
{
    public class Utility
    {
        public bool IsStringTooLong(int maxLength, string String) // Todo - Add Tests (True & False)
        {
            return String.Length > maxLength;
        }
    }
}
