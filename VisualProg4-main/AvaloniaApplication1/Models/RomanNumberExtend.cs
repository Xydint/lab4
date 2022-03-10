using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class RomanNumberExtend : RomanNumber
{
    static char[] roman_number = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
    static ushort Digit_Roman_to_10(char Z)
    {
        if (Z == roman_number[0]) return 1;
        if (Z == roman_number[1]) return 5;
        if (Z == roman_number[2]) return 10;
        if (Z == roman_number[3]) return 50;
        if (Z == roman_number[4]) return 100;
        if (Z == roman_number[5]) return 500;
        if (Z == roman_number[6]) return 1000;
        return 0;
    }

    static ushort String_to_10(string? num)
    {
        if (num == null) throw new RomanNumberException("Enter Number");
        ushort result = 0;
        int i = 0;
        for (; i < num.Length - 1; i++)
        {
            if (Digit_Roman_to_10(num[i]) < Digit_Roman_to_10(num[i + 1]))
            {
                result += (ushort)(Digit_Roman_to_10(num[i + 1]) - Digit_Roman_to_10(num[i]));
                i++;
            }
            else result += Digit_Roman_to_10(num[i]);
        }
        if (i != num.Length) result += Digit_Roman_to_10(num[i]);
        if (String.Compare(num, new RomanNumber(result).ToString()) != 0)
            throw new RomanNumberException("Error Enter");
        return result;
    }
    public RomanNumberExtend(string? num) : base(String_to_10(num)) { }
}
