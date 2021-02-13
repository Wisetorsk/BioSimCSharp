using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosim.Parameters
{
    public interface IAnimalParams
    {
        double Mu { get; set; }
        double Omega { get; set; }
        double Eta { get; set; }
    }
}
