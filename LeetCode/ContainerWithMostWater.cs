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
    public class ContainerWithMostWater
    {
        // https://leetcode.com/problems/container-with-most-water/

        [TestMethod]
        [DataTestMethod]
        [DataRow("[1,8,6,2,5,4,8,3,7]", 49)]
        [DataRow("[2,3,4,5,18,17,6]", 17)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            MaxArea(input).Should().Be(expected);
        }

        public int MaxArea(int[] height)
        {
            var leftIndex = 0;
            var rightIndex = height.Length - 1;
            var maxArea = GetArea(height, leftIndex, rightIndex);

            while (leftIndex < rightIndex - 1)
            {
                if (height[leftIndex] < height[rightIndex])
                {
                    leftIndex++;
                }
                else
                {
                    rightIndex--;
                }

                var newArea = GetArea(height, leftIndex, rightIndex);
                maxArea = Math.Max(maxArea, newArea);
            }
            return maxArea;
        }

        private int GetArea(int[] height, int l, int r)
        {
            return Math.Min(height[l], height[r]) * (r - l);
        }

        // brute force
        //public int MaxArea(int[] height)
        //{
        //    var result = 0;
        //    for (var i = 0; i < height.Length; ++i)
        //    {
        //        for (var j = 0; j < height.Length; ++j)
        //        {
        //            if (i == j) continue;

        //            var wall1 = height[i];
        //            var wall2 = height[j];
        //            var minWall = Math.Min(wall1, wall2);
        //            var area = minWall * Math.Abs(i - j);
        //            result = Math.Max(result, area);
        //        }
        //    }
        //    return result;
        //}
    }
}
