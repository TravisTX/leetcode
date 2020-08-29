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
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class PancakeSort
    {
        // https://leetcode.com/problems/pancake-sorting/

        /*
         
        idea 1:
        focus on 1 digit at a time: the last unsorted digit

        a = 3,2,4,1

        work on "4"
        k = 2; a = 4,2,3,1
        k = 3; a = 1,3,2,4

        work on "3"
        k = 1; a = 3,1,2,4
        k = 2; a = 2,1,3,4

        work on "2"
        k = 1; a = 1,2,3,4

        work on "1" - already in the correct spot

        answer = 2,3,1,2,1
        problem can have multiple answers as long as the steps is less than 10 * A.Length

        
        how to implement:
        for i in a.length-1 to 0
            find i+1 in array A, call that index j
            pancakeFlip(A, j)
            // desired character is now in position 0
            pancakeFlip(A, i)
            // desired character is now in position i, which is it's home
                

         */


        [DataTestMethod]
        [DataRow("[3,2,4,1]", "[3,4,2,3,2]")]
        [DataRow("[1,2,3]", "[]")] // already sorted
        [DataRow("[3,2,4,1]", "[3,4,2,3,2]")]
        public void Test(string inputStr, string expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var s = new Solution();
            var output = s.PancakeSort(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }


        [DataTestMethod]
        [DataRow("[3,2,4,1]", 2, "[4,2,3,1]")]
        [DataRow("[3,2,4,1]", 3, "[1,4,2,3]")]
        public void Test_PancakeFlip(string inputStr, int k, string expected)
        {
            var input = JsonConvert.DeserializeObject<List<int>>(inputStr);
            var s = new Solution();
            s.PancakeFlip(input, k);
            var outputStr = JsonConvert.SerializeObject(input);
            outputStr.Should().Be(expected);
        }



        public class Solution
        {
            public IList<int> PancakeSort(int[] A)
            {
                var a = A.ToList();
                return PancakeSortImpl(a);
            }

            public IList<int> PancakeSortImpl(List<int> a)
            {
                var answer = new List<int>();
                for (var i = a.Count - 1; i >= 0; i--)
                {
                    var j = 0;
                    for (j = 0; j < a.Count; j++)
                    {
                        if (a[j] == i + 1)
                        {
                            break;
                        }
                    }

                    if (j == i)
                    {
                        // already in the correct position
                        continue;
                    }

                    if (j > 0)
                    {
                        answer.Add(j + 1);
                        PancakeFlip(a, j);
                    }
                    if (i > 0)
                    {
                        answer.Add(i + 1);
                        PancakeFlip(a, i);
                    }
                }

                return answer;
            }

            public void PancakeFlip(List<int> a, int k)
            {
                var left = 0;
                var right = k;
                while (left < right)
                {
                    var temp = a[right];
                    a[right] = a[left];
                    a[left] = temp;
                    left++;
                    right--;
                }
            }
        }
    }
}