using AI_lab_4.Models;
using AI_lab_4.ReadData;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AI_lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            getSelectedGenresCountAsync();
            //getAllGenresCount();
        }

        static async Task getSelectedGenresCountAsync()
        {
            List<string> allowedGenres = new List<string> { "Crime Fiction", "Horror", "Suspense", "Romance novel", "Detective fiction", "Dystopia", "Spy fiction", "Adventure novel", "Historical novel", "Autobiography", "Children's literature", "High fantasy", "Science" };

            List<Book> books = ModifyData.ShapeData(ReadLines.GetSplitLinesFromFile(@"C:\Users\Gordon Freeman\Desktop\booksummaries.txt"));
            books = ModifyData.FilterFenres(books, allowedGenres);

            await using FileStream createStream = File.Create(@"H:\AI LAB 4\dane.json");
            await JsonSerializer.SerializeAsync(createStream, books);

            Console.WriteLine("Total Books: " + books.Count);


            foreach (string s in allowedGenres)
            {
                Console.WriteLine(s + ": " + books.FindAll(o => o.genre == s).Count);
            }
        }

        static void getAllGenresCount()
        {

            List<string> allowedGenres = new List<string> { "Crime Fiction", "Horror", "Suspense", "Romance novel", "Detective fiction", "Dystopia", "Spy fiction" };

            List<Book> books = ModifyData.ShapeData(ReadLines.GetSplitLinesFromFile(@"C:\Users\Gordon Freeman\Desktop\booksummaries.txt"));

            Console.WriteLine("Total Books: " + books.Count);

            List<(string, int)> genresAndCount = new List<(string, int)>();

            foreach (string s in ModifyData.GetAllGenres(books))
            {
                genresAndCount.Add((s, books.FindAll(o => o.genresHolder.Contains(s)).Count));

            }
            genresAndCount.Sort((o1, o2) => o2.Item2 - o1.Item2);

            foreach ((string, int) si in genresAndCount)
            {
                Console.WriteLine(si.Item1 + ": " + si.Item2);

            }
        }
    }
}
