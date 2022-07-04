using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;

namespace AI_lab.Logic
{
    public interface MutationMethod 
    {
        public void Mutate(List<Position> layout, (int, int) layoutSize);
    }

    public class FlipFlopMutation : MutationMethod
    {
        public void Mutate(List<Position> layout, (int, int) layoutSize)
        {
            Random rnd = new Random();

            int index1 = rnd.Next(layout.Count);
            int index2 = 0;
            do
            {
                index2 = rnd.Next(layout.Count);
            } while (index1 == index2);

            (int, int) buffer = layout[index1].coordinates;
            layout[index1].coordinates = layout[index2].coordinates;
            layout[index2].coordinates = buffer;

            if (DebugMethods.LayoutHasDuplicatePositions(layout))
                throw new Exception();
        }
    }

    public class MoveMutation : MutationMethod
    {
        public void Mutate(List<Position> layout, (int, int) layoutSize)
        {
            Random rnd = new Random();

            int index = rnd.Next(layout.Count);

            int coordX = layout[index].coordinates.Item1;
            int coordY = layout[index].coordinates.Item2;


            if (layoutSize.Item2 == 1 || (rnd.Next(2) == 1 && layoutSize.Item1 > 1))
            {
                if (coordX == layoutSize.Item1)
                    coordX = coordX - 1;
                else if (coordX == 0)
                    coordX = coordX + 1;
                else
                coordX = rnd.Next(2) == 1 ? coordX + 1 : coordX - 1;
            }
            else 
            {
                if (coordY == layoutSize.Item2)
                    coordY = coordY - 1;
                else if (coordY == 0)
                    coordY = coordY + 1;
                else
                    coordY = rnd.Next(2) == 1 ? coordY + 1 : coordY - 1;
            }

            (int, int) newPlace = (coordX, coordY);

            Position alreadyPresent = layout.Find(o => o.coordinates == newPlace);
            if (alreadyPresent != null)
            {
                alreadyPresent.coordinates = layout[index].coordinates;
                layout[index].coordinates = newPlace;
            }
            else
            {
                layout[index].coordinates = newPlace;
            }
        }
    }
}
