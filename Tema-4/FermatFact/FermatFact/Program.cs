using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Scrieti un numar:");
        int number = int.Parse(Console.ReadLine());

        List<int> factors = Fermat(number);

        Console.WriteLine($"Factorizarea in numere prime a nr. {number}: ");
        foreach (int factor in factors)
        {
            Console.Write(factor + " ");
        }
    }

    static List<int> Fermat(int n)
    {
        List<int> factors = new List<int>();

        while (n % 2 == 0)
        {
            factors.Add(2);
            n /= 2;
        }

        for (int i = 3; i * i <= n; i += 2)
        {
            while (n % i == 0)
            {
                factors.Add(i);
                n /= i;
            }
        }

        if (n > 2)
        {
            factors.Add(n);
        }

        return factors;
    }
}
