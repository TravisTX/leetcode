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
    public class ReorderListProblem
    {
        // https://leetcode.com/problems/reorder-list/

        /*

        Given 1->2->3->4, reorder it to 1->4->2->3.

        put every node in a stack
        
        while true
        curr = node 1
        temp = curr.next (2)
        curr.next = pop (node 4)
        pop.next = temp
        curr = temp (node 2)

        curr = node 2
        temp = curr.next (3)
        pop = node 3

        temp == pop, break

        --
        Given 1->2->3->4->5, reorder it to 1->5->2->4->3.
        curr = 1
        temp = 2
        pop = 5

        curr = 2
        temp = 3
        pop = 4

        curr = 3
        temp = 4
        pop = 3

        curr == pop, break

         */

        [TestMethod]
        public void Test_1()
        {
            // Given 1->2->3->4, reorder it to 1->4->2->3.
            var input = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4))));
            ReorderList(input);
            input.val.Should().Be(1);
            input.next.val.Should().Be(4);
            input.next.next.val.Should().Be(2);
            input.next.next.next.val.Should().Be(3);
        }

        [TestMethod]
        public void Test_2()
        {
            // Given 1->2->3->4->5, reorder it to 1->5->2->4->3.
            var input = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
            ReorderList(input);
            input.val.Should().Be(1);
            input.next.val.Should().Be(5);
            input.next.next.val.Should().Be(2);
            input.next.next.next.val.Should().Be(4);
            input.next.next.next.next.val.Should().Be(3);
        }

        public void ReorderList(ListNode head)
        {
            if (head == null) return;
            ListNode curr = head;
            ListNode temp;
            ListNode pop;
            Stack<ListNode> stack = new Stack<ListNode>();

            // build stack
            while (curr != null)
            {
                stack.Push(curr);
                curr = curr.next;
            }

            curr = head;
            while(true)
            {
                temp = curr.next;
                pop = stack.Pop();
                if (curr == pop || temp == pop)
                {
                    pop.next = null;
                    break;
                }
                curr.next = pop;
                pop.next = temp;
                curr = temp;
            }
        }
    }
}