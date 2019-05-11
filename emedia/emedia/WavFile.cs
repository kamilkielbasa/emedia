using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    public class WAVFile
    {
        public string ChunkId { get; set; }
        public UInt32 ChunkSize { get; set; }
        public string Format { get; set; }
        public string Subchunk1ID { get; set; }
        public UInt32 Subchunk1Size { get; set; }
        public UInt16 AudioFormat { get; set; }
        public UInt16 NumChannels { get; set; }
        public UInt32 SampleRate { get; set; }
        public UInt32 ByteRate { get; set; }
        public Int16  BlockAlign { get; set; }
        public UInt16 BitsPerSample { get; set; }
        public string Subchunk2ID { get; set; }
        public UInt32 Subchunk2Size { get; set; }
        public byte[] Data { get; set; }

        public void DisplayHeader()
        {
            Console.WriteLine("ChunkId = {0}", ChunkId);
            Console.WriteLine("ChunkSize = {0}", ChunkSize);
            Console.WriteLine("Format = {0}", Format);
            Console.WriteLine("Subchunk1ID = {0}", Subchunk1ID);
            Console.WriteLine("Subchunk1Size = {0}", Subchunk1Size);
            Console.WriteLine("AudioFormat = {0}", AudioFormat);
            Console.WriteLine("NumChannels = {0}", NumChannels);
            Console.WriteLine("SampleRate = {0}", SampleRate);
            Console.WriteLine("ByteRate = {0}", ByteRate);
            Console.WriteLine("BlockAlign = {0}", BlockAlign);
            Console.WriteLine("BitsPerSample = {0}", BitsPerSample);
            Console.WriteLine("Subchunk2ID = {0}", Subchunk2ID);
            Console.WriteLine("Subchunk2Size = {0}", Subchunk2Size);
        }
    }
}
