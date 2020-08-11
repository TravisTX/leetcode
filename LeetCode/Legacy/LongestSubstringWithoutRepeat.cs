using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    public class LongestSubstringWithoutRepeat
    {
        /*
            https://leetcode.com/problems/longest-substring-without-repeating-characters/

            3. Longest Substring Without Repeating Characters
            Given a string, find the length of the longest substring without repeating characters.

            Example 1:
            Input: "abcabcbb"
            Output: 3 
            Explanation: The answer is "abc", with the length of 3. 

            Example 2:
            Input: "bbbbb"
            Output: 1
            Explanation: The answer is "b", with the length of 1.

            Example 3:
            Input: "pwwkew"
            Output: 3
            Explanation: The answer is "wke", with the length of 3. 
             Note that the answer must be a substring, "pwke" is a subsequence and not a substring.
         
         */

        public void Solve()
        {
            string input = "pwwkew";

            var result = LengthOfLongestSubstring(input);
            Console.WriteLine(result);
        }

        public int LengthOfLongestSubstring(string s)
        {
            var set = new HashSet<char>();
            int inputLength = s.Length;
            int ans = 0, i = 0, j = 0;
            while (i < inputLength && j < inputLength)
            {
                if (!set.Contains(s[j]))
                {
                    set.Add(s[j]);
                    j++;
                    ans = Math.Max(ans, j - i);
                }
                else
                {
                    set.Remove(s[i]);
                    i++;
                }
            }
            return ans;

            //for (int i = 0; i < s.Length; ++i)
            //{
            //    string substring = "";
            //    for(int j = i; j < s.Length; ++j)
            //    {
            //        if (substring.Contains(s[j]))
            //        {
            //            break;
            //        }
            //        substring += s[j];
            //    }

            //    if (substring.Length > longestLength)
            //    {
            //        longestLength = substring.Length;
            //    }
            //}
            //return longestLength;
        }
    }
}
