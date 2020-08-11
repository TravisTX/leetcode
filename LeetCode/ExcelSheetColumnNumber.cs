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
    public class ExcelSheetColumnNumber
    {
        /*

        A > 1
        B > 2
        ..
        Z > 26
        AA > 27
        AB > 28
        ..
        AZ > 52
        BA > 53
        ..
        ZZ > 


        AA > 1*26,1

        BA > 2*26,1

        ZZ > 26*26,26 = 702

        AAA > 1*26*26,1*26,1 = 703


        charVal = charnum * Math.Pow(26, i)
        sum += charVal

         */

        [TestMethod]
        public void Test_A()
        {
            TitleToNumber("A").Should().Be(1);
        }

        [TestMethod]
        public void Test_AB()
        {
            TitleToNumber("AB").Should().Be(28);
        }

        [TestMethod]
        public void Test_ZY()
        {
            TitleToNumber("ZY").Should().Be(701);
        }

        [TestMethod]
        public void Test_FXSHRXW()
        {
            TitleToNumber("FXSHRXW").Should().Be(2147483647);
        }

        public int TitleToNumber(string s)
        {
            var sum = 0;
            var pos = 0;
            for (var i = s.Length - 1; i >= 0; --i)
            {
                int charNum = GetCharNum(s[i]);
                sum += charNum * (int)Math.Pow(26, pos);
                ++pos;
            }
            return sum;
        }

        private int GetCharNum(char c)
        {
            return (int)c - 'A' + 1;
        }
    }
}
