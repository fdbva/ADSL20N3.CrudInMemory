using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AutorController : Controller
    {
        private readonly AutorRepository _autorRepository;

        public AutorController(AutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public IActionResult Index(string keyword)
        {
            ViewBag.SearchKeyword = keyword;
            return View(_autorRepository.Search(keyword));
        }

        public IActionResult Details(int id)
        {
            return View(_autorRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(AutorModel autorModel)
        {
            _autorRepository.Add(autorModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_autorRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(AutorModel autorModel)
        {
            _autorRepository.Edit(autorModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_autorRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Delete(AutorModel autorModel)
        {
            _autorRepository.Remove(autorModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
