using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_lab_2.CheckLogic
{
    class CheckFutoshikiBack : CheckStep
    {
        public bool checkPlane(Plane plane)
        {
            foreach (RowColumn i in plane.rowsAndColumns) 
            {
                List<int> possibleVals = Enumerable.Range(1, i.fields.Count).ToList();
                foreach (Field j in i.fields) 
                {
                    if (j.value != int.MinValue && !possibleVals.Contains(j.value))
                        return false;

                    possibleVals.Remove(j.value);
                }
                
            }

            foreach (Field i in plane.fields.FindAll(o => o.greaterThan != null))
            {
                foreach (Field j in i.greaterThan) 
                {
                    if (i.value != int.MinValue && i.value <= j.value) 
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public override string ToString()
        {
            return "Futoshiki Back Tracking";
        }
    }
}
