namespace Biosim.Parameters
{
    public class HerbivoreParams
    {
        public static double BirthWeight = 8.0;
        public static double BirthSigma = 1.5;
        public static double Beta = 0.9;
        public static double Eta = 0.05;
        public static double AHalf = 40.0;
        public static double PhiAge = 0.2;
        public static double WHalf = 10.0;
        public static double PhiWeight = 0.1;
        public static double Mu = 0.25;
        public static double Gamma = 0.2;
        public static double Xi = 1.2;
        public static double Omega = 0.4;
        public static double F = 10.0;
    }

    public class CarnivoreParams
    {
        public static double BirthWeight = 6.0;
        public static double BirthSigma = 1;
        public static double Beta = 0.75;
        public static double Eta = 0.125;
        public static double AHalf = 60.0;
        public static double PhiAge = 0.4;
        public static double WHalf = 4.0;
        public static double PhiWeight = 0.4;
        public static double Mu = 0.4;
        public static double Gamma = 0.8;
        public static double Xi = 1.1;
        public static double Omega = 0.9;
        public static double F = 50.0;
        public static double DeltaPhiMax = 10.0;
    }

    public class JungleParams
    {
        public static double Fmax = 800;
    }

    public class SavannahParams
    {
        public static double Fmax = 300;
        public static double Alpha = 0.3;
    }
}