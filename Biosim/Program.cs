﻿using System;
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
                { "D", "Desert" },
                {"S", "Savannah" },
                {"J", "Jungle" },
                {"M", "Mountain" },
                {"O", "Ocean" }
            };

        static void Main(string[] args)
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
                    sw.WriteLine($"{i},{envi.NumberOfHerbivores},{envi.NumberOfCarnivores},{envi.HerbivoreAvgFitness.ToString().Replace(',', '.')},{envi.CarnivoreAvgFitness.ToString().Replace(',','.')},{envi.HerbivoreAvgAge},{envi.CarnivoreAvgAge},{envi.HerbivoreAvgWeight},{envi.CarnivoreAvgWeight}");
                    if (envi.Herbivores.Count() + envi.Carnivores.Count() <= 0) {
                        Console.WriteLine($"All animals have died at year {i}");
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine($"Total Herbivores created: {envi.TotalHerbivoreLives}\nTotal Carnivores created: {envi.TotalCarnivoreLives}");
        }

        
    }
}
