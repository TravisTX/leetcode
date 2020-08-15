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
    public class NonOverlappingIntervals
    {
        // https://leetcode.com/problems/non-overlapping-intervals/

        /*
         
        algo 1: (didn't work)
        inspect each interval
        compare to each other interval to the right
        if overlap occurs, add the widest interval (or right-most interval) to intervals to remove

        [[1,100],[11,22],[1,11],[2,12]]
        removes 0,1,3... optimal removal would have been 0, 3

        --

        algo 2: (didn't work)
        inspect each interval
        compare to each other interval to the right
        keep overlaps hashmap of interval id, and interval ids it overlaps with

        [[1,100],[11,22],[1,11],[2,12]]
        0: 1,2,3
        1: 0,3,
        2: 0,3,
        3: 0,1,2

        loop through overlaps to find the item with the most conflicts
        remove that item from each
        repeat until overlaps only contains lists of 0


        --
        algo 3
        sort ascending by end
        store the prevEnd = end of interval[0]
        loop through intervals, counting invalid intervals
        update prevEnd as you go
        return invalid count

         */

        [TestMethod]
        [DataTestMethod]
        [DataRow("[[1,2],[2,3],[3,4],[1,3]]", 1)] // [1,3] can be removed
        [DataRow("[[1,2],[1,2],[1,2]]", 2)] // remove two [1,2]'s
        [DataRow("[[1,2],[2,3]]", 0)]
        [DataRow("[[1,100],[11,22],[1,11],[2,12]]", 2)]
        [DataRow("[[0,2],[1,3],[1,3],[2,4],[3,5],[3,5],[4,6]]", 4)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[][]>(inputStr);
            EraseOverlapIntervals(input).Should().Be(expected);
        }

        public int EraseOverlapIntervals(int[][] intervals)
        {
            if (intervals.Length == 0) return 0;

            int result = 0;
            Array.Sort(intervals, (a, b) => { return a[1].CompareTo(b[1]); });
            int prevEnd = intervals[0][1];

            // iterate, counting invalid intervals, and updating the prev end for valid intervals
            for (var i = 1; i < intervals.Length; ++i)
            {
                if (intervals[i][0] < prevEnd)
                    result++;
                else
                    prevEnd = intervals[i][1];
            }

            return result;
        }
    }
}
