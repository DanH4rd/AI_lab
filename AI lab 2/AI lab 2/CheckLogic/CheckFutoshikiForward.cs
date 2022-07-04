using AI_lab_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_lab_2.CheckLogic
{
    class CheckFutoshikiForward : CheckStep
    {
        public bool checkPlane(Plane plane)
        {
            foreach (RowColumn i in plane.rowsAndColumns)
            {
                List<int> possibleVals = Enumerable.Range(1, i.fields.Count).ToList();

                foreach (Field j in i.fields)
                {
                    possibleVals.Remove(j.value);
                }

                foreach (Field j in i.fields.FindAll(o=> o.value == int.MinValue))
                {
                    foreach (int k in new List<int>(j.possibleValues)) 
                    {
                        if (!possibleVals.Contains(k))
                            j.possibleValues.Remove(k);
                    }
                }
            }


            foreach (Field baseP in plane.fields.FindAll(o => o.greaterThan != null))
            {
                foreach (Field smaller in baseP.greaterThan)
                {
                    if (baseP.value != int.MinValue)
                    {
                        foreach (int smallerVal in new List<int>(smaller.possibleValues))
                        {
                            if (smallerVal >= baseP.value)
                                smaller.possibleValues.Remove(smallerVal);
                        }
                        
                    }

                    else if (smaller.value != int.MinValue)
                    {
                        foreach (int basePval in new List<int>(baseP.possibleValues))
                        {
                            if (basePval <= smaller.value)
                                baseP.possibleValues.Remove(basePval);
                        }
                    }

                    
                }
            }

            return !plane.fields.Any(o => o.possibleValues.Count == 0);
        }


        public override string ToString()
        {
            return "Futoshiki Forward Checking";
        }
    }
}
