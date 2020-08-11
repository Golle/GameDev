using System;
using System.Collections.Generic;

namespace Titan.Core.Common
{
    public static class StringExtensions
    {
        public static string[] SplitQuotedStrings(this string str, char delimiter)
        {
            if (str.IndexOf('"') != -1)
            {
                var parts = new List<string>();
                var partStart = 0;
                do
                {
                    var quoteStart = str.IndexOf('"', partStart);
                    var quoteEnd = str.IndexOf('"', quoteStart + 1);
                    var partEnd = str.IndexOf(delimiter, partStart);

                    if (partEnd == -1)
                    {
                        partEnd = str.Length;
                    }

                    // part has quotes, find the delimiter outside of the quote
                    if (partEnd < quoteEnd && partEnd > quoteStart)
                    {
                        partEnd = str.IndexOf(delimiter, quoteEnd + 1);
                    }

                    parts.Add(str.Substring(partStart, partEnd - partStart));
                    partStart = partEnd + 1;

                } while (partStart < str.Length - 1);
                return parts.ToArray();
            }
            // no quotes
            return str.Split(delimiter.ToString(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}