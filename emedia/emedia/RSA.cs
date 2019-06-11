using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace emedia
{
    public class RSA
    {
        private readonly double p;// = 1123;
        private readonly double q;// = 1237;
        private readonly double e;// = 834781;
        private readonly double d;// = 1087477;
        private double n;

        public RSA(BigInteger p, BigInteger q)
        {
            BigInteger phi = EulerFunction(p, q);
            BigInteger modulus = p * q;

            // znajdz liczbe  taka ze NWD(e, phi) = 1 && 1 < e < modulus
            BigInteger e = 0;
            for (e = 3; GreatestCommonDivisor(e, phi) != 1; e += 2) ;

            // znajdz liczbe odwrotna do d tak a ze d * e % phi = 1
            BigInteger d = modInverse(e, phi);

            this.p = (double)p;
            this.q = (double)q;
            this.n = (double)modulus;
            this.e = (double)e;
            this.d = (double)d;
        }

        public BigInteger EulerFunction(BigInteger p, BigInteger q)          //euler function: f(n) = (p-1)(q-1)
        {
            return (p - 1) * (q - 1);
        }

        // algorytm Euklidesa
        public BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        // rozszerony algorytm Euklidesa
        public BigInteger modInverse(BigInteger a, BigInteger m)
        {
            a = a % m;
            for (BigInteger x = 1; x < m; ++x)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }

        // petla szyfrujaca. Na wejsciu otrzymuje wartosc oryginalna a takze potege.
        // Poniewaz potega moze byc zbyt duza, wiec aby mozna bylo w normalny sposob ja obliczyc.
        // Jednakze nas nie interesuje wartosc liczbowa potegi, a jedynie reszta z dzielenia jej przez this.n.
        // Dlatego mozemy rozlozyc potege na iloczyn skladnikow o wykladniach rownym kolejnym potega liczby dwa.
        // Np. 103 = 64 + 32 + 4 + 2 + 1
        //
        // Przyklad
        // 7103 mod 143 = 764 + 32 + 4 + 2 + 1 mod 143 =
        // (7^64 mod 143) × (7^32 mod 143) × (7^4 mod 143) × (7^2 mod 143) × 7^1 mod 143
        //
        // 7^1 mod 143 = 7
        // 7^2 mod 143 = (7^1 mod 143)^2 mod 143 = 49 mod 143 = 49
        // 7^4 mod 143 = (7^2 mod 143)^2 mod 143 = 492 mod 143 = 113
        // 7^8 mod 143 = (7^4 mod 143)^2 mod 143 = 1132 mod 143 = 42
        // 7^16 mod 143 = (7^8 mod 143)^2 mod 143 = 422 mod 143 = 48
        // 7^32 mod 143 = (7^16 mod 143)^2 mod 143 = 482 mod 143 = 16
        // 7^64 mod 143 = (7^32 mod 143)^2 mod 143 = 162 mod 143 = 113
        //
        // Do wyliczenia potęgi bierzemy tylko te reszty, które występują w sumie potęg 2: 
        // (jeśli byłoby ich bardzo dużo, to każde mnożenie można wykonać z operacją modulo,
        // dzięki czemu wynik nigdy nie wyjdzie poza wartość modułu)
        // 
        // t = 7^103 mod 143 = 113 × 16 × 113 × 49 × 7 mod 143 = 123
        private double CipherLoop(double exponent, double originalValue)
        {
            double cipheredValue = 1;
            for (double i = exponent; i > 0; i /= 2)
            {
                if (i % 2 == 1)
                {
                    cipheredValue = (originalValue * cipheredValue) % this.n;
                }
                originalValue = (originalValue * originalValue) % this.n;
            }
            return cipheredValue;
        }

        // tutaj jedynie podajemy tablice bajtow, z ramki WAVE a nastepnie bedziemy zwracac
        // tablice double'i juz zaszyfrowanych.
        public double[] GetCipheredValue(byte[] normalSample)
        {
            // alokujemy tyle samo double'i co byte'ów 
            double[] rsaData = new double[normalSample.Length];

            for (int i = 0; i < normalSample.Length; i++)
            {
                // wartosc z tablicy bajtow konwetujemy na double dzielac modulo this.n
                double value = normalSample[i] % this.n;
                // nastpenie wartosc pobrana przekazujemy do petli szyfrujacej i zapisujemy w talibcy wyjosciowej.
                rsaData[i] = this.CipherLoop(this.e, value);
            }

            return rsaData;
        }

        // operacja analogiczna do powyzszej, jedyna roznica to jest to ze zwracamy tablice bajtow juz rozkodowana
        public byte[] GetDecipheredValue(double[] cipheredSample)
        {
            byte[] rsaData = new byte[cipheredSample.Length];

            for (int i = 0; i < cipheredSample.Length; i++)
            {
                double value = cipheredSample[i] % this.n;
                rsaData[i] = (byte)this.CipherLoop(this.d, value);
            }

            return rsaData;
        }
    }
}
