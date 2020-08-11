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
    public class RomanToInteger
    {
        /*

         */

        [TestMethod]
        public void Test_3()
        {
            RomanToInt("III").Should().Be(3);
        }

        [TestMethod]
        public void Test_4()
        {
            RomanToInt("IV").Should().Be(4);
        }

        [TestMethod]
        public void Test_9()
        {
            RomanToInt("IX").Should().Be(9);
        }

        [TestMethod]
        public void Test_58()
        {
            RomanToInt("LVIII").Should().Be(58);
        }

        [TestMethod]
        public void Test_1994()
        {
            RomanToInt("MCMXCIV").Should().Be(1994);
        }

        public int RomanToInt(string s)
        {
            var digits = new Dictionary<char, int>
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 },
            };
            int accum = 0;
            for (var i = 0; i < s.Length; ++i)
            {
                var cur = digits[s[i]];
                if (i < s.Length -1)
                {
                    var next = digits[s[i + 1]];
                    if (cur < next)
                    {
                        cur = next - cur;
                        i++;
                    }
                }
                accum += cur;
            }
            return accum;
        }
    }
}
