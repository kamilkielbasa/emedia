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
            string originalFIle = "11k16bitpcm.wav";
            string encodedFile = "encodedWAVFile.wav";
            string decodedFile = "decodedWAVFile.wav";

            WAVFile wavFile = new WAVReader(path + originalFIle).ReadWAVFile(false);
            wavFile.DisplayHeader();

            WAVFile cipheredWavFile = wavFile;
            float[] encoded = Cipher.getCipheredData(cipheredWavFile.Data);
            Data dataModifiyer = new Data();
            cipheredWavFile.Data = dataModifiyer.Normalize(encoded);
            WAVWriter wavWriter = new WAVWriter(path + encodedFile);
            wavWriter.WriteWAVFile(cipheredWavFile);

            WAVFile decipheredWavFile = new WAVReader(path + encodedFile).ReadWAVFile(true);
            float[] decipheredFloats = dataModifiyer.Denormalize(decipheredWavFile.Data);
            decipheredWavFile.Data = Cipher.getDecipheredData(decipheredFloats);

            WAVWriter wavWriterForDecoded = new WAVWriter(path + decodedFile);
            wavWriterForDecoded.WriteWAVFile(decipheredWavFile);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
