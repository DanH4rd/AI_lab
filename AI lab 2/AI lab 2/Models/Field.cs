using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab_2.Models
{
    public class Field
    {
        public (int, int) coordinates { get; set; }
        public int value { get; set; }
        public List<Field> greaterThan { get; set; }
        public List<int> possibleValues { get; set; }

        public override string ToString()
        {
            return String.Format("({0}, {1})[{2}]", coordinates.Item1, coordinates.Item2, value);
        }
    }
}
