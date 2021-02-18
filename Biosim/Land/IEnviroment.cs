﻿using Biosim.Animals;
using Biosim.Parameters;
using Biosim.Land;
using System.Collections.Generic;
using System.Linq;

namespace Biosim.Land
{
    public interface IEnviroment
    {
        List<Carnivore> Carnivores { get; set; }
        Position Pos { get; set; }
        double Food { get; set; }
        List<Herbivore> Herbivores { get; set; }
        bool Passable { get; set; }
        int NumberOfHerbivores { get; }
        int NumberOfCarnivores { get; }
        int TotalIndividuals { get; }
        double CarnivoreFood { get; }
        int KilledByCarnivores { get; set; }
        int DeadCarnivores { get; set; }
        int DeadHerbivores { get; set; }
        int NewHerbivores { get; set; }
        int NewCarnivores { get; set; }
        double HerbivoreAvgFitness { get; }
        double CarnivoreAvgFitness { get; }
        double HerbivoreAvgWeight { get; }
        double CarnivoreAvgWeight { get; }
        double HerbivoreAvgAge { get; }
        double CarnivoreAvgAge { get; }
        EnvParams Params { get; set; }

        void HerbivoreFeedingCycle();
        void CarnivoreFeedingCycle();
        void DeathCycle();
        void BirthCycle();
        void GrowFood();
        int ResetGivenBirthParameter(); //Returns the number of births in a given cycle
        int ResetMigrationParameter();
        void AgeCycle();
        void WeightLossCycle();
        void RemoveDeadIndividuals();
        void DEBUG_OneCycle();
        double[] GetAverageWeight();
        void OverloadParameters(IAnimal animal, IAnimalParams parameters);
        void OverloadParameters(int index, string type, IAnimalParams parameters);
        void OverloadAllHerbivores(HerbivoreParams parameters);
        void OverloadAllCarnivores(CarnivoreParams parameters);
    }
}