using System;
using System.Collections.Generic;
using System.Text;

namespace PartialsCiphers
{
    public static class Glyph3wk
    {
        private static readonly Dictionary<char, string> L = new Dictionary<char, string>
        {
            {'A', "Q7M"}, {'B', "L2X"}, {'C', "T9R"}, {'D', "J5P"},
            {'E', "W8K"}, {'F', "N3D"}, {'G', "H6Q"}, {'H', "Z1T"},
            {'I', "R4B"}, {'J', "C7Y"}, {'K', "P2L"}, {'L', "X5N"},
            {'M', "V8C"}, {'N', "D1W"}, {'O', "K4H"}, {'P', "B7J"},
            {'Q', "Y2F"}, {'R', "M5Z"}, {'S', "T8G"}, {'T', "L1Q"},
            {'U', "R7N"}, {'V', "C4P"}, {'W', "H2X"}, {'X', "J8D"},
            {'Y', "F5K"}, {'Z', "N1R"}
        };

        private static readonly Dictionary<char, string> N = new Dictionary<char, string>
        {
            {'0', "AV3"}, {'1', "XV8"}, {'2', "MV1"}, {'3', "PV7"},
            {'4', "KV5"}, {'5', "RV9"}, {'6', "TV2"}, {'7', "GV6"},
            {'8', "BV4"}, {'9', "ZV0"}
        };

        private static readonly Dictionary<string, char> R1 = new Dictionary<string, char>();
        private static readonly Dictionary<string, char> R2 = new Dictionary<string, char>();
        
        private static string FlipCase(string s)
        {
            char[] chars = s.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsUpper(chars[i])) chars[i] = char.ToLowerInvariant(chars[i]);
                else if (char.IsLower(chars[i])) chars[i] = char.ToUpperInvariant(chars[i]);
            }
            return new string(chars);
        }

        static Glyph3wk()
        {
            var lowerL = new Dictionary<char, string>();
            foreach (var kvp in L)
            {
                lowerL[char.ToLowerInvariant(kvp.Key)] = FlipCase(kvp.Value);
            }
            foreach (var kvp in lowerL) L[kvp.Key] = kvp.Value;

            foreach (var kvp in L) R1[kvp.Value] = kvp.Key;
            foreach (var kvp in N) R2[kvp.Value] = kvp.Key;
        }

        private static char Shift(char c, int x)
        {
            if (c >= 'A' && c <= 'Z')
            {
                int p = c - 'A';
                int shifted = (p + x) % 26;
                if (shifted < 0) shifted += 26;
                return (char)('A' + shifted);
            }
            if (c >= 'a' && c <= 'z')
            {
                int p = c - 'a';
                int shifted = (p + x) % 26;
                if (shifted < 0) shifted += 26;
                return (char)('a' + shifted);
            }
            return c;
        }
        
        private static char ReverseShift(char c, int y)
        {
            if (c >= 'A' && c <= 'Z')
            {
                int p = c - 'A';
                int shifted = (p - y) % 26;
                if (shifted < 0) shifted += 26;
                return (char)('A' + shifted);
            }
            if (c >= 'a' && c <= 'z')
            {
                int p = c - 'a';
                int shifted = (p - y) % 26;
                if (shifted < 0) shifted += 26;
                return (char)('a' + shifted);
            }
            return c;
        }

        public static string Encode(string input, int key)
        {
            if (string.IsNullOrEmpty(input)) return "";
            key = (key % 26 + 26) % 26;
            StringBuilder result = new StringBuilder();
            
            foreach (char c in input)
            {
                if (L.ContainsKey(c))
                {
                    result.Append(L[Shift(c, key)]);
                }
                else if (N.ContainsKey(c))
                {
                    result.Append(N[c]);
                }
                else
                {
                    result.Append($"V4U{((int)c):X4}");
                }
            }
            return result.ToString();
        }

        public static string Decode(string input, int key)
        {
            if (string.IsNullOrEmpty(input)) return "";
            key = (key % 26 + 26) % 26;
            StringBuilder result = new StringBuilder();
            int i = 0;
            
            while (i < input.Length)
            {
                if (i + 7 <= input.Length && input.Substring(i, 3) == "V4U")
                {
                    string hex = input.Substring(i + 3, 4);
                    try
                    {
                        int codePoint = Convert.ToInt32(hex, 16);
                        result.Append((char)codePoint);
                    }
                    catch
                    {
                        result.Append('?');
                    }
                    i += 7;
                }
                else
                {
                    if (i + 3 <= input.Length)
                    {
                        string t = input.Substring(i, 3);
                        if (R2.TryGetValue(t, out char numChar))
                        {
                            result.Append(numChar);
                        }
                        else if (R1.TryGetValue(t, out char letterChar))
                        {
                            result.Append(ReverseShift(letterChar, key));
                        }
                        else
                        {
                            result.Append('?');
                        }
                        i += 3;
                    }
                    else
                    {
                        result.Append('?');
                        i++; 
                    }
                }
            }
            return result.ToString();
        }
    }
}
