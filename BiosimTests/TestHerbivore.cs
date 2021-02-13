﻿using System;
using Xunit;
using Biosim.Animals;

namespace BiosimTests
{
    public class TestHerbivore
    {
        [Fact]
        public void HerbivoreFeedReducesAmountTest()
        {
            double initialAmount = 100.0;
            Herbivore testHerb = new Herbivore();
            double remainder = testHerb.Feed(initialAmount);
            Assert.NotEqual(initialAmount, remainder);
            Assert.Equal(initialAmount, remainder + testHerb.Params.F);
        }

        [Fact]
        public void HerbivoreFeedIncreasesWeightTest()
        {
            Herbivore testHerb = new Herbivore();
            double initialWeight = testHerb.Weight;
            testHerb.Feed(100.0);
            double endWeight = testHerb.Weight;
            Assert.True(initialWeight < endWeight);
        }

        [Fact]
        public void HerbivoreFeedWhenLessThanFTest()
        {
            Herbivore testHerb = new Herbivore();
            double F = testHerb.Params.F;
            double available = F - 5.0;
            double remainder = testHerb.Feed(available);
            Assert.Equal(0, remainder);
        }

        [Fact]
        public void HerbivoreFeedZeroAvailableTest()
        {
            Herbivore testHerb = new Herbivore();
            double initialWeight = testHerb.Weight;
            double initalAmount = 0.0;
            double remainder = testHerb.Feed(initalAmount);
            double endWeight = testHerb.Weight;
            Assert.Equal(0, remainder);
            Assert.Equal(initialWeight, endWeight);
        }

        [Fact]
        public void HerbivoreMoveTest()
        {
            // Set Mu parameter so that berbiovre must move. 
            Herbivore testHerb = new Herbivore();
        }
    }
}
