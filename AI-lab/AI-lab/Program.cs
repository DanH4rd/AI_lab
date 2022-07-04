using System;
using AI_lab.Models;
using AI_lab.Logic;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace AI_lab
{
    class Program
    {
        static string folderPath = @"C:\Users\Gordon Freeman\Desktop\flo_dane_v1.2";
        static string resultFolderPath = @"C:\Users\Gordon Freeman\Desktop\results";

        static int popCount = 50;
        static int selCount = 10;
        static int cycCount = 50;
        static double crChnc = 0.5;
        static double mutCount = 0.2;

        static public Evolution CreateEvolution()
        {
            // Setting up evolution operators
            Evolution ev = new Evolution(
                generateMethod: new BruteRandomGenerate(),
                selectMethod: new TournamentSelect(5),
                crossingMethod: new SplitIdCrossing(),
                mutationMethod: new FlipFlopMutation()
                );
            // Loading studyed data
            ev.LoadData(new HardData(folderPath));

            // Setting evolution params
            ev.SetCycleParams(
                populationCount: popCount,
                selectCount: selCount,
                cycleCount: cycCount,
                crossChance: crChnc,
                mutationChance: mutCount
                );

            return ev;
        }

        static void Main(string[] args)
        {
            bool perform = true;
            while (perform)
            {
                Console.WriteLine("Enter experiment name: ");
                string exName = Console.ReadLine();

                Evolution ev;
                List<Result> evResult;
                // True if we study one AG, false if 10
                if (false)
                {
                    ev = CreateEvolution();
                    evResult = RunEvolutionExperiment(ev);

                    ExportResultToCSV(ExperimentName: exName, experiment: evResult);
                }
                else 
                {
                    // run for diff values
                    evResult = new List<Result>();
                    Console.WriteLine("Performing diff tests");
                    DateTime start = DateTime.Now;

                    List<double> values = new List<double> {0, 0.25, 0.5, 0.75, 1};

                    foreach (double i in values)
                    {
                        ev = CreateEvolution();
                        ev.crossChance = i;
                        evResult.Add(Run10EvolutionExperiments(ev));
                        evResult.Last().index = i.ToString();
                    }
                    Console.WriteLine(String.Format("Success: {0} ms", (DateTime.Now - start).TotalMilliseconds));



                    ExportResultToCSVIndexed(ExperimentName: exName, experiment: evResult);
                }

                // Show only end result
                if (false)
                {
                    Console.WriteLine("--------------------------");

                    Console.WriteLine(String.Format("Evolution result:\n{0}", GetResultForAllGenerations(evResult).ToString()));

                    Console.WriteLine("");
                }

                // Show all iterations
                if (false) 
                {
                    Console.WriteLine("--------------------------");

                    for (int i = 0; i < evResult.Count; i++) 
                    {
                        Console.WriteLine(String.Format("Evolution {0}:\n{1}",i+1, evResult[i].ToString()));

                        Console.WriteLine("");
                    }

                }

                // Perform random search
                if (false)
                {
                    List<Result> randResult = RunRandomExperiment(ev);

                    Console.WriteLine("--------------------------");

                    Console.WriteLine(String.Format("Random result:\n{0}", randResult[0].ToString()));

                    Console.WriteLine("");
                }



                Console.WriteLine(String.Format("Repeat? (Y, y)"));
                perform = Console.ReadLine().ToLower() == "y" ? true : false;
            }
           

        }

        static public List<Result> RunEvolutionExperiment(Evolution ev, bool saveAllEntities = false)
        {
            Console.WriteLine("Evolution experiment start");
            DateTime start = DateTime.Now;
            List<Result> results = ev.PerformEvolution(saveAllEntities);
            Console.WriteLine(String.Format("Evolution experiment finished, taken time: {0} ms", (DateTime.Now - start).TotalMilliseconds));
            return results;
        }

        static public Result Run10EvolutionExperiments(Evolution ev, bool saveAllEntities = false)
        {
            Console.WriteLine("10 Evolution experiment start");
            DateTime start = DateTime.Now;
            List<Result> results = new List<Result>();
            while (results.Count < 10)
            {
                results.Add(GetResultForExperiment(ev.PerformEvolution(false)));
                ev.Reset();
            }
            Console.WriteLine(String.Format("10 Evolution experiment finished, taken time: {0} ms", (DateTime.Now - start).TotalMilliseconds));
            return GetTotalIterationResult(results);
        }

        static public List<Result> RunRandomExperiment(Evolution ev)
        {
            Console.WriteLine("Random experiment start");
            DateTime start = DateTime.Now;
            List<Result> results = ev.PerformRandom();
            Console.WriteLine(String.Format("Random experiment finished, taken time: {0} ms", (DateTime.Now - start).TotalMilliseconds));
            return results;
        }

       
        static public Result GetResultForAllGenerations(List<Result> generations)
        {
            double meanHolder = generations.Sum(o => o.population.Sum(o2 => o2.Item2)) / generations.Sum(o => o.population.Count);

            return new Result
            {
                bestScore = generations.Find(o => o.bestScore.Item2 == generations.Min(mo => mo.bestScore.Item2)).bestScore,
                worstScore = generations.Find(o => o.worstScore.Item2 == generations.Max(mo => mo.worstScore.Item2)).worstScore,
                meanScore = meanHolder,
                standDev = Math.Sqrt(generations.Sum(o => o.population.Sum(o2 => (o2.Item2 - meanHolder) * (o2.Item2 - meanHolder) / generations.Sum(o => o.population.Count))))

            };
        }

        // stats for multiple invocation
        static public Result GetResultForExperiment(List<Result> generations)
        {
            return new Result
            {
                bestScore = generations.Find(o => o.bestScore.Item2 == generations.Min(mo => mo.bestScore.Item2)).bestScore,
                worstScore = generations.Find(o => o.worstScore.Item2 == generations.Max(mo => mo.worstScore.Item2)).worstScore,
            };
        }

        static public Result GetTotalIterationResult(List<Result> iterations)
        {
            double meanHolder = iterations.Sum(o => o.bestScore.Item2) / iterations.Count;

            return new Result
            {
                bestScore = iterations.Find(o => o.bestScore.Item2 == iterations.Min(mo => mo.bestScore.Item2)).bestScore,
                worstScore = iterations.Find(o => o.worstScore.Item2 == iterations.Max(mo => mo.worstScore.Item2)).worstScore,
                meanScore = meanHolder,
                standDev = Math.Sqrt(iterations.Sum(o => (o.bestScore.Item2 - meanHolder) * (o.bestScore.Item2 - meanHolder) / iterations.Count))
            };
        }

        static public void ExportResultToCSV(string ExperimentName, List<Result> experiment) 
        {
            string fullPath = resultFolderPath + @"\" + ExperimentName ;
            Directory.CreateDirectory(fullPath);

            Console.WriteLine("Writing generationsresults...");

            // writing generations info
            using (var writer = new StreamWriter(fullPath + @"\generations.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(experiment.Select(o => new { best = o.bestScore.Item2, worst = o.worstScore.Item2, avg = o.meanScore, std = o.standDev }));
            }

            Console.WriteLine(String.Format("Saved to {0}", fullPath));
        }

        static public void ExportResultToCSVIndexed(string ExperimentName, List<Result> experiment)
        {
            string fullPath = resultFolderPath + @"\" + ExperimentName;
            Directory.CreateDirectory(fullPath);

            Console.WriteLine("Writing generationsresults...");

            // writing generations info
            using (var writer = new StreamWriter(fullPath + @"\generations.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(experiment.Select(o => new { id = o.index,  best = o.bestScore.Item2, worst = o.worstScore.Item2, avg = o.meanScore, std = o.standDev }));
            }

            Console.WriteLine(String.Format("Saved to {0}", fullPath));
        }

        static public String PrintLayout(List<Position> layout, (int, int) layout_size)
        {
            int[,] answer = new int[layout_size.Item1, layout_size.Item2];
            // initialise
            for (int i = 0; i < layout_size.Item1; i++)
                for (int j = 0; j < layout_size.Item2; j++)
                    answer[i, j] = -1;

            // fill in machines and make a string
            foreach (Position i in layout)
            {
                answer[i.coordinates.Item1, i.coordinates.Item2] = i.id;
            }

            string answerStr = "";
            for (int i = 0; i < layout_size.Item1; i++)
            {
                for (int j = 0; j < layout_size.Item2; j++)
                    answerStr += String.Format("{0, 4:N0}", answer[i, j] >= 0 ? answer[i, j].ToString() : "-");
                answerStr += "\n";
            }


            return answerStr;
        }


    }
}
