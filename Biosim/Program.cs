using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biosim.Animal;
using Biosim.Enviroment;
using Biosim.Parameters;

namespace Biosim
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal.Animal test1 = new Animal.Animal();
            test1.Pos.x = 100;
            Console.WriteLine(test1.Pos.x);

            Herbivore testHerbivore = new Herbivore();
            testHerbivore.Pos.y = 123;
            Console.WriteLine(testHerbivore.Pos.y);


            Console.ReadLine(); // Halts end of exec

        }
    }
}
