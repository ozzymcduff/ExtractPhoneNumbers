using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExtractPhoneNumbers
{
    public class MatchToken
    {
        public MatchToken(Type token, string value)
        {
            Value = value;
            Token = token;
        }
        public enum Type
        {
            Text, Number
        }
        public Type Token { get; private set; }

        public string Value { get; private set; }

        public static MatchToken Number(string value)
        {
            return new MatchToken(Type.Number, value);
        }
        public static MatchToken Text(string value)
        {
            return new MatchToken(Type.Text, value);
        }

        public static IEnumerable<MatchToken> Tokens(string input)
        {
            var matches = PhoneNumbers.PhoneNumberRegex.Matches(input).Cast<Match>();
            int index = 0;
            foreach (var t in matches)
            {
                if (t.Index > index)
                {
                    yield return Text(input.Substring(index, t.Index - index));
                }
                yield return Number(t.Value);
                index = t.Index + t.Length;
            }
            if (input.Length > index)
            {
                yield return Text(input.Substring(index, input.Length - index));
            }
        }

    }
}