using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    // klasa ta jest jedynie wrapper'em ktora posiada obiekty dzieki ktorym moze:
    public class Cipher
    {
        // genreowac duze liczby pierwsze
        public PrimeNumberGenerator generator = new PrimeNumberGenerator(16, 3);
        // kodowac te liczby pierwsze
        public RSA rsa;

        public Cipher()
        {
            rsa = new RSA(generator.GenerateNumber(), generator.GenerateNumber());
        }

        public double[] getCipheredData(byte[] Data)
        {
            return rsa.GetCipheredValue(Data);
        }

        public byte[] getDecipheredData(double[] Data)
        {
            return rsa.GetDecipheredValue(Data);
        }
    }
}
