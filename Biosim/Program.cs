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
            var sim = new Sim("../../Results/SimResult.csv", 1500, template);
            sim.Build();
            sim.NoMigration = false;
            for (int i = 0; i < xDim; i++)
            {
                for (int j = 0; j < yDim; j++)
                {
                    if (sim.Land[i][j].Passable)
                    {
                        var pos = new Position { x = i, y = j };
                        List<Herbivore> herbs = Enumerable.Range(0, 50).Select(k => new Herbivore(sim.Rng, pos)).ToList();
                        List<Carnivore> carns = Enumerable.Range(0, 10).Select(k => new Carnivore(sim.Rng, pos)).ToList();
                        //sim.AddAnimals(carns, pos);  //Errors due to "cannot convert from IAnimal
                        //sim.AddAnimals(herbs, pos);
                        Enumerable.Range(0, 50).ToList().ForEach(k => sim.AddHerbivore(pos));
                        Enumerable.Range(0, 8).ToList().ForEach(k => sim.AddCarnivore(pos));
                    }
                }
            }
            sim.Simulate();
        }
    }
}
