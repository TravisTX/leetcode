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
    public class NonDecreasingArray
    {
        // https://leetcode.com/problems/non-decreasing-array/

        [TestMethod]
        [DataTestMethod]
        [DataRow("[4,2,3]", true)]
        [DataRow("[4,2,1]", false)]
        [DataRow("[5,7,1,8]", true)]
        [DataRow("[3,4,2,3]", false)]
        [DataRow("[1,4,1,2]", true)]
        public void Test(string inputStr, bool expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            CheckPossibility(input).Should().Be(expected);
        }

        public bool CheckPossibility(int[] nums)
        {
            bool adjustmentMade = false;

            for (var i = 1; i < nums.Length; ++i)
            {
                if (nums[i] >= nums[i - 1])
                {
                    continue;
                }

                if (adjustmentMade)
                {
                    // we found a decrease, and we've already made adjustments
                    return false;
                }

                // can we adjust i-1?
                var canAdjustPrev = i <= 1 || nums[i] >= nums[i - 2];

                // can we adjust i?  check if i-1 <= i+1
                var canAdjustCurr = i == nums.Length - 1 || (nums[i - 1] <= nums[i + 1]);

                if (!canAdjustPrev && !canAdjustCurr)
                {
                    return false;
                }

                adjustmentMade = true;
            }

            return true;
        }
    }
}