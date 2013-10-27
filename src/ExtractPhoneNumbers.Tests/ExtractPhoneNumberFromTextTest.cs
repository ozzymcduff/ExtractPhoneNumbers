﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ExtractPhoneNumbers.Tests
{
    [TestFixture]
    public class ExtractPhoneNumberFromTextTest 
    {
        private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec urna urna, mollis cursus posuere et, rhoncus et leo. Vestibulum mollis pellentesque aliquam. Curabitur lacinia enim eu egestas dictum. Quisque mollis vehicula risus, tincidunt convallis justo tincidunt eget. Donec ullamcorper aliquet nisl, eu aliquet eros aliquam nec. Donec id accumsan mauris. Suspendisse potenti. Vestibulum iaculis id sem eu egestas. Quisque sollicitudin in massa sit amet suscipit. Nam mattis tristique felis, eget sodales lacus rhoncus tincidunt. Sed quis leo blandit, blandit tellus ut, rutrum nunc. Fusce ut bibendum est, at ultricies augue. Etiam ac convallis libero. Ut suscipit diam nec sodales auctor. Aliquam eu venenatis arcu, ut ullamcorper nunc. Nulla varius interdum mollis.";

        private IEnumerable<KeyValuePair<string, string>> testcases =
            @"0705-123456  :: MATCH
+46705123456  :: MATCH
0705123456 :: MATCH
0854055509 :: MATCH
+46854055509 :: MATCH
0046854055509 :: MATCH
08 540 555 09 :: MATCH
08 54 05 55 09 :: MATCH
520-555-5542 :: MATCH 
520.555.5542 :: MATCH 
5205555542 :: MATCH 
520 555 5542 :: MATCH 
520) 555-5542 :: FAIL 
(520 555-5542 :: FAIL 
(520)555-5542 :: MATCH 
(520) 555-5542 :: MATCH 
(520) 555 5542 :: MATCH 
520-555.5542 :: MATCH 
520 555-0555 :: MATCH 
(520)5555542 :: MATCH 
520.555-4523 :: MATCH 
19991114444 :: FAIL 
19995554444 :: MATCH 
514 555 1231 :: MATCH 
1 555 555 5555 :: MATCH 
1.555.555.5555 :: MATCH 
1-555-555-5555 :: MATCH 
520) 555-5542 :: FAIL 
(520 555-5542 :: FAIL 
1(555)555-5555 :: MATCH".Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(
                            line => line.Split(new[] { "::" }, StringSplitOptions.None).Select(column => column.Trim()).ToArray()
                        ).Select(line => new KeyValuePair<string, string>(line[0], line[1]));

        private string _input;
        [SetUp]
        public void SetUp()
        {
            _input = String.Join(LoremIpsum,
                             testcases.Select(tc => tc.Key));
        }

        [Test]
        public void All_test_cases_supposed_to_be_matched()
        {
            var matches = MatchToken.Tokens(_input).ToArray();
            Assert.That(testcases
                            .Where(testcase => testcase.Value.Equals("MATCH"))
                            .Where(testcase => !matches.Any(m => m.Value == testcase.Key))
                            .Select(testcase => testcase.Key), Is.EquivalentTo(new string[0]));
        }

        [Test]
        public void Should_not_lose_text()
        {
            var matches = MatchToken.Tokens(_input).ToArray();
            Assert.That(matches.JoinToString(), Is.EqualTo(_input));
        }

        [Test]
        public void A_phone_number_surrounded_by_text_should_not_mean_loss_of_text()
        {
            var format = string.Format("{0} 520-555.5542 {0}", LoremIpsum);
            Assert.That(MatchToken.Tokens(format).ToArray().JoinToString(), Is.EqualTo(format));
        }
    }
}