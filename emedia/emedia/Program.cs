using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToWAVFile = @"C:\Users\kielbkam\Desktop\semestr 6\emedia\emedia\emedia\Yamaha-V50-Rock-Beat-120bpm.wav";

            WAVFile wavFile = new WAVReader(pathToWAVFile).ReadWAVFile();
            wavFile.DisplayHeader();
           
            Console.ReadKey();
        }
    }
}
