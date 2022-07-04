using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI_lab.Logic;
using AI_lab.Models;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenerateLayoutTest()
        {
            int areaX = 3;
            int areaY = 3;
            int amount = 9;

            List<Position> randLayout = (new BruteRandomGenerate()).GenerateLayout((areaX, areaY), amount);

            Assert.IsTrue(randLayout.All(o => !randLayout.Any(b => b.coordinates == o.coordinates && b.id != o.id)));
            Assert.IsTrue(randLayout.All(o => o.coordinates.Item1 < areaX && o.coordinates.Item2 < areaY));
            Assert.AreEqual(randLayout.Count, amount);
        }

        [TestMethod]
        public void CalculateEffectiveness()
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
                    coordinates = (1,3)
                },
                new Position
                {
                    id=2,
                    coordinates = (4,3)
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

            Assert.AreEqual(1500, DataAnalysis.calculateLayoutEffect(3, layout, cost, amount));
        }

        [TestMethod]
        public void ReadTest()
        {
            GetData data = new EasyData(@"C:\Users\Gordon Freeman\Desktop\flo_dane_v1.2");

            Assert.IsNotNull(data.getFlows());
            Assert.IsNotNull(data.getCosts());

            Assert.IsTrue(data.getFlows().Count() > 0);
            Assert.IsTrue(data.getCosts().Count() > 0);

        }
    }
}
