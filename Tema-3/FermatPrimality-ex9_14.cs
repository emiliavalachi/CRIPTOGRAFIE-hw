using System;


///////ex 9, numarul 14:
//Aplicați algoritmul lui Fermat pentru a determina primalitatea numărului 36791.
class FermatPrimalityTest
{
    static void Main(string[] args)
    {
        int number = 36791;
        
        bool isPrime = IsPrimeFermat(number);
        
        Console.WriteLine($"{number} {(isPrime ? " este prim." : "nu este prim.")}");
    }

    static bool IsPrimeFermat(int n)
    {
        if (n <= 1) return false;
        if (n == 2 || n == 3) return true;
        if (n % 2 == 0) return false;

        for (int a = 2; a <= n - 2; a++)
        {
            if (ModularExponentiation(a, n - 1, n) != 1)
            {
                return false;
            }
        }
        return true;
    }

    static int ModularExponentiation(int baseValue, int exponent, int modulus)
    {
        if (modulus == 1) return 0;
        int result = 1;
        baseValue = baseValue % modulus;
        while (exponent > 0)
        {
            if ((exponent % 2) == 1)
                result = (result * baseValue) % modulus;
            exponent = exponent >> 1; 
            baseValue = (baseValue * baseValue) % modulus;
        }
        return result;
    }
}
