namespace Shuffle.Utilities
{
    public class Utility
    {
        public bool IsStringTooLong(int maxLength, string String)
        {
            return String.Length > maxLength;
        }
    }
}
