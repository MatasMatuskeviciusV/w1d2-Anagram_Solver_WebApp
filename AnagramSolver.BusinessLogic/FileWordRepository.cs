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
        private readonly WordNormalizer _normalized = new WordNormalizer();

        public FileWordRepository(string path)
        {
            _path = path;
        }

        public async Task<IEnumerable<string>> GetAllWordsAsync(CancellationToken ct = default)
        {
            var lines = await File.ReadAllLinesAsync(_path, Encoding.UTF8, ct);
            return lines;
        }

        public async Task<AddWordResult> AddWordAsync(string rawWord, CancellationToken ct  = default)
        {
            var word = _normalized.NormalizeUserWords(rawWord);

            if (string.IsNullOrWhiteSpace(word))
            {
                return AddWordResult.Invalid;
            }

            var linesRaw = await File.ReadAllLinesAsync(_path, Encoding.UTF8, ct);

            var linesClean = _normalized.NormalizeFileWords(linesRaw);

            if(linesClean.Any(l => string.Equals(l, word, StringComparison.Ordinal)))
            {
                return AddWordResult.AlreadyExists;
            }

            await File.AppendAllTextAsync(_path, word + Environment.NewLine, Encoding.UTF8, ct);

            return AddWordResult.Added;
        }
    }
}
