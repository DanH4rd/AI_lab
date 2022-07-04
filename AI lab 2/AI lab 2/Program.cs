using System;
using AI_lab_2.Models;
using AI_lab_2.ReadData;
using AI_lab_2.CheckLogic;
using AI_lab_2.VarPick;
using System.Collections.Generic;
using System.Linq;

namespace AI_lab_2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine((new PickByField()));
            runStandardHeurestics(new PickByField());
            Console.WriteLine("\n");

            Console.WriteLine("------------------------");

            Console.WriteLine((new PickByRowColumn()));
            Console.WriteLine("\n");
            runStandardHeurestics(new PickByRowColumn());
        }

        static void runStandardHeurestics(PickVar pickVar)
        {
            Plane binary6x6 = (new ReadBinary()).ReadPlane(@"C:\Users\Gordon Freeman\Desktop\Dane\binary_6x6");
            Plane binary8x8 = (new ReadBinary()).ReadPlane(@"C:\Users\Gordon Freeman\Desktop\Dane\binary_8x8");
            Plane binary10x10 = (new ReadBinary()).ReadPlane(@"C:\Users\Gordon Freeman\Desktop\Dane\binary_10x10");


            Plane futoshiki4x4 = (new ReadFutoshiki()).ReadPlane(@"C:\Users\Gordon Freeman\Desktop\Dane\futoshiki_4x4");
            Plane futoshiki5x5 = (new ReadFutoshiki()).ReadPlane(@"C:\Users\Gordon Freeman\Desktop\Dane\futoshiki_5x5");
            Plane futoshiki6x6 = (new ReadFutoshiki()).ReadPlane(@"C:\Users\Gordon Freeman\Desktop\Dane\futoshiki_6x6");

            new BackTracking(new CheckBinaryBack(), pickVar).findAnswers(binary6x6);
            new BackTracking(new CheckBinaryForward(), pickVar).findAnswers(binary6x6);
            Console.WriteLine("\n");

            new BackTracking(new CheckBinaryBack(), pickVar).findAnswers(binary8x8);
            new BackTracking(new CheckBinaryForward(), pickVar).findAnswers(binary8x8);
            Console.WriteLine("\n");

            new BackTracking(new CheckBinaryBack(), pickVar).findAnswers(binary10x10);
            new BackTracking(new CheckBinaryForward(), pickVar).findAnswers(binary10x10);
            Console.WriteLine("\n");
            Console.WriteLine("\n");




            new BackTracking(new CheckFutoshikiBack(), pickVar).findAnswers(futoshiki4x4);
            new BackTracking(new CheckFutoshikiForward(), pickVar).findAnswers(futoshiki4x4);
            Console.WriteLine("\n");

            new BackTracking(new CheckFutoshikiBack(), pickVar).findAnswers(futoshiki5x5);
            new BackTracking(new CheckFutoshikiForward(), pickVar).findAnswers(futoshiki5x5);
            Console.WriteLine("\n");

            new BackTracking(new CheckFutoshikiBack(), pickVar).findAnswers(futoshiki6x6);
            new BackTracking(new CheckFutoshikiForward(), pickVar).findAnswers(futoshiki6x6);
            Console.WriteLine("\n");

        }

        static void singleRun()
        {
            ReadPlane read = new ReadFutoshiki();

            Plane startPlane = read.ReadPlane(@"C:\Users\Gordon Freeman\Desktop\Dane\futoshiki_6x6");
            //Plane startPlane = getSmallBinaryPlane();
            //Plane startPlane = getSmallFutoshikiPlane();

            Console.WriteLine(startPlane);

            BackTracking backTracking = new BackTracking(new CheckFutoshikiBack(), new PickByField());

            List<Plane> solutions = backTracking.findAnswers(startPlane);


            if (false)
                foreach (Plane i in solutions)
                {
                    Console.WriteLine(i.ToStringDisplay());
                    Console.WriteLine("\n\n");
                }

            Console.WriteLine("Found solutions: {0}", solutions.Count);
        }

      

        static Plane getSmallFutoshikiPlane()
        {
            Plane plane = new Plane
            {
                size = (3, 3),
                fields = new List<Field>
                {
                    new Field
                    {
                        coordinates = (0,0),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                     new Field
                    {
                        coordinates = (1,0),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                      new Field
                    {
                        coordinates = (2,0),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                       new Field
                    {
                        coordinates = (0,1),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                        new Field
                    {
                        coordinates = (1,1),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                         new Field
                    {
                        coordinates = (2,1),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                          new Field
                    {
                        coordinates = (0,2),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                           new Field
                    {
                        coordinates = (1,2),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()
                    },
                            new Field
                    {
                        coordinates = (2,2),
                        value = int.MinValue,
                        possibleValues = Enumerable.Range(1, 3).ToList()                   },
                }
            };

            plane.fields.Find(o => o.coordinates == (0, 0)).greaterThan = new List<Field> { plane.fields.Find(o => o.coordinates == (1, 0)), plane.fields.Find(o => o.coordinates == (0, 1)) };
            plane.fields.Find(o => o.coordinates == (1, 1)).greaterThan = new List<Field> { plane.fields.Find(o => o.coordinates == (2, 1)) };
            plane.fields.Find(o => o.coordinates == (2, 2)).greaterThan = new List<Field> { plane.fields.Find(o => o.coordinates == (1, 2)) };

            return plane;
        }

        static Plane getSmallBinaryPlane() 
        {
            Plane plane = new Plane
            {
                size = (4, 4),
                fields = new List<Field>
                {
                    new Field
                    {
                        coordinates = (0,0),
                        value = 0,
                        possibleValues = new List<int>{0, 1}
                    },
                     new Field
                    {
                        coordinates = (1,0),
                        value = int.MinValue,
                        possibleValues = new List<int>{0, 1}
                    },
                      new Field
                    {
                        coordinates = (2,0),
                        value = int.MinValue,
                        possibleValues = new List<int>{0, 1}
                    },
                      new Field
                    {
                        coordinates = (3,0),
                        value = 1,
                        possibleValues = new List<int>{0, 1}
                    },
                       new Field
                    {
                        coordinates = (0,1),
                        value = int.MinValue,
                        possibleValues = new List<int>{0, 1}
                    },
                        new Field
                    {
                        coordinates = (1,1),
                        value = 1,
                        possibleValues = new List<int>{0, 1}
                    },
                         new Field
                    {
                        coordinates = (2,1),
                        value = 0,
                        possibleValues = new List<int>{0, 1}
                    },
                         new Field
                    {
                        coordinates = (3,1),
                        value = 1,
                        possibleValues = new List<int>{0, 1}
                    },
                          new Field
                    {
                        coordinates = (0,2),
                        value = 1,
                        possibleValues = new List<int>{0, 1}
                    },
                           new Field
                    {
                        coordinates = (1,2),
                        value = int.MinValue,
                        possibleValues = new List<int>{0, 1}
                    },
                            new Field
                    {
                        coordinates = (2,2),
                        value = int.MinValue,
                        possibleValues = new List<int>{0, 1}
                    },
                            new Field
                    {
                        coordinates = (3,2),
                        value = 0,
                        possibleValues = new List<int>{0, 1}
                    },
                            new Field
                    {
                        coordinates = (0,3),
                        value = 0,
                        possibleValues = new List<int>{0, 1}
                    },
                           new Field
                    {
                        coordinates = (1,3),
                        value = 1,
                        possibleValues = new List<int>{0, 1}
                    },
                            new Field
                    {
                        coordinates = (2,3),
                        value = int.MinValue,
                        possibleValues = new List<int>{0, 1}
                    },
                            new Field
                    {
                        coordinates = (3,3),
                        value = int.MinValue,
                        possibleValues = new List<int>{0, 1}
                    },
                }
            };

            return plane;
        }
    }
}
