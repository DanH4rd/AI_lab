using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_lab_2.CheckLogic
{
    public interface CheckStep
    {
        public bool checkPlane(Plane plane);
    }
}
