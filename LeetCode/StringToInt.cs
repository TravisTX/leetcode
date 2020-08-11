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
    public class StringToInt
    {
        /*

        Implement atoi which converts a string to an integer.
        
        The function first discards as many whitespace characters as necessary until the
        first non-whitespace character is found. Then, starting from this character, takes an optional 
        initial plus or minus sign followed by as many numerical digits as possible, and interprets them as a numerical value.

        The string can contain additional characters after those that form the integral number, which are ignored 
        and have no effect on the behavior of this function.

        If the first sequence of non-whitespace characters in str is not a valid integral number, or if no such sequence 
        exists because either str is empty or it contains only whitespace characters, no conversion is performed.

        If no valid conversion could be performed, a zero value is returned.

        Note:

        Only the space character ' ' is considered as whitespace character.
        Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−231,  231 − 1]. If the numerical value is out of the range of representable values, INT_MAX (231 − 1) or INT_MIN (−231) is returned.
        Example 1:

        Input: "42"
        Output: 42
        Example 2:

        Input: "   -42"
        Output: -42
        Explanation: The first non-whitespace character is '-', which is the minus sign.
                     Then take as many numerical digits as possible, which gets 42.
        Example 3:

        Input: "4193 with words"
        Output: 4193
        Explanation: Conversion stops at digit '3' as the next character is not a numerical digit.
        Example 4:

        Input: "words and 987"
        Output: 0
        Explanation: The first non-whitespace character is 'w', which is not a numerical 
                     digit or a +/- sign. Therefore no valid conversion could be performed.
        Example 5:

        Input: "-91283472332"
        Output: -2147483648
        Explanation: The number "-91283472332" is out of the range of a 32-bit signed integer.
                     Thefore INT_MIN (−231) is returned.
    




         */

        [TestMethod]
        public void Test_1()
        {
            MyAtoi("42").Should().Be(42);
        }

        [TestMethod]
        public void Test_2()
        {
            MyAtoi("   -42").Should().Be(-42);
        }

        [TestMethod]
        public void Test_3()
        {
            MyAtoi("4193 with words").Should().Be(4193);
        }

        [TestMethod]
        public void Test_4()
        {
            MyAtoi("words and 987").Should().Be(0);
        }

        [TestMethod]
        public void Test_5()
        {
            MyAtoi("-91283472332").Should().Be(-2147483648);
        }

        [TestMethod]
        public void Test_6()
        {
            MyAtoi("20000000000000000000").Should().Be(2147483647);
        }

        [TestMethod]
        public void Test_7()
        {
            MyAtoi("-2147483647").Should().Be(-2147483647);
        }


        public int MyAtoi(string str)
        {
            bool numStrFound = false;
            var numStr = "";
            bool isPositive = true;
            for (var i = 0; i < str.Length; ++i)
            {
                var c = str[i];
                if (numStrFound == false)
                {
                    if (c == ' ')
                    {
                        continue;
                    }

                    if (c == '+')
                    {
                        numStrFound = true;
                        continue;
                    }
                    if (c == '-')
                    {
                        numStrFound = true;
                        isPositive = false;
                        continue;
                    }

                    if (!IsNumber(c))
                    {
                        break;
                    }
                }

                if (IsNumber(c))
                {
                    numStrFound = true;
                    numStr += c;
                }

                if (numStrFound == true)
                {
                    if (!IsNumber(c))
                    {
                        break;
                    }
                }
            }

            if (numStr == "")
            {
                return 0;
            }

            return IntParse(numStr, isPositive);
        }

        private bool IsNumber(char c)
        {
            return c >= '0' && c <= '9';
        }

        private int IntParse(string s, bool isPositive)
        {
            int result = 0;
            int sign = isPositive ? 1 : -1;
            for(var i = 0; i < s.Length; ++i)
            {
                var newDigit = (s[i] - '0') * sign;

                if (isPositive && (
                    result > int.MaxValue / 10 ||
                    (result == int.MaxValue / 10 && newDigit > int.MaxValue % 10)))
                {
                    return int.MaxValue;
                }

                if (!isPositive && (
                    result < int.MinValue / 10 ||
                    (result == int.MinValue / 10 && newDigit < int.MinValue % 10)))
                {
                    return int.MinValue;
                }

                if (result > int.MaxValue)
                {
                    return int.MaxValue;
                }

                if (result < int.MinValue)
                {
                    return int.MinValue;
                }

                result *= 10;
                result += newDigit;
            }

            return result;
        }
    }
}
