using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biosim.Animals;
using Biosim.Land;
using Biosim.Parameters;

namespace Biosim
{
    class Program
    {
        static void Main(string[] args)
        {
            //testNewMethod();
            //tNewMethod();
            var rng = new Random();
            //testDeaths(rng);
            var carnivore = new Carnivore(rng) { Weight = 5, Age = 5 };
            Console.WriteLine(carnivore.Fitness);
            carnivore.Params.F = 50;
            var herbs = new List<Herbivore>();
            for (int i = 0; i < 10; i++) herbs.Add(new Herbivore(rng) { Age = 90, Weight = 10 });
            carnivore.Feed(herbs);
            Console.WriteLine(carnivore.Fitness);


            Console.ReadLine();
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
            Console.ReadLine();
        }

        private static void testNewMethod()
        {
            Herbivore testHerbivore = new Herbivore(new Random()) { Age = 10, Pos = new Position { x = 50, y = 30 } };
            Carnivore testCarnivore = new Carnivore(new Random()) { Pos = new Position { x = 50, y = 30 } };
            Console.WriteLine(testHerbivore);
            //Console.WriteLine(testCarnivore);
            testHerbivore.Weight = 15;
            Console.WriteLine(testHerbivore);
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine("Press any key top show island");
            Console.ReadKey();
            Island testIsland = new Island();
            testIsland.Display();
            Console.ReadLine(); // Halts end of exec
        }
    }
}
