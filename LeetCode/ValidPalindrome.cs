using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    [TestClass]
    public class ValidPalindromeProblem
    {
        /*

Given a string, determine if it is a palindrome, considering only alphanumeric characters and ignoring cases.

Note: For the purpose of this problem, we define empty string as valid palindrome.

Example 1:

Input: "A man, a plan, a canal: Panama"
Output: true
Example 2:

Input: "race a car"
Output: false
 

Constraints:

s consists only of printable ASCII characters.


         */

        [TestMethod]
        public void Test_Simple()
        {
            var input = "racecar";
            IsPalindrome(input).Should().Be(true);
        }

        [TestMethod]
        public void Test_1()
        {
            var input = "A man, a plan, a canal: Panama";
            IsPalindrome(input).Should().Be(true);
        }

        [TestMethod]
        public void Test_2()
        {
            var input = "race a car";
            IsPalindrome(input).Should().Be(false);
        }
        [TestMethod]
        public void Test_3()
        {
            var input = ".,";
            IsPalindrome(input).Should().Be(true);
        }
        [TestMethod]
        public void Test_4()
        {
            var input = "";
            IsPalindrome(input).Should().Be(true);
        }

        public bool IsPalindrome(string s)
        {
            s = s.ToLower();
            int left = 0;
            int right = s.Length - 1;
            while (left < right)
            {
                while (!IsAlphaNumeric(s[left]) && left < s.Length - 1)
                {
                    ++left;
                }
                while (!IsAlphaNumeric(s[right]) && right > 0)
                {
                    --right;
                }

                if (left >= right)
                {
                    break;
                }

                if (s[left] != s[right])
                {
                    return false;
                }

                ++left;
                --right;
            }
            return true;
        }

        private bool IsAlphaNumeric(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9');
        }
    }
}
