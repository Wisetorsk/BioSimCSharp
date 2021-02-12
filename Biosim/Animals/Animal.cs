using Biosim.Parameters;
using MathNet.Numerics;
using System;
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

        // Constructors & Overloads

        public Animal()
        {
            rng = new Random();
            Age = 0;
        }


        public Animal Birth(int sameSpeciesInCell)
        {
            throw new NotImplementedException();
        }

        public bool Death()
        {
            throw new NotImplementedException();
        }

        public void UpdateFitness()
        {
            throw new NotImplementedException();
        }

        public void UpdateWeight()
        {
            throw new NotImplementedException();
        }

        public void GrowOlder()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}\nAge: {Age}\nWeight: {Weight}\nFitness: {Fitness}\n{this.Pos}";
        }
    }

    public class Herbivore : Animal
    {
        public override double Qplus => 1 / (1 + Math.Exp(HerbivoreParams.PhiAge * (Age - HerbivoreParams.AHalf)));
        public override double Qneg => 1 / (1 + Math.Exp(-HerbivoreParams.PhiWeight * (Weight - HerbivoreParams.WHalf)));
        public Herbivore() : base()
        {
            var norm = new MathNet.Numerics.Distributions.Normal(HerbivoreParams.BirthWeight, HerbivoreParams.BirthSigma);
            Weight = norm.Sample();
        }

        double Feed(double availableFood)
        {
            double leftOver = availableFood - HerbivoreParams.F;
            return (leftOver < 0) ? 0 : leftOver;
        }
    }

    public class Carnivore : Animal
    {
        public override double Qplus => 1 / (1 + Math.Exp(CarnivoreParams.PhiAge * (Age - CarnivoreParams.AHalf)));
        public override double Qneg => 1 / (1 + Math.Exp(-CarnivoreParams.PhiWeight * (Weight - CarnivoreParams.WHalf)));
        public Carnivore() : base()
        {
            var norm = new MathNet.Numerics.Distributions.Normal(CarnivoreParams.BirthWeight, CarnivoreParams.BirthSigma);
            Weight = norm.Sample();
        }

        public Directions ?Migrate(Directions[] dir)
        {
            if (this.Fitness * CarnivoreParams.Mu > rng.NextDouble())
            {
                return dir[rng.Next(dir.Length)];
            } else
            {
                return null;
            }
        }

        bool Feed()
        {
            return true;
        }
    }

}