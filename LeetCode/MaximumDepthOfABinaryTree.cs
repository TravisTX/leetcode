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
    public class MaximumDepthOfABinaryTree
    {
        // https://leetcode.com/problems/maximum-depth-of-binary-tree/

        [TestMethod]
        [DataTestMethod]
        [DataRow("3,9,20,null,null,15,7", 3)]
        [DataRow("", 0)]
        public void Test_1(string inputStr, int expected)
        {
            var input = BinaryTreeConvert.Deserialize(inputStr);
            MaxDepth(input).Should().Be(expected);
        }

        public int MaxDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.left == null && root.right == null)
            {
                return 1;
            }

            return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
        }






    }
}
