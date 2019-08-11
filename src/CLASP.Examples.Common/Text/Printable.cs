
namespace Clasp.Examples.Common.Text
{
    using System;

    /// <summary>
    ///  Utility class for determining printability of characters
    /// </summary>
    public static class Printable
    {
        /// <summary>
        ///  Obtains an <see cref="int"/> character code for the
        ///  given character
        /// </summary>
        /// <param name="c">
        ///  The character
        /// </param>
        /// <returns>
        ///  The character code
        /// </returns>
        public static int CharacterCode(char c)
        {
            return (int)c;
        }

        /// <summary>
        ///  Obtains the character representation string from the character
        /// </summary>
        /// <param name="c">
        ///  The character
        /// </param>
        /// <returns>
        ///  The character representation
        /// </returns>
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
