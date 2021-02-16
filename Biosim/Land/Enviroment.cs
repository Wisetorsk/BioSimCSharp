﻿using System;
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
        public Random Rng { get; set; }
        public bool Passable { get; set; } = false;
        public Position Pos { get; set; }
        public List<Herbivore> Herbivores { get; set; }
        public List<Carnivore> Carnivores { get; set; }
        public double Food { get; set; }
        public int NumberOfHerbivores => Herbivores.Count();
        public int NumberOfCarnivores => Carnivores.Count();
        public int TotalIndividuals => NumberOfCarnivores + NumberOfHerbivores;
        public double CarnivoreFood => Herbivores.Select(i => i.Weight).Sum();

        // Constructor & Overloads

        public Enviroment(Position pos, Random rng, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null)
        {
            Rng = rng;
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
            List<Herbivore> newbornHerbivores = new List<Herbivore>();
            List<Carnivore> newbornCarnivores = new List<Carnivore>();
            Herbivores = Herbivores.OrderBy(i => i.Fitness).ToList();
            foreach (var herb in Herbivores)
            {
                var result = herb.Birth(Herbivores.Count());
                if (!(result is null))
                {
                    newbornHerbivores.Add((Herbivore)result);
                }
            }
            foreach (var child in newbornHerbivores)
            {
                Herbivores.Add(child);
            }


            Carnivores = Carnivores.OrderBy(i => i.Fitness).ToList();
            foreach (var carn in Carnivores)
            {
                var result = carn.Birth(Carnivores.Count());
                if (!(result is null)) 
                {
                    newbornCarnivores.Add((Carnivore)result);
                }
            }
            foreach (var child in newbornCarnivores)
            {
                Carnivores.Add(child);
            }
            Console.WriteLine($"births: {newbornHerbivores.Count()}\t{newbornCarnivores.Count()}");
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
            List<Herbivore> survivingHerbivores = new List<Herbivore>();
            foreach (var herb in Herbivores)
            {
                if (herb.IsAlive)
                {
                    survivingHerbivores.Add(herb);
                }
            }

            List<Carnivore> survivingCarnivores = new List<Carnivore>();
            foreach (var carn in Carnivores)
            {
                if (carn.IsAlive)
                {
                    survivingCarnivores.Add(carn);
                }
            }
            Herbivores = survivingHerbivores;
            Carnivores = survivingCarnivores;
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

        public void DEBUG_OneCycle()
        {// ResetMigration must be done by Simulation Object
            GrowFood();
            BirthCycle();
            //Migration
            AgeCycle();
            WeightLossCycle();
            DeathCycle();
            RemoveDeadIndividuals();
            ResetGivenBirthParameter();
        }

        // Methods
    }

    public class Desert : Enviroment
    {

        // Fields

        // Properties {get; set;}

        // Constructor & Overloads

        public Desert(Position pos, Random rng, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null) : base(pos, rng, initialHerbivores, initialCarnivores)
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

        public Ocean(Position pos, Random rng) : base(pos, rng)
        {

        }

        // Methods
    }

    public class Mountain : Enviroment
    {

        // Fields

        // Properties {get; set;}

        // Constructor & Overloads

        public Mountain(Position pos, Random rng) : base(pos, rng)
        {

        }

        // Methods
    }

    public class Savannah : Enviroment
    {
        SavannahParams Params = new SavannahParams();
        public Savannah(Position pos, Random rng, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null) : base(pos, rng, initialHerbivores, initialCarnivores)
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
        public Jungle(Position pos, Random rng, List<Herbivore> initialHerbivores = null, List<Carnivore> initialCarnivores = null) : base(pos, rng, initialHerbivores, initialCarnivores)
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