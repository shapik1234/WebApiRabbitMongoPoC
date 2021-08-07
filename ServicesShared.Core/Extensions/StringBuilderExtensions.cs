using System;
using System.Text;

namespace ServicesShared.Core
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a copy of the specified string to this instance if condition is true.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
        {
            if (condition)
            {
                builder.Append(value);
            }

            return builder;
        }

        /// <summary>
        /// Appends a copy of the specified string to this instance if condition is true.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="condition">The condition function.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendIf(this StringBuilder builder, Func<bool> condition, string value)
        {
            if (condition())
            {
                builder.Append(value);
            }

            return builder;
        }

        /// <summary>
        /// Appends a copy of the specified string to this instance if condition is true.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="condition">The condition function.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendIf(this StringBuilder builder, Func<StringBuilder, bool> condition, string value)
        {
            if (condition(builder))
            {
                builder.Append(value);
            }

            return builder;
        }

        /// <summary>
        /// Appends a copy of the specified string to this instance if value is not null or empty string.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="value">The string to append if it is not null or empty.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendIf(this StringBuilder builder, string value)
        {
            if (!value.IsNullOrEmpty())
            {
                builder.Append(value);
            }

            return builder;
        }

        /// <summary>
        /// Appends a copy of the specified string to this instance if value is not null or empty string.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="conditionValue">The string to append if it is not null or empty.</param>
        /// <param name="value">The string to append if <paramref name="conditionValue" /> is not null or empty.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendIf(this StringBuilder builder, string conditionValue, string value)
        {
            if (!conditionValue.IsNullOrEmpty())
            {
                builder.Append(conditionValue).Append(value);
            }

            return builder;
        }

        /// <summary>
        /// Appends a copy of the specified string followed by the default line terminator to the end of the current System.Text.StringBuilder object if condition is true.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, string value)
        {
            if (condition)
            {
                builder.AppendLine(value);
            }

            return builder;
        }

        /// <summary>
        /// Appends a copy of the specified string followed by the default line terminator to the end of the current System.Text.StringBuilder object if condition is true.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="condition">The condition function.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendLineIf(this StringBuilder builder, Func<StringBuilder, bool> condition, string value)
        {
            if (condition(builder))
            {
                builder.AppendLine(value);
            }

            return builder;
        }

        /// <summary>
        /// Appends a copy of the specified string followed by the default line terminator to the end of the current System.Text.StringBuilder object if condition is true.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <param name="@object">The instance of <see cref="T" />.</param>
        /// <param name="function">The function returns string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendLineIf<T>(this StringBuilder builder, T @object, Func<T, string> function)
        {
            if (@object != null)
            {
                builder.AppendLine(function(@object));
            }

            return builder;
        }

        /// <summary>
        /// Indicates whether the specified <see cref="StringBuilder" /> object equals empty string.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <returns>A true if the specified <see cref="StringBuilder"/> object equals empty string; otherwise, false.</returns>
        public static bool IsEmpty(this StringBuilder builder)
            => builder.Length == 0;

        /// <summary>
        /// Indicates whether the specified <see cref="StringBuilder" /> object does not equal empty string.
        /// </summary>
        /// <param name="builder">The instance of builder.</param>
        /// <returns>A true if the specified <see cref="StringBuilder"/> object does not equal empty string; otherwise, false.</returns>
        public static bool IsNotEmpty(this StringBuilder builder)
            => builder.Length > 0;
    }
}
