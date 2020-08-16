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
            var s = S;
            var stack = new Stack<TreeNode>();

            // prime the stack with the root node
            var prevSegment = GetNextSegment(s);
            s = s.Substring(prevSegment.segmentLength);
            TreeNode root = new TreeNode { val = prevSegment.val };
            stack.Push(root);

            while (s.Length > 0)
            {
                var segment = GetNextSegment(s);
                s = s.Substring(segment.segmentLength);
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
                    for(var i = 0; i <= prevSegment.depth - segment.depth; ++i)
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

        public (int depth, int val, int segmentLength) GetNextSegment(string s)
        {
            var foundNumber = false;
            var depth = 0;
            var valStr = "";
            var segmentLength = 0;
            for (var i = 0; i < s.Length; ++i)
            {
                if (foundNumber && s[i] == '-')
                {
                    break;
                }

                if (s[i] == '-')
                {
                    depth++;
                }
                else
                {
                    foundNumber = true;
                    valStr += s[i];
                }
                segmentLength++;
            }
            return (depth, int.Parse(valStr), segmentLength);
        }
    }
}
