using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;
using System.Linq;

namespace AI_lab.Logic
{
    public class DebugMethods
    {
        static public bool LayoutHasDuplicatePositions(List<Position> layout) 
        {
            return !layout.All(o => !layout.Any(b => b.coordinates == o.coordinates && b.id != o.id));
        }

        static public bool PopulatonHasDuplicatePositions(List<List<Position>> population)
        {
            return !population.All(oo => oo.All(o => !oo.Any(b => b.coordinates == o.coordinates && b.id != o.id)));
        }
    }
}
