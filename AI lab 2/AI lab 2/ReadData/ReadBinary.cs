using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab_2.ReadData
{
    class ReadBinary : ReadPlane
    {
        public Plane ReadPlane (string fullFilePath)
        {
            Plane answer = null;
            string[] lines = System.IO.File.ReadAllLines(fullFilePath);

            if (lines.Length != lines[0].Length)
                throw new Exception();

            int i = 0, j = 0;

            List<Field> fields = new List<Field>();

            foreach (string line in lines) 
            {
                foreach (char chara in line) 
                {
                    if (chara == 'x' || chara == 'X')
                        fields.Add(new Field 
                        {
                            coordinates = (i,j),
                            value = int.MinValue,
                            greaterThan = null,
                            possibleValues = new List<int> { 0, 1 }
                        });
                    else
                        fields.Add(new Field
                        {
                            coordinates = (i, j),
                            value = Int32.Parse(chara.ToString()),
                            greaterThan = null,
                            possibleValues = new List<int> { 0, 1 }
                        });

                    i++;
                }

                i = 0;
                j++;
            }

            if (fields.Count != 0) 
            {
                answer = new Plane
                {
                    size = (lines.Length, lines.Length),
                    fields = fields
                };
                answer.rowsAndColumns = Plane.SplitFieldsIntoRows(answer);
            }

            return answer;
        }
    }
}
