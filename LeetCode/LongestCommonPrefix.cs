using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    // https://leetcode.com/problems/longest-common-prefix/

    [TestClass]
    public class LongestCommonPrefixProblem
    {
        /*

         */

        [TestMethod]
        public void Test_1()
        {
            var input = new string[] { "flower", "flow", "flight" };
            LongestCommonPrefix(input).Should().Be("fl");
        }

        [TestMethod]
        public void Test_2()
        {
            var input = new string[] { "dog", "racecar" };
            LongestCommonPrefix(input).Should().Be("");
        }

        [TestMethod]
        public void Test_3()
        {
            var input = new string[] { };
            LongestCommonPrefix(input).Should().Be("");
        }

        [TestMethod]
        public void Test_4()
        {
            var input = new string[] { "a" };
            LongestCommonPrefix(input).Should().Be("a");
        }

        [TestMethod]
        public void Test_5()
        {
            var input = new string[] { "c", "c" };
            LongestCommonPrefix(input).Should().Be("c");
        }

        [TestMethod]
        public void Test_6()
        {
            var input = new string[] { "aa", "a" };
            LongestCommonPrefix(input).Should().Be("a");
        }

        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
            {
                return "";
            }

            for (var charIndex = 0; charIndex < strs[0].Length; ++charIndex)
            {
                char currChar = strs[0][charIndex];
                bool conflictFound = false;
                for (var i = 1; i < strs.Length; ++i)
                {
                    var str = strs[i];
                    if (charIndex > str.Length - 1)
                    {
                        conflictFound = true;
                    }

                    if (!conflictFound && str[charIndex] != currChar)
                    {
                        conflictFound = true;
                    }
                }

                if (conflictFound)
                {
                    return strs[0].Substring(0, charIndex);
                }

                if (charIndex == strs[0].Length - 1)
                {
                    return strs[0];
                }
            }
            return "";
        }
    }
}
