using Biosim.Parameters;

namespace Biosim.Animal
{
    public class Animal
    {

        // Fields
        private double weight;
        private int age;
        private Position pos = new Position(); // Should replace with faster way of holding key-value pairs
       

        // Properties
        public double Weight
        {
            get { return weight; }
            set { weight = (value < 0) ? Parameters.Parameters.BaseWeight : value; }
        }

        public int Age
        {
            get { return Age;  }
            set { age = (value < 0) ? 0 : value; }
        }

        public Position Pos
        {
            get { return pos;  }
            set { pos = value;  }
        }

        // Constructors & Overloads

        public Animal()
        {

        }

        // Methods


    }

    public class Herbivore : Animal
    {
        public Herbivore() : base()
        {

        }
    }

    public class Carnivore : Animal
    {
        public Carnivore() : base()
        {

        }
    }

}