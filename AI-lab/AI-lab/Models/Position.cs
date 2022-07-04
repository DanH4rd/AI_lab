using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab.Models
{
    public class Position
    {
        public int id { get; set; }
        public (int, int) coordinates { get; set; }
        public override string ToString()
        {
            return String.Format("Id: {0}, X: {1}, Y: {2}", id, coordinates.Item1, coordinates.Item2);
        }
    }
}
