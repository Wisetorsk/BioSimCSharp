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
        public int TotalDeadHerbivores { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TotalDeadCarnivores { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int DeadHerbivoresThisYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int DeadCarnivoresThisYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double AverageHerbivoreFitness => throw new NotImplementedException();

        public double AverageCarnivoreFitness => throw new NotImplementedException();

        public double AverageHerbivoreAge => throw new NotImplementedException();

        public double AverageCarnivoreAge => throw new NotImplementedException();

        public double AverageHerbivoreWeight => throw new NotImplementedException();

        public double AverageCarnivoreWeight => throw new NotImplementedException();

        public double PeakHerbiovreFitness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double PeakCarnivoreFitness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double PeakHerbivoreWeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double PeakCarnivoreWeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int HerbivoresBornThisYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CarnivoresBornThisYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TotalHerbivoresCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TotalCarnivoresCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            foreach (var line in Land)
            {
                foreach (var cell in line)
                {
                    cell.DeathCycle();
                }
            }
        }

        public void DisplayIslandString()
        {
            throw new NotImplementedException();
        }

        public void Feed()
        {
            foreach (var line in Land)
            {
                foreach (var cell in line)
                {
                    cell.HerbivoreFeedingCycle();
                    cell.CarnivoreFeedingCycle();
                }
            }
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
            if (parameters.GetType().Name == "HerbivoreParams")
            {
                Land[cellPos.x][cellPos.y].Herbivores.ForEach(i => i.Params = parameters);
                return;
            }
            if (parameters.GetType().Name == "CarnivoreParams")
            {
                Land[cellPos.x][cellPos.y].Carnivores.ForEach(i => i.Params = parameters);
            }

            
        }

        public bool LoadCustomParametersOnAnimal(IAnimal animal, IAnimalParams parameters)
        {
            /*Find the animal*/
            foreach (var line in Land)
            {
                foreach (var cell in line)
                {
                    foreach (var herb in cell.Herbivores)
                    {
                        if (herb == animal)
                        {
                            herb.Params = parameters;
                            return true;
                        }
                    }
                    foreach (var carn in cell.Carnivores)
                    {
                        if (carn == animal)
                        {
                            carn.Params = parameters;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public string Migrate(IEnviroment cell)
        {
            /*
            Get the new layout of animal populations based on the enviroments methods goveringin what animals want to move, and to what cell they want to move to
            Create a "second" island*/
            var surrounding = GetSurroundingCells(cell.Pos);
            return "Migration Result";
        }

        public string OneYear()
        {
            for (int i = 0; i < Dimentions.x; i++)
            {
                for (int j = 0; j < Dimentions.y; j++)
                {
                    var cell = Land[i][j];
                    string firstHalfResult = OneCellYearFirstHalf(cell);
                    string migrationResult = Migrate(cell);
                    string secondHalfResult = OneCellYearSecondHalf(cell);
                }
            }
            return "TOTAL RESULT";
        }
        public string OneCellYearFirstHalf(IEnviroment cell)
        {
            //GrowFood();
            //HerbivoreFeedingCycle();
            //CarnivoreFeedingCycle();
            //BirthCycle();
            return "";
        }
        public string OneCellYearSecondHalf(IEnviroment cell)
        {
            //AgeCycle();
            //WeightLossCycle();
            //DeathCycle();
            //RemoveDeadIndividuals();
            //ResetGivenBirthParameter();
            return "";
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

        public void MoveAnimals()
        {
            throw new NotImplementedException();
        }
    }
}
