using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    public class Data
    {
        public byte[] GetBytes(string str)
        {
            BigInteger number;
            return BigInteger.TryParse(str, out number) ? number.ToByteArray() : null;
        }

        public byte[] Normalize(float[] cipheredData)
        {
            List<byte[]> bytesList = new List<byte[]>();

            for (int i = 0; i < cipheredData.Length; ++i)
            {
                byte[] result = new byte[4];
                byte[] r = this.GetBytes(cipheredData[i].ToString());

                for (int j = 0; j < r.Length; ++j)
                {
                    result[j] = r[j];
                }

                bytesList.Add(result);
            }

            return bytesList.SelectMany(x => x).ToArray();
        }

        public float[] Denormalize(byte[] Data)
        {
            List<byte[]> cipheredBytes = new List<byte[]>();

            for (int i = 0; i + 3 < Data.Length; i += 4)
            {
                byte[] bytes = new byte[]
                {
                    Data[i + 0],
                    Data[i + 1],
                    Data[i + 2],
                    Data[i + 3],
                };

                cipheredBytes.Add(bytes);
            }

            List<float> floats = new List<float>();

            foreach (byte[] chunk in cipheredBytes)
            {
                floats.Add(BitConverter.ToUInt32(chunk, 0));
            }

            return floats.ToArray();
        }
    }
}
