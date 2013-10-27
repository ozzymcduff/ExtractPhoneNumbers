using System;

namespace ExtractPhoneNumbers.Tests
{
    public class TestCase
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public TestCase(string[] line)
        {
            Key = line[0];
            Value = line[1];
        }

        public bool Fail()
        {
            return Value.Equals("FAIL");
        }

        public bool Match()
        {
            return Value.Equals("MATCH");
        }
    }
}