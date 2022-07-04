using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab_4.ReadData
{
    public class ReadLines
    {
        public static List<string> ReadFile(string path) 
        {
            List<string> answer = new List<string>();
            foreach (string line in System.IO.File.ReadLines(path))
            {
                answer.Add(line);
            }
            return answer;
        }

        public static List<List<string>> SplitLines(List<string> lines)
        {
            List<List<string>> answer = new List<List<string>>();
            foreach (string line in lines)
            {
                answer.Add(new List<string>(line.Split('\t')));
            }
            return answer;
        }

        public static List<List<string>> GetSplitLinesFromFile(string path)
        {
            
            return SplitLines(ReadFile(path));
        }
    }
}
