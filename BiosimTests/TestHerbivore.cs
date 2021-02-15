using System;
using Xunit;
using Biosim.Animals;
using Biosim.Parameters;

namespace BiosimTests
{
    public class TestHerbivore
    {
        [Fact]
        public void HerbivoreFeedReducesAmountTest()
        {
            double initialAmount = 100.0;
            Herbivore testHerb = new Herbivore(new Random());
            double remainder = testHerb.Feed(initialAmount);
            Assert.NotEqual(initialAmount, remainder);
            Assert.Equal(initialAmount, remainder + testHerb.Params.F);
        }

        [Fact]
        public void HerbivoreFeedIncreasesWeightTest()
        {
            Herbivore testHerb = new Herbivore(new Random());
            double initialWeight = testHerb.Weight;
            testHerb.Feed(100.0);
            double endWeight = testHerb.Weight;
            Assert.True(initialWeight < endWeight);
        }

        [Fact]
        public void HerbivoreFeedWhenLessThanFTest()
        {
            Herbivore testHerb = new Herbivore(new Random());
            double F = testHerb.Params.F;
            double available = F - 5.0;
            double remainder = testHerb.Feed(available);
            Assert.Equal(0, remainder);
        }

        [Fact]
        public void HerbivoreFeedZeroAvailableTest()
        {
            Herbivore testHerb = new Herbivore(new Random());
            double initialWeight = testHerb.Weight;
            double initalAmount = 0.0;
            double remainder = testHerb.Feed(initalAmount);
            double endWeight = testHerb.Weight;
            Assert.Equal(0, remainder);
            Assert.Equal(initialWeight, endWeight);
        }

        [Fact] //fct shortcut
        public void HerbivoreGiveBirthTest()
        {
            //Assert.method(logical);
        }

        [Fact]
        public void HerbivoreMoveTest()
        {
            // Set Mu parameter so that berbiovre must move. 
            Directions[] allDir = new Directions[] { Directions.Up, Directions.Left, Directions.Right, Directions.Down};
            Directions[] upDown = new Directions[] { Directions.Up, Directions.Down };
            Directions[] leftRight = new Directions[] { Directions.Left, Directions.Right };
            Directions[] onlyOne = new Directions[] { Directions.Left };
            Herbivore testHerb = new Herbivore(new Random()) { Weight=150, Age=5 };
            testHerb.Migrate(allDir);
        }

        [Fact]
        public void SetWeightTest()
        {
            Herbivore testHerb = new Herbivore(new Random()) { Weight = 10 };
            Assert.Equal(10, testHerb.Weight);
        }
    }
}
