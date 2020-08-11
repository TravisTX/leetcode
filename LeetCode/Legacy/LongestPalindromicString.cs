using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    public class LongestPalindromicString
    {
        /*
5. Longest Palindromic Substring
Given a string s, find the longest palindromic substring in s. You may assume that the maximum length of s is 1000.

Example 1:

Input: "babad"
Output: "bab"
Note: "aba" is also a valid answer.

Example 2:

Input: "cbbd"
Output: "bb"
         
         */

        public void Solve()
        {
            string input = "babad";

            var result = LongestPalindrome(input);
            Console.WriteLine(result);
        }

        public string LongestPalindrome(string s)
        {
            int longestLeft = 0, longestRight = 0, longestLen = 0;
            for(var i = 0; i < s.Length; ++i)
            {
                var longest1 = LongestFromCenter(s, i, i);
                var longest2 = LongestFromCenter(s, i, i + 1);
                if (longest1.length > longestLen)
                {
                    longestLeft = longest1.left;
                    longestRight = longest1.right;
                    longestLen = longest1.length;
                }
                if (longest2.length > longestLen)
                {
                    longestLeft = longest2.left;
                    longestRight = longest2.right;
                    longestLen = longest2.length;
                }
            }
            if (longestLen == 0)
            {
                return "";
            }
            return s.Substring(longestLeft, longestRight - longestLeft + 1);
        }

        private (int left, int right, int length) LongestFromCenter(string s, int left, int right)
        {
            while(left >= 0 && right < s.Length && s[left] == s[right])
            {
                --left;
                ++right;
            }

            ++left;
            --right;

            return (left, right, right - left + 1);
        }
    }
}
