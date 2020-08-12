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
    public class PathSum
    {
        /*
Given a binary tree and a sum, determine if the tree has a root-to-leaf path such that adding up all the values along the path equals the given sum.

Note: A leaf is a node with no children.

Example:

Given the below binary tree and sum = 22,

      5
     / \
    4   8
   /   / \
  11  13  4
 /  \      \
7    2      1
return true, as there exist a root-to-leaf path 5->4->11->2 which sum is 22.




         */

        [TestMethod]
        public void Test_1()
        {
            var input = BinaryTreeConvert.Deserialize("5,4,8,11,null,13,4,7,2,null,null,null,1");
            var output = HasPathSum(input, 22);
            output.Should().Be(true);
        }

        public bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null)
            {
                return false;
            }

            sum -= root.val;
            if (root.left == null && root.right == null)
            {
                return sum == 0;
            }

            return HasPathSum(root.left, sum) || HasPathSum(root.right, sum);
        }
    }
}
