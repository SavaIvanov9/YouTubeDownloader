using System;
using System.IO;
using System.Linq;

namespace YD.Common.Extentions
{
    public static class StringExtentions
    {
        // Use this to keep digits. 
        private static readonly char[] _delimitersKeepDigits = new char[] {
            '{', '}', '(', ')', '[', ']', '>', '<','-', '_', '=', '+',
            '|', '\\', ':', ';', ' ', ',', '.', '/', '?', '~', '!',
            '@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t' };

        // This will discard digits 
        private static readonly char[] _delimitersNoDigits = new char[] {
            '{', '}', '(', ')', '[', ']', '>', '<','-', '_', '=', '+',
            '|', '\\', ':', ';', ' ', ',', '.', '/', '?', '~', '!',
            '@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        ///  Tokenizes text into an array of words, using whitespace and
        ///  all punctuation as delimiters.
        /// </summary>
        /// <param name="text"> the text to tokenize</param>
        /// <returns> an array of resulted tokens</returns>
        public static string[] GreedyTokenize(this string text)
        {
            return text.Split(_delimitersKeepDigits, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///  Tokenizes a text into an array of words, using whitespace and
        ///  all punctuation except the apostrophe "'" as delimiters. Digits
        ///  are handled based on user choice.
        /// </summary>
        /// <param name="text"> the text to tokenize</param>
        /// <param name="keepDigits"> true to keep digits; false to discard digits.</param>
        /// <returns> an array of resulted tokens</returns>
        public static string[] Tokenize(this string text, bool keepDigits)
        {
            string[] tokens = null;

            if (keepDigits)
                tokens = text.Split(_delimitersKeepDigits, StringSplitOptions.RemoveEmptyEntries);
            else
                tokens = text.Split(_delimitersNoDigits, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];

                // Change token only when it starts and/or ends with "'" and the 
                // toekn has at least 2 characters. 
                if (token.Length > 1)
                {
                    if (token.StartsWith("'") && token.EndsWith("'"))
                        tokens[i] = token.Substring(1, token.Length - 2); // remove the starting and ending "'" 

                    else if (token.StartsWith("'"))
                        tokens[i] = token.Substring(1); // remove the starting "'" 

                    else if (token.EndsWith("'"))
                        tokens[i] = token.Substring(0, token.Length - 1); // remove the last "'" 
                }
            }

            return tokens;
        }

        public static string CleanFileName(this string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}