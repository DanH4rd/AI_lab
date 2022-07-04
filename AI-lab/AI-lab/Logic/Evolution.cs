using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;
using System.Linq;

namespace AI_lab.Logic
{
    public class Result 
    {
        public string index { get; set; }

        //Result variables
        public List<(List<Position>, double)> population { get; set; }
        public (List<Position>, double) bestScore { get; set; }

        public (List<Position>, double) worstScore { get; set; }

        public double meanScore { get; set; }
        public double standDev { get; set; }

        public override string ToString()
        {
            return String.Format("Best score: {0},\nWorst score: {1},\nMean score: {2},\nStandart deviation: {3}", bestScore.Item2, worstScore.Item2, meanScore, standDev);
        }
    }

    public class Evolution
    {
        // entity info
        public (int, int) layoutSize { get; set; }
        public int deviceCount { get; set; }
        public List<Cost> costs { get; set; }
        public List<Flow> flows { get; set; }

        // Cycle info
        public int populationCount { get; set; }
        public int selectCount { get; set; }
        public int cycleCount { get; set; }
        public double crossChance { get; set; }
        public double mutationChance { get; set; }

        //Cycle methods
        public SelectMethod selectMethod { get; set; }
        public CrossingMethod crossingMethod { get; set; }
        public MutationMethod mutationMethod { get; set; }

        // Random generation method
        public GenerateMethod generateMethod { get; set; }

        // Technical variables
        private List<List<Position>> currentGeneration { get; set; }
        private List<List<Position>> selectedEntities { get; set; }

        // Result set variables methods
        public (List<Position>, double) pickMax(List<(List<Position>, double)> finalGeneration) 
            => finalGeneration.Find(o => o.Item2 == finalGeneration.Max(mo => mo.Item2));
        public (List<Position>, double) pickMin(List<(List<Position>, double)> finalGeneration) 
            => finalGeneration.Find(o => o.Item2 == finalGeneration.Min(mo => mo.Item2));
        public double calculateMean(List<(List<Position>, double)> finalGeneration) 
            => finalGeneration.Sum(o => o.Item2) / finalGeneration.Count;
        public double calculateStandDev(List<(List<Position>, double)> finalGeneration, double meanScore) 
            => Math.Sqrt(finalGeneration.Sum(o => (o.Item2 - meanScore) * (o.Item2 - meanScore)) / finalGeneration.Count);

        public Evolution(GenerateMethod generateMethod, SelectMethod selectMethod, CrossingMethod crossingMethod, MutationMethod mutationMethod)
        {
            this.generateMethod = generateMethod;
            this.selectMethod = selectMethod;
            this.crossingMethod = crossingMethod;
            this.mutationMethod = mutationMethod;
        }

        public void SetCycleParams(int populationCount, int selectCount, int cycleCount, double crossChance, double mutationChance)
        {
            if (populationCount <= 0 || selectCount <= 0 || cycleCount <= 0 || crossChance < 0 || crossChance > 1 || mutationChance < 0 || mutationChance > 1)
                throw new Exception(String.Format("Some params are bad: populationCount({0}), selectCount({1}), cycleCount({2}), crossChance({3}), mutationChance({4})",
                    populationCount, selectCount, cycleCount, crossChance, mutationChance));

            this.populationCount = populationCount;
            this.selectCount = selectCount;
            this.cycleCount = cycleCount;
            this.crossChance = crossChance;
            this.mutationChance = mutationChance;
        }

        public void LoadData(GetData source)
        {
            layoutSize = source.getGridSize();
            deviceCount = source.getDeviceCount();
            costs = source.getCosts();
            flows = source.getFlows();
        }

        public List<Result> PerformRandom()
        {
            List<List<Position>> generatedLayouts = new List<List<Position>>();

            while (generatedLayouts.Count < populationCount * cycleCount)
            {
                generatedLayouts.Add(generateMethod.GenerateLayout(layoutSize, deviceCount));
            }

            List<(List<Position>, double)> result = generatedLayouts.Select(o => (CloneLayout(o), DataAnalysis.calculateLayoutEffect(deviceCount, o, costs, flows))).ToList();
            double meanScoreHolder = calculateMean(result);

            return new List<Result> 
            {
                new Result
                {
                    population = result,
                    bestScore = pickMin(result),
                    worstScore = pickMax(result),
                    meanScore = meanScoreHolder,
                    standDev = calculateStandDev(result, meanScoreHolder)
                } 
            };
        }

