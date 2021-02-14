using Biosim.Parameters;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biosim.Animals
{
    public class Animal : IAnimal
    {

        // Fields
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

        public double Fitness => Qneg * Qplus;
        public virtual double Qplus { get; set; }
        public virtual double Qneg { get; set; }
        public virtual IAnimalParams Params { get; set; }
        public bool IsAlive { get; set; } = true;
        public bool GivenBirth { get; set; } = false;

        // Constructors & Overloads

        public Animal()
        {
            rng = new Random();
            Age = 0;
        }

        public Directions? Migrate(Directions[] dir)
        {
            // The animal gets a set of directions it is allowed to move to (based on the passable attribute passed in from the main controller (island)
            if (this.Fitness * Params.Mu > rng.NextDouble())
            {
                return dir[rng.Next(dir.Length)];
            }
            else
            {
                return null;
            }
        }


        public Animal Birth(int sameSpeciesInCell)
        {
            if (GivenBirth) return null;
            double probability;
            if (Weight < (Params.Zeta * (Params.BirthWeight + Params.BirthSigma)))
            {
                probability = 0.0;
            }else
            {
                probability = Params.Gamma * Fitness * (sameSpeciesInCell - 1);
                probability = (probability > 1) ? 1 : probability;
            }

            if (probability > rng.NextDouble())
            {
                Animal newborn;
                if (this.GetType().Name == "Herbivore")
                {
                    newborn = new Herbivore();
                } else
                {
                    newborn = new Carnivore();
                }
                double bWeight = newborn.Weight;
                if (bWeight >= Weight || bWeight <= 0) return null;
                Weight -= Params.Xi * bWeight;
                GivenBirth = true;
                return newborn;
            }
            return null;
        }

        public bool Death()
        {
            return (rng.NextDouble() < Params.Omega * (1 - Fitness));
        }

        public void UpdateWeight()
        {
            Weight *= Params.Eta;
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
    }

    public class Herbivore : Animal
    {
        public new HerbivoreParams Params = new HerbivoreParams();
        public override double Qplus => 1 / (1 + Math.Exp(Params.PhiAge * (Age - Params.AHalf)));
        public override double Qneg => 1 / (1 + Math.Exp(-Params.PhiWeight * (Weight - Params.WHalf)));
        public Herbivore() : base()
        {
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

    public class Carnivore : Animal
    {
        public new CarnivoreParams Params = new CarnivoreParams();
        public override double Qplus => 1 / (1 + Math.Exp(Params.PhiAge * (Age - Params.AHalf)));
        public override double Qneg => 1 / (1 + Math.Exp(-Params.PhiWeight * (Weight - Params.WHalf)));
        public Carnivore() : base()
        {
            var norm = new MathNet.Numerics.Distributions.Normal(Params.BirthWeight, Params.BirthSigma);
            Weight = norm.Sample();
        }

        public bool Feed(List<Herbivore> herbivores)
        {
            // Go through all Herbivores one by one, killing to reach Params.F. 
            // Check if the herbivore is alive with herb.IsAlive
            double eaten = 0;
            foreach (var herb in herbivores)
            {
                if (eaten >= Params.F) return true; // Animal is full
                if (Fitness <= herb.Fitness) continue; // Cannot kill animal, try the next one
                if (0 < Fitness - herb.Fitness && Fitness - herb.Fitness < Params.DeltaPhiMax)
                { // Try to kill and eat
                    if ((Fitness - herb.Fitness)/Params.DeltaPhiMax > rng.NextDouble())
                    {
                        eaten += herb.Weight;
                        herb.IsAlive = false;
                    } else
                    {
                        continue; // Fails to kill animal, try the next one
                    }
                } else
                { // Kill it and eat
                    eaten += herb.Weight;
                    herb.IsAlive = false;
                }
            }
            return false; // Animal is still hungry
        }
    }

}