using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Threading;

namespace GridEngineCore.Extensions
{
    public static class StringExtensions
    {
        const char arabicYa01 = '\u0649';
        const char arabicYa02 = '\u064A';
        const char arabicKaf = '\u0643';

        const char farsiYa = '\u06CC';
        const char farsiKaf = '\u06A9';

        public static string ArabicToFarsi(this string arabicString)
        {
            if (string.IsNullOrWhiteSpace(arabicString))
                return arabicString;

            return arabicString
                .Replace(arabicYa01, farsiYa)
                .Replace(arabicYa02, farsiYa)
                .Replace(arabicKaf, farsiKaf);
        }

        public static string FixPersianChars(this string str)
        {
            return str.Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");//.Replace("ئ", "ی");
        }

        public static string LatinNumbersToFarsiNumbers(this string value)
        {
            return value.Replace('1', '۱')
                    .Replace('2', '۲')
                    .Replace('3', '۳')
                    .Replace('4', '۴')
                    .Replace('5', '۵')
                    .Replace('6', '۶')
                    .Replace('7', '۷')
                    .Replace('8', '۸')
                    .Replace('9', '۹')
                    .Replace('0', '۰')
                    .Replace('.', '\u066B');
        }

        public static string FarsiNumbersToLatinNumbers(this string value)
        {
            return value.Replace('۱', '1')
                        .Replace('۲', '2')
                        .Replace('۳', '3')
                        .Replace('۴', '4')
                        .Replace('۵', '5')
                        .Replace('۶', '6')
                        .Replace('۷', '7')
                        .Replace('۸', '8')
                        .Replace('۹', '9')
                        .Replace('۰', '0')
                        .Replace('\u066B', '.')
                        //iphone numeric
                        .Replace("٠", "0")
                        .Replace("١", "1")
                        .Replace("٢", "2")
                        .Replace("٣", "3")
                        .Replace("٤", "4")
                        .Replace("٥", "5")
                        .Replace("٦", "6")
                        .Replace("٧", "7")
                        .Replace("٨", "8")
                        .Replace("٩", "9");
        }

        public static bool ContainsIgnoreCase(this string value, string term)
        {
            return value.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        public static bool EqualsIgnoreCase(this string theString, string value)
        {
            return theString.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string ToBase64String(this string value)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
        }

        public static string Base64ToNormalString(this string base64String)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }


        public static string LimitsTo(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Substring(0, Math.Min(value.Length, maxLength));
        }

        public static string FixNoteSubtitle(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : $" ({value})";
        }


        public static string PersianToEnglish(this string input)
        {

            return input.Replace('\u06f0', '0')
                           .Replace('\u06f1', '1')
                           .Replace('\u06f2', '2')
                           .Replace('\u06f3', '3')
                           .Replace('\u06f4', '4')
                           .Replace('\u06f5', '5')
                           .Replace('\u06f6', '6')
                           .Replace('\u06f7', '7')
                           .Replace('\u06f8', '8')
                           .Replace('\u06f9', '9');
        }

       
        // 1401.05.12
        // https://www.michaelrose.dev/posts/exploring-system-text-json/
        private static string ToSnakeOrKebabCase(this string str, char character)
        {
            if (str is null)
                return string.Empty;

            var upperCaseLength = str.Skip(1).Count(t => char.IsUpper(t));

            var bufferSize = str.Length + upperCaseLength;

            Span<char> buffer = new char[bufferSize];

            var bufferPosition = 0;

            var position = 0;

            while (position < str.Length)
            {
                if (position > 0 && char.IsUpper(str[position]))
                {
                    buffer[bufferPosition] = character;
                    bufferPosition++;
                }

                buffer[bufferPosition] = str[position];

                bufferPosition++;
                
                position++;
            }

            return new string(buffer).ToLower();
        }
         
        public static string ToPascalCase(this string str)
        {
            if (str is null)
                return string.Empty;

            var tempValue = str.ToLower().Replace("-", " ").Replace("_", " ");

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tempValue).Replace(" ", string.Empty);
        }

        public static string ToSnakeCase(this string str)
        {
            return ToSnakeOrKebabCase(str, '_').Replace("-", "_");
        }

        // 1401.09.08
        // https://betterprogramming.pub/string-case-styles-camel-pascal-snake-and-kebab-case-981407998841
        public static string ToSankeCaseAllCaps(this string str)
        {
            return str.ToSnakeCase().ToUpperInvariant();
        }

        public static string ToKebbabCase(this string str)
        {
            return ToSnakeOrKebabCase(str, '-').Replace("_", "-");
        }

        public static string ToCamelCase(this string str)
        {
            return System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(str);
        }

        public static string ToScreamingCase(this string str)
        {
            return str.ToUpperInvariant()
                        .Replace("-", string.Empty)
                        .Replace("_", string.Empty);
        }

        public static string ToLazyCase(this string str)
        {
            return str.ToLowerInvariant()
                        .Replace("-", string.Empty)
                        .Replace("_", string.Empty);
        }

        public static string ApplyCase(this string str, StringCase @case)
        {
            switch (@case)
            {
                case StringCase.CamelCase:
                    return ToCamelCase(str);

                case StringCase.PascalCase:
                    return ToPascalCase(str);

                case StringCase.SnakeCase:
                    return ToSnakeCase(str);

                case StringCase.SankeCaseAllCaps:
                    return ToSankeCaseAllCaps(str);

                case StringCase.KebbabCase:
                    return ToKebbabCase(str);

                case StringCase.ToScreamingCase:
                    return ToScreamingCase(str);

                case StringCase.ToLazyCase:
                    return ToLazyCase(str);

                default:
                    throw new NotImplementedException("StringExtensions > ApplyCase");
            }
        }
    }
}