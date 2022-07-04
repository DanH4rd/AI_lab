using AI_lab_4.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_lab_4.ReadData
{
    public class ModifyData
    {
        static public List<Book> ShapeData(List<List<string>> records) 
        {
            List<Book> answer = new List<Book>();


            foreach (List<string> line in records)
            {

                if (line[2] != "" && 
                    line[3] != "" && 
                    line[6] != "" && 
                    (line[5] != "" ? JObject.Parse(line[5]).Properties().Select(p => p.Value.ToString()).ToList().Count > 0 : false)) 
                {
                    answer.Add(new Book
                    {
                        bookName = line[2],
                        author = line[3],
                        description = line[6],
                        genresHolder = JObject.Parse(line[5]).Properties().Select(p => p.Value.ToString()).ToList()
                    });
                }
                
            }



            Console.WriteLine("Initially rows: " + records.Count);
            Console.WriteLine("Remained rows: " + answer.Count);

            return answer;
        }

        public static List<string> GetAllGenres(List<Book> books) 
        {
            HashSet<string> answer = new HashSet<string>();

            foreach (Book book in books) 
            {
                answer.UnionWith(book.genresHolder);
            }

            return answer.ToList();
        }

        public static List<Book> FilterFenres(List<Book> books, List<string> allowedGenres)
        {
            List<Book> answer = new List<Book>();

            foreach (Book book in books)
            {
                if (book.genresHolder.Intersect(allowedGenres).ToList().Count > 0) 
                {
                    List<string> buffer = book.genresHolder.Intersect(allowedGenres).ToList();
                    book.genre = buffer[0];
                    book.genresHolder = null;
                    answer.Add(book);
                }
            }

            return answer;
        }
    }
}
