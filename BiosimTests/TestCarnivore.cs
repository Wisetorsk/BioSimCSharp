using System;
using Xunit;
using Biosim.Animals;
using System.Collections.Generic;
using System.Linq;

namespace BiosimTests
{
    public class TestCarnivore
    {
        [Fact]
        public void CarnivoreFeedingKillTest()
        {
            var rng = new Random();
            var testCarn = new Carnivore(rng) { Age = 5, Weight = 10 }; // Healthy Carnivore
            var herbs = new List<Herbivore>();
            for (int i = 0; i < 10; i++)
            {
                herbs.Add(new Herbivore(rng){ Age = 90, Weight = 5}); // Add ten old herbivores
            }
            var initialW = testCarn.Weight;
            testCarn.Params.F = 10.0; // Set "Hunger" to two herbivores
            testCarn.Feed(herbs);
            Assert.True(herbs.Where(i => !i.IsAlive).Count() > 0); // Check that some herbvores have been killed
            //Assert.True(initialW < testCarn.Weight);
        }


        [Fact]
        public void CarnivoreFeedingInceaseWeightTest()
        {
            var rng = new Random();
            var testCarn = new Carnivore(rng) { Age = 5, Weight = 10}; // Healthy Carnivore
            var herbs = new List<Herbivore>();
            for (int i = 0; i < 10; i++)
            {
                herbs.Add(new Herbivore(rng) { Age = 90, Weight = 5 }); // Add ten old herbivores
            }
            var initialW = testCarn.Weight;
            testCarn.Params.F = 10.0; // Set "Hunger" to two herbivores
            testCarn.Feed(herbs);
            var endW = testCarn.Weight;
            Assert.True(initialW < endW);
        }

    }
}
