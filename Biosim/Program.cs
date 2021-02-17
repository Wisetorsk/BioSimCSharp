using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biosim.Animals;
using Biosim.Simulation;
using Biosim.Land;
using Biosim.Parameters;
using System.IO;

namespace Biosim
{
    class Program
    {
        public static Random rng;
        static void Main(string[] args)
        {
            int yearsToSimulate = 0;
            if (args[0] == "/y")
            {
                int.TryParse(args[1], out yearsToSimulate);
            }
            yearsToSimulate = yearsToSimulate == 0 ? 100 : yearsToSimulate;
            rng = new Random();
            var sim = new Sim("", yearsToSimulate);
            //Console.WriteLine(sim);

            SingleCell(yearsToSimulate);
            Console.WriteLine("All runs complete! Press enter to run scripts");
            Console.ReadLine();
            System.Diagnostics.Process.Start("CMD.exe", "/C python ../../Scripts/plot.py");
            Console.WriteLine("Press enter to close this window");
            Console.ReadLine();
        }

        public static void SingleCell(int simYears)
        {
            Console.WriteLine($"Running simulation for {simYears} Years");
            bool debug = false;
            using (TextWriter sw = new StreamWriter("../../Results/SimResult.csv"))
            {
                sw.WriteLine("Year,Herbivores,Carnivores,HerbivoreAvgFitness,CarnivoreAvgFitness");
                var thisCell = new Position { x = 1, y = 1 };
                var jungle = new Jungle(thisCell, rng, Enumerable.Range(0, 25).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), Enumerable.Range(0, 10).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                for (int i = 0; i < simYears; i++)
                {
                    if (i % 25 == 0 && debug)
                    {
                        var weights = jungle.GetAverageWeight();
                        Console.WriteLine(new string('=', Console.WindowWidth));
                        Console.WriteLine($"Year: {i}");
                        Console.WriteLine($"AverageCarn Weight: {weights[1]}\nAverageHerb Weight: {weights[0]}");
                        Console.WriteLine($"Plants: {jungle.Food}\tHerbivore Biomass: {jungle.CarnivoreFood}\nHerbivores: {jungle.Herbivores.Count()}\tCarnivores: {jungle.Carnivores.Count()}");
                    }
                    jungle.DEBUG_OneCycle();
                    sw.WriteLine($"{i},{jungle.NumberOfHerbivores},{jungle.NumberOfCarnivores},{jungle.HerbivoreAvgFitness.ToString().Replace(',', '.')},{jungle.CarnivoreAvgFitness.ToString().Replace(',','.')}");
                    if (jungle.Herbivores.Count() + jungle.Carnivores.Count() <= 0) break;
                }
            }
            
        }

        
    }
}
