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
    public class FindElementsInContaminatedBinaryTree
    {
        // https://leetcode.com/problems/find-elements-in-a-contaminated-binary-tree/

        /*
         
        idea 1:
        
        in ctor, "repair" the binary tree and store the repaired tree
        then in Find, walk the tree

        --

        idea 2:
        store the contaminated tree
        in Find, walk the tree, doing the uncontaminate calculation as we go
        
        *this has to be slower than uncontaminating during ctor. not even gonna
        try this.

        --

        idea 3:
        maybe we can do something with the knowledge of how the numbering system works
        ie: 5 is always going to be root.right.left

        the path is deterministic. if target is odd, it must be left, odd must be right

        target == 5 (odd)
        push "L" to stack
        target = target - 1 / 2

        target == 2 (even)
        push "R" to stack
        target = target - 2 / 2

        target == 0
        stack consists of RL, now traverse that path to see if it exists

        we could do this without ever decontaminating

        --

        idea 4: 🎉 most performant 🎉
        we don't actually care about the node, just a true/false for whether it 
        exists during uncontaminate phase, store the values in a hashset and 
        just look up the hash during find.  cheating?


         
         */

        [DataTestMethod]
        [DataRow("-1,null,-1", "[1,2]", "[false,true]")]
        [DataRow("-1,-1,-1,-1,-1", "[1,3,5]", "[true,true,false]")]
        [DataRow("-1,null,-1,-1,null,-1", "[2,3,4,5]", "[true,false,false,true]")]
        public void Test(string inputStr, string inputStr2, string expectedStr)
        {
            var input = BinaryTreeConvert.Deserialize(inputStr);
            var input2 = JsonConvert.DeserializeObject<int[]>(inputStr2);
            var expected = JsonConvert.DeserializeObject<bool[]>(expectedStr);
            var s = new FindElements(input);

            for(var i = 0; i < input2.Length; i++)
            {
                s.Find(input2[i]).Should().Be(expected[i]);
            }
        }

        // idea 3
        //public class FindElements
        //{
        //    TreeNode root;
        //    // true = left
        //    // false = right
        //    Stack<bool> path;

        //    public FindElements(TreeNode root)
        //    {
        //        this.root = root;
        //    }

        //    public bool Find(int target)
        //    {
        //        DeterminePath(target);
        //        var node = root;

        //        while(path.Count > 0)
        //        {
        //            var direction = path.Pop();
        //            if (direction == true)
        //            {
        //                node = node.left;
        //            }
        //            else
        //            {
        //                node = node.right;
        //            }
        //            if (node == null)
        //            {
        //                return false;
        //            }
        //        }
        //        return true;
        //    }

        //    public void DeterminePath(int target)
        //    {
        //        path = new Stack<bool>();
        //        while (target > 0)
        //        {
        //            if (target % 2 == 0)
        //            {
        //                path.Push(false);
        //                target = (target - 2) / 2;
        //            }
        //            else
        //            {
        //                path.Push(true);
        //                target = (target - 1) / 2;
        //            }
        //        }
        //    }
        //}



        // idea 4
        public class FindElements
        {
            HashSet<int> values = new HashSet<int>();

            public FindElements(TreeNode root)
            {
                Uncontaminate(root, 0);
            }

            private void Uncontaminate(TreeNode node, int newVal)
            {
                values.Add(newVal);

                if (node.left != null)
                    Uncontaminate(node.left, 2 * newVal + 1);
                if (node.right != null)
                    Uncontaminate(node.right, 2 * newVal + 2);
            }

            public bool Find(int target)
            {
                return values.Contains(target);
            }
        }


        // idea 1
        //public class FindElements
        //{
        //    TreeNode root;

        //    public FindElements(TreeNode root)
        //    {
        //        this.root = root;
        //        Uncontaminate(this.root, 0);
        //    }

        //    private void Uncontaminate(TreeNode node, int newVal)
        //    {
        //        if (node == null) return;
        //        node.val = newVal;

        //        Uncontaminate(node.left, 2 * newVal + 1);
        //        Uncontaminate(node.right, 2 * newVal + 2);
        //    }

        //    public bool Find(int target)
        //    {
        //        return FindImpl(root, target);
        //    }

        //    public bool FindImpl(TreeNode node, int target)
        //    {
        //        if (node == null) return false;
        //        if (node.val == target) return true;

        //        return FindImpl(node.left, target) || FindImpl(node.right, target);
        //    }
        //}

    }
}