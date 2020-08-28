using FluentAssertions;
using LeetCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class ImplementRand10UsingRand7
    {
        // https://leetcode.com/problems/implement-rand10-using-rand7/

        [TestMethod]
        public void Test()
        {
            var s = new Solution();
            var output = s.Rand10();
        }

        public class Solution : SolBase
        {
            public int Rand10()
            {
                var r5 = int.MaxValue;
                while(r5 > 5)
                {
                    r5 = Rand7();
                }

                var doubler = 4;
                while (doubler == 4)
                {
                    doubler = Rand7();
                }

                if (doubler > 4)
                {
                    return r5 + 5;
                }
                else
                {
                    return r5;
                }
            }
        }

        public class SolBase
        {
            public int Rand7()
            {
                return new Random().Next(1, 7);
            }
        }
    }
}