        public List<Result> PerformEvolution(bool saveAllEntities)
        {
            if (currentGeneration == null)
                currentGeneration = GeneratePopulation();

            List<Result> results = new List<Result>();

            for (int i = 0; i < cycleCount; i++) 
            {
                Console.WriteLine("Cycle ({0}/{1})", i+1, cycleCount);
                SelectEntities();
                MakeNewPopulation();
                MutatePopulation();

                List<(List<Position>, double)> result = currentGeneration.Select(o => (CloneLayout(o), DataAnalysis.calculateLayoutEffect(deviceCount, o, costs, flows))).ToList();
                double meanScoreHolder = calculateMean(result);
                results.Add
                    (
                        new Result
                        {
                            population = saveAllEntities ? result.Select(o => (CloneLayout(o.Item1), o.Item2)).ToList() : null,
                            bestScore = pickMin(result),
                            worstScore = pickMax(result),
                            meanScore = meanScoreHolder,
                            standDev = calculateStandDev(result, meanScoreHolder)
                        }
                    );

                // Remove previous line in console
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

                // this could be made a free-standing static method for multiple calls
                //char[] blankline = new char[80];
                //Console.Write(blankline, 0, 80); // this will line-wrap at 80 so use Write()

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
            }


            return results;
        }

        public void SelectEntities() 
        {
            selectedEntities = new List<List<Position>>();
            List<List<Position>> selectPool = new List<List<Position>>(currentGeneration);
            List<Position> selectedLayout;

            if (selectMethod is TournamentSelect)
                if (populationCount - selectCount < ((TournamentSelect)selectMethod).tournamentSize)
                    throw new Exception("Tournament size is too big");

            while (selectedEntities.Count < selectCount)
            {
                selectedLayout = selectMethod.Select(deviceCount, selectPool, costs, flows);
                selectedEntities.Add(selectedLayout);
                selectPool.Remove(selectedLayout);
            }
        }

        public void MakeNewPopulation() 
        {
            Random rnd = new Random();

            currentGeneration = new List<List<Position>>(selectedEntities);

            if (mutationChance == 0)
                while (currentGeneration.Count < populationCount)
                {
                    currentGeneration.Add(CloneLayout(currentGeneration[rnd.Next(currentGeneration.Count)]));
                }
            else 
            {
                List<Position> parent1;
                List<Position> parent2;
                while (currentGeneration.Count < populationCount) 
                {
                    parent1 = currentGeneration[rnd.Next(currentGeneration.Count)];
                    do
                    {
                        parent2 = currentGeneration[rnd.Next(currentGeneration.Count)];
                    } while (parent1 == parent2);

                    if (rnd.NextDouble() <= crossChance)
                    {
                        currentGeneration.Add(crossingMethod.Cross(parent1, parent2, layoutSize));
                    }
                    else
                        currentGeneration.Add(CloneLayout(rnd.Next(2) == 1 ? parent1 : parent2));
                }
            }

        }

        public void MutatePopulation() 
        {
            if (mutationChance == 0)
                return;

            Random rnd = new Random();

            foreach (List<Position> i in currentGeneration) 
            {
                if (rnd.NextDouble() <= mutationChance)
                {
                    mutationMethod.Mutate(i, layoutSize);
                }
            }
        }

        public List<List<Position>> GeneratePopulation()
        {
            List<List<Position>> answer = new List<List<Position>>();
            while (answer.Count < populationCount) 
            {
                answer.Add(generateMethod.GenerateLayout(layoutSize, deviceCount));
            }
            return answer;
        }

        public List<Position> CloneLayout(List<Position> layout) 
        {
            List<Position> clone = new List<Position>();
            foreach (Position i in layout) 
            {
                clone.Add(new Position { id = i.id, coordinates = i.coordinates });
            }
            return clone;
        }

        public void Reset()
        {
            currentGeneration = null;
            selectedEntities = null;
        }
    }
}
