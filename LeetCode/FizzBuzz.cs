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
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class FizzBuzz
    {
        // https://leetcode.com/problems/fizz-buzz/

        [DataTestMethod]
        [DataRow(15, "[\"1\",\"2\",\"Fizz\",\"4\",\"Buzz\",\"Fizz\",\"7\",\"8\",\"Fizz\",\"Buzz\",\"11\",\"Fizz\",\"13\",\"14\",\"FizzBuzz\"]")]
        public void Test(int input, string expected)
        {
            var s = new Solution();
            var output = s.FizzBuzz(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public class Solution
        {
            public IList<string> FizzBuzz(int n)
            {
                var output = new List<string>();

                for(var i = 1; i <= n; i++)
                {
                    if (i % 15 == 0)
                        output.Add("FizzBuzz");
                    else if (i % 3 == 0)
                        output.Add("Fizz");
                    else if (i % 5 == 0)
                        output.Add("Buzz");
                    else
                        output.Add(i.ToString());
                }

                return output;
            }
        }
    }
}