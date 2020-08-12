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
    public class ShuffleTheArray
    {
        /*
            https://leetcode.com/problems/shuffle-the-array/
         */

        [TestMethod]
        [DataTestMethod]
        [DataRow("[2,5,1,3,4,7]", 3, "[2,3,5,4,1,7]")]
        public void Test(string inputStr, int n, string expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var output = Shuffle(input, n);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public int[] Shuffle(int[] nums, int n)
        {
            var result = new int[nums.Length];

            for (var i = 0; i < n; ++i)
            {
                result[i * 2] = nums[i];
                result[(i * 2) + 1] = nums[i + n];
            }

            return result;
        }
    }
}
