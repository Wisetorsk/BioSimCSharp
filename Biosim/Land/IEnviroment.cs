using Biosim.Animals;
using System.Collections.Generic;

namespace Biosim.Land
{
    public interface IEnviroment
    {
        List<Carnivore> Carnivores { get; set; }
        double Food { get; set; }
        List<Herbivore> Herbivores { get; set; }
        bool Passable { get; set; }

        void HerbivoreFeedingCycle();
        void CarnivoreFeedingCycle();
        void DeathCycle();
        void BirthCycle();
        void AgeCycle();
        void WeightLossCycle();
    }
}