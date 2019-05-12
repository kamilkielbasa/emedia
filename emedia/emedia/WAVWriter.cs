using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    public class WAVWriter
    {
        private string pathToWAVFile { get; set; }

        public WAVWriter(string pathToWAVFile)
        {
            this.pathToWAVFile = pathToWAVFile;
        }

        private int ReverseEndianness(int value)
        {
            return BinaryPrimitives.ReverseEndianness(value);
        }

        private int ConvertStringToInt(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public void WriteWAVFile(WAVFile wavFile)
        {
            using (FileStream fs = new FileStream(this.pathToWAVFile, FileMode.OpenOrCreate))
            {
                BinaryWriter binaryWriter = new BinaryWriter(fs);

                binaryWriter.Write(ReverseEndianness(ConvertStringToInt(wavFile.ChunkId)));
                binaryWriter.Write(wavFile.ChunkSize);
                binaryWriter.Write(ReverseEndianness(ConvertStringToInt(wavFile.Format)));
                binaryWriter.Write(ReverseEndianness(ConvertStringToInt(wavFile.Subchunk1ID)));
                binaryWriter.Write(wavFile.Subchunk1Size);
                binaryWriter.Write((UInt16)wavFile.AudioFormat);
                binaryWriter.Write((UInt16)wavFile.NumChannels);
                binaryWriter.Write(wavFile.SampleRate);
                binaryWriter.Write(wavFile.ByteRate);
                binaryWriter.Write((Int16)wavFile.BlockAlign);
                binaryWriter.Write((UInt16)wavFile.BitsPerSample);
                binaryWriter.Write(ReverseEndianness(ConvertStringToInt(wavFile.Subchunk2ID)));
                binaryWriter.Write(wavFile.Subchunk2Size);
                binaryWriter.Write(wavFile.Data);
            }
        }
    }
}
