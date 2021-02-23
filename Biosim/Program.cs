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
        public static Dictionary<string, string> cellTypes = new Dictionary<string, string>() {
                {"D", "Desert" },
                {"S", "Savannah" },
                {"J", "Jungle" },
                {"M", "Mountain" },
                {"O", "Ocean" }
            };

        static void Main(string[] args)
        {
            //SingleCellSimulation(args); //This one will give you some nice output too!
            TryOutSimulationClass();
            Console.ReadLine();
        }

        private static void TryOutSimulationClass()
        {
            var template = "DDDDD\nDSSSD\nDJJJD\nDSSSD\nDDDDD";
            int xDim = template.Split('\n')[0].Length;
            int yDim = template.Split('\n').Length;
            var sim = new Sim("../../Results/SimResult.csv", 500, template);
            sim.Build();
            sim.NoMigration = true;
            for (int i = 0; i < xDim; i++)
            {
                for (int j = 0; j < yDim; j++)
                {
                    var pos = new Position { x = i, y = j };
                    List<Herbivore> herbs = Enumerable.Range(0, 10).Select(k => new Herbivore(sim.Rng, pos)).ToList();
                    List<Carnivore> carns = Enumerable.Range(0, 10).Select(k => new Carnivore(sim.Rng, pos)).ToList();
                    //sim.AddAnimals(carns, pos);  Errors due to "cannot convert from IAnimal
                    //sim.AddAnimals(herbs, pos);
                    Enumerable.Range(0, 10).ToList().ForEach(k => sim.AddHerbivore(pos));
                    Enumerable.Range(0, 3).ToList().ForEach(k => sim.AddCarnivore(pos));
                    
                }
            }
            int ind = 0;
            for (int i = 0; i < 500; i++)
            {
                sim.OneYear();
                if (i%10==0)
                {
                    int num = ind % 4;
                    Console.Clear();
                    Console.WriteLine($"SIMULATION IS NOW RUNNING{new string('.', num)}");
                    ind+=1;
                }
            }
            sim.SaveCSV();
            sim.Plot();
            sim.FoodLog.LogCSV();
            Console.WriteLine("Simulation Over");
        }

        private static void SingleCellSimulation(string[] args)
        {
            int yearsToSimulate = 0;
            string celltype = "J";
            int herbStart = 0;
            int carnStart = 0;

            try
            {
                try
                {
                    if (args[0] == "/y")
                    {
                        int.TryParse(args[1], out yearsToSimulate);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    yearsToSimulate = 100;
                }

                try
                {
                    if (args[2] == "/s")
                    {
                        celltype = args[3];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    celltype = "J";
                }

                try
                {
                    if (args[4] == "/h")
                    {
                        int.TryParse(args[5], out herbStart);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    herbStart = 25;
                }

                try
                {
                    if (args[6] == "/c")
                    {
                        int.TryParse(args[7], out carnStart);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    carnStart = 10;
                }

                rng = new Random();
                var sim = new Sim("", yearsToSimulate);
                //Console.WriteLine(sim);


                SingleCell(yearsToSimulate, celltype, herbStart, carnStart);
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

        public static void SingleCell(int simYears, string type, int herbStart, int carnStart)
        {
            Console.WriteLine($"Cell type: {cellTypes[type]}\nStarting Herbivores: {herbStart}\nStarting Carnivores: {carnStart}");
            Console.WriteLine($"Running simulation for {simYears} Years{((simYears > 300) ? ". Simulations over 300 years may take a long time, please wait." : ".")}");
            var thisCell = new Position { x = 1, y = 1 };
            IEnviroment envi;
            var PHf = new List<double>();
            var PCf = new List<double>();
            var PHw = new List<double>();
            var PCw = new List<double>();
            var PHa = new List<double>();
            var PCa = new List<double>();
            switch (type)
            {
                case "D":
                    envi = new Desert(thisCell, 
                        rng, 
                        Enumerable.Range(0, herbStart).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), 
                        Enumerable.Range(0, carnStart).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
                case "J":
                    envi = new Jungle(thisCell, 
                        rng, 
                        Enumerable.Range(0, herbStart).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), 
                        Enumerable.Range(0, carnStart).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
                case "S":
                    envi = new Savannah(thisCell, 
                        rng, 
                        Enumerable.Range(0, herbStart).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), 
                        Enumerable.Range(0, carnStart).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
                default:
                    envi = new Jungle(thisCell, 
                        rng, 
                        Enumerable.Range(0, herbStart).Select(i => new Herbivore(rng, thisCell) { Weight = 25 }).ToList(), 
                        Enumerable.Range(0, carnStart).Select(i => new Carnivore(rng, thisCell) { Weight = 20 }).ToList());
                    break;
            }
            using (TextWriter sw = new StreamWriter("../../Results/SimResult.csv"))
            {
                sw.WriteLine("Year,Herbivores,Carnivores,HerbivoreAvgFitness,CarnivoreAvgFitness,HerbivoreAvgAge,CarnivoreAvgAge,HerbivoreAvgWeight,CarnivoreAvgWeight");
                
                for (int i = 0; i < simYears; i++)
                {
                    if (i == 200) envi.Params.Fmax = 150; // At year 200, decrease total food available
                    if (i == 600) envi.Params.Fmax = 500; // Increase food at year 600
                    envi.DEBUG_OneCycle();
                    sw.WriteLine($"{i},{envi.NumberOfHerbivores},{envi.NumberOfCarnivores},{envi.HerbivoreAvgFitness.ToString().Replace(',', '.')},{envi.CarnivoreAvgFitness.ToString().Replace(',','.')},{envi.HerbivoreAvgAge.ToString().Replace(',', '.')},{envi.CarnivoreAvgAge.ToString().Replace(',', '.')},{envi.HerbivoreAvgWeight.ToString().Replace(',', '.')},{envi.CarnivoreAvgWeight.ToString().Replace(',', '.')}");
                    PHa.Add(envi.PeakHerbivoreAge);
                    PCa.Add(envi.PeakCarnivoreAge);
                    PHf.Add(envi.PeakHerbivoreFitness);
                    PCf.Add(envi.PeakHerbivoreFitness);
                    PHw.Add(envi.PeakHerbivoreWeight);
                    PCw.Add(envi.PeakCarnivoreWeight);
                    if (envi.Herbivores.Count() + envi.Carnivores.Count() <= 0) {
                        Console.WriteLine($"All animals have died at year {i}");
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine($"Total Herbivores created: {envi.TotalHerbivoreLives}\nTotal Carnivores created: {envi.TotalCarnivoreLives}");
            Console.WriteLine($"Peak Herb fitness - {PHf.Max()}\nPeak Carn fitness - {PCf.Max()}\nPeak Herb weight - {PHw.Max()}\nPeak Carn weight - {PCw.Max()}\nPeak Herb age - {PHa.Max()}\nPeak Carn age - {PCa.Max()}");
        }

        
    }
}
