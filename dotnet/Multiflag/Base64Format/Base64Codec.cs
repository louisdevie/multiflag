using System;
using System.Text;

namespace Multiflag.Base64Format
{
    internal static class Base64Codec
    {
        private const char ZERO = 'A';
        private const char TWENTY_SIX = 'a';
        private const char FIFTY_TWO = '0';
        private const char SIXTY_TWO = '-';
        private const char SIXTY_THREE = '_';

        private static readonly string zero = ZERO.ToString();

        public static string Zero => zero;

        public static char EncodeByte(int value)
        {
            if (value < 26)
            {
                return (char)(ZERO + value);
            }
            else if (value < 52)
            {
                return (char)(TWENTY_SIX + value - 26);
            }
            else if (value < 62)
            {
                return (char)(FIFTY_TWO + value - 52);
            }
            else
            {
                return value == 62 ? SIXTY_TWO : SIXTY_THREE;
            }
        }

        public static int DecodeByte(char encodedValue)
        {
            if (encodedValue == SIXTY_THREE)
            {
                return 63;
            }
            else if (encodedValue == SIXTY_TWO)
            {
                return 62;
            }
            else if (encodedValue >= TWENTY_SIX)
            {
                return encodedValue - TWENTY_SIX + 26;
            }
            else if (encodedValue >= ZERO)
            {
                return encodedValue - ZERO;
            }
            else
            {
                return encodedValue - FIFTY_TWO + 52;
            }
        }

        public static string EncodeSingleFlag(int flagIndex)
        {
            int indexFromZero = flagIndex - 1;
            int bigEnd = indexFromZero % 6;
            int leadingBytes = indexFromZero / 6;
            return new string(ZERO, leadingBytes) + EncodeByte((byte)(1 << bigEnd));
        }

        public static bool DecodesToZero(string encodedValue)
        {
            var result = true;
            for (var i = 0; i < encodedValue.Length && result; i++)
            {
                result = encodedValue[i] == ZERO;
            }

            return result;
        }

        public static bool DecodesToSingleFlag(string encodedValue)
        {
            var powersOfTwo = 0;
            foreach (char t in encodedValue)
            {
                int value = DecodeByte(t);
                if (value != 0 && (value & value - 1) == 0)
                {
                    powersOfTwo++;
                }
            }

            return powersOfTwo == 1;
        }

        public static string BitwiseOr(string a, string b)
        {
            var result = new StringBuilder();

            string shorter, longer;
            if (a.Length < b.Length)
            {
                shorter = a;
                longer = b;
            }
            else
            {
                shorter = b;
                longer = a;
            }

            var i = 0;
            // OR the bytes one by one 
            for (; i < shorter.Length; i++)
            {
                int value = DecodeByte(shorter[i]) | DecodeByte(longer[i]);
                result.Append(EncodeByte(value));
            }

            // if one string is longer than the other, append the remaining bytes (x | 0 = x) 
            for (; i < longer.Length; i++)
            {
                result.Append(longer[i]);
            }

            // make sure there is always one digit in the string
            // empty strings are considered equal to zero, but we always try to normalise the output
            if (i < 1)
            {
                result.Append(ZERO);
            }

            return result.ToString();
        }

        public static string BitwiseAndNot(string first, string second)
        {
            var result = new StringBuilder();

            int shorterLength = Math.Min(first.Length, second.Length);
            var i = 0;
            // AND the bytes one by one 
            for (; i < shorterLength; i++)
            {
                int value = DecodeByte(first[i]) & ~DecodeByte(second[i]);
                result.Append(EncodeByte(value));
            }

            // if the first string is longer than the other, append its remaining bytes (x & ~0 = x)
            // if the second string is longer, don't add anything (0 & ~y = 0)
            for (; i < first.Length; i++)
            {
                result.Append(first[i]);
            }

            // make sure there is always one digit in the string
            // empty strings are considered equal to zero, but we always try to normalise the output
            if (i < 1)
            {
                result.Append(ZERO);
            }

            return result.ToString();
        }

        public static bool BitwiseAndEquals(string first, string second)
        {
            var result = true;

            int shorterLength = Math.Min(first.Length, second.Length);
            var i = 0;
            // AND the bytes one by one and check
            // if one is false we don't need to check further
            for (; i < shorterLength && result; i++)
            {
                int secondValue = DecodeByte(second[i]);
                result = (DecodeByte(first[i]) & secondValue) == secondValue;
            }

            // if there are more characters in the second string, they must all be zeros
            // (0 & x is only equal to x when x is also 0)
            for (; i < second.Length && result; i++)
            {
                result = second[i] == ZERO;
            }

            return result;
        }
    }
}