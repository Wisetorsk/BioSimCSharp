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
            string celltype = "J";
            try
            {
                if (args[0] == "/y")
                {
                    int.TryParse(args[1], out yearsToSimulate);
                }
                if (args[2] == "/s")
                {
                    celltype = args[3];
                }
                yearsToSimulate = yearsToSimulate == 0 ? 100 : yearsToSimulate;
                rng = new Random();
                var sim = new Sim("", yearsToSimulate);
                //Console.WriteLine(sim);

                SingleCell(yearsToSimulate, celltype);
                Console.WriteLine("All runs complete! Press enter to run data processing scripts");
                Console.ReadLine();
                System.Diagnostics.Process.Start("CMD.exe", "/C python ../../Scripts/plot.py");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

            
            Console.WriteLine("Press enter to close this window");
            Console.ReadLine();
        }

        public static void SingleCell(int simYears, string type)
        {
            Console.WriteLine($"Running simulation for {simYears} Years{((simYears > 300) ? ". Simulations over 300 years may take a long time, please wait." : ".")}");
            var thisCell = new Position { x = 1, y = 1 };
            IEnviroment envi;

            switch (type)
            {
                case "D":
                    envi = new Desert(thisCell, rng, Enumerable.Range(0, 25).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), Enumerable.Range(0, 10).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
                case "J":
                    envi = new Jungle(thisCell, rng, Enumerable.Range(0, 25).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), Enumerable.Range(0, 10).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
                case "S":
                    envi = new Savannah(thisCell, rng, Enumerable.Range(0, 25).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), Enumerable.Range(0, 10).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
                default:
                    envi = new Jungle(thisCell, rng, Enumerable.Range(0, 25).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), Enumerable.Range(0, 10).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
            }
            bool debug = false;
            using (TextWriter sw = new StreamWriter("../../Results/SimResult.csv"))
            {
                sw.WriteLine("Year,Herbivores,Carnivores,HerbivoreAvgFitness,CarnivoreAvgFitness");
                
                for (int i = 0; i < simYears; i++)
                {
                    if (i % 25 == 0 && debug)
                    {
                        var weights = envi.GetAverageWeight();
                        Console.WriteLine(new string('=', Console.WindowWidth));
                        Console.WriteLine($"Year: {i}");
                        Console.WriteLine($"AverageCarn Weight: {weights[1]}\nAverageHerb Weight: {weights[0]}");
                        Console.WriteLine($"Plants: {envi.Food}\tHerbivore Biomass: {envi.CarnivoreFood}\nHerbivores: {envi.Herbivores.Count()}\tCarnivores: {envi.Carnivores.Count()}");
                    }
                    envi.DEBUG_OneCycle();
                    sw.WriteLine($"{i},{envi.NumberOfHerbivores},{envi.NumberOfCarnivores},{envi.HerbivoreAvgFitness.ToString().Replace(',', '.')},{envi.CarnivoreAvgFitness.ToString().Replace(',','.')}");
                    if (envi.Herbivores.Count() + envi.Carnivores.Count() <= 0) {
                        Console.WriteLine($"All animals have died at year {i}");
                        break;
                    }
                }
            }
            
        }

        
    }
}
