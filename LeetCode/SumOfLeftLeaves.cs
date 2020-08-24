using FluentAssertions;
using LeetCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class SumOfLeftLeaves
    {
        // https://leetcode.com/problems/sum-of-left-leaves/

        [DataTestMethod]
        [DataRow("3,9,20,null,null,15,7", 24)]
        [DataRow("1,2,3,4,5", 4)]
        public void Test(string inputStr, int expected)
        {
            var input = BinaryTreeConvert.Deserialize(inputStr);
            var s = new Solution();
            s.SumOfLeftLeaves(input).Should().Be(expected);
        }


        public class Solution
        {
            public int SumOfLeftLeaves(TreeNode root)
            {
                var sum = 0;
                if (root == null) return sum;

                if (root.left != null && root.left.left == null && root.left.right == null)
                {
                    sum += root.left.val;
                }

                sum += SumOfLeftLeaves(root.left);
                sum += SumOfLeftLeaves(root.right);

                return sum;
            }
        }
    }
}