using Biosim.Animals;
using System.Collections.Generic;
using System.Linq;

namespace Biosim.Land
{
    public interface IEnviroment
    {
        List<Carnivore> Carnivores { get; set; }
        double Food { get; set; }
        List<Herbivore> Herbivores { get; set; }
        bool Passable { get; set; }
        int NumberOfHerbivores { get; }
        int NumberOfCarnivores { get; }
        int TotalIndividuals { get; }

        void HerbivoreFeedingCycle();
        void CarnivoreFeedingCycle();
        void DeathCycle();
        void BirthCycle();
        void AgeCycle();
        void WeightLossCycle();
        void RemoveDeadIndividuals();
    }
}