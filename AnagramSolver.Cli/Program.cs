using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using AnagramSolver.BusinessLogic;

namespace AnagramSolver.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            int minUserLen = int.Parse(config["Settings:MinUserWordLength"]);
            int maxResults = int.Parse(config["Settings:MaxResults"]);
            int maxWords = int.Parse(config["Settings:MaxWordsInAnagram"]);
            string path = config["Dictionary:WordFilePath"];

            var repo = new FileWordRepository(path); 
            var normalizer = new WordNormalizer();
            var mapBuilder = new AnagramMapBuilder();

            var rawWords = repo.GetAllWords();
            var cleanWords = normalizer.NormalizeFileWords(rawWords);
            var map = mapBuilder.Build(cleanWords);

            var solver = new DefaultAnagramSolver(map, maxResults, maxWords);

            Console.WriteLine("Įveskite žodžius: ");
            string input = Console.ReadLine();

            if(input.Length < minUserLen)
            {
                Console.WriteLine("Įvestas per trumpas žodis.");
                return;
            }

            var combined = normalizer.NormalizeUserWords(input);
            var sortedKey = AnagramKeyBuilder.BuildKey(combined);

            var results = solver.GetAnagrams(sortedKey);

            if(results.Count == 0)
            {
                Console.WriteLine("Anagramų nerasta.");
            }
            else
            {
                Console.WriteLine("Anagramos: ");
                foreach(var r in results)
                {
                    Console.WriteLine(r);
                }
            }
        }
    }
}