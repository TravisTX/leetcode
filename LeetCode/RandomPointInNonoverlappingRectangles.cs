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
    public class RandomPointInNonoverlappingRectangles
    {
        // https://leetcode.com/problems/random-point-in-non-overlapping-rectangles/

        [DataTestMethod]
        //[DataRow("[[1,1,5,5]]")]
        [DataRow("[[-2,-2,-1,-1],[1,0,3,0]]")]
        //[DataRow("[[82918473, -57180867, 82918476, -57180863], [83793579, 18088559, 83793580, 18088560], [66574245, 26243152, 66574246, 26243153], [72983930, 11921716, 72983934, 11921720]]")]
        //[DataRow("[[-90422366, -77205020, -90420883, -77204118], [16757635, 1406232, 16758197, 1407356], [84960435, -41134310, 84960461, -41132813], [-21695995, 75043524, -21695341, 75044946], [4298878, 67299889, 4300319, 67301577], [-97852729, 49631103, -97851872, 49632435], [-60241254, -7446141, -60240732, -7444393]]")]
        //[DataRow("[[99358434, 62418790, 99360410, 62419739], [9949520, 63556732, 9949788, 63556965]]")]
        public void Test(string inputStr)
        {
            var input = JsonConvert.DeserializeObject<int[][]>(inputStr);
            var s = new Solution(input);
            var output = s.Pick();
            // didn't bother writing assertions becuase this has randomized output
            //output.Should().BeEquivalentTo(new int[0]);
        }

        public class Solution
        {
            private readonly int[][] rects;
            private readonly int[] weights;
            int searchSpace = 0;
            Random rand = new Random();

            public Solution(int[][] rects)
            {
                this.rects = rects;
                weights = new int[rects.Length];
                for (var i = 0; i < rects.Length; ++i)
                {
                    var rect = rects[i];
                    var size = (rect[2] - rect[0] + 1) * (rect[3] - rect[1]) + 1;
                    searchSpace += size;
                    weights[i] = searchSpace;
                }
            }

            public int[] Pick()
            {
                var weight = rand.Next(weights.Last()) + 1;
                var i = Array.BinarySearch(weights, weight);
                if (i < 0)
                {
                    i = -i - 1;
                }

                var rect = rects[i];
                var rndX = rand.Next(rect[0], rect[2] + 1);
                var rndY = rand.Next(rect[1], rect[3] + 1);
                return new int[] { rndX, rndY };
            }
        }
    }
}