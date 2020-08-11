using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    public class ReverseInteger
    {
        // https://leetcode.com/problems/reverse-integer/

        /*
            Given a 32-bit signed integer, reverse digits of an integer.

            Example 1:
            Input: 123
            Output: 321

            Example 2:
            Input: -123
            Output: -321

            Example 3:
            Input: 120
            Output: 21

            Note:
            Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−231,  231 − 1]. For the purpose of this problem, assume that your function returns 0 when the reversed integer overflows.
         */

        public void Solve()
        {
            var input = 120;
            var result = Reverse(input);
            Console.WriteLine(result);

        }

        public int Reverse(int x)
        {
            var neg = x < 0;
            var input = x.ToString();
            string output = "";
            for (int i = input.Length - 1; i >= 0; --i)
            {
                if (!int.TryParse(input[i].ToString(), out _))
                {
                    continue;
                }

                output += input[i];
            }

            var outInt = int.Parse(output);
            if (neg)
            {
                outInt *= -1;
            }
            return outInt;

        }
    }
}
