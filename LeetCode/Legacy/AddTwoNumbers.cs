using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    public class AddTwoNumbers
    {
        /*
         * https://leetcode.com/problems/add-two-numbers/
You are given two non-empty linked lists representing two non-negative integers. The digits are stored in reverse order
        and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.

You may assume the two numbers do not contain any leading zero, except the number 0 itself.

Example:

Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
Output: 7 -> 0 -> 8
Explanation: 342 + 465 = 807.

         */

        public void Solve()
        {
            ListNode l1 = new ListNode(2, new ListNode(4, new ListNode(3)));
            ListNode l2 = new ListNode(5, new ListNode(6, new ListNode(4)));

            var result = AddTwoNumbersSolve(l1, l2);
            while (result != null)
            {
                Console.WriteLine(result.val);
                result = result.next;
            }
        }

        public ListNode AddTwoNumbersSolve(ListNode l1, ListNode l2)
        {
            ListNode answer = new ListNode(0);
            ListNode lastAnswerNode = answer;
            int carry = 0;
            while (true)
            {
                var newValue = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
                carry = newValue / 10;
                newValue = newValue % 10;

                lastAnswerNode.next = new ListNode(newValue);
                lastAnswerNode = lastAnswerNode.next;
                l1 = l1?.next;
                l2 = l2?.next;
                if (l1 == null && l2 == null)
                {
                    break;
                }
            }
            if (carry > 0)
            {
                lastAnswerNode.next = new ListNode(carry);
            }
            return answer.next;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

    }
}
