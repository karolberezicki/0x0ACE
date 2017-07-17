using System;
using System.Collections;
using System.Collections.Generic;

namespace Primes
{
    public static class PrimeGenerator
    {
        public static int ApproximateNthPrime(int nn)
        {
            double n = nn;
            double p;
            if (nn >= 7022)
            {
                p = n * Math.Log(n) + n * (Math.Log(Math.Log(n)) - 0.9385);
            }
            else if (nn >= 6)
            {
                p = n * Math.Log(n) + n * Math.Log(Math.Log(n));
            }
            else if (nn > 0)
            {
                p = new[] {2, 3, 5, 7, 11}[nn - 1];
            }
            else
            {
                p = 0;
            }
            return (int) p;
        }

        // Find all primes up to and including the limit
        public static BitArray SieveOfEratosthenes(int limit)
        {
            BitArray bits = new BitArray(limit + 1, true)
            {
                [0] = false,
                [1] = false
            };
            for (int i = 0; i * i <= limit; i++)
            {
                if (!bits[i])
                {
                    continue;
                }
                for (int j = i * i; j <= limit; j += i)
                {
                    bits[j] = false;
                }
            }
            return bits;
        }

        public static List<int> GeneratePrimesSieveOfEratosthenes(int primeCount)
        {
            int limit = ApproximateNthPrime(primeCount);
            BitArray bits = SieveOfEratosthenes(limit);
            List<int> primes = new List<int>();
            for (int i = 0, found = 0; i < limit && found < primeCount; i++)
            {
                if (!bits[i])
                {
                    continue;
                }
                primes.Add(i);
                found++;
            }
            return primes;
        }


        public static List<int> GeneratePrimesSieveOfAtkin(int primeCount)
        {
            int limit = ApproximateNthPrime(primeCount);
            List<int> primes = new List<int>();

            bool[] isPrime = new bool[limit + 1];
            double sqrt = Math.Sqrt(limit);

            for (int x = 1; x <= sqrt; x++)
                for (int y = 1; y <= sqrt; y++)
                {
                    int n = 4 * x * x + y * y;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                    {
                        isPrime[n] ^= true;
                    }

                    n = 3 * x * x + y * y;
                    if (n <= limit && n % 12 == 7)
                    {
                        isPrime[n] ^= true;
                    }

                    n = 3 * x * x - y * y;
                    if (x > y && n <= limit && n % 12 == 11)
                    {
                        isPrime[n] ^= true;
                    }
                }

            for (int n = 5; n <= sqrt; n++)
            {
                if (!isPrime[n])
                {
                    continue;
                }
                int s = n * n;
                for (int k = s; k <= limit; k += s)
                {
                    isPrime[k] = false;
                }
            }

            primes.Add(2);
            primes.Add(3);
            for (int n = 5; n <= limit; n += 2)
            {
                if (isPrime[n])
                {
                    primes.Add(n);
                }
            }

            return primes;
        }


    }
}