using Biosim.Land;
using Biosim.Animals;
using Biosim.Parameters;
using System.Collections.Generic;
using System;

namespace Biosim.Simulation
{
    internal interface ISimulation
    {
        string TemplateString { get; set; }
        bool NoMigration { get; set; }
        List<List<IEnviroment>> Land { get; set; } // Nested List
        Position DefaultDim { get; set; }
        int YearsToSimulate { get; set; }
        Random Rng { get; set; }
        LogWriter Logger { get; set; }
        Position Dimentions { get; set; }
        int TotalDeadHerbivores { get; set; }
        int TotalDeadCarnivores { get; set; }
        int DeadHerbivoresThisYear { get; set; }
        int DeadCarnivoresThisYear { get; set; }
        int HerbivoresBornThisYear { get; set; }
        int CarnivoresBornThisYear { get; set; }
        int TotalHerbivoresCreated { get; set; }
        int TotalCarnivoresCreated { get; set; }
        double AverageHerbivoreFitness { get; }
        double AverageCarnivoreFitness { get; }
        double AverageHerbivoreAge { get; }
        double AverageCarnivoreAge { get; }
        double AverageHerbivoreWeight { get; }
        double AverageCarnivoreWeight { get; }
        double PeakHerbiovreFitness { get; set; }
        double PeakCarnivoreFitness { get; set; }
        double PeakHerbivoreWeight { get; set; }
        double PeakCarnivoreWeight { get; set; }


        Position Build();
        string Migrate(IEnviroment cell);
        void Death();
        void Feed();
        void Age();
        void AddAnimals(List<IAnimal> animals, Position cellPosition);
        void AddHerbivore(int age, double w, IAnimalParams par, Position cellPosition);
        void AddCarnivore(int age, double w, IAnimalParams par, Position cellPosition);
        void RemoveDead();
        void Breed();
        void ResetSeasonalParams();
        void Simulate(int years);
        string OneCellYearFirstHalf(IEnviroment cell);
        string OneCellYearSecondHalf(IEnviroment cell);
        string OneYear(); // Runs the simulation for one year and returns a string of data
        void DisplayIslandString();
        void LoadCustomOnCellParameters(Position cellPos, IAnimalParams parameters); // Parameters for all animals of a type in cell
        bool LoadCustomParametersOnAnimal(IAnimal animal, IAnimalParams parameters);
        Position[] GetSurroundingCells(Position cellPos);
        void MoveAnimals(); // Tests must ensure that the animal is actually moved, not copied or replaced with age 0 animal
        void Plot();
        void ChangeCellParameters(Position cellPos, EnvParams newParams);
    }
}