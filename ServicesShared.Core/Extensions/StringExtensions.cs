using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesShared.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <returns>true if the value parameter is null or System.String.Empty, or if value consists exclusively of white-space characters.</returns>
        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);

        public static int? ParseToInt(this string source, bool asNullable = false)
        {
            int? result = null;
            int number;
            if (!source.IsNullOrEmpty() && (int.TryParse(source, out number) || !asNullable))
            {
                result = number;
            }

            return result;
        }
    }
}
