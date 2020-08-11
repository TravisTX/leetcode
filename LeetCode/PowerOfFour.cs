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
    public class PowerOfFour
    {
        /*

Given an integer (signed 32 bits), write a function to check whether it is a power of 4.

Example 1:

Input: 16
Output: true
Example 2:

Input: 5
Output: false
Follow up: Could you solve it without loops/recursion?




         */

        [TestMethod]
        public void Test_1()
        {
            IsPowerOfFour(16).Should().Be(true);
        }

        [TestMethod]
        public void Test_2()
        {
            IsPowerOfFour(5).Should().Be(false);
        }

        [TestMethod]
        public void Test_3()
        {
            IsPowerOfFour(0).Should().Be(false);
        }

        [TestMethod]
        public void Test_4()
        {
            IsPowerOfFour(4).Should().Be(true);
        }

        public bool IsPowerOfFour(int num)
        {
            for(var i = 0; i < num; ++i)
            {
                var ans = Math.Pow(4, i);
                if (ans == num)
                {
                    return true;
                }
                if (ans > num)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
