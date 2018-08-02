using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
