using FluentAssertions;
using LeetCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class GoatLatin
    {
        // 
        [TestMethod]
        [DataTestMethod]
        [DataRow("I speak Goat Latin", "Imaa peaksmaaa oatGmaaaa atinLmaaaaa")]
        [DataRow("The quick brown fox jumped over the lazy dog", "heTmaa uickqmaaa rownbmaaaa oxfmaaaaa umpedjmaaaaaa overmaaaaaaa hetmaaaaaaaa azylmaaaaaaaaa ogdmaaaaaaaaaa")]
        public void Test(string input, string expected)
        {
            ToGoatLatin(input).Should().Be(expected);
        }

        public string ToGoatLatin(string S)
        {
            var output = new StringBuilder();
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
            var words = S.Split(' ');
            var suffix = "maa";
            for (var i = 0; i < words.Length; ++i)
            {
                var word = words[i];
                var firstChar = word[0];
                if (vowels.Contains(firstChar))
                {
                    output.Append(word);
                }
                else
                {
                    output.Append(word.Substring(1)).Append(firstChar);
                }
                output.Append(suffix).Append(" ");

                suffix += "a";
            }

            return output.ToString().Trim();
        }
    }
}