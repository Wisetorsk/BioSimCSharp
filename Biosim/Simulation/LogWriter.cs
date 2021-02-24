using System.IO;

namespace Biosim.Simulation
{
    public class LogWriter
    {
        /*
         Collects all data and writes it to csv */
        public string FilePath { get; set; }
        private string _data;
        public LogWriter(string filepath, string header)
        {
            FilePath = filepath;
            _data += $"{header}\n";
        }

        public void Log(string line)
        {
            _data += $"{line}\n";
        }

        public void LogCSV()
        {
            using (TextWriter sw = new StreamWriter(FilePath))
            {
                foreach (var line in _data.Split('\n'))
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
