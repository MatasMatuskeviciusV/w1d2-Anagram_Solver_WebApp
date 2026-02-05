using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic
{
    public static class AnagramKeyBuilder
    {
        public static string BuildKey(string letters)
        {
            var arr = letters.ToCharArray();
            Array.Sort(arr);
            return new string(arr);
        }
    }
}
