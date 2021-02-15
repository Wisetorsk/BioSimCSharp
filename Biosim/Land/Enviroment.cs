using System;
using System.Collections.Generic;
using System.Linq;
using Biosim.Animals;
using Biosim.Parameters;

namespace Biosim.Land
{
    public class Enviroment : IEnviroment
    {

        // Fields

        // Properties {get; set;}
        public bool Passable { get; set; } = false;
        public Position Pos { get; set; }
        public List<Herbivore> Herbivores { get; set; }
        public List<Carnivore> Carnivores { get; set; }
        public double Food { get; set; }
        public int NumberOfHerbivores => Herbivores.Count();
        public int NumberOfCarnivores => Carnivores.Count();
        public int TotalIndividuals => NumberOfCarnivores + NumberOfHerbivores;

        // Constructor & Overloads

        public Enviroment(Position pos, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null)
        {
            Pos = pos;
            if(initialHerbivores is null)
            {
                Herbivores = new List<Herbivore>();
            } else
            {
                Herbivores = initialHerbivores;
            }

            if (initialCarnivores is null)
            {
                Carnivores = new List<Carnivore>();
            } else
            {
                Carnivores = initialCarnivores;
            }
        }

        public void HerbivoreFeedingCycle()
        {
            Herbivores = Herbivores.OrderBy(i => i.Fitness).ToList();
            foreach (var herb in Herbivores)
            {
                Food = herb.Feed(Food);
            }
        }

        public void CarnivoreFeedingCycle()
        {
            Carnivores = Carnivores.OrderBy(i => i.Fitness).ToList();
            foreach (var carn in Carnivores)
            {
                carn.Feed(Herbivores);
            }
        }

        public void DeathCycle()
        {
            Carnivores.ForEach(i => i.Death());
            Herbivores.ForEach(i => i.Death());
        }

        public void BirthCycle()
        {
            Herbivores = Herbivores.OrderBy(i => i.Fitness).ToList();
            Carnivores = Carnivores.OrderBy(i => i.Fitness).ToList();
        }

        public void AgeCycle()
        {
            Herbivores.ForEach(i => i.GrowOlder());
            Carnivores.ForEach(i => i.GrowOlder());
        }

        public void WeightLossCycle()
        {
            Herbivores.ForEach(i => i.UpdateWeight());
            Carnivores.ForEach(i => i.UpdateWeight());
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
            var births = Carnivores.Where(i => i.GivenBirth).Count() + Herbivores.Where(i => i.GivenBirth).Count();
            Carnivores.ForEach(i => i.GivenBirth = false);
            Herbivores.ForEach(i => i.GivenBirth = false);
            return births;
        }

        public virtual void GrowFood()
        {
            
        }

        public int ResetMigrationParameter()
        {
            var migrations = Carnivores.Where(i => i.Migrated).Count() + Herbivores.Where(i => i.Migrated).Count();
            Carnivores.ForEach(i => i.Migrated = false);
            Herbivores.ForEach(i => i.Migrated = false);
            return migrations;
        }

        // Methods
    }

    public class Desert : Enviroment
    {

        // Fields

        // Properties {get; set;}

        // Constructor & Overloads

        public Desert(Position pos, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null) : base(pos, initialHerbivores, initialCarnivores)
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

        public Ocean(Position pos) : base(pos)
        {

        }

        // Methods
    }

    public class Mountain : Enviroment
    {

        // Fields

        // Properties {get; set;}

        // Constructor & Overloads

        public Mountain(Position pos) : base(pos)
        {

        }

        // Methods
    }

    public class Savannah : Enviroment
    {
        SavannahParams Params = new SavannahParams();
        public Savannah(Position pos, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null) : base(pos, initialHerbivores, initialCarnivores)
        {
            Passable = true;
        }

        // Methods
        public override void GrowFood()
        {
            Food += Params.Alpha * (Params.Fmax - Food);
        }
    }

    public class Jungle : Enviroment
    {
        JungleParams Params = new JungleParams();
        public Jungle(Position pos, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null) : base(pos, initialHerbivores, initialCarnivores)
        {
            Passable = true;
        }

        // Methods
        public override void GrowFood()
        {
            Food = Params.Fmax;
        }
    }

}