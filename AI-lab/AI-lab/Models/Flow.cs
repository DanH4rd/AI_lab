using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab.Models
{
    public class Flow
    {
        public int source { get; set; }
        public int dest { get; set; }
        public int amount { get; set; }

        public override string ToString()
        {
            return String.Format("Source: {0}, Destination: {1}, Amount: {2}", source, dest, amount);
        }

    }
}
