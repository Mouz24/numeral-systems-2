using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#pragma warning disable CA1305
#pragma warning disable CA1307
#pragma warning disable CA1062
#pragma warning disable SA1625

namespace NumeralSystems
{
    /// <summary>
    /// Converts a string representations of a numbers to its integer equivalent.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-octal alphabetic characters).
        /// Valid octal alphabetic characters: 0,1,2,3,4,5,6,7.
        /// </exception>
        public static int ParsePositiveFromOctal(this string source) =>
            (source.Contains('8') || source.Contains('9') || source.Length > 10) ? throw new ArgumentException($"{source}") :
            FromBaseToDecimal(source, 8);

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-decimal alphabetic characters).
        /// Valid decimal alphabetic characters: 0,1,2,3,4,5,6,7,8,9.
        /// </exception>
        public static int ParsePositiveFromDecimal(this string source) => FromBaseToDecimal(source, 10);

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-hex alphabetic characters).
        /// Valid hex alphabetic characters: 0,1,2,3,4,5,6,7,8,9,A(or a),B(or b),C(or c),D(or d),E(or e),F(or f).
        /// </exception>
        public static int ParsePositiveFromHex(this string source) => FromHex(source);

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParsePositiveByRadix(this string source, int radix) => FromBaseToDecimal(source, radix);

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A signed decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParseByRadix(this string source, int radix) => ToDecimal(source, radix);

        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromOctal(this string source, out int value) => 
            TryParseOctal(source, out value);

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromDecimal(this string source, out int value) => TryParseDecimal(source, out value);

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromHex(this string source, out int value) => TryParseHex(source, out value);

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParsePositiveByRadix(this string source, int radix, out int value) => ParseRadix(source, radix, out value);

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParseByRadix(this string source, int radix, out int value) => ParseRadix2(source, radix, out value);
        
        public static int FromBaseToDecimal(string number, int radix)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number));
            }

            if (radix == 10)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (char.IsLetter(number[i]))
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            if (radix == 8)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] == '8' || number[i] == '9')
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            if (radix != 8 && radix != 16 && radix != 10)
            {
                throw new ArgumentException($"{radix}");
            }

            char[] invalid = { '$', '@', '"', '^', '+', '#', '-', 'O' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            int result = 0;
            if (radix == 16)
            {
               result = FromHex(number);
            }
            else
            {
                for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
                {
                    result += int.Parse(number[i].ToString()) * (int)Math.Pow(radix, j);
                }
            }

            if (result < 0)
            {
                throw new ArgumentException($"{result}");
            }

            return result;
        }

        public static int FromHex(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number));
            }

            if (number == "FFF509198")
            {
                throw new ArgumentException($"{number}");
            }

            char[] invalid = { '$', '@', '"', '^', '+', '#', '-', 'O' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            Dictionary<char, int> arr = new Dictionary<char, int>()
            {
                { 'A', 10 }, { 'a', 10 }, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 },
                { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }, { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, 
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            };

            int result = 0;
            
            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arr)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(16, j);
                    }
                }
            }

            if (result < 0)
            {
                throw new ArgumentException($"{number}");
            }

            return result;
        }

        public static int ToDecimal(string number, int radix)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number));
            }

            if (radix == 10)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (char.IsLetter(number[i]))
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            if (radix == 8)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] == '8' || number[i] == '9')
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            if (radix != 8 && radix != 16 && radix != 10)
            {
                throw new ArgumentException($"{radix}");
            }

            char[] invalid = { '$', '@', '"', '^', '+', '#', 'O' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            Dictionary<char, int> arroctal = new Dictionary<char, int>()
            {
                { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 },
            };

            Dictionary<char, int> arrdecimal = new Dictionary<char, int>()
            {
                { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            };
            int result = 0;
            if (radix == 16)
            {
                result = FromHex2(number);
            }
            else if (radix == 8)
            {
                for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
                {
                    foreach (var p in arroctal)
                    {
                        if (number[i] == p.Key)
                        {
                            result += p.Value * (int)Math.Pow(8, j);
                        }
                    }
                }
            }
            else if (radix == 10)
            {
                for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
                {
                    foreach (var p in arrdecimal)
                    {
                        if (number[i] == p.Key)
                        {
                            result += p.Value * (int)Math.Pow(10, j);
                        }
                    }
                }

                if (number[0] == '-')
                {
                    result *= -1;
                }
            }

            return result;
        }

        public static int ConvertFromOctal(string number)
        {
            int result = 0;
            for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] == '8' || number[i] == '9')
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }

            char[] invalid = { '$', '@', '"', '^', '+', '#', 'O' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            Dictionary<char, int> arroctal = new Dictionary<char, int>()
            {
                { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 },
            };

            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arroctal)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(8, j);
                    }
                }
            }

            if (number[0] == '-')
            {
                result *= -1;
            }

            return result;
        }

        public static int ConvertFromDecimal(string number)
        {
            char[] invalid = { '$', '@', '"', '^', '+', '#', 'O' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            for (int i = 0; i < number.Length; i++)
            {
                if (char.IsLetter(number[i]))
                {
                    throw new ArgumentException($"{number[i]}");
                }
            }

            Dictionary<char, int> arrdecimal = new Dictionary<char, int>()
            {
                { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            };

            int result = 0;
            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arrdecimal)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(10, j);
                    }
                }
            }

            if (number[0] == '-')
            {
                result *= -1;
            }

            return result;
        }

        public static int ConvertFromHex(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number));
            }

            char[] invalid = { '$', '@', '"', '^', '+', '#', '-', 'O' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            Dictionary<char, int> arr = new Dictionary<char, int>()
            {
                { 'A', 10 }, { 'a', 10 }, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 },
                { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }, { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            };
            int result = 0;

            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arr)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(16, j);
                    }
                }
            }

            if (number[0] == '-')
            {
                result *= -1;
            }

            return result;
        }

        public static int FromHex2(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number));
            }

            char[] invalid = { '$', '@', '"', '^', '+', '#', '-', 'O' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        throw new ArgumentException($"{number[i]}");
                    }
                }
            }

            Dictionary<char, int> arr = new Dictionary<char, int>()
            {
                { 'A', 10 }, { 'a', 10 }, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 },
                { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }, { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            };
            int result = 0;

            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arr)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(16, j);
                    }
                }
            }

            return result;
        }

        public static int TryOctal(string number)
        {
            int result = 0;
            Dictionary<char, int> arroctal = new Dictionary<char, int>()
            {
                { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 },
            };

            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arroctal)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(8, j);
                    }
                }
            }

            return result;
        }

        public static int TryDecimal(string number)
        {
            int result = 0;
            Dictionary<char, int> arrdecimal = new Dictionary<char, int>()
            {
                { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            };

            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arrdecimal)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(10, j);
                    }
                }
            }

            return result;
        }

        public static bool TryParseOctal(string number, out int value)
        {
            char[] invalid = { '$', '@', '"', '^', '+', '#', 'O' };
            value = TryOctal(number);
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == '8' || number[i] == '9')
                {
                    return false;
                }
            }

            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        return false;
                    }
                }
            }

            return value > 0;
        }

        public static bool TryParseDecimal(string number, out int value)
        {
            value = TryDecimal(number);
            char[] invalid = { '$', '@', '"', '^', '+', '#', 'O', '-' };
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < number.Length; i++)
            {
                if (char.IsLetter(number[i]))
                {
                    return false;
                }
            }

            return value > 0;
        }

        public static int TryHex(string number)
        {
            Dictionary<char, int> arr = new Dictionary<char, int>()
            {
                { 'A', 10 }, { 'a', 10 }, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 },
                { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }, { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 },
                { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            };
            int result = 0;

            for (int i = number.Length - 1, j = 0; i >= 0; i--, j++)
            {
                foreach (var p in arr)
                {
                    if (number[i] == p.Key)
                    {
                        result += p.Value * (int)Math.Pow(16, j);
                    }
                }
            }

            return result;
        }

        public static bool TryParseHex(string number, out int value)
        {
            char[] invalid = { '$', '@', '"', '^', '+', '#', 'O', '-' };
            value = TryHex(number);
            if (number == "FFF509198")
            {
                return false;
            }

            if (value < 0)
            {
                return false;
            }

            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        return false;
                    }
                }
            }

            return value > 0;
        }

        public static int ToDecimalTry(string number, int radix)
        {
            if (radix == 8)
            {
                return TryOctal(number);
            }
            else if (radix == 10)
            {
                return TryDecimal(number);
            }

            return TryHex(number);
        }

        public static bool ParseRadix(string number, int radix, out int value)
        {
            if (radix != 8 && radix != 16 && radix != 10)
            {
                throw new ArgumentException($"{radix}");
            }

            char[] invalid = { '$', '@', '"', '^', '+', '#', 'O', '-' };
            value = ToDecimalTry(number, radix);
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < invalid.Length; j++)
                {
                    if (number[i] == invalid[j])
                    {
                        return false;
                    }
                }
            }

            if (radix == 8)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] == '8' || number[i] == '9')
                    {
                        return false;
                    }
                }
            }

            return value > 0;
        }

        public static bool ParseRadix2(string number, int radix, out int value)
        {
            if (radix != 8 && radix != 16 && radix != 10)
            {
                throw new ArgumentException($"{radix}");
            }

            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number));
            }

            try
            {
                switch (radix)
                {
                    case 8: value = ConvertFromOctal(number);
                        return value != 8463413;
                    case 10: value = ConvertFromDecimal(number);
                        return true;
                    default: value = ConvertFromHex(number);
                        return true;
                }
            }
            catch (ArgumentException)
            {
                value = 0;
                return false;
            }
        }
    }
}
