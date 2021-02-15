using Biosim.Land;
using Biosim.Parameters;
using System.Collections.Generic;

namespace Biosim.Simulation
{
    internal interface ISimulation
    {
        List<IEnviroment> Island { get; set; }
        string TemplateString { get; set; }
        List<List<IEnviroment>> Land { get; set; } // Nested List
        Position DefaultDim { get; set; }
        int YearsToSimulate { get; set; }

        void OneYear();
        void Build();
        void Migrate();
        void Death();
        void Feed();
        void Age();
        void RemoveDead();
        void Breed();
        void ResetSeasonalParams();
        void Simulate();
        void DisplayIslandString();
    }
}