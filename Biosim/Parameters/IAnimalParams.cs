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
        double Mu { get; set; }
        double Omega { get; set; }
        double Eta { get; set; }
        double Zeta { get; set; }
        double PhiWeight { get; set; }
        double Gamma { get; set; }
        double Xi { get; set; }
    }
}
