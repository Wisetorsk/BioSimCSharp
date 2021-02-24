using Biosim.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biosim.Animals
{
    public class Animal : IAnimal
    {

        // Fields
        //private double _fitness;
        private double weight;
        private Position pos = new Position(); // Should replace with faster way of holding key-value pairs
        internal Random rng;
        // Properties
        public double Weight
        {
            get { return weight; }
            set { weight = (value < 0) ? 0 : value; }
        }

        public int Age { get; set; }

        public Position Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        //public double Fitness { get { return Qneg * Qplus; } set { _fitness = value; } }
        public double Fitness => Qneg * Qplus;
        public virtual double Qplus { get; set; }
        public virtual double Qneg { get; set; }
        public virtual IAnimalParams Params { get; set; }
        public bool IsAlive { get; set; } = true;
        public bool GivenBirth { get; set; } = false;
        public bool Migrated { get; set; } = false;
        public Position GoingToMoveTo { get; set; }

        // Constructors & Overloads

        public Animal(Random _rng, Position pos)
        {
            rng = _rng;
            Age = 0;
            if (pos is null)
            {
                Pos = new Position { x = 0, y = 0 };
            }
            else
            {
                Pos = pos;
            }
            //Console.WriteLine(rng.NextDouble());
        }

        public void Migrate(List<Position> dir)
        {
            // The animal gets a set of directions it is allowed to move to (based on the passable attribute passed in from the main controller (island)
            if (Fitness * Params.Mu > rng.NextDouble())
            {
                Migrated = true;
                GoingToMoveTo = dir[rng.Next(dir.Count())];
            }
            else
            {
                GoingToMoveTo = new Position { x = Pos.x, y = Pos.y };
            }
        }


        public IAnimal Birth(int sameSpeciesInCell)
        {
            if (GivenBirth) return null;
            double probability;
            if (Weight < (Params.Zeta * (Params.BirthWeight + Params.BirthSigma)))
            {
                probability = 0.0;
            }
            else
            {
                probability = Params.Gamma * Fitness * (sameSpeciesInCell - 1);
                probability = (probability > 1) ? 1 : probability;
            }

            if (probability > rng.NextDouble())
            {
                IAnimal newborn;
                if (this.GetType().Name == "Herbivore")
                {
                    newborn = new Herbivore(rng, this.Pos);
                    newborn.Params.OverloadParameters(Params.CopyParameters()); // Inherit params from parent
                }
                else
                {
                    newborn = new Carnivore(rng, this.Pos);
                    newborn.Params.OverloadParameters(Params.CopyParameters()); // Inherit params from parent
                }
                double bWeight = newborn.Weight;
                if (bWeight >= Weight || bWeight <= 0) return null;
                Weight -= Params.Xi * bWeight;
                GivenBirth = true;
                return newborn;
            }
            return null;
        }

        public void Death()
        {
            IsAlive = (rng.NextDouble() > Params.Omega * (1 - Fitness));
        }

        public void UpdateWeight()
        {
            Weight -= Weight * Params.Eta;
        }

        public void GrowOlder()
        {
            Age += 1;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}\nAlive: {(IsAlive ? "Alive" : "Dead")}\nAge: {Age}\nWeight: {Weight}\nFitness: {Fitness}\n{this.Pos}";
        }

        public bool Kill()
        {
            if (IsAlive)
            {
                IsAlive = false;
                return true;
            }
            return false;
        }

        public void UpdateParameters()
        {
            // Animal can update it's own parameters based on external data and randomness. 
            throw new NotImplementedException();
        }
    }

    public class Herbivore : Animal, IAnimal
    {

        public override double Qplus => 1 / (1 + Math.Exp(Params.PhiAge * (Age - Params.AHalf)));
        public override double Qneg => 1 / (1 + Math.Exp(-Params.PhiWeight * (Weight - Params.WHalf)));
        public Herbivore(Random rng, Position pos = null, IAnimalParams customParameters = null) : base(rng, pos)
        {
            if (customParameters is null)
            {
                Params = new HerbivoreParams();
            }
            else
            {
                Params = customParameters;
            }
            var norm = new MathNet.Numerics.Distributions.Normal(Params.BirthWeight, Params.BirthSigma);
            Weight = norm.Sample();
        }

        public double Feed(double availableFood)
        {
            double willEat = (availableFood >= Params.F) ? Params.F : availableFood;
            double leftOver = availableFood - willEat;
            Weight += Params.Beta * willEat;
            return (leftOver < 0) ? 0 : leftOver;
        }
    }

    public class Carnivore : Animal, IAnimal
    {

        public override double Qplus => 1 / (1 + Math.Exp(Params.PhiAge * (Age - Params.AHalf)));
        public override double Qneg => 1 / (1 + Math.Exp(-Params.PhiWeight * (Weight - Params.WHalf)));
        public Carnivore(Random rng, Position pos = null, IAnimalParams customParameters = null) : base(rng, pos)
        {
            if (customParameters is null)
            {
                Params = new CarnivoreParams();
            }
            else
            {
                Params = customParameters;
            }
            var norm = new MathNet.Numerics.Distributions.Normal(Params.BirthWeight, Params.BirthSigma);
            Weight = norm.Sample();
        }

        public void FeedOld(List<Herbivore> herbivores)
        {
            // Go through all Herbivores one by one, killing to reach Params.F. 
            // Check if the herbivore is alive with herb.IsAlive
            double eaten = 0;
            foreach (var herb in herbivores)
            {
                if (eaten >= Params.F)
                {
                    Console.WriteLine("Carnivore has reached F eaten of H.Weight");
                    break; // Animal is full
                }
                if (!herb.IsAlive) continue; // Animal is already dead
                if (Fitness <= herb.Fitness) continue; // Cannot kill animal, try the next one
                if (0 < Fitness - herb.Fitness && Fitness - herb.Fitness < Params.DeltaPhiMax)
                { // Try to kill and eat
                    if ((Fitness - herb.Fitness) / Params.DeltaPhiMax > rng.NextDouble())
                    {
                        eaten += herb.Weight;
                        Weight += herb.Weight * Params.Beta;
                        herb.Kill();
                    }
                    else
                    {
                        continue; // Fails to kill animal, try the next one
                    }
                }
                else
                { // Kill it and eat
                    eaten += herb.Weight;
                    Weight += herb.Weight * Params.Beta;
                    herb.Kill();
                }


            }
        }

        public int Feed(List<Herbivore> herbs)
        {
            int killed = 0;
            double eaten = 0.0;
            foreach (var h in herbs)
            {
                if (eaten >= Params.F)
                {
                    //Carnivore is full, stop hunting
                    break;
                }
                if (!h.IsAlive) continue; // Animal is already dead
                if (Fitness < h.Fitness)
                {
                    //Herbivore has too high fitness to kill, go to next H
                    continue;
                }
                else if (0 < Fitness - h.Fitness && Fitness - h.Fitness < Params.DeltaPhiMax)
                {
                    // try to kill
                    if (rng.NextDouble() < (Fitness - h.Fitness) / Params.DeltaPhiMax)
                    {
                        // Animal is killed
                        eaten += KillHerbivore(h);
                        killed++;
                    }
                    else
                    {
                        // Kill fails
                        continue; // Redundant continue...
                    }
                }
                else
                {
                    // Carnivore will kill
                    eaten += KillHerbivore(h);
                    killed++;
                }
            }
            return killed;
        }

        public double KillHerbivore(Herbivore herb)
        {
            Weight += herb.Weight * Params.Beta;
            herb.Kill();
            return herb.Weight;
        }
    }

}