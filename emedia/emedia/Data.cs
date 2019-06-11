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

        // konwersja zaszyfrowanych danych (tablica double'i) na (tablice byte'ów)
        // bez zmiany rozmiaru. Tak wiec na tym samym bloku pamieci zapisujemy tablice
        // double'i natomiast wymagana jest przez format WAVE tablica bajtow.
        public byte[] Normalize(double[] cipheredData)
        {
            List<byte[]> bytesList = new List<byte[]>();

            for (int i = 0; i < cipheredData.Length; ++i)
            {
                byte[] result = new byte[8];
                byte[] r = this.GetBytes(cipheredData[i].ToString());

                for (int j = 0; j < r.Length; ++j)
                {
                    result[j] = r[j];
                }

                bytesList.Add(result);
            }

            return bytesList.SelectMany(x => x).ToArray();
        }

        // metoda lustrzne odbicie powyzszej metoda.
        public double[] Denormalize(byte[] Data)
        {
            List<byte[]> cipheredBytes = new List<byte[]>();

            for (int i = 0; i + 7 < Data.Length; i += 8)
            {
                byte[] bytes = new byte[]
                {
                    Data[i + 0],
                    Data[i + 1],
                    Data[i + 2],
                    Data[i + 3],
                    Data[i + 4],
                    Data[i + 5],
                    Data[i + 6],
                    Data[i + 7],
                };

                cipheredBytes.Add(bytes);
            }

            List<double> floats = new List<double>();

            foreach (byte[] chunk in cipheredBytes)
            {
                floats.Add(BitConverter.ToUInt64(chunk, 0));
            }

            return floats.ToArray();
        }
    }
}
