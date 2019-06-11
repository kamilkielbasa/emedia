using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    class WAVReader
    {
        private string pathToWAVFile { get; set; }

        public WAVReader(string pathToWAVFile)
        {
            this.pathToWAVFile = pathToWAVFile;
        }

        // odwrócenie endianess'ów
        private int ReverseEndianness(int value)
        {
            return BinaryPrimitives.ReverseEndianness(value);
        }

        private string ConvertIntToString(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return Encoding.UTF8.GetString(bytes);
        }

        public WAVFile ReadWAVFile(bool isEncoded)
        {
            WAVFile wavFile = new WAVFile();

            using (FileStream fs = new FileStream(this.pathToWAVFile, FileMode.Open))
            {
                BinaryReader binaryReader = new BinaryReader(fs);

                wavFile.ChunkId = ConvertIntToString(ReverseEndianness(binaryReader.ReadInt32()));
                wavFile.ChunkSize = binaryReader.ReadUInt32();
                wavFile.Format = ConvertIntToString(ReverseEndianness(binaryReader.ReadInt32()));
                wavFile.Subchunk1ID = ConvertIntToString(ReverseEndianness(binaryReader.ReadInt32()));
                wavFile.Subchunk1Size = binaryReader.ReadUInt32();
                wavFile.AudioFormat = binaryReader.ReadUInt16();
                wavFile.NumChannels = binaryReader.ReadUInt16();
                wavFile.SampleRate = binaryReader.ReadUInt32();
                wavFile.ByteRate = binaryReader.ReadUInt32();
                wavFile.BlockAlign = binaryReader.ReadInt16();
                wavFile.BitsPerSample = binaryReader.ReadUInt16();
                wavFile.Subchunk2ID = ConvertIntToString(ReverseEndianness(binaryReader.ReadInt32()));
                wavFile.Subchunk2Size = binaryReader.ReadUInt32();
                // jesli enkodujemy to musimy zwiekszyc rozmiar ramki danych samego pliku WAVE.
                // W naszym przypadku jest to double (8 bajtow) dlatego sam rozmiar mnozymi razy sizeof(double).
                wavFile.Data = isEncoded == true ?
                    binaryReader.ReadBytes((int)wavFile.Subchunk2Size * 8) :
                    binaryReader.ReadBytes((int)wavFile.Subchunk2Size);
            }

            return wavFile;
        }
    }
}
