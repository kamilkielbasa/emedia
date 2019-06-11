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
            string originalFIle = "Yamaha-V50-Rock-Beat-120bpm.wav";
            string encodedFile = "encodedWAVFile.wav";
            string decodedFile = "decodedWAVFile.wav";

            WAVFile wavFile = new WAVReader(path + originalFIle).ReadWAVFile(false);
            wavFile.DisplayHeader();

            WAVFile cipheredWavFile = wavFile;
            Cipher cipher = new Cipher();
            double[] encoded = cipher.getCipheredData(cipheredWavFile.Data);
            Data dataModifiyer = new Data();
            cipheredWavFile.Data = dataModifiyer.Normalize(encoded);
            WAVWriter wavWriter = new WAVWriter(path + encodedFile);
            wavWriter.WriteWAVFile(cipheredWavFile);

            WAVFile decipheredWavFile = new WAVReader(path + encodedFile).ReadWAVFile(true);
            double[] decipheredFloats = dataModifiyer.Denormalize(decipheredWavFile.Data);
            decipheredWavFile.Data = cipher.getDecipheredData(decipheredFloats);

            WAVWriter wavWriterForDecoded = new WAVWriter(path + decodedFile);
            wavWriterForDecoded.WriteWAVFile(decipheredWavFile);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
