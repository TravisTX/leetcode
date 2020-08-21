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
    public class SortArrayByParityProblem
    {
        // https://leetcode.com/problems/sort-array-by-parity/

        [DataTestMethod]
        [DataRow("[3,1,2,4]", "[2,4,3,1]")]
        [DataRow(
            "[3363,4833,290,3381,4227,1711,1253,2984,2212,874,2358,2049,2846,2543,1557,1786,4189,1254,2803,62,3708,1679,228,1404,1200,4766,1761,1439,1796,4735,3169,3106,3578,1940,2072,3254,7,961,1672,1197,3187,1893,4377,2841,2072,2011,3509,2091,3311,233]",
            "[1200,1940,1796,2072,3254,4766,1404,228,3708,62,1672,1254,3578,1786,2984,290,2072,2212,874,3106,2846,2358,4377,961,1893,2841,7,1197,2011,3509,2091,3187,3363,4735,4833,3381,4227,1711,1253,2049,3169,2543,4189,2803,1679,3311,1761,1439,1557,233]")]
        public void Test(string inputStr, string expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var output = SortArrayByParity(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public int[] SortArrayByParity(int[] A)
        {
            Array.Sort(A, (a, b) => { return (a % 2).CompareTo(b % 2); });
            return A;
        }


        // too much memory
        //public int[] SortArrayByParity(int[] A)
        //{
        //    var output = new List<int>();
        //    var odds = new List<int>();
        //    for(var i = 0; i < A.Length; ++i)
        //    {
        //        var num = A[i];
        //        if (num % 2 == 0)
        //        {
        //            output.Add(num);
        //        }
        //        else
        //        {
        //            odds.Add(num);
        //        }
        //    }

        //    output.AddRange(odds);
        //    return output.ToArray();
        //}
    }
}