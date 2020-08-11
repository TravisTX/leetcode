using FluentAssertions;
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
    public class VerticalOrderTraversalOfaBinaryTree
    {
        /*



         */

        [TestMethod]
        public void Test_1()
        {
            //Input:[3,9,20,null,null,15,7]
            //Output:[[9],[3,15],[20],[7]]


            var inputTree = new TreeNode(3,
                new TreeNode(9),
                new TreeNode(20,
                    new TreeNode(15),
                    new TreeNode(7)));
            var output = VerticalTraversal(inputTree);

            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be("[[9],[3,15],[20],[7]]");
        }

        [TestMethod]
        public void Test_Create()
        {
            //Input:[3,9,20,null,null,15,7]
            //Output:[[9],[3,15],[20],[7]]

            var input = new int?[] { 3, 9, 20, null, null, 15, 7 };
            var root = CreateBinaryTree(input, 0);

            //var input = new TreeNode(3,
            //    new TreeNode(9),
            //    new TreeNode(20,
            //        new TreeNode(15),
            //        new TreeNode(7)));
            //var output = VerticalTraversal(input);

            var outputStr = JsonConvert.SerializeObject(root);
        }

        [TestMethod]
        public void Test_3()
        {
            // input
            //[0,8,1,null,null,3,2,null,4,5,null,null,7,6]
            //Expected:
            //[[8],[0,3,6],[1,4,5],[2,7]]

            var inputTree = new TreeNode(0,
                new TreeNode(8),
                new TreeNode(1,
                    new TreeNode(3, null, new TreeNode(4, null, new TreeNode(7))),
                    new TreeNode(2, new TreeNode(5, new TreeNode(6)))));
            var output = VerticalTraversal(inputTree);

            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be("[[8],[0,3,6],[1,4,5],[2,7]]");
        }

        [TestMethod]
        public void Test_4()
        {
            // Input:
            // [0,5,1,9,null,2,null,null,null,null,3,4,8,6,null,null,null,7]
            // Output:
            // [[9,7],[5,6],[0,2,4],[1,3],[8]]

            var inputTree = new TreeNode(0,
                new TreeNode(5, new TreeNode(9)),
                new TreeNode(1,
                    new TreeNode(2, null, new TreeNode(3,
                    new TreeNode(4, new TreeNode(6, new TreeNode(7))),
                    new TreeNode(8)))));
            var output = VerticalTraversal(inputTree);

            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be("[[9,7],[5,6],[0,2,4],[1,3],[8]]");
        }

        private TreeNode CreateBinaryTree(int?[] arr, int i)
        {
            TreeNode root = null;
            // Base case for recursion 
            if (i < arr.Length)
            {
                var value = arr[i];
                if (value.HasValue)
                {
                    TreeNode temp = new TreeNode(value.Value);
                    root = temp;

                    // insert left child 
                    root.left = CreateBinaryTree(arr, 2 * i + 1);

                    // insert right child 
                    root.right = CreateBinaryTree(arr, 2 * i + 2);

                }
            }
            return root;
        }

        public IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            var result = new List<IList<int>>();
            var itemList = new List<PositionedItem>();
            PopulateItemList(itemList, root, 0, 0);

            itemList = itemList.OrderBy(x => x.x).ThenBy(x => x.y).ThenBy(x => x.val).ToList();

            int lastX = int.MinValue;
            var lastResultGroup = new List<int>();
            foreach (var item in itemList)
            {
                if (item.x != lastX)
                {
                    lastResultGroup = new List<int>();
                    result.Add(lastResultGroup);
                    lastX = item.x;
                }
                lastResultGroup.Add(item.val);
            }
            return result;
        }

        private void PopulateItemList(List<PositionedItem> itemList, TreeNode root, int x, int y)
        {
            itemList.Add(new PositionedItem
            {
                x = x,
                y = y,
                val = root.val
            });

            if (root.left != null)
            {
                PopulateItemList(itemList, root.left, x - 1, y + 1);
            }
            if (root.right != null)
            {
                PopulateItemList(itemList, root.right, x + 1, y + 1);
            }
        }

        public class PositionedItem
        {
            public int x;
            public int y;
            public int val;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }
}
