using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Helpers
{
    public class BinaryTreeConvert
    {
        // Encodes a tree to a single string.
        public static string Serialize(TreeNode root)
        {
            if (root == null)
                return string.Empty;

            var sb = new StringBuilder();

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            sb.Append(root.val + ",");

            while (queue.Count > 0)
            {
                var levelSize = queue.Count;

                var levelString = new StringBuilder();
                var hasAtLeastOneChild = false;

                for (int i = 0; i < levelSize; i++)
                {
                    var node = queue.Dequeue();

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                        levelString.Append(node.left.val + ",");
                        hasAtLeastOneChild = true;
                    }
                    else
                    {
                        levelString.Append("null,");
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                        levelString.Append(node.right.val + ",");
                        hasAtLeastOneChild = true;
                    }
                    else
                    {
                        levelString.Append("null,");
                    }
                }

                if (hasAtLeastOneChild)
                {
                    sb.Append(levelString.ToString());
                }
            }

            return sb.ToString().Trim(',');
        }

        // Decodes your encoded data to tree.
        public static TreeNode Deserialize(string data)
        {
            if (data == null || data.Length == 0)
                return null;

            var split = data.Split(',');

            var root = new TreeNode(Convert.ToInt32(split[0]));
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int index = 1;
            while (queue.Count > 0)
            {
                var levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    var visit = queue.Dequeue();

                    string left = "null";
                    string right = "null";
                    if (index < split.Length)
                        left = split[index++];
                    if (index < split.Length)
                        right = split[index++];

                    if (left.CompareTo("null") != 0)
                    {
                        visit.left = new TreeNode(Convert.ToInt32(left));
                        queue.Enqueue(visit.left);
                    }

                    if (right.CompareTo("null") != 0)
                    {
                        visit.right = new TreeNode(Convert.ToInt32(right));
                        queue.Enqueue(visit.right);
                    }
                }
            }

            return root;
        }
    }
}
