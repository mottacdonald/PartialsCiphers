using System;
using System.Collections.Generic;
using System.Text;

namespace PartialsCiphers
{
    public static class Symb4wk
    {
        private static readonly Dictionary<char, string> L = new Dictionary<char, string>
        {
            {'A', "@q!7"}, {'B', "#m$2"}, {'C', "&z*4"}, {'D', "!x^9"},
            {'E', "%r@1"}, {'F', "$p#8"}, {'G', "*k&3"}, {'H', "^t!6"},
            {'I', "@v%0"}, {'J', "#c*5"}, {'K', "&b$7"}, {'L', "!n@2"},
            {'M', "%w^4"}, {'N', "$h#9"}, {'O', "*y&1"}, {'P', "^j!3"},
            {'Q', "@d%8"}, {'R', "#s*6"}, {'S', "&f$0"}, {'T', "!g@5"},
            {'U', "%u^7"}, {'V', "$a#2"}, {'W', "*l&4"}, {'X', "^o!9"},
            {'Y', "@e%1"}, {'Z', "#i*3"}
        };

        private static readonly Dictionary<char, string> N = new Dictionary<char, string>
        {
            {'0', "!V@3"}, {'1', "#V$8"}, {'2', "&V*1"}, {'3', "^V!7"},
            {'4', "@V%5"}, {'5', "$V#9"}, {'6', "*V&2"}, {'7', "!V^6"},
            {'8', "#V@4"}, {'9', "&V$0"}
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

        static Symb4wk()
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
                    result.Append($"S4U{((int)c):X4}");
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
                if (i + 7 <= input.Length && input.Substring(i, 3) == "S4U")
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
                    if (i + 4 <= input.Length)
                    {
                        string t = input.Substring(i, 4);
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
                        i += 4;
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
