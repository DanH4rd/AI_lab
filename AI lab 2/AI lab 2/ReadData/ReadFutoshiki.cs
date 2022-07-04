using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_lab_2.ReadData
{
    class ReadFutoshiki : ReadPlane
    {
        public Plane ReadPlane(string fullFilePath)
        {
            Plane answer = null;
            string[] lines = System.IO.File.ReadAllLines(fullFilePath);

            if ((lines.Length - (lines.Length-1)/2) != (lines[0].Length - (lines[0].Length-1)/2))
                throw new Exception();

            int i = 0, j = 0, l = 0; ;

            List<Field> fields = new List<Field>();
            Dictionary<(int,int), List<(int, int)>> greaterThanDict = new Dictionary<(int, int), List<(int, int)>>();

            foreach (string line in lines)
            {

                foreach (char chara in line)
                {
                    if (l % 2 == 0)
                    {
                        switch (chara)
                        {
                            case 'X':
                                continue;
                            case 'x':
                                fields.Add(new Field
                                {
                                    coordinates = (i, j),
                                    value = int.MinValue,
                                    greaterThan = null,
                                    possibleValues = new List<int>(Enumerable.Range(1, (lines.Length - (lines.Length - 1) / 2)).ToList())
                                });
                                i++;
                                break;
                            case '>':
                                if (greaterThanDict.ContainsKey((i - 1, j)))
                                {
                                    greaterThanDict[(i - 1, j)].Add((i, j));
                                }
                                else
                                    greaterThanDict.Add((i - 1, j), new List<(int, int)> { (i, j) });
                                break;

                            case '<':
                                if (greaterThanDict.ContainsKey((i, j)))
                                {
                                    greaterThanDict[(i, j)].Add((i - 1, j));
                                }
                                else
                                    greaterThanDict.Add((i, j), new List<(int, int)> { (i - 1, j) });
                                break;

                            case '-':
                                break;

                            default:
                                fields.Add(new Field
                                {
                                    coordinates = (i, j),
                                    value = Int32.Parse(chara.ToString()),
                                    greaterThan = null,
                                    possibleValues = Enumerable.Range(1, (lines.Length - (lines.Length - 1) / 2)).ToList()
                                });

                                i++;
                                break;
                        }
                    }
                    else 
                    {
                        switch (chara)
                        {
                            case '>':
                                if (greaterThanDict.ContainsKey((i, j - 1)))
                                {
                                    greaterThanDict[(i, j - 1)].Add((i, j));
                                }
                                else
                                    greaterThanDict.Add((i, j - 1), new List<(int, int)> { (i, j) });
                                break;

                            case '<':
                                if (greaterThanDict.ContainsKey((i, j)))
                                {
                                    greaterThanDict[(i, j)].Add((i, j - 1));
                                }
                                else
                                    greaterThanDict.Add((i, j), new List<(int, int)> { (i, j - 1) });
                                break;
                        }
                        i++;
                    }
                    
                }

                if (l % 2 == 0)
                    j++;

                i = 0;
                l++;
            }



            if (fields.Count != 0)
            {
                Field buffer;
                foreach ((int, int) coord in greaterThanDict.Keys) 
                {
                    buffer = fields.Find(o => o.coordinates == coord);
                    buffer.greaterThan = new List<Field>();
                    foreach ((int, int) coord2 in greaterThanDict[coord]) 
                    {
                        buffer.greaterThan.Add(fields.Find(o => o.coordinates == coord2));
                    }
                }


                answer = new Plane
                {
                    size = ((lines.Length - (lines.Length - 1) / 2), (lines[0].Length - (lines[0].Length - 1) / 2)),
                    fields = fields
                };
                answer.rowsAndColumns = Plane.SplitFieldsIntoRows(answer);
            }

            return answer;
        }
    }
}
