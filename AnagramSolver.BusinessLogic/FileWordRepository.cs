using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts;

namespace AnagramSolver.BusinessLogic
{
    public class FileWordRepository : IWordRepository
    {
        private readonly string _path;

        public FileWordRepository(string path)
        {
            _path = path;
        }

        public IEnumerable<string> GetAllWords()
        {
            return File.ReadAllLines(_path, Encoding.UTF8);
        }
    }
}
