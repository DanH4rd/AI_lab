using System;
using System.Collections.Generic;
using System.Text;
using AI_lab_2.Models;

namespace AI_lab_2.VarPick
{
    public interface PickVar
    {
        public Field pickNext(Plane plane);
    }
}
