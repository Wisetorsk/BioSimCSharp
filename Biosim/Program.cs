using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biosim.Animals;
using Biosim.Simulation;
using Biosim.Land;
using Biosim.Parameters;

namespace Biosim
{
    class Program
    {
        public static Random rng;
        static void Main(string[] args)
        {
            rng = new Random();
            var sim = new Sim();
            Console.WriteLine(sim);

            SingleCell();

            Console.ReadLine();
        }

        public static void SingleCell()
        {
            var thisCell = new Position { x = 1, y = 1 };
            var jungle = new Jungle(thisCell, rng, Enumerable.Range(0, 25).Select(i => new Herbivore(rng, thisCell) { Weight=25}).ToList(), Enumerable.Range(0, 10).Select(i => new Carnivore(rng, thisCell) { Weight=20}).ToList());
            for (int i = 0; i < 200; i++)
            {
                var carnFit = jungle.Carnivores.Count() == 0 ? 0 : jungle.Carnivores.Select(x => x.Fitness).Sum() / jungle.Carnivores.Count();
                var herbFit = jungle.Herbivores.Count() == 0 ? 0 : jungle.Herbivores.Select(x => x.Fitness).Sum() / jungle.Herbivores.Count();
                if (i%25==0)
                {
                    var weights = jungle.GetAverageWeight();
                    Console.WriteLine(new string('=', Console.WindowWidth));
                    Console.WriteLine($"Year: {i}");
                    Console.WriteLine($"AverageCarn Fitness: {carnFit}\nAverageHerb Fitness: {herbFit}");
                    Console.WriteLine($"AverageCarn Weight: {weights[1]}\nAverageHerb Weight: {weights[0]}");
                    Console.WriteLine($"Plants: {jungle.Food}\tHerbivore Biomass: {jungle.CarnivoreFood}\nHerbivores: {jungle.Herbivores.Count()}\tCarnivores: {jungle.Carnivores.Count()}");
                }
                jungle.DEBUG_OneCycle();
                if (jungle.Herbivores.Count() + jungle.Carnivores.Count() <= 0) break;
            }
        }

        public static void CTest()
        {
            var rng = new Random();
            var testCarn = new Carnivore(rng) { Age = 5, Weight = 60 }; // Healthy Carnivore
            var herbs = new List<Herbivore>();
            for (int i = 0; i < 100; i++)
            {
                herbs.Add(new Herbivore(rng) { Age = 50, Weight = 5 }); // Add ten old herbivores
            }
            var initialW = testCarn.Weight;
            testCarn.Params.F = 10.0; // Set "Hunger" to two herbivores
            testCarn.Feed(herbs);
            var endW = testCarn.Weight;
            Console.WriteLine(initialW < endW);
        }

        private static void testDeaths(Random rng)
        {
            List<bool> results = new List<bool>();
            for (int i = 0; i < 10000; i++)
            {
                var testHerbivore = new Herbivore(rng) { Age = 6, Weight = 100 }; // Herbivore with low fitness
                testHerbivore.Params.Omega = 1;
                testHerbivore.Death();
                results.Add(testHerbivore.IsAlive);
            }
            Console.WriteLine($"deaths: {results.Where(i => !i).Count() / (double)results.Count() * 100}");
        }

        private static void tNewMethod()
        {
            var rng = new Random();
            var animals = new List<Herbivore> { new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng) };

            animals[1].Weight = 100;
            animals[1].Age = 5;
            animals[1].Params.Mu = 1;
            animals[1].Migrate(new Directions[] { Directions.Down, Directions.Up, Directions.Left, Directions.Right });
        }

        private static void OverloadFeedCarn()
        {
            
        }
    }
}
