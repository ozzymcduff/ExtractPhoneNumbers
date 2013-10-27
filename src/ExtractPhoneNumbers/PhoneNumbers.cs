using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExtractPhoneNumbers
{
    public class PhoneNumbers
    {
        internal static readonly Regex PhoneNumberRegex = new Regex(@"
\+? 
(?:
    \(?\d*\)? 
    \s?
    \(?\d+\)?
    \d*
)
(?:
    [\s\./-]?\d{2,}
)+ #
(?:
    \s*
    (?:
        \#|x\.?|ext\.?|extension
    )
    \s*
    (
        \d+
    )
)?", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

        public static IEnumerable<string> Extract(string input)
        {
            var matches = PhoneNumberRegex.Matches(input).Cast<Match>();
            return matches.Select(t => t.Value);
        }
    }
}