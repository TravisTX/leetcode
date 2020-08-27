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
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    [TestClass]
    public class FindRightInterval
    {
        // https://leetcode.com/problems/find-right-interval/

        /*
         
        // idea 1:
        create interval class storing start,end,index
        create an array of interval, sorted by start
        binary search on interval array to find the best RIGHT interval for each
        left interval

         */


        [DataTestMethod]
        [DataRow("[ [1,2] ]", "[-1]")]
        [DataRow("[ [3,4], [2,3], [1,2] ]", "[-1,0,1]")]
        [DataRow("[ [1,4], [2,3], [3,4] ]", "[-1,2,-1]")]
        public void Test(string inputStr, string expectedStr)
        {
            var input = JsonConvert.DeserializeObject<int[][]>(inputStr);

            var s = new Solution();

            var output = s.FindRightInterval(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expectedStr);
        }

        public class Solution
        {
            public int[] FindRightInterval(int[][] intervals)
            {
                var ints = new List<Interval>();
                for (var i = 0; i < intervals.Length; i++)
                {
                    ints.Add(new Interval
                    {
                        Index = i,
                        Start = intervals[i][0],
                        End = intervals[i][1],
                    });
                }

                ints.Sort((a, b) => a.Start.CompareTo(b.Start));

                return FindRightIntervalsImpl(ints);
            }

            private static int[] FindRightIntervalsImpl(List<Interval> intervals)
            {
                var output = new int[intervals.Count];
                for (int i = 0; i < output.Length; i++)
                {
                    output[i] = -1;
                }

                foreach (var item in intervals)
                {
                    var left = 0;
                    var right = intervals.Count - 1;
                    while (right > left + 1)
                    {
                        var mid = left + (right - left) / 2;

                        if (intervals[mid].Start >= item.End)
                        {
                            right = mid;
                        }
                        else
                        {
                            left = mid;
                        }
                    }

                    if (intervals[right].Start >= item.End)
                    {
                        output[item.Index] = intervals[right].Index;
                    }
                }
                return output;
            }

            private class Interval
            {
                public int Index { get; set; }
                public int Start { get; set; }
                public int End { get; set; }
            }
        }
    }
}