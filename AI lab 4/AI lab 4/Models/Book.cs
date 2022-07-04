using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab_4.Models
{
    public class Book
    {
        public string bookName { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public string description { get; set; }
        public List<string> genresHolder { get; set; }

        public override string ToString()
        {
            string answer = String.Format("Name: {0}\nAuthor: {1}\nGenre: {2}\nDescription: {3}", bookName, author, genre, description);
            if (genresHolder != null) 
            {
                String genresStr = "";
                foreach (string s in genresHolder) 
                {
                    genresStr += s + ", ";
                }
                answer += "\nGenres list: " + genresStr;
            }
            return answer;
        }

    }
}
