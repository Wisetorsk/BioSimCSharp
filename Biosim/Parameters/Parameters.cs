using System;

namespace Biosim.Parameters
{

    public class ParameterBase : IAnimalParams
    {
        public virtual double BirthWeight { get; set; }
        public virtual double BirthSigma { get; set; }
        public virtual double Beta { get; set; }
        public virtual double Eta { get; set; }
        public virtual double AHalf { get; set; }
        public virtual double PhiAge { get; set; }
        public virtual double WHalf { get; set; }
        public virtual double PhiWeight { get; set; }
        public virtual double Mu { get; set; }
        public virtual double Gamma { get; set; }
        public virtual double Zeta { get; set; }
        public virtual double Xi { get; set; }
        public virtual double Omega { get; set; }
        public virtual double F { get; set; }
        public virtual double DeltaPhiMax { get; set; }

        public void OverloadParameters(double bWeight, double bSigma, double beta, double eta, double aHalf, 
            double phiAge, double wHalf, double phiWeight, double mu, double gamma, double zeta,
            double xi, double omega, double f, double deltaPhiMax)
        {
            BirthWeight = bWeight;
            BirthSigma = bSigma;
            Beta = beta;
            Eta = eta;
            AHalf = aHalf;
            PhiAge = phiAge;
            WHalf = wHalf;
            PhiWeight = phiWeight;
            Mu = mu;
            Gamma = gamma;
            Zeta = zeta;
            Xi = xi;
            Omega = omega;
            F = f;
            DeltaPhiMax = deltaPhiMax;
        }

    }

    public class HerbivoreParams : ParameterBase , IAnimalParams
    {
        public override double BirthWeight { get; set; } = 8.0;
        public override double BirthSigma { get; set; } = 1.5;
        public override double Beta { get; set; } = 0.9;
        public override double Eta { get; set; } = 0.05;
        public override double AHalf { get; set; } = 40.0;
        public override double PhiAge { get; set; } = 0.2;
        public override double WHalf { get; set; } = 10.0;
        public override double PhiWeight { get; set; } = 0.1;
        public override double Mu { get; set; } = 0.25;
        public override double Gamma { get; set; } = 0.2;
        public override double Zeta { get; set; } = 3.5;
        public override double Xi { get; set; } = 1.2;
        public override double Omega { get; set; } = 0.4;
        public override double F { get; set; } = 10.0;
        public override double DeltaPhiMax { get => throw new Exception("NOT AVAILABLE FOR HERBIVORES"); set => throw new Exception("NOT AVAILABLE FOR HERBIVORES"); }
    }

    public class CarnivoreParams : ParameterBase, IAnimalParams
    {
        public override double BirthWeight { get; set; } = 6.0;
        public override double BirthSigma { get; set; } = 1;
        public override double Beta { get; set; } = 0.75;
        public override double Eta { get; set; } = 0.125;
        public override double AHalf { get; set; } = 60.0;
        public override double PhiAge { get; set; } = 0.4;
        public override double WHalf { get; set; } = 4.0;
        public override double PhiWeight { get; set; } = 0.4;
        public override double Mu { get; set; } = 0.4;
        public override double Gamma { get; set; } = 0.8;
        public override double Zeta { get; set; } = 3.5;
        public override double Xi { get; set; } = 1.1;
        public override double Omega { get; set; } = 0.9;
        public override double F { get; set; } = 50.0;
        public override double DeltaPhiMax { get; set; } = 0.5; //= 10.0;
    }

    public class EnvParams
    {
        public virtual double Fmax { get; set; }
        public virtual double Alpha { get; set; }
    }

    public class JungleParams : EnvParams
    {
        public override double Fmax { get; set; } = 800;
    }

    public class SavannahParams : EnvParams
    {
        public override double Fmax { get; set; } = 300;
        public override double Alpha { get; set; } = 0.3;
    }
}