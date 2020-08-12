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
    public class VerticalOrderTraversalOfaBinaryTree
    {
        /*



         */

        [TestMethod]
        [DataTestMethod]
        [DataRow("3,9,20,null,null,15,7", "[[9],[3,15],[20],[7]]")]
        [DataRow("0,8,1,null,null,3,2,null,4,5,null,null,7,6", "[[8],[0,3,6],[1,4,5],[2,7]]")]
        [DataRow("0,5,1,9,null,2,null,null,null,null,3,4,8,6,null,null,null,7", "[[9,7],[5,6],[0,2,4],[1,3],[8]]")]
        public void Test(string inputStr, string expectedStr)
        {
            var input = BinaryTreeConvert.Deserialize(inputStr);
            var output = VerticalTraversal(input);

            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expectedStr);
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
    }
}
