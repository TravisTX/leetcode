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
    public class PascalsTriangle2
    {
        // https://leetcode.com/problems/pascals-triangle-ii/

        /*

0              1
1             1,1
2            1,2,1
3           1,3,3,1
4          1,4,6,4,1
5        1,5,10,10,5,1

        */
        [TestMethod]
        [DataTestMethod]
        [DataRow(0, "[1]")]
        [DataRow(3, "[1,3,3,1]")]
        [DataRow(4, "[1,4,6,4,1]")] // "[[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]"
        [DataRow(5, "[1,5,10,10,5,1]")]
        public void Test(int input, string expected)
        {
            var output = GetRow(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public IList<int> GetRow(int rowIndex)
        {
            IList<int> prevRow = new int[] { 1 };
            IList<int> currRow = new int[] { 1 };

            if (rowIndex == 0) return currRow;


            for (var i = 1; i <= rowIndex; ++i)
            {
                currRow = new int[i + 1];
                currRow[0] = 1;
                currRow[i] = 1;
                for(var j = 1; j < i; ++j)
                {
                    currRow[j] = prevRow[j - 1] + prevRow[j];
                }
                prevRow = currRow;
            }

            return currRow;
        }
    }
}
