using System;
using System.Collections.Generic;

//////////////ex. 5 calculul simb. Jacobi
class JacobiSymbol
{
    public static int Legendre(int a, int p)
    {
        if (p <= 0 || p % 2 == 0)
            throw new ArgumentException("p trebuie sa fie natural impar!");

        a = a % p;
        if (a == 0) return 0;
        if (a == 1) return 1;

        int result;
        if (a % 2 == 0)
        {
            result = Legendre(a / 2, p);
            if (p % 8 == 3 || p % 8 == 5)
                result = -result;
        }
        else
        {
            result = Legendre(p % a, a);
            if (a % 4 == 3 && p % 4 == 3)
                result = -result;
        }

        return result;
    }

    public static Dictionary<int, int> PrimeFactorize(int n)
    {
        var factors = new Dictionary<int, int>();
        for (int i = 2; i <= Math.Sqrt(n); i++)
        {
            while (n % i == 0)
            {
                if (factors.ContainsKey(i))
                    factors[i]++;
                else
                    factors[i] = 1;
                n /= i;
            }
        }

        if (n > 1)
            factors[n] = 1;

        return factors;
    }

    public static int CalculateJacobi(int a, int n)
    {
        if (n <= 0 || n % 2 == 0)
            throw new ArgumentException("n trebuie sa fie natural impar!");

        var primeFactors = PrimeFactorize(n);
        int result = 1;

        foreach (var factor in primeFactors)
        {
            int p = factor.Key;
            int alpha = factor.Value;
            int legendreSymbol = Legendre(a, p);
            result *= (int)Math.Pow(legendreSymbol, alpha);
        }

        return result;
    }

    public static int ModularExponentiation(int baseNum, int exp, int mod)
    {
        int result = 1;
        baseNum = baseNum % mod;

        while (exp > 0)
        {
            if ((exp & 1) == 1)
                result = (result * baseNum) % mod;

            exp >>= 1;
            baseNum = (baseNum * baseNum) % mod;
        }

        return result;
    }

    public static bool SolovayStrassen(int n, int k)
    {
        if (n == 2 || n == 3) return true;
        if (n < 2 || n % 2 == 0) return false;

        Random rand = new Random();

        for (int i = 0; i < k; i++)
        {
            int a = rand.Next(2, n - 1);
            int jacobi = CalculateJacobi(a, n);
            int modExp = ModularExponentiation(a, (n - 1) / 2, n);
            if (jacobi == 0 || (jacobi % n + n) % n != modExp)
                return false;
        }

        return true;
    }

    static void Main(string[] args)
    {
        Console.Write("a = ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("n = (numar natural impar): ");
        int n = int.Parse(Console.ReadLine());

        try
        {
            int jacobiSymbol = CalculateJacobi(a, n);
            Console.WriteLine($"Simbolul Jacobi (a/n) pentru a = {a}, n = {n} este {jacobiSymbol}\n");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }


///////////////ex.6 implementare Solovay-Strassen
        Console.Write("Introduceti numarul pe care doriti sa il testati pentru primalitate: ");
        int number = int.Parse(Console.ReadLine());

        Console.Write("Introduceti numarul de iteratii pentru testul de primalitate: ");
        int iterations = int.Parse(Console.ReadLine());

        try
        {
            if (SolovayStrassen(number, iterations))
            {
                Console.WriteLine($"{number} este prim (cel mai probabil).");
            }
            else
            {
                Console.WriteLine($"{number} este compus.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
