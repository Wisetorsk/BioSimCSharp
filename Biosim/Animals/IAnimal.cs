using Biosim.Parameters;

namespace Biosim.Animals
{
    public interface IAnimal
    {
        int Age { get; set; }
        Position Pos { get; set; }
        Position GoingToMoveTo { get; set; } // The cell the animal is going to move to under simulation.migration
        double Weight { get; set; }
        double Qplus { get; set; }
        double Qneg { get; set; }
        IAnimalParams Params { get; set; }
        bool IsAlive { get; set; }
        bool GivenBirth { get; set; }
        bool Migrated { get; set; }

        //Directions Migrate(Directions[] dir);
        IAnimal Birth(int sameSpeciesInCell);
        void Death();
        void UpdateWeight(); //Lose weight
        void GrowOlder(); // Increment Age
        bool Kill();
        void UpdateParameters();
    }
}