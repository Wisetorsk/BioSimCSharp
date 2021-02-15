using Biosim.Land;
using Biosim.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosim.Simulation
{
    public class Simulation : ISimulation

    {
        public Simulation(int yearsToSimulate = 100)
        {
            YearsToSimulate = yearsToSimulate;
        }

        public int YearsToSimulate { get; set; }
        public List<IEnviroment> Island { get; set; }
        public string TemplateString { get; set; }
        public List<List<IEnviroment>> Land { get; set; }
        public Position DefaultDim { get; set; } = new Position { x = 10, y = 10 };

        public void Age()
        {
            foreach (var row in Land)
            {
                foreach (var cell in row)
                {
                    cell.AgeCycle();
                }
            }
        }

        public void Breed()
        {
            throw new NotImplementedException();
        }

        public void Build()
        {
            throw new NotImplementedException();
        }

        public void Death()
        {
            throw new NotImplementedException();
        }

        public void DisplayIslandString()
        {
            throw new NotImplementedException();
        }

        public void Feed()
        {
            throw new NotImplementedException();
        }

        public void Migrate()
        {
            throw new NotImplementedException();
        }

        public void OneYear()
        {
            throw new NotImplementedException();
        }

        public void RemoveDead()
        {
            throw new NotImplementedException();
        }

        public void ResetSeasonalParams()
        {
            throw new NotImplementedException();
        }

        public void Simulate()
        {
            throw new NotImplementedException();
        }
    }
}
