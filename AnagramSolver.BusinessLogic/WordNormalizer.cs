using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic
{
    public class WordNormalizer
    {
        public string NormalizeUserWords(string rawWord)
        {
            var words = rawWord.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach(var word in words)
            {
                var normalized = word.Trim().ToLower();
                sb.Append(normalized);
            }
            return sb.ToString();
        }

        public HashSet<string> NormalizeFileWords(IEnumerable<string> rawWords)
        {
            var distinctWords = new HashSet<string>();

            foreach(var word in rawWords)
            {
                var normalized = word.Trim().ToLower();
                distinctWords.Add(normalized);
            }

            return distinctWords;
        }
    }
}
