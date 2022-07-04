using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AI_lab_2.Models;

namespace AI_lab_2.CheckLogic
{
    class CheckBinaryForward : CheckStep
    {
        public bool checkPlane(Plane plane)
        {
            foreach (RowColumn i in plane.rowsAndColumns)
            {
                int currentNum = int.MinValue;
                int elemInRow = 0;

                for (int j = 0; j < i.fields.Count; j++)
                {
                    if (i.fields[j].value != currentNum)
                    {
                        currentNum = i.fields[j].value;
                        elemInRow = 0;
                    }
                    else
                    {
                        elemInRow++;
                    }

                    if (currentNum != int.MinValue && elemInRow >= 1)
                    {
                        if (j < i.fields.Count - 1 && i.fields[j + 1].value == int.MinValue)
                            i.fields[j + 1].possibleValues.Remove(currentNum);
                    }


                    if (j < i.fields.Count - 2 && i.fields[j].value != int.MinValue && i.fields[j + 1].value == int.MinValue && i.fields[j + 2].value != int.MinValue)
                    {
                        if (i.fields[j].value == 1 && i.fields[j + 2].value == 1)
                            i.fields[j + 1].possibleValues.Remove(1);

                        if (i.fields[j].value == 0 && i.fields[j + 2].value == 0)
                            i.fields[j + 1].possibleValues.Remove(0);
                    }
                }


                currentNum = int.MinValue;
                elemInRow = 0;

                for (int j = i.fields.Count-1; j >=0; j--)
                {
                    if (i.fields[j].value != currentNum)
                    {
                        currentNum = i.fields[j].value;
                        elemInRow = 0;
                    }
                    else
                    {
                        elemInRow++;
                    }

                    if (currentNum != int.MinValue && elemInRow >= 1)
                    {
                        if (j > 0 && i.fields[j - 1].value == int.MinValue)
                            i.fields[j - 1].possibleValues.Remove(currentNum);
                    }
                }

                if (i.fields.FindAll(o => o.value == 0).Count >= i.fields.Count / 2)
                {
                    foreach (Field j in i.fields.FindAll(o => o.value == int.MinValue)) 
                    {
                        j.possibleValues.Remove(0);
                    }
                }
                if (i.fields.FindAll(o => o.value == 1).Count >= i.fields.Count / 2)
                {
                    foreach (Field j in i.fields.FindAll(o => o.value == int.MinValue))
                    {
                        j.possibleValues.Remove(1);
                    }
                }
            }

            //Similarities cos I have no idea how to implement this
            List<RowColumn> columns = plane.rowsAndColumns.FindAll(o => o.isComplete() && o.fields.Any(j1 => o.fields.Any(j2 => j1.coordinates != j2.coordinates
                && j1.coordinates.Item1 == j2.coordinates.Item1)));

            if (columns.Any(o1 => columns.Any(o2 => !Object.ReferenceEquals(o1, o2) && o1 == o2)))
                return false;

            List<RowColumn> rows = plane.rowsAndColumns.FindAll(o => o.isComplete() && o.fields.Any(j1 => o.fields.Any(j2 => j1.coordinates != j2.coordinates
                 && j1.coordinates.Item2 == j2.coordinates.Item2)));
            if (rows.Any(o1 => rows.Any(o2 => !Object.ReferenceEquals(o1, o2) && o1 == o2)))
                return false;

            return !plane.fields.Any(o => o.possibleValues.Count == 0);
        }


        public override string ToString()
        {
            return "Binary Forward Check";
        }
    }
}
