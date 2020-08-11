using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    public class DetectCapital
    {
        /*
Given a word, you need to judge whether the usage of capitals in it is right or not.

We define the usage of capitals in a word to be right when one of the following cases holds:

All letters in this word are capitals, like "USA".
All letters in this word are not capitals, like "leetcode".
Only the first letter in this word is capital, like "Google".
Otherwise, we define that this word doesn't use capitals in a right way.
 

Example 1:

Input: "USA"
Output: True
 

Example 2:

Input: "FlaG"
Output: False
 

Note: The input will be a non-empty word consisting of uppercase and lowercase latin letters.



         */

        public void Solve()
        {
            var result = DetectCapitalUse("flag");
            Console.WriteLine(result);
        }

        public bool DetectCapitalUse(string word)
        {
            bool firstLetterCap = IsCapital(word[0]);
            int capitalCount = 0;
            for (int i = 0; i < word.Length; ++i)
            {
                if (IsCapital(word[i]))
                {
                    capitalCount++;
                }
            }
            if (capitalCount == 0)
            {
                return true;
            }
            if (capitalCount == word.Length)
            {
                return true;
            }
            if (firstLetterCap && capitalCount == 1)
            {
                return true;
            }
            return false;
        }


        private bool IsCapital(char character)
        {
            // todo: optimize
            return character.ToString().ToUpper() == character.ToString();
        }

    }
}
