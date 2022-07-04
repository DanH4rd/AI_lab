using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;
using System.Linq;

namespace AI_lab.Logic
{
    public class DataAnalysis
    {
        static public double calculateLayoutEffect(int itemCount, List<Position> layout, List<Cost> costs, List<Flow> flows) 
        {
            double answer = 0d;

            for(int i = 0; i < itemCount; i++)
            {
                for (int j = i; j < itemCount; j++)
                {
                    Cost costInfo = costs.Where(o => o.source == i && o.dest == j).FirstOrDefault();
                    Flow flowInfo = flows.Where(o => o.source == i && o.dest == j).FirstOrDefault();
                    Position item1Position = layout.Where(o => o.id == i).FirstOrDefault();
                    Position item2Position = layout.Where(o => o.id == j).FirstOrDefault();

                    if (item1Position.coordinates != item2Position.coordinates && costInfo != null && flowInfo != null && item1Position != null && item2Position != null)
                    {
                        answer += costInfo.cost * flowInfo.amount * calculateDistance(item1Position.coordinates, item2Position.coordinates);
                        if (calculateDistance(item1Position.coordinates, item2Position.coordinates) == 0)
                            throw new Exception();
                    }
                }
            }

            if (answer == 0)
                throw new Exception();

            return answer;
        }

        static public double calculateDistance((int, int) item1, (int, int) item2) => Math.Abs(item1.Item1 - item2.Item1) + Math.Abs(item1.Item2 - item2.Item2);
        
    }

}
