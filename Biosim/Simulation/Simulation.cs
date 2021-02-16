using Biosim.Animals;
using Biosim.Land;
using Biosim.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosim.Simulation
{
    public class Sim : ISimulation

    {
        public Sim(int yearsToSimulate = 100, string template = null)
        {
            Rng = new Random();
            YearsToSimulate = yearsToSimulate;
            if (template is null)
            {
                TemplateString = DefaultParameters.DefaultIsland;
            } else
            {
                TemplateString = template;
            }
        }

        public int YearsToSimulate { get; set; }
        public string TemplateString { get; set; }
        public List<List<IEnviroment>> Land { get; set; }
        public Position DefaultDim { get; set; } = new Position { x = 10, y = 10 };
        public Random Rng { get; set; }

        public void AddAnimals(List<IAnimal> animals)
        {
            throw new NotImplementedException();
        }

        public void AddCarnivore(int age, double w, IAnimalParams par)
        {
            throw new NotImplementedException();
        }

        public void AddHerbivore(int age, double w, IAnimalParams par)
        {
            throw new NotImplementedException();
        }

        public void Age()
        {
            foreach (var row in Land)
            {
                foreach (var cell in row)
                {
                    cell.AgeCycle();
                }
            }
        }

        public void Breed()
        {
            throw new NotImplementedException();
        }

        public void Build()
        {
            var lines = TemplateString.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                List<IEnviroment> islandLine = new List<IEnviroment>();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    switch (lines[i][j])
                    {
                        case 'D':
                            islandLine.Add(new Desert(new Position { x = i, y = j }, Rng));
                            break;
                        case 'M':
                            islandLine.Add(new Mountain(new Position { x = i, y = j }, Rng));
                            break;
                        case 'S':
                            islandLine.Add(new Savannah(new Position { x = i, y = j }, Rng));
                            break;
                        case 'J':
                            islandLine.Add(new Jungle(new Position { x = i, y = j }, Rng));
                            break;
                        case 'O':
                            islandLine.Add(new Ocean(new Position { x = i, y = j }, Rng));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Death()
        {
            throw new NotImplementedException();
        }

        public void DisplayIslandString()
        {
            throw new NotImplementedException();
        }

        public void Feed()
        {
            throw new NotImplementedException();
        }

        public void Migrate()
        {
            throw new NotImplementedException();
        }

        public void OneYear()
        {
            throw new NotImplementedException();
        }

        public void RemoveDead()
        {
            throw new NotImplementedException();
        }

        public void ResetSeasonalParams()
        {
            throw new NotImplementedException();
        }

        public void Simulate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return TemplateString;
        }
    }
}
