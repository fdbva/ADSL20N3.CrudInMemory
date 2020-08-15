using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class LivroController : Controller
    {
        public static List<LivroModel> Livros { get; } = new List<LivroModel>();

        public IActionResult Index()
        {
            return View(Livros);
        }

        public IActionResult Details(int id)
        {
            //var livro = Livros.FirstOrDefault(x => x.Id == id);

            foreach (var livroModel in Livros)
            {
                if (livroModel.Id == id)
                {
                    return View(livroModel);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(LivroModel livroModel)
        {
            Livros.Add(livroModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(Livros.First(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Edit(LivroModel livroModel)
        {
            var livroToEdit = Livros.First(x => x.Id == livroModel.Id);

            livroToEdit.Isbn = livroModel.Isbn;
            livroToEdit.Publicacao = livroModel.Publicacao;
            livroToEdit.Titulo = livroModel.Titulo;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(Livros.First(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Delete(LivroModel livroModel)
        {
            var livroToRemove = Livros.FirstOrDefault(x => x.Id == livroModel.Id);
            Livros.Remove(livroToRemove);

            return RedirectToAction(nameof(Index));
        }
    }
}
