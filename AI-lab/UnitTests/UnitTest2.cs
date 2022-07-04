using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI_lab.Logic;
using AI_lab.Models;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void BruhTest()
        {
            Position A = new Position 
            {
                id = 1,
                coordinates = (1, 3)
            };

            Position B = new Position
            {
                id = 1,
                coordinates = (1, 3)
            };

            Position C = A;

            Assert.IsTrue(A == C);
        }


        [TestMethod]
        public void GlobalTournamentTest()
        {
            List<List<Position>> layoutPool = new List<List<Position>>
            {

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (0,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (3,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (3,4)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (1,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (4,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (3,3)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (0,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (1,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (4,3)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (1,2)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (4,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (4,3)
                    }
                }

            };

            List<Cost> cost = new List<Cost>{
                new Cost
                {
                    source=0,
                    dest = 1,
                    cost = 10
                },
                new Cost
                {
                    source=0,
                    dest = 2,
                    cost = 20
                },
                new Cost
                {
                    source=1,
                    dest = 2,
                    cost = 30
                }
            };

            List<Flow> amount = new List<Flow>{
                new Flow
                {
                    source=0,
                    dest = 1,
                    amount = 1
                },
                new Flow
                {
                    source=0,
                    dest = 2,
                    amount = 4
                },
                new Flow
                {
                    source=1,
                    dest = 2,
                    amount = 10
                }
            };

            Assert.AreEqual(360, DataAnalysis.calculateLayoutEffect(3,
                (new TournamentSelect(4)).Select(3, layoutPool, cost, amount)
                , cost, amount));
        }

        [TestMethod]
        public void ManualTournamentTest()
        {
            List<List<Position>> layoutPool = new List<List<Position>>
            {

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (0,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (3,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (3,4)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (1,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (4,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (3,3)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (0,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (1,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (4,3)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (1,2)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (4,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (4,3)
                    }
                }

            };

            List<Cost> cost = new List<Cost>{
                new Cost
                {
                    source=0,
                    dest = 1,
                    cost = 10
                },
                new Cost
                {
                    source=0,
                    dest = 2,
                    cost = 20
                },
                new Cost
                {
                    source=1,
                    dest = 2,
                    cost = 30
                }
            };

            List<Flow> amount = new List<Flow>{
                new Flow
                {
                    source=0,
                    dest = 1,
                    amount = 1
                },
                new Flow
                {
                    source=0,
                    dest = 2,
                    amount = 4
                },
                new Flow
                {
                    source=1,
                    dest = 2,
                    amount = 10
                }
            };

            Assert.IsTrue((new TournamentSelect(2)).Select(3, layoutPool, cost, amount) != null);
        }

        [TestMethod]
        public void ManualRouletteTest()
        {
            List<List<Position>> layoutPool = new List<List<Position>>
            {

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (0,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (3,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (3,4)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (1,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (4,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (3,3)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (0,0)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (1,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (4,3)
                    }
                },

                new List<Position>
                {
                    new Position
                    {
                        id=0,
                        coordinates = (1,2)
                    },
                    new Position
                    {
                        id=1,
                        coordinates = (4,3)
                    },
                    new Position
                    {
                        id=2,
                        coordinates = (4,3)
                    }
                }

            };

            List<Cost> cost = new List<Cost>{
                new Cost
                {
                    source=0,
                    dest = 1,
                    cost = 10
                },
                new Cost
                {
                    source=0,
                    dest = 2,
                    cost = 20
                },
                new Cost
                {
                    source=1,
                    dest = 2,
                    cost = 30
                }
            };

            List<Flow> amount = new List<Flow>{
                new Flow
                {
                    source=0,
                    dest = 1,
                    amount = 1
                },
                new Flow
                {
                    source=0,
                    dest = 2,
                    amount = 4
                },
                new Flow
                {
                    source=1,
                    dest = 2,
                    amount = 10
                }
            };


            Assert.IsTrue((new RouletteSelect()).Select(3, layoutPool, cost, amount) != null);
        }

        [TestMethod]
        public void CrossingTest() 
        {
            List<Position> parent1 = new List<Position>{
                new Position
                {
                    id=0,
                    coordinates = (0,0)
                },
                new Position
                {
                    id=1,
                    coordinates = (1,3)
                },
                new Position
                {
                    id=2,
                    coordinates = (4,3)
                },
                new Position
                {
                    id=3,
                    coordinates = (2,3)
                }
                ,
                new Position
                {
                    id=4,
                    coordinates = (3,2)
                }
            };

            List<Position> parent2 = new List<Position>{
                new Position
                {
                    id=0,
                    coordinates = (1,1)
                },
                new Position
                {
                    id=1,
                    coordinates = (4,1)
                },
                new Position
                {
                    id=2,
                    coordinates = (2,2)
                },
                new Position
                {
                    id=3,
                    coordinates = (3,4)
                }
                ,
                new Position
                {
                    id=4,
                    coordinates = (4,1)
                }
            };

            List<Position> child = (new SplitIdCrossing()).Cross(parent1, parent2, (10,10));

            //Child exists
            Assert.IsTrue(child != null);
            // Child isnt a reference copy
            Assert.IsTrue(child != parent1 && child != parent2);
            // Child isnt a structure copy of parent1 and, thus, parent2
            Assert.IsTrue(!child.TrueForAll(o => parent1.Any(p => p.id == o.id && p.coordinates == o.coordinates)));
            // Child has no references to parent's positions
            Assert.IsTrue(!child.Any(o => parent1.Any(p => p == o) || parent2.Any(p => p == o)));

        }

        [TestMethod]
        public void FlipFlopMutationTest()
        {
            List<Position> layoutOG = new List<Position>{
                new Position
                {
                    id=0,
                    coordinates = (0,0)
                },
                new Position
                {
                    id=1,
                    coordinates = (1,3)
                },
                new Position
                {
                    id=2,
                    coordinates = (4,3)
                },
                new Position
                {
                    id=3,
                    coordinates = (2,3)
                }
                ,
                new Position
                {
                    id=4,
                    coordinates = (3,2)
                }
            };

            List<Position> layout = new List<Position>{
                new Position
                {
                    id=0,
                    coordinates = (0,0)
                },
                new Position
                {
                    id=1,
                    coordinates = (1,3)
                },
                new Position
                {
                    id=2,
                    coordinates = (4,3)
                },
                new Position
                {
                    id=3,
                    coordinates = (2,3)
                }
                ,
                new Position
                {
                    id=4,
                    coordinates = (3,2)
                }
            };

            // layoutOG struct eq layout
            Assert.IsTrue(layout.TrueForAll(o => layoutOG.Any(p => p.id == o.id && p.coordinates == o.coordinates)));

            (new FlipFlopMutation()).Mutate(layout, (1,1));

            //layout mutated
            Assert.IsTrue(!layout.TrueForAll(o => layoutOG.Any(p => p.id == o.id && p.coordinates == o.coordinates)));

        }

        [TestMethod]
        public void MoveMutationTest()
        {
            List<Position> layoutOG = new List<Position>{
                new Position
                {
                    id=0,
                    coordinates = (0,0)
                },
                new Position
                {
                    id=1,
                    coordinates = (1,3)
                },
                new Position
                {
                    id=2,
                    coordinates = (4,3)
                },
                new Position
                {
                    id=3,
                    coordinates = (2,3)
                }
                ,
                new Position
                {
                    id=4,
                    coordinates = (3,2)
                }
            };

            List<Position> layout = new List<Position>{
                new Position
                {
                    id=0,
                    coordinates = (0,0)
                },
                new Position
                {
                    id=1,
                    coordinates = (1,3)
                },
                new Position
                {
                    id=2,
                    coordinates = (4,3)
                },
                new Position
                {
                    id=3,
                    coordinates = (2,3)
                }
                ,
                new Position
                {
                    id=4,
                    coordinates = (3,2)
                }
            };

            // layoutOG struct eq layout
            Assert.IsTrue(layout.TrueForAll(o => layoutOG.Any(p => p.id == o.id && p.coordinates == o.coordinates)));

            (new MoveMutation()).Mutate(layout, (5, 5));

            //layout mutated
            Assert.IsTrue(!layout.TrueForAll(o => layoutOG.Any(p => p.id == o.id && p.coordinates == o.coordinates)));

        }

        [TestMethod]
        public void FixTest()
        {
            List<Position> layout = new List<Position>{
                new Position
                {
                    id=0,
                    coordinates = (0,0)
                },
                new Position
                {
                    id=1,
                    coordinates = (0,0)
                },
                new Position
                {
                    id=2,
                    coordinates = (1,1)
                },
                new Position
                {
                    id=3,
                    coordinates = (1,1)
                }
                ,
                new Position
                {
                    id=4,
                    coordinates = (1,2)
                }
            };

            Assert.IsTrue(!layout.All(o => !layout.Any(b => b.coordinates == o.coordinates && b.id != o.id)));
            Assert.IsTrue(layout.All(o => o.coordinates.Item1 < 3 && o.coordinates.Item2 < 3));

            (new SplitIdCrossing()).FixValidity(layout, (3,3));

            //layout mutated
            Assert.IsTrue(layout.All(o => !layout.Any(b => b.coordinates == o.coordinates && b.id != o.id)));
            Assert.IsTrue(layout.All(o => o.coordinates.Item1 < 3 && o.coordinates.Item2 < 3));
            Debug.WriteLine("");
        }

        [TestMethod]
        public void MeanScore()
        {
            List<(int, int)> array = new List<(int, int)> { (1, 1), (1, 2), (1, 3), (1, 4) };
            int sum = array.Sum(o => o.Item1);
            Assert.AreEqual(4, sum);
        }
        [TestMethod]
        public void DevTest()
        {
            List<(double, double)> array = new List<(double, double)> { (1d, 1d), (1d, 2d), (1d, 3d), (1d, 4d) };
            double result = Math.Sqrt(array.Sum(o => (o.Item2 - 5d)* (o.Item2 - 5d)) / 4d);
            Assert.AreEqual(Math.Sqrt(15d/2), result);
        }

        [TestMethod]
        public void DublicateArray()
        {
            List<Position> array1 = new List<Position> { new Position(), new Position(), new Position(), new Position() };
            List<Position> array2 = new List<Position>(array1);
            //Arrays are different
            Assert.IsTrue(array1 != array2);
            //Elements the same
            Assert.IsTrue(array1.All(o => array2.Any(o2 => o2 == o)));

            array2.RemoveAt(0);

            Assert.AreEqual(4, array1.Count);
            Assert.AreEqual(3, array2.Count);
        }
    }
}
