using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_lab_2.CheckLogic
{
    class CheckBinaryBack : CheckStep
    {
        public bool checkPlane(Plane plane)
        {
            foreach (RowColumn i in plane.rowsAndColumns) 
            {
                int currentNum = int.MinValue;
                int elemInRow = 0;

                foreach (Field j in i.fields) 
                {
                    if (j.value != currentNum)
                    {
                        currentNum = j.value;
                        elemInRow = 0;
                    }
                    else 
                    {
                        elemInRow++;
                    }

                    if (currentNum != int.MinValue && elemInRow > 1) 
                    {
                        return false;
                    }
                }

                if (i.fields.FindAll(o => o.value == 0).Count > i.fields.Count/2)
                    return false;
                if (i.fields.FindAll(o => o.value == 1).Count > i.fields.Count/2)
                    return false;
            }

            //rows similarities
            List<RowColumn> columns = plane.rowsAndColumns.FindAll(o => o.isComplete() && o.fields.Any(j1 => o.fields.Any(j2 => j1.coordinates != j2.coordinates
                && j1.coordinates.Item1 == j2.coordinates.Item1)));

            if (columns.Any(o1 => columns.Any(o2 => !Object.ReferenceEquals(o1, o2) && o1 == o2)))
                return false;

            List<RowColumn> rows = plane.rowsAndColumns.FindAll(o => o.isComplete() && o.fields.Any(j1 => o.fields.Any(j2 => j1.coordinates != j2.coordinates
                 && j1.coordinates.Item2 == j2.coordinates.Item2)));
            if (rows.Any(o1 => rows.Any(o2 => !Object.ReferenceEquals(o1, o2) && o1 == o2)))
                return false;

            return true;
        }

        public override string ToString()
        {
            return "Binary Back Tracking";
        }

    }
}
