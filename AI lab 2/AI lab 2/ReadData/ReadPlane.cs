using System;
using System.Collections.Generic;
using System.Text;
using AI_lab_2.Models;

namespace AI_lab_2.ReadData
{
    public interface ReadPlane
    {
        public Plane ReadPlane(string fullFilePath);
    }
}
