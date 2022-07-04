using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;
using System.Linq;

namespace AI_lab.Logic
{
    public interface CrossingMethod
    {
        public List<Position> Cross(List<Position> parent1, List<Position> parent2, (int, int) layoutSize);
    }

    public class SplitIdCrossing : CrossingMethod
    {
        public List<Position> Cross(List<Position> parent1, List<Position> parent2, (int, int) layoutSize)
        {
            if (parent1.Count() != parent2.Count())
                throw new Exception("Different machine count");

            parent1.Sort((o1, o2) => -o1.id.CompareTo(o2.id));
            parent2.Sort((o1, o2) => -o1.id.CompareTo(o2.id));

            Random rnd = new Random();

            int split;
            int tries = 0;

            List<Position> answer = null;

            split = rnd.Next(0, parent1.Count());
            answer = GlueTwoLayoutsById(parent1, parent2, split, rnd.Next(2) == 1);
            tries++;
            FixValidity(answer, layoutSize);

            return answer;
        }


        List<Position> GlueTwoLayoutsById(List<Position> layout1, List<Position> layout2, int pivot, bool reverse)
        {
            List<Position> glue1 = reverse ? layout2 : layout1;
            List<Position> glue2 = reverse ? layout1 : layout2;

            List<Position> answer = new List<Position>();
            int index = 0;
            while (index < pivot)
            {
                answer.Add(new Position { id = glue1[index].id, coordinates = glue1[index].coordinates });
                index++;
            }
            while (index < layout2.Count())
            {
                answer.Add(new Position { id = glue2[index].id, coordinates = glue2[index].coordinates });
                index++;
            }
            return answer;
        }

        public bool FixValidity(List<Position> layout, (int, int) layoutSize)
        {
            (int, int) buffer;
            Random rnd = new Random();
            Position p;
            while (layout.Where(o => layout.Any(o2 => o2 != o && o.coordinates == o2.coordinates)).Count() != 0)
            {
                p = layout.Find(o => layout.Any(o2 => o2 != o && o.coordinates == o2.coordinates));
                do
                {
                    buffer = (rnd.Next(layoutSize.Item1), rnd.Next(layoutSize.Item2));
                } while (layout.Where(o => o.coordinates == buffer).Count() != 0);

                layout.Find(o => o.id == p.id).coordinates = buffer;
            }
            return true;
        }
    }
}
