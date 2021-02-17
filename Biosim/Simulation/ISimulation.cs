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

        void OneYear();
        void Build();
        void Migrate();
        void Death();
        void Feed();
        void Age();
        void AddAnimals(List<IAnimal> animals);
        void AddHerbivore(int age, double w, IAnimalParams par);
        void AddCarnivore(int age, double w, IAnimalParams par);
        void RemoveDead();
        void Breed();
        void ResetSeasonalParams();
        void Simulate();
        void DisplayIslandString();

        void Plot();
    }
}