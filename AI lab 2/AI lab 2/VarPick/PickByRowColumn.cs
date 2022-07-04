using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AI_lab_2.Models;
using System.Linq;

namespace AI_lab_2.VarPick
{
    class PickByRowColumn : PickVar
    {
        public Field pickNext(Plane plane)
        {
            List<Field> answer;
            List<Field> possibleVars = plane.fields.FindAll(o => o.value == int.MinValue);
            if (possibleVars.Count == 0)
                return null;

            List<RowColumn> uncompleteRowColumns = plane.rowsAndColumns.FindAll(o => o.fields.Any(f => possibleVars.Contains(f)));

            uncompleteRowColumns.Sort((o1, o2) => o1.getEntropy() - o2.getEntropy());
            answer = uncompleteRowColumns[0].fields.FindAll(o => o.value == int.MinValue);

            answer.Sort((o1, o2) => o1.possibleValues.Count - o2.possibleValues.Count);

            return answer[0];
        }

        public override string ToString()
        {
            return "Pick by RowColumn";
        }
    }
}
