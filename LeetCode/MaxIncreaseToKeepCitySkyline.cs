using FluentAssertions;
using LeetCode.Helpers;
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
    public class MaxIncreaseToKeepCitySkyline
    {
        // https://leetcode.com/problems/max-increase-to-keep-city-skyline/

        /*
        [3,0,8,4],
        [2,4,5,7],
        [9,2,6,3],
        [0,3,1,0]

        maxRows = [8,7,9,3]
        maxCols = [9,4,8,7]
         */


        [TestMethod]
        [DataTestMethod]
        [DataRow("[[3,0,8,4],[2,4,5,7],[9,2,6,3],[0,3,1,0]]", 35)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[][]>(inputStr);
            MaxIncreaseKeepingSkyline(input).Should().Be(expected);
        }

        public int MaxIncreaseKeepingSkyline(int[][] grid)
        {
            var maxRows = new int[grid.Length];
            var maxCols = new int[grid[0].Length];

            for (var x = 0; x < grid.Length; ++x)
            {
                for (var y = 0; y < grid[x].Length; ++y)
                {
                    maxRows[x] = Math.Max(maxRows[x], grid[x][y]);
                    maxCols[y] = Math.Max(maxCols[y], grid[x][y]);
                }
            }

            var increases = 0;
            for (var x = 0; x < grid.Length; ++x)
            {
                for (var y = 0; y < grid[0].Length; ++y)
                {
                    var ceiling = Math.Min(maxRows[x], maxCols[y]);
                    increases += ceiling - grid[x][y];
                }
            }

            return increases;
        }
    }
}
