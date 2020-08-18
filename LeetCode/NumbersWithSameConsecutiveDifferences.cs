using FluentAssertions;
using LeetCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class NumbersWithSameConsecutiveDifferences
    {
        // https://leetcode.com/problems/numbers-with-same-consecutive-differences/

        /*
         
        N=4
        K=1

        1010
        1212
        1232
        1234

        algo:
        recursive function that accepts the num so far, N, K
        and returns an array of final nums

        function will call itself with all possible next characters, 
        appending to the final nums

         
         */

        [TestMethod]
        [DataTestMethod]
        [DataRow(3, 7, "[181,292,707,818,929]")]
        [DataRow(2, 1, "[10,12,21,23,32,34,43,45,54,56,65,67,76,78,87,89,98]")]
        [DataRow(1, 0, "[0,1,2,3,4,5,6,7,8,9]")]
        [DataRow(2, 0, "[11,22,33,44,55,66,77,88,99]")]
        public void Test(int N, int K, string expected)
        {
            var output = NumsSameConsecDiff(N, K);
            Array.Sort(output);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public int[] NumsSameConsecDiff(int N, int K)
        {
            var resultList = new List<int>();

            if (N == 1)
            {
                return new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }

            for(var i = 1; i <= 9; ++i)
            {
                resultList.AddRange(NumSameConsecDiffImpl(i, N - 1, K));
            }

            var result = resultList.ToArray();
            return result;
        }

        public List<int> NumSameConsecDiffImpl(int curr, int n, int k)
        {
            var results = new List<int>();

            if (n == 0)
            {
                results.Add(curr);
                return results;
            }

            var lastDigit = curr % 10;

            if (lastDigit >= k)
            {
                results.AddRange(NumSameConsecDiffImpl((curr * 10) + (lastDigit - k), n - 1, k));
            }

            if (lastDigit <= 9 - k && k > 0)
            {
                results.AddRange(NumSameConsecDiffImpl((curr * 10) + (lastDigit + k), n - 1, k));
            }

            return results;
        }
    }
}