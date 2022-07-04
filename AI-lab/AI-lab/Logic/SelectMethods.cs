using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;
using System.Linq;

namespace AI_lab.Logic
{
    public interface SelectMethod 
    {
        public List<Position> Select(int itemCount, List<List<Position>> layoutPool, List<Cost> costs, List<Flow> flows);
    }

    public class TournamentSelect : SelectMethod
    {
        public int tournamentSize { get; set; }

        public TournamentSelect(int tournamentSize) 
        {
            this.tournamentSize = tournamentSize;
        }

        public List<Position> Select(int itemCount, List<List<Position>> layoutPool, List<Cost> costs, List<Flow> flows)
        {
            List<List<Position>> tournament = new List<List<Position>>();
            Random rnd = new Random();

            List<Position> buffer;
            if (layoutPool.Count == tournamentSize)
                tournament = new List<List<Position>>(layoutPool);
            else
                for (int i = 0; i < tournamentSize; i++)
                {
                    do
                    {
                        buffer = layoutPool[rnd.Next(layoutPool.Count)];
                        bool kek = tournament.Count() > 0 ? true : false;
                    } while (tournament.Count() > 0 ? tournament.Find(o => o == buffer) != null : false);
                    tournament.Add(buffer);
                }

            List<(List<Position>, double)> fitness = new List<(List<Position>, double)>();

            foreach (List<Position> i in tournament)
            {
                fitness.Add((i, DataAnalysis.calculateLayoutEffect(itemCount, i, costs, flows)));
            }

            //List<Position> answer = fitness.Find(o => o.Item2 == fitness.Max(mo => mo.Item2)).Item1;
            fitness.Sort((o1, o2) => o1.Item2.CompareTo(o2.Item2));
            List<Position> answer = fitness[0].Item1;

            return answer;
        }
    }

    public class RouletteSelect : SelectMethod
    {
        public List<Position> Select(int itemCount, List<List<Position>> layoutPool, List<Cost> costs, List<Flow> flows)
        {
            List<(List<Position>, double)> poolPerfScoreTuples = new List<(List<Position>, double)>();
            Random rnd = new Random();

            List<Position> buffer;

            foreach (List<Position> i in layoutPool)
            {
                poolPerfScoreTuples.Add((i, DataAnalysis.calculateLayoutEffect(itemCount, i, costs, flows)));
            }

            // Reference value for defining how much small values must take, 110% of max value
            double referenceValue = poolPerfScoreTuples.Max(o => o.Item2) * 1.2;
            List<(List<Position>, double)> poolScoreTuples = poolPerfScoreTuples.Select(o => (o.Item1, referenceValue - o.Item2)).ToList();

            double costSum = poolScoreTuples.Sum(o => o.Item2);
            List<(List<Position>, double)> poolPercentTuple = new List<(List<Position>, double)>();
            double ceiling = 0;

            foreach ((List<Position>, double) i in poolScoreTuples)
            {
                ceiling += i.Item2;
                poolPercentTuple.Add((i.Item1, (ceiling / costSum)));
            }

            poolPercentTuple.Sort((o1, o2) => o1.Item2.CompareTo(o2.Item2));

            List<Position> answer = null;

            double randValue = rnd.NextDouble();
            int index = 0;
            do
            {
                answer = poolPercentTuple[index].Item1;
                index++;
            } while (poolPercentTuple[index - 1].Item2 < randValue
                || index < poolPercentTuple.Count ? poolPercentTuple[index].Item2 <= randValue : false);

            return answer;
        }
    }
}
