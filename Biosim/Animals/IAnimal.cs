using Biosim.Parameters;

namespace Biosim.Animals
{
    public interface IAnimal
    {
        int Age { get; set; }
        Position Pos { get; set; }
        double Weight { get; set; }
        double Qplus { get; set; }
        double Qneg { get; set; }

        //Directions Migrate(Directions[] dir);
        Animal Birth(int sameSpeciesInCell);
        bool Death();
        void UpdateFitness();
        void UpdateWeight(); //Lose weight
        void GrowOlder(); // Increment Age
    }
}