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
    public class NumberOfStepsToReduceNumberToZero
    {
        // https://leetcode.com/problems/number-of-steps-to-reduce-a-number-to-zero/

        [TestMethod]
        [DataTestMethod]
        [DataRow(14, 6)]
        [DataRow(8, 4)]
        public void Test(int input, int expected)
        {
            NumberOfSteps(input).Should().Be(expected);
        }

        public int NumberOfSteps(int num)
        {
            var steps = 0;
            while(num > 0)
            {
                if (num %2 == 0)
                {
                    num /= 2;
                }
                else
                {
                    num -= 1;
                }
                steps++;
            }
            return steps;
        }
    }
}
