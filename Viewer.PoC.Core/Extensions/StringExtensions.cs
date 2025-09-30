using System;
using System.Linq;

namespace Viewer.PoC.Core.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitAndTrim(this string input, char separator)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return [];
            }
            return input
                .Split([separator], StringSplitOptions.RemoveEmptyEntries)
                .Select(part => part.Trim())
                .Where(part => !string.IsNullOrEmpty(part))
                .ToArray();
        }

        public static string NormalizeDecimalSeparator(this string input)
        {
            return input.Replace(',', '.');
        }

        public static byte ToByte(this string input)
        {
            if (byte.TryParse(input, out var b))
            {
                return b;
            }
            if (int.TryParse(input, out var i))
            {
                return (byte)Math.Max(0, Math.Min(255, i));
            }
            return 0;
        }
    }
}