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
    public class SumOfNodesWithEvenValuedGrandparents
    {
        // https://leetcode.com/problems/sum-of-nodes-with-even-valued-grandparent/

        /*

        DFS, keeping track of parent and grandparent even
        if grandparent even, add curr to sum

         */

        [TestMethod]
        [DataTestMethod]
        [DataRow("6,7,8,2,7,1,3,9,null,1,4,null,null,null,5", 18)] // 2,7,1,3,5
        public void Test(string inputStr, int expected)
        {
            var input = BinaryTreeConvert.Deserialize(inputStr);
            SumEvenGrandparent(input).Should().Be(expected);
        }

        public int SumEvenGrandparent(TreeNode root)
        {
            return SumEvenGrandparentImpl(root, false, false);
        }

        public int SumEvenGrandparentImpl(TreeNode root, bool parentEven, bool grandParentEven)
        {
            int sum = 0;
            if (root == null) return sum;

            if (grandParentEven)
            {
                sum += root.val;
            }

            var isEven = root.val % 2 == 0;

            sum += SumEvenGrandparentImpl(root.left, isEven, parentEven);
            sum += SumEvenGrandparentImpl(root.right, isEven, parentEven);

            return sum;
        }
    }
}