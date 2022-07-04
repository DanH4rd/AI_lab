using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;
using System.Linq;

namespace AI_lab.Logic
{
    public interface GenerateMethod 
    {
        public List<Position> GenerateLayout((int, int) layoutSize, int itemCount);
    }
    public class BruteRandomGenerate : GenerateMethod
    {
        public List<Position> GenerateLayout((int, int) layoutSize, int itemCount)
        {
            if (layoutSize.Item1 * layoutSize.Item2 < itemCount)
                throw new Exception();

            Random rnd = new Random();
            List<Position> layout = new List<Position>();

            Position buffer;
            for (int i = 0; i < itemCount; i++)
            {
                buffer = new Position();
                buffer.id = i;
                do
                {
                    buffer.coordinates = (rnd.Next(layoutSize.Item1), rnd.Next(layoutSize.Item2));
                } while (layout.Where(o => o.coordinates == buffer.coordinates).Count() != 0);

                layout.Add(buffer);
            }

            return layout;
        }
    }
}
