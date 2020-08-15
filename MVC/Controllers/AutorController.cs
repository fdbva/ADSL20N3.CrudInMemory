using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class AutorController : Controller
    {
        public static ConcurrentDictionary<int, AutorModel> Autores { get; } = new ConcurrentDictionary<int, AutorModel>();

        public IActionResult Index()
        {
            var autores = Autores.Select(x => x.Value);
            return View(autores);
        }

        public IActionResult Details(int id)
        {
            if (Autores.TryGetValue(id, out var autorModel))
            {
                return View(autorModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(AutorModel autorModel)
        {
            Autores.AddOrUpdate(autorModel.Id, autorModel, (key, model) => autorModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (Autores.TryGetValue(id, out var autorModel))
            {
                return View(autorModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(AutorModel autorModel)
        {
            if (Autores.TryGetValue(autorModel.Id, out var autorToEdit))
            {
                autorToEdit.Nome = autorModel.Nome;
                autorToEdit.UltimoNome = autorModel.UltimoNome;
                autorToEdit.Nascimento = autorModel.Nascimento;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(nameof(AutorModel.Id), "Id não encontrado em memória!");
            return View(autorModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (Autores.TryGetValue(id, out var autorModel))
            {
                return View(autorModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(AutorModel autorModel)
        {
            Autores.TryRemove(autorModel.Id, out var autorRemoved);

            return RedirectToAction(nameof(Index));
        }
    }
}
