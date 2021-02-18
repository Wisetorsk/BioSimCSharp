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
        List<List<IEnviroment>> Land { get; set; } // Nested List
        Position DefaultDim { get; set; }
        int YearsToSimulate { get; set; }
        Random Rng { get; set; }
        LogWriter Logger { get; set; }
        Position Dimentions { get; set; }

        void OneYear();
        Position Build();
        void Migrate();
        void Death();
        void Feed();
        void Age();
        void AddAnimals(List<IAnimal> animals, Position cellPosition);
        void AddHerbivore(int age, double w, IAnimalParams par, Position cellPosition);
        void AddCarnivore(int age, double w, IAnimalParams par, Position cellPosition);
        void RemoveDead();
        void Breed();
        void ResetSeasonalParams();
        void Simulate();
        void DisplayIslandString();
        void LoadCustomOnCellParameters(Position cellPos, IAnimalParams parameters);
        void LoadCustomParametersOnAnimal(IAnimal animal, IAnimalParams parameters);
        IEnviroment[] GetSurroundingCells(Position cellPos);
        void Plot();
    }
}