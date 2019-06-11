using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    public class PrimeNumberGenerator
    {
        public ulong numberOfBits;
        public int numberOfRabinMillerTests;

        public PrimeNumberGenerator(ulong numberOfBits, int numberOfRabinMillerTests)
        {
            this.numberOfBits = numberOfBits;
            this.numberOfRabinMillerTests = numberOfRabinMillerTests;
        }

        // metoda ta losuje duza liczbe
        public BigInteger GenerateNumber()
        {
            BigInteger output = new BigInteger();
            output = 0;

            // losuj liczba do póki nie bedzie nieparzysta
            while (output % 2 == 0)
            {
                byte[] key = new byte[numberOfBits / 8];
                Random random = new Random();
                random.NextBytes(key);
                output = new BigInteger(key);
                output = BigInteger.Abs(output);
            }

            return output;
        }

        public static BigInteger RandomIntegerBelow(BigInteger N)
        {
            byte[] bytes = N.ToByteArray();
            BigInteger R;
            Random random = new Random();

            do
            {
                random.NextBytes(bytes);
                //bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger(bytes);
                R = BigInteger.Abs(R);
            } while (R >= N);

            Console.WriteLine("{0} < {1}", R, N);

            return R;
        }

        // test Miller'a-Rabin'a.
        public bool IsProbablePrime(BigInteger source, int certainty)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            // There is no built-in method for generating random BigInteger values.
            // Instead, random BigIntegers are constructed from randomly generated
            // byte arrays of the same length as the source.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    // This may raise an exception in Mono 2.10.8 and earlier.
                    // http://bugzilla.xamarin.com/show_bug.cgi?id=2761
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }

        // metoda ta zwraca duza liczbe pierwsza z dokladnoscia do liczby testow Miller'a-Rabin'a.
        public BigInteger Execute()
        {
            bool status = false;
            BigInteger output = 0;

            while (status == false)
            {
                output = GenerateNumber();
                status = IsProbablePrime(output, numberOfRabinMillerTests);
            }

            return output;
        }
    }
}
