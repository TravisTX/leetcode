using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    [TestClass]
    public class LongestPalindromeProblem
    {
        // https://leetcode.com/problems/longest-palindrome/

        [TestMethod]
        [DataTestMethod]
        [DataRow("abccccdd", 7)] // dccaccd
        public void Test(string input, int expected)
        {
            LongestPalindrome(input).Should().Be(expected);
        }

        public int LongestPalindrome (string s)
        {
            var palindromeSize = 0;
            var singleExists = false;

            var dictionary = new Dictionary<char, int>();
            for(var i = 0; i < s.Length; ++i)
            {
                var c = s[i];
                if (dictionary.ContainsKey(c))
                {
                    dictionary[c]++;
                }
                else
                {
                    dictionary[c] = 1;
                }
            }

            foreach (var item in dictionary)
            {
                var count = (item.Value / 2) * 2;
                palindromeSize += count;
                if (!singleExists && item.Value % 2 != 0)
                {
                    singleExists = true;
                }
            }
            if (singleExists)
            {
                palindromeSize++;
            }
            return palindromeSize;
        }
    }
}
