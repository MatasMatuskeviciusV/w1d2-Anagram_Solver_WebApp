using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts
{
    public interface IAnagramSolver
    {
        IList<string> GetAnagrams(string myWords);
    }
}
