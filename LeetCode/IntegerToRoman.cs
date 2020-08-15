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
    public class IntegerToRoman
    {
        // https://leetcode.com/problems/container-with-most-water/

        /*

        Symbol       Value
        I             1
        V             5
        X             10
        L             50
        C             100
        D             500
        M             1000

        There are six instances where subtraction is used:

        I can be placed before V (5) and X (10) to make 4 and 9. 
        X can be placed before L (50) and C (100) to make 40 and 90. 
        C can be placed before D (500) and M (1000) to make 400 and 900.


        algo:
        subtract the largest number you can, and repeat until 0
        if the left most digit starts with a 4 or 9, special case

         */


        [TestMethod]
        [DataTestMethod]
        [DataRow(3, "III")]
        [DataRow(4, "IV")]
        [DataRow(9, "IX")]
        [DataRow(20, "XX")]
        [DataRow(58, "LVIII")] // L=50, V=5, III=3
        [DataRow(1994, "MCMXCIV")] // M = 1000, CM = 900, XC = 90 and IV = 4.

        public void Test(int input, string expected)
        {
            IntToRoman(input).Should().Be(expected);
        }

        public string IntToRoman(int num)
        {
            var output = "";

            var digitMap = new Dictionary<int, string>
            {
                { 1000, "M" },
                { 900, "CM" },
                { 500, "D" },
                { 400, "CD" },
                { 100, "C" },
                { 90, "XC" },
                { 50, "L" },
                { 40, "XL" },
                { 10, "X" },
                { 9, "IX" },
                { 5, "V" },
                { 4, "IV" },
                { 1, "I" },
            };


            while (num > 0)
            {
                foreach (var item in digitMap)
                {
                    if (num - item.Key >= 0)
                    {
                        output += item.Value;
                        num -= item.Key;
                        break;
                    }
                }
            }

            return output;
        }
    }
}
