using System;
using System.Collections.Generic;
using System.Linq;
using Biosim.Animals;


namespace Biosim.Land
{
    public class Enviroment : IEnviroment
    {

        // Fields

        // Properties {get; set;}
        public bool Passable { get; set; } = false;
        public List<Herbivore> Herbivores { get; set; }
        public List<Carnivore> Carnivores { get; set; }
        public double Food { get; set; }
        public int NumberOfHerbivores => Herbivores.Count();
        public int NumberOfCarnivores => Carnivores.Count();
        public int TotalIndividuals => NumberOfCarnivores + NumberOfHerbivores;
        

        // Constructor & Overloads

        public Enviroment()
        {

        }

        public void HerbivoreFeedingCycle()
        {
            throw new NotImplementedException();
        }

        public void CarnivoreFeedingCycle()
        {
            throw new NotImplementedException();
        }

        public void DeathCycle()
        {
            throw new NotImplementedException();
        }

        public void BirthCycle()
        {
            throw new NotImplementedException();
        }

        public void AgeCycle()
        {
            throw new NotImplementedException();
        }

        public void WeightLossCycle()
        {
            throw new NotImplementedException();
        }

        public void RemoveDeadIndividuals()
        {
            foreach (var herb in Herbivores)
            {
                if (!herb.IsAlive)
                {
                    Herbivores.Remove(herb);
                }
            }

            foreach (var carn in Carnivores)
            {
                if (!carn.IsAlive)
                {
                    Carnivores.Remove(carn);
                }
            }
        }

        public int ResetGivenBirthParameter()
        {
            throw new NotImplementedException();
        }

        public void GrowFood()
        {
            throw new NotImplementedException();
        }

        // Methods
    }

    public class Desert : Enviroment
    {

        // Fields

        // Properties {get; set;}

        // Constructor & Overloads

        public Desert() : base()
        {
            Passable = true;
        }

        // Methods
    }

    public class Ocean : Enviroment
    {

        // Fields

        // Properties {get; set;}

        // Constructor & Overloads

        public Ocean() : base()
        {

        }

        // Methods
    }

    public class Mountain : Enviroment
    {

        // Fields

        // Properties {get; set;}

        // Constructor & Overloads

        public Mountain() : base()
        {

        }

        // Methods
    }

    public class Savannah : Enviroment
    {
        public Savannah() : base()
        {
            Passable = true;
        }

        // Methods
    }

    public class Jungle : Enviroment
    {
        public Jungle() : base()
        {
            Passable = true;
        }
    }

}