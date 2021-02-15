using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosim.Parameters
{
    public interface IAnimalParams
    {
        double BirthWeight { get; set; }
        double BirthSigma { get; set; }
        double Beta { get; set; }
        double Eta { get; set; }
        double AHalf { get; set; }
        double PhiAge { get; set; }
        double WHalf { get; set; }
        double PhiWeight { get; set; }
        double Mu { get; set; }
        double Gamma { get; set; }
        double Zeta { get; set; }
        double Xi { get; set; }
        double Omega { get; set; } // Passive chance of death (1 - omega) 
        double F { get; set; }
        double DeltaPhiMax { get; set; }
    }
}
