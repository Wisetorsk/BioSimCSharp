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
        public Sim(string filepath,  int yearsToSimulate = 100, string template = null)
        {
            Rng = new Random();
            Logger = new LogWriter(filepath, "Year,Herbivores,Carnivores,HerbivoreAvgFitness,CarnivoreAvgFitness");
            YearsToSimulate = yearsToSimulate;
            if (template is null)
            {
                TemplateString = DefaultParameters.DefaultIsland;
            } else
            {
                TemplateString = template;
            }
            Dimentions = Build();
        }

        public int YearsToSimulate { get; set; }
        public string TemplateString { get; set; }
        public List<List<IEnviroment>> Land { get; set; }
        public Position DefaultDim { get; set; } = new Position { x = 10, y = 10 };
        public Random Rng { get; set; }
        public LogWriter Logger { get; set; }
        public Position Dimentions { get; set; }

        public void AddAnimals(List<IAnimal> animals, Position cellPosition)
        {
            throw new NotImplementedException();
        }

        public void AddCarnivore(int age, double w, IAnimalParams par, Position cellPosition)
        {
            throw new NotImplementedException();
        }

        public void AddHerbivore(int age, double w, IAnimalParams par, Position cellPosition)
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

        public Position Build()
        {
            Land = new List<List<IEnviroment>>();
            var lines = TemplateString.Split('\n');
            var xDim = lines.Length;
            var yDim = lines[0].Length-1;
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
                Land.Add(islandLine);
            }
            return new Position() { x = xDim, y = yDim };
        }

        public void ChangeCellParameters(Position cellPos, EnvParams newParams)
        {
            throw new NotImplementedException();
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

        public Position[] GetSurroundingCells(Position cellPos)
        {
            var neighbors = new List<IEnviroment>();
            // Rerurns the surrounding cells as array;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (cellPos.x + i <= 0 || cellPos.x + i > Dimentions.x) continue;
                    if (cellPos.y + j <= 0 || cellPos.y + j > Dimentions.y) continue;
                    if (Land[cellPos.x+i][cellPos.y+j].Passable)
                    {
                        neighbors.Add(Land[cellPos.x + i][cellPos.y + j]);
                    }
                }
            }
            return neighbors.Select(k => k.Pos).ToArray();
        }

        public void LoadCustomOnCellParameters(Position cellPos, IAnimalParams parameters)
        {
            throw new NotImplementedException();
        }

        public void LoadCustomParametersOnAnimal(IAnimal animal, IAnimalParams parameters)
        {
            throw new NotImplementedException();
        }

        public void Migrate()
        {
            for (int i = 0; i < Dimentions.x; i++)
            {
                for (int j = 0; j < Dimentions.y; j++)
                {

                }
            }
        }

        public void OneYear()
        {
            throw new NotImplementedException();
        }

        public void Plot()
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C python ../../Scripts/plot.py");
            Console.WriteLine("Press enter to close this window");
        }

        public void RemoveDead()
        {
            throw new NotImplementedException();
        }

        public void ResetSeasonalParams()
        {
            throw new NotImplementedException();
        }

        public void Simulate(int years)
        { //Runs the simulation for x years
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return TemplateString;
        }
    }
}
