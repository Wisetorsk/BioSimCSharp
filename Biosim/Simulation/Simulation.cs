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
        public Sim(string filepath="../../Results/SimResult.csv", int yearsToSimulate = 100, string template = null, bool noMigration = false)
        {
            Rng = new Random();
            Logger = new LogWriter(filepath, "Year,Herbivores,Carnivores,HerbivoreAvgFitness,CarnivoreAvgFitness,HerbivoreAvgAge,CarnivoreAvgAge,HerbivoreAvgWeight,CarnivoreAvgWeight,HerbivoresBornThisYear,CarnivoresBornThisYear");
            NoMigration = noMigration;
            YearsToSimulate = yearsToSimulate;
            if (template is null)
            {
                TemplateString = DefaultParameters.DefaultIsland;
            } else
            {
                TemplateString = template;
            }
            Dimentions = Build(); // Builds the "Land" prop and returns it's dimentions as object.
        }

        public int YearsToSimulate { get; set; }
        public bool NoMigration { get; set; }
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
        public double AverageHerbivoreFitness => Land.Select(i => i.Select(j => j.HerbivoreAvgFitness).Average()).Average();
        public double AverageCarnivoreFitness => Land.Select(i => i.Select(j => j.CarnivoreAvgFitness).Average()).Average();
        public double AverageHerbivoreAge => Land.Select(i => i.Select(j => j.HerbivoreAvgAge).Average()).Average();
        public double AverageCarnivoreAge => Land.Select(i => i.Select(j => j.CarnivoreAvgAge).Average()).Average();
        public double AverageHerbivoreWeight => Land.Select(i => i.Select(j => j.HerbivoreAvgWeight).Average()).Average();
        public double AverageCarnivoreWeight => Land.Select(i => i.Select(j => j.CarnivoreAvgWeight).Average()).Average();
        public double PeakHerbiovreFitness => Land.Select(i => i.Select(j => j.PeakHerbivoreFitness).Max()).Max();
        public double PeakCarnivoreFitness => Land.Select(i => i.Select(j => j.PeakCarnivoreFitness).Max()).Max();
        public double PeakHerbivoreWeight => Land.Select(i => i.Select(j => j.PeakHerbivoreWeight).Max()).Max();
        public double PeakCarnivoreWeight => Land.Select(i => i.Select(j => j.PeakCarnivoreWeight).Max()).Max();
        public int HerbivoresBornThisYear => Land.Select(i => i.Select(j => j.NewHerbivores).Sum()).Sum();
        public int CarnivoresBornThisYear => Land.Select(i => i.Select(j => j.NewCarnivores).Sum()).Sum();
        public int TotalHerbivoresCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TotalCarnivoresCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CurrentYear { get; set; } = 0;

        public int LiveHerbivores => Land.Select(i => i.Select(j => j.NumberOfHerbivores).Sum()).Sum();

        public int LiveCarnivores => Land.Select(i => i.Select(j => j.NumberOfCarnivores).Sum()).Sum();

        public void AddAnimals(List<IAnimal> animals, Position cellPosition)
        {
            if (animals.Count() <= 0 || animals is null) throw new Exception("You must provide animals to insert");
            if (animals[0].GetType().Name == "Herbivore")
            {
                animals.ForEach(i => Land[cellPosition.x][cellPosition.y].Herbivores.Add((Herbivore)i));
            } else
            {
                animals.ForEach(i => Land[cellPosition.x][cellPosition.y].Carnivores.Add((Carnivore)i));
            }

        }

        public void AddCarnivore(int age, double w, Position cellPosition, IAnimalParams par = null)
        {
            Land[cellPosition.x][cellPosition.y].Carnivores.Add(
                new Carnivore(
                    Rng,
                    new Position { x = cellPosition.x, y = cellPosition.y },
                    par)
                { Weight=w, Age=age });
        }

        public void AddHerbivore(int age, double w, Position cellPosition, IAnimalParams par = null)
        {
            Land[cellPosition.x][cellPosition.y].Herbivores.Add(
                new Herbivore(
                    Rng,
                    new Position { x = cellPosition.x, y = cellPosition.y },
                    par)
                { Weight = w, Age = age });
        }

        public void AddCarnivore(Position cellPosition, IAnimalParams par = null)
        {
            Land[cellPosition.x][cellPosition.y].Carnivores.Add(
                new Carnivore(
                    Rng,
                    new Position { x = cellPosition.x, y = cellPosition.y },
                    par));
        }

        public void AddHerbivore( Position cellPosition, IAnimalParams par = null)
        {
            Land[cellPosition.x][cellPosition.y].Herbivores.Add(
                new Herbivore(
                    Rng,
                    new Position { x = cellPosition.x, y = cellPosition.y },
                    par));
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
            var yDim = lines[0].Length;
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

        public void Migrate(IEnviroment cell)
        {
            /*
            Get the new layout of animal populations based on the enviroments methods goveringin what animals want to move, and to what cell they want to move to
            Create a "second" island*/
            if (NoMigration) return;
            var surrounding = GetSurroundingCells(cell.Pos);
            // Migration stuff...
        }

        public void OneYear()
        {
            for (int i = 0; i < Dimentions.x; i++)
            {
                for (int j = 0; j < Dimentions.y; j++)
                {
                    var cell = Land[i][j];
                    OneCellYearFirstHalf(cell);
                    Migrate(cell);
                    OneCellYearSecondHalf(cell);
                    //Save Log info here.
                    
                }
            }
            Logger.Log($"{CurrentYear.ToString().Replace(',','.')},{LiveHerbivores.ToString().Replace(',', '.')},{LiveCarnivores.ToString().Replace(',', '.')},{AverageHerbivoreFitness.ToString().Replace(',', '.')},{AverageCarnivoreFitness.ToString().Replace(',', '.')},{AverageHerbivoreAge.ToString().Replace(',', '.')},{AverageCarnivoreAge.ToString().Replace(',', '.')},{AverageHerbivoreWeight.ToString().Replace(',', '.')},{AverageCarnivoreWeight.ToString().Replace(',', '.')},{HerbivoresBornThisYear.ToString().Replace(',', '.')},{CarnivoresBornThisYear.ToString().Replace(',', '.')}");
            CurrentYear++;
        }

        public string GetCellInformation(IEnviroment cell)
        {
            return null;
        }

        public void OneCellYearFirstHalf(IEnviroment cell)
        {
            cell.GrowFood();
            cell.HerbivoreFeedingCycle();
            cell.CarnivoreFeedingCycle();
            cell.BirthCycle();
        }
        public void OneCellYearSecondHalf(IEnviroment cell)
        {
            cell.AgeCycle();
            cell.WeightLossCycle();
            cell.DeathCycle();
            cell.RemoveDeadIndividuals();
            cell.ResetGivenBirthParameter();
            cell.ResetMigrationParameter();
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

        public void UpdateStatTrackers()
        {
            //DeadHerbivoresThisYear;
            //DeadCarnivoresThisYear;
            //HerbivoresBornThisYear;
            //CarnivoresBornThisYear;

            //TotalCarnivoresCreated += Land.Select(i => i.Select(j => j.));
            //TotalHerbivoresCreated;
            //TotalDeadCarnivores;
            //TotalDeadHerbivores;
        }

        public void SaveCSV()
        {
            Logger.LogCSV();
        }
    }
}
