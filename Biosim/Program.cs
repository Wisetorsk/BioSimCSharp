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
            var rng = new Random();
            var animals = new List<Herbivore> { new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng), new Herbivore(rng)};
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
