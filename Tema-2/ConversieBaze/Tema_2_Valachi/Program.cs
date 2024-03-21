using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

//CERINTA:
//Scrieți un program care să convertească un număr din baza b1 în baza b2 și să permită baze de la 2 la 26.

class Conversie
{
    public static string ConvertBase(string numar, int b1, int b2)
    {
        if (b1 < 2 || b1 > 26 || b2 < 2 || b2 > 26)
        {
            throw new ArgumentException("Baza trebuie sa fie intre 2 si 26.");
        }
        if (b1 == 10)
            return InB2(int.Parse(numar), b2);
        else
        {
            int numarB10 = InBaza10(numar, b1);
            return InB2(numarB10, b2);
        }
        
    }

    //intai se face conversia numarului din b1 in baza 10
    private static int InBaza10(string numar, int b1) 
    {
        int rez = 0;
        foreach (char digit in numar) //numarul poate contine si litere
        {
            int value = CharToValue(digit);
            if (value < 0 || value >= b1)
            {
                throw new ArgumentException("Acest numar nu este in baza " + b1);
            }
            rez = rez * b1 + value;
        }
        return rez;
    }

    //conversia numarului din baza 10 in b2
    private static string InB2(int numar, int b2)
    {
        if (numar == 0) 
            return "0";

        string rez = "";

        while (numar > 0)
        {
            int rest = numar % b2;
            rez = ValueToChar(rest) + rez;
            numar /= b2;
        }
        return rez;
    }


    private static int CharToValue(char c)
    {
        if (char.IsDigit(c))
            return c-'0';  //se scade valoarea Unicode a lui '0' pt a obtine valoarea integer a caracterului c
        else if (char.IsLetter(c))

            //de exemplu, daca avem:
            //c='B' ----> 'B'-'A'+10 = 1+10 = 11,
            //c='C' ----> 'C'-'A'+10 = 2+10 = 12
            //etc.

            return char.ToUpper(c) - 'A' + 10;
        else
            throw new ArgumentException(c+" nu este un caracter valid.");
    }

    private static char ValueToChar(int val)
    {
        char rez;

        if (val < 10)
        {
            rez = (char)(val+'0'); 
        }
        else
        {
            //de data aceasta, daca, de exemplu:
            // val=11 ----> 11-10+'A' = 1+'A' = 'B'
            //etc.

            rez = (char)(val - 10 + 'A');
        }
        return rez;
    }

    static void Main()
    {
        Console.WriteLine("Alege un numar: ");
        string numar = Console.ReadLine();

        Console.WriteLine("Baza acestui numar: ");
        int b1 = int.Parse(Console.ReadLine());  // input ul se va percepe ca un integer, dupa ce este folosit parse-ul

        Console.WriteLine("Baza in care va fi convertit: ");
        int b2 = int.Parse(Console.ReadLine());

        try
        {
            string rez = ConvertBase(numar, b1, b2);
            Console.WriteLine("Conversia numarului "+numar+" din baza "+b1+" in baza " +b2+" ---> "+rez);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Eroare!");
        }
    }
}
