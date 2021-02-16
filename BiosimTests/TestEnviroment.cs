using System;
using Xunit;
using Biosim.Land;
using Biosim.Animals;
using Biosim.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace BiosimTests
{
    public class TestEnviroment
    {
        public static Random rng = new Random();
        public static List<Carnivore> testCarns = Enumerable.Range(0, 100).Select(i => new Carnivore(rng)).ToList();
        public static List<Herbivore> testHerbs = Enumerable.Range(0, 100).Select(i => new Herbivore(rng)).ToList();
        [Fact]
        public void MigrationResetTest()
        {
            var env = new Jungle(new Position() { x=5, y=5}, rng);
            env.Herbivores = Enumerable.Range(0, 10).Select(i => new Herbivore(rng)).ToList();
            env.Herbivores[0].Migrated = true;
            env.Herbivores[4].Migrated = true;
            env.Herbivores[6].Migrated = true;
            int migrated = env.Herbivores.Where(i => i.Migrated).Count();
            env.ResetMigrationParameter();
            Assert.NotEqual(migrated, env.Herbivores.Where(i => i.Migrated).Count());
        }
    }
}
