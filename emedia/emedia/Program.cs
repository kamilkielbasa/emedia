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
            string path = @"C:\Users\kielbkam\Desktop\semestr 6\emedia\emedia\emedia\";
            string file1 = "Yamaha-V50-Rock-Beat-120bpm.wav";
            string file2 = "Yamaha-V50-Rock-Beat-120bpm2.wav";

            WAVFile wavFile = new WAVReader(path + file1).ReadWAVFile();
            wavFile.DisplayHeader();

            WAVWriter wavWriter = new WAVWriter(path + file2);
            wavWriter.WriteWAVFile(wavFile);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
