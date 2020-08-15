using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class AutorController : Controller
    {
        public static List<AutorModel> Autores { get; } = new List<AutorModel>();

        public IActionResult Index()
        {
            return View(Autores);
        }

        public IActionResult Details(int id)
        {
            //var autor = Autores.FirstOrDefault(x => x.Id == id);

            foreach (var autorModel in Autores)
            {
                if (autorModel.Id == id)
                {
                    return View(autorModel);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(AutorModel autorModel)
        {
            Autores.Add(autorModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(Autores.First(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Edit(AutorModel autorModel)
        {
            var autorToEdit = Autores.First(x => x.Id == autorModel.Id);

            autorToEdit.Nome = autorModel.Nome;
            autorToEdit.UltimoNome = autorModel.UltimoNome;
            autorToEdit.Nascimento = autorModel.Nascimento;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(Autores.First(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Delete(AutorModel autorModel)
        {
            var autorToRemove = Autores.FirstOrDefault(x => x.Id == autorModel.Id);
            Autores.Remove(autorToRemove);

            return RedirectToAction(nameof(Index));
        }
    }
}
