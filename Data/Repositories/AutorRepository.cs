using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Data.Repositories
{
    public class AutorRepository
    {
        public static List<AutorModel> Autores { get; } = new List<AutorModel>();

        public IEnumerable<AutorModel> Search(string keywords)
        {
            if (string.IsNullOrWhiteSpace(keywords))
                return GetAll();

            if(keywords.Length < 3)
                return GetAll();

            var results = new List<AutorModel>();

            foreach (var autorModel in GetAll())
            {
                //var contains = autorModel.Nome.Contains(keywords);
                //var equals = string.Equals(autorModel.Nome, keywords, StringComparison.OrdinalIgnoreCase);
                if (autorModel.Nome
                    .IndexOf(keywords, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(autorModel);
                }
                else if(autorModel.UltimoNome
                    .IndexOf(keywords, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(autorModel);
                }
            }

            //Solução por Linq
            //var whereResults = Autores
            //    .Where(x =>
            //        x.Nome.IndexOf(keywords, StringComparison.OrdinalIgnoreCase) >= 0 ||
            //        x.UltimoNome.IndexOf(keywords, StringComparison.OrdinalIgnoreCase) >= 0)
            //    .ToList();

            return results;
        }

        public IEnumerable<AutorModel> GetAll()
        {
            return Autores;
        }

        public AutorModel GetById(int id)
        {
            var autor = Autores.First(x => x.Id == id);

            return autor;
        }

        public void Add(AutorModel autorModel)
        {
            Autores.Add(autorModel);
        }

        public void Edit(AutorModel autorModel)
        {
            var autorInMemory = GetById(autorModel.Id);

            autorInMemory.Nome = autorModel.Nome;
            autorInMemory.UltimoNome = autorModel.UltimoNome;
            autorInMemory.Nascimento = autorModel.Nascimento;
        }

        public void Remove(AutorModel autorModel)
        {
            var autorInMemory = GetById(autorModel.Id);

            Autores.Remove(autorInMemory);
        }
    }
}
