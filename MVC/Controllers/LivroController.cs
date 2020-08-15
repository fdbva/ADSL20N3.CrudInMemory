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

            if (livroModel.AutorId > 0)
            {
                var autor = AutorController
                    .Autores
                    .First(x => x.Id == livroModel.AutorId);

                livroModel.Autor = autor;
                autor.Livros.Add(livroModel);
            }

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

            if (livroToEdit.AutorId != livroModel.AutorId)
            {
                var autor = AutorController
                    .Autores
                    .First(x => x.Id == livroModel.AutorId);

                var autorAntigo = AutorController
                    .Autores
                    .First(x => x.Id == livroToEdit.AutorId);

                var livroParaRemover = autorAntigo.Livros.First(x => x.Id == livroToEdit.Id);
                autorAntigo.Livros.Remove(livroParaRemover);
                autor.Livros.Add(livroToEdit);

                livroToEdit.Autor = autor;
                livroToEdit.AutorId = autor.Id;
            }

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

            if (livroModel.AutorId > 0)
            {
                var autor = AutorController
                    .Autores
                    .First(x => x.Id == livroModel.AutorId);

                autor.Livros.Remove(livroModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
