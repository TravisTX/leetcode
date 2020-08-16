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
    public class RecoverTreeFromPreorderTraversal
    {
        // https://leetcode.com/problems/recover-a-tree-from-preorder-traversal/

        /*

        input: 1-2--3--4-5--6--7
        output: 
            1
         2     5
        3 4   6 7

        if num hyphens increases, go left
        if num hyphens stays the same, go right
        if num hyphens decreases, go up and then right

        --
        stack<node>
        part = 1
            stack.push(1)
        part = -2
            n = new node(2)
            stack.peek().left=n
            stack.push(n)
        part = --3
            n = new node(3)
            stack.peek().left=n
            stack.push(n)
        part = --4
            n = node(4)
            stack.pop(); // because the hyphens stays the same
            stak.peek().right=n
            stack.push(n)
        part = -5
            n = node(5)
            stack.pop().pop() // because the hypens decreased by 1
            stack.peek().right=n
            stack.push(n)

         */


        [TestMethod]
        [DataTestMethod]
        [DataRow("1-2--3--4-5--6--7", "1,2,5,3,4,6,7")]
        [DataRow("1-2--3---4-5--6---7", "1,2,5,3,null,6,null,4,null,7,null")]
        [DataRow("1-401--349---90--88", "1,401,null,349,88,90,null,null,null")]
        public void Test(string inputStr, string expected)
        {
            var output = RecoverFromPreorder(inputStr);
            var outputStr = BinaryTreeConvert.Serialize(output);
            outputStr.Should().Be(expected);
        }

        public TreeNode RecoverFromPreorder(string S)
        {
            var segments = Parse(S);
            var stack = new Stack<TreeNode>();

            // prime the stack with the root node
            var prevSegment = segments[0];
            TreeNode root = new TreeNode { val = prevSegment.val };
            stack.Push(root);

            for(var i = 1; i < segments.Count; ++i)
            {
                var segment = segments[i];
                var node = new TreeNode { val = segment.val };

                if (segment.depth > prevSegment.depth)
                {
                    stack.Peek().left = node;
                }
                if (segment.depth == prevSegment.depth)
                {
                    stack.Pop();
                    stack.Peek().right = node;
                }
                if (segment.depth < prevSegment.depth)
                {
                    for(var j = 0; j <= prevSegment.depth - segment.depth; ++j)
                    {
                        stack.Pop();
                    }
                    stack.Peek().right = node;
                }

                stack.Push(node);
                prevSegment = segment;
            }
            return root;
        }

        public List<(int depth, int val)> Parse(string s)
        {
            var depthValues = new List<(int depth, int val)>();
            Regex regex = new Regex("^(-*)(\\d+)");
            while (s.Length > 0)
            {
                var match = regex.Match(s);
                var depth = match.Groups[1].Value.Length;
                var val = match.Groups[2].Value;
                depthValues.Add((depth, int.Parse(val)));
                s = s.Substring(match.Value.Length);
            }
            return depthValues;
        }
    }
}
