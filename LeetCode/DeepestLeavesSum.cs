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
    public class DeepestLeavesSumProblem
    {
        // https://leetcode.com/problems/deepest-leaves-sum/

        [TestMethod]
        [DataTestMethod]
        [DataRow("1,2,3,4,5,null,6,7,null,null,null,null,8", 15)]
        public void Test_Bfs(string inputStr, int expected)
        {
            var root = BinaryTreeConvert.Deserialize(inputStr);
            DeepestLeavesSumBfs(root).Should().Be(expected);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("1,2,3,4,5,null,6,7,null,null,null,null,8", 15)]
        public void Test_Dfs(string inputStr, int expected)
        {
            var root = BinaryTreeConvert.Deserialize(inputStr);
            DeepestLeavesSumDfs(root).Should().Be(expected);
        }

        // -----------------------
        // BFS
        public int DeepestLeavesSumBfs(TreeNode root)
        {
            var q = new Queue<(int depth, TreeNode node)>();
            q.Enqueue((0, root));

            int lastDepth = 0;
            int sum = 0;

            while (q.Count > 0)
            {
                var depthNode = q.Dequeue();
                if (depthNode.depth != lastDepth)
                {
                    lastDepth = depthNode.depth;
                    sum = 0;
                }

                sum += depthNode.node.val;

                if (depthNode.node.left != null)
                {
                    q.Enqueue((depthNode.depth + 1, depthNode.node.left));
                }
                if (depthNode.node.right != null)
                {
                    q.Enqueue((depthNode.depth + 1, depthNode.node.right));
                }
            }
            return sum;
        }


        // -----------------------
        // DFS

        public int DeepestLeavesSumDfs(TreeNode root)
        {
            var leaves = GetLeaves(root, 0);

            var maxDepth = 0;
            foreach (var item in leaves)
            {
                maxDepth = Math.Max(maxDepth, item.depth);
            }

            var sum = 0;
            foreach (var item in leaves)
            {
                if (item.depth == maxDepth)
                    sum += item.value;
            }
            return sum;
        }

        public List<(int depth, int value)> GetLeaves(TreeNode root, int depth)
        {
            var result = new List<(int depth, int value)>();
            if (root == null) return result;

            if (root.left == null && root.right == null)
            {
                result.Add((depth, root.val));
                return result;
            }

            result.AddRange(GetLeaves(root.left, depth + 1));
            result.AddRange(GetLeaves(root.right, depth + 1));
            return result;
        }
    }
}
