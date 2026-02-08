using Microsoft.AspNetCore.Mvc;
using AnagramSolver.Contracts;
using AnagramSolver.BusinessLogic;
using AnagramSolver.WebApp.Models;

namespace AnagramSolver.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordRepository _repo;
        private readonly UserProcessor _userProcessor;
        private int PageSize = 100;

        public WordsController(IWordRepository repo, UserProcessor userProcessor)
        {
            _repo = repo;
            _userProcessor = userProcessor;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            var all = (await _repo.GetAllWordsAsync()).Select(w => (w ?? "").Trim()).Where(w => w.Length > 0).ToList();

            var totalPages = (int)Math.Ceiling(all.Count / (double)PageSize);
            if (totalPages == 0)
            {
                totalPages = 1;
            }
            if (page > totalPages)
            {
                page = totalPages;
            }

            var items = all.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var model = new PagedWordsViewModel
            {
                Items = items,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string word, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                ViewBag.Error = "Neįvestas žodis.";
                return View();
            }

            if (!_userProcessor.IsValid(word)){
                ViewBag.Error = "Įvestas per trumpas žodis.";
                ViewBag.Word = word;
                return View();
            }

            var result = await _repo.AddWordAsync(word, ct);
            ViewBag.Result = result;
            ViewBag.Word = word;

            return View();
        }
    }
}
