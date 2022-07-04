using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab_2.VarPick
{
    class PickByField : PickVar
    {
        public Field pickNext(Plane plane)
        {
            List<Field> possibleVars = plane.fields.FindAll(o => o.value == int.MinValue);

            if (possibleVars.Count == 0)
                return null;

            possibleVars.Sort((o1, o2) => o1.possibleValues.Count - o2.possibleValues.Count);

            return possibleVars[0];
        }

        public override string ToString()
        {
            return "Pick by field";
        }
    }
}
