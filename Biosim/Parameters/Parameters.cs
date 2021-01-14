namespace Biosim.Parameters
{
    public  class UniversalParameters
    {
        public static double BirthWeight = 5;
        public static double BirthSigma = 1;
        public static double Beta;
        public static double Eta;
        public static double AHalf;
        public static double PhiAge;
        public static double WHalf;
        public static double PhiWeight;
        public static double Mu;
        public static double Gamma;
        public static double Zeta = 3.5;
        public static double Xi;
        public static double Omega;
        public static double F;
        //public static double DeltaPhiMax;
    }

    public class HerbivoreParams : UniversalParameters
    {
        public new static double BirthWeight = 8.0;
        public new static double BirthSigma = 1.5;
        public new static double Beta = 0.9;
        public new static double Eta = 0.05;
        public new static double AHalf = 40.0;
        public new static double PhiAge = 0.2;
        public new static double WHalf = 10.0;
        public new static double PhiWeight = 0.1;
        public new static double Mu = 0.25;
        public new static double Gamma = 0.2;
        public new static double Xi = 1.2;
        public new static double Omega = 0.4;
        public new static double F = 10.0;
        //public new static double DeltaPhiMax = 0;
    }

    public class CarnivoreParams : UniversalParameters
    {
        public new static double BirthWeight = 6.0;
        public new static double BirthSigma = 1;
        public new static double Beta;
        public new static double Eta;
        public new static double AHalf;
        public new static double PhiAge;
        public new static double WHalf;
        public new static double PhiWeight;
        public new static double Mu;
        public new static double Gamma;
        public new static double Xi;
        public new static double Omega;
        public new static double F;
        public static double DeltaPhiMax;
    }
}