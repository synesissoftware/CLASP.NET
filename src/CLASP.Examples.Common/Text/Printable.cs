
namespace Clasp.Examples.Common.Text
{
    using System;

    public static class Printable
    {
        public static int CharacterCode(char c)
        {
            return (int)c;
        }

        public static string CharacterRepr(char c)
        {
            if (Char.IsWhiteSpace(c) || Char.IsControl(c))
            {
                switch(c)
                {
                case '\b': return @"\b";
                case '\n': return @"\n";
                case '\r': return @"\r";
                case '\t': return @"\t";
                case '\v': return @"\v";
                default:

                    return String.Format(@"\U{0:04}", (int)c);
                }
            }
            else
            {
                return new String(c, 1);
            }
        }
    }
}
