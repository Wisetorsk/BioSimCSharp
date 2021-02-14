namespace Biosim.Parameters
{

    public class HerbivoreParams : IAnimalParams
    {
        public double BirthWeight { get; set; } = 8.0;
        public double BirthSigma { get; set; } = 1.5;
        public double Beta { get; set; } = 0.9;
        public double Eta { get; set; } = 0.05;
        public double AHalf { get; set; } = 40.0;
        public double PhiAge { get; set; } = 0.2;
        public double WHalf { get; set; } = 10.0;
        public double PhiWeight { get; set; } = 0.1;
        public double Mu { get; set; } = 0.25;
        public double Gamma { get; set; } = 0.2;
        public double Zeta { get; set; } = 3.5;
        public double Xi { get; set; } = 1.2;
        public double Omega { get; set; } = 0.4;
        public double F { get; set; } = 10.0;
    }

    public class CarnivoreParams : IAnimalParams
    {
        public double BirthWeight { get; set; } = 6.0;
        public double BirthSigma { get; set; } = 1;
        public double Beta { get; set; } = 0.75;
        public double Eta { get; set; } = 0.125;
        public double AHalf { get; set; } = 60.0;
        public double PhiAge { get; set; } = 0.4;
        public double WHalf { get; set; } = 4.0;
        public double PhiWeight { get; set; } = 0.4;
        public double Mu { get; set; } = 0.4;
        public double Gamma { get; set; } = 0.8;
        public double Zeta { get; set; } = 3.5;
        public double Xi { get; set; } = 1.1;
        public double Omega { get; set; } = 0.9;
        public double F { get; set; } = 50.0;
        public double DeltaPhiMax { get; set; } = 10.0;
    }

    public class JungleParams
    {
        public  double Fmax = 800;
    }

    public class SavannahParams
    {
        public double Fmax = 300;
        public double Alpha = 0.3;
    }
}