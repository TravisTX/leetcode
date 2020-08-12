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
    public class PascalsTriangle
    {
        // https://leetcode.com/problems/pascals-triangle/

        [TestMethod]
        [DataTestMethod]
        [DataRow(5, "[[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]")]
        public void Test(int input, string expected)
        {
            var output = Generate(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public IList<IList<int>> Generate(int numRows)
        {
            var result = new List<IList<int>>();
            if (numRows <= 0) return result;
            result.Add(new List<int> { 1 });

            for (var i = 1; i < numRows; ++i)
            {
                var prevRow = result[i - 1];
                var newRow = new int[i + 1];
                newRow[0] = 1;
                newRow[i] = 1;
                for (var j = 1; j < i; ++j)
                {
                    newRow[j] = prevRow[j - 1] + prevRow[j];
                }
                result.Add(newRow);
            }

            return result;
        }
    }
}
