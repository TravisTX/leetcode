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
    public class HIndexProblem
    {
        /*
        https://leetcode.com/problems/h-index/

         */

        [TestMethod]
        [DataTestMethod]
        [DataRow("[3,0,6,1,5]", 3)]
        [DataRow("[100]", 1)]
        [DataRow("[]", 0)]
        [DataRow("[4,4,0,0]", 2)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            HIndex(input).Should().Be(expected);
        }

        public int HIndex(int[] citations)
        {
            citations = citations.OrderByDescending(x => x).ToArray();
            // 6,5,3,1,0
            // 4,4,0,0
            for (var i = 0; i < citations.Length; ++i)
            {
                if (i + 1 == citations[i])
                {
                    return i + 1;
                }
                if (i + 1 > citations[i])
                {
                    return i;
                }
            }
            return citations.Length;
        }
    }
}
