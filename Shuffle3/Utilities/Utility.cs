namespace Shuffle.Utilities
{
    public class Utility
    {
        public static bool IsStringTooLong(int maxLength, string String)
        {
            return String.Length > maxLength;
        }
    }
}
