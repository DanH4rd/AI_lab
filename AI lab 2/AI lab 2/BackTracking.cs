using System;
using System.Collections.Generic;
using System.Text;
using AI_lab_2.Models;
using AI_lab_2.CheckLogic;
using AI_lab_2.VarPick;
using System.Linq;

namespace AI_lab_2
{
    public class BackTracking
    {
        CheckStep checkMethod;
        PickVar pickMethod;

        public BackTracking(CheckStep checkMethod, PickVar pickMethod) 
        {
            this.checkMethod = checkMethod;
            this.pickMethod = pickMethod;
        }

        public List<Plane> findAnswers(Plane startPlane) 
        {
            List<Plane> answer = new List<Plane>();
            Stack<Plane> stack = new Stack<Plane>();
            stack.Push((Plane) startPlane.Clone());
            ulong nodesVisited = 0;
            DateTime startTime = DateTime.Now;

            while (stack.Count != 0) 
            {
                nodesVisited++;
                Plane currentPlane = stack.Pop();

                Console.WriteLine("Stack size: {0, 6:N0}; Current empty vals: {1, 6:N0}; Solutions found: {2, 6}                                ", stack.Count, currentPlane.fields.FindAll(o => o.value == int.MinValue).Count, answer.Count);


                if (checkMethod.checkPlane(currentPlane))
                {
                    if (!currentPlane.fields.Any(o => o.value == int.MinValue))
                    {
                        answer.Add(currentPlane);
                    }
                    else
                    {
                        Field pickedVal = pickMethod.pickNext(currentPlane);

                        if (pickedVal != null && pickedVal.possibleValues.Count != 0)
                        {
                            Plane clone = (Plane)currentPlane.Clone();
                            Field pickedValClone = clone.fields.Find(o => o.coordinates == pickedVal.coordinates);

                            pickedVal.possibleValues.RemoveAt(0);
                            pickedValClone.value = pickedValClone.possibleValues[0];

                            stack.Push(currentPlane);
                            stack.Push(clone);
                        }
                    }

                }
                else 
                {
                    int a = 1 + 1;
                }

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            }
            Console.WriteLine("                                                                             ");
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("{0} {1}, Solutions: {2}, Nodes Visited: {3}, Total time: {4} seconds", checkMethod.ToString(), startPlane.size, answer.Count, nodesVisited, (DateTime.Now - startTime).TotalSeconds);
            return answer;
        }
    }
}
