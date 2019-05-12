using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    public class Cipher
    {
        public static float[] getCipheredData(byte[] Data)
        {
            RSA rsa = new RSA();
            return rsa.GetCipheredValue(Data);
        }

        public static byte[] getDecipheredData(float[] Data)
        {
            RSA rsa = new RSA();
            return rsa.GetDecipheredValue(Data);
        }
    }
}
