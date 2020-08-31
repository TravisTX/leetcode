using FluentAssertions;
using LeetCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class DeleteNodeInBst
    {
        // https://leetcode.com/problems/delete-node-in-a-bst/

        /*
        
        idea 1:
        find target node and target's parent node using DFS
        call target's left and right orphan1 and orphan2

        DFS down from the root until we find a valid null spot for each orphan


         5
        / \
       3   6
      / \   \
     2   4   7

         5
        / \
       2   6
        \   \
         4   7


         */

        [DataTestMethod]
        [DataRow("5,3,6,2,4,null,7", 3, "5,2,6,null,4,null,7")]
        [DataRow("0", 0, "")]
        [DataRow("2,1,3", 2, "1,null,3")]
        [DataRow("3,1,4,null,2", 1, "3,2,4")]
        [DataRow("1,null,2", 1, "2")]
        public void Test(string inputStr, int key, string expected)
        {
            var root = BinaryTreeConvert.Deserialize(inputStr);
            var s = new Solution();
            var output = s.DeleteNode(root, key);
            var outputStr = BinaryTreeConvert.Serialize(output);
            outputStr.Should().Be(expected);
        }

        public class Solution
        {
            public TreeNode DeleteNode(TreeNode root, int key)
            {
                if (root == null) return null;
                var found = FindNode(null, root, key);
                var parent = found.parent;
                var target = found.target;
                if (target == null) return root;

                var orphan1 = target.left;
                var orphan2 = target.right;

                if (parent == null)
                {
                    // we're removing the root
                    if (orphan1 != null)
                        return PlaceNode(orphan1, orphan2);
                    else
                        return orphan2;
                }

                if (parent.left == target) parent.left = null;
                if (parent.right == target) parent.right = null;

                PlaceNode(root, orphan1);
                PlaceNode(root, orphan2);
                return root;
            }

            public (TreeNode parent, TreeNode target) FindNode(TreeNode parent, TreeNode root, int key)
            {
                if (root == null || root.val == key)
                    return (parent, root);

                if (key < root.val)
                    return FindNode(root, root.left, key);
                else
                    return FindNode(root, root.right, key);
            }

            public TreeNode PlaceNode(TreeNode root, TreeNode orphan)
            {
                if (root == null || orphan == null) return root ?? orphan;

                if (orphan.val < root.val)
                {
                    if (root.left == null)
                    {
                        root.left = orphan;
                        return root;
                    }
                    return PlaceNode(root.left, orphan);
                }

                else
                {
                    if (root.right == null)
                    {
                        root.right = orphan;
                        return root;
                    }
                    return PlaceNode(root.right, orphan);
                }
            }
        }
    }
}