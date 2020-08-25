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
    public class MinimumCostForTickets
    {
        // https://leetcode.com/problems/minimum-cost-for-tickets/

        /*
         
        can we do anything with determining the min days required for each discount
        1 day = $2
        7 days = $7
        30 days = $15

        7 day pass is only smart if you're travelling a min of 4 days in a 7 day period
        30 day pass is only smart if you're travelling a min of 15 days in a 7 day period ($7 + $7 + $2 = $16)


        loop through every possible day 0..365
        if travel day is present we have 3 options: buy 1,7, or 30 day pass
        investigate each of these options, repeating (recurse) the entire process 
        for the subarray consisting of i+1..356, i+7..365 and i+30..365.
        return wichever had the lowest cost

        --
        idea 2:
        loop backwards from 365..0
        keep track of the best price for each date
        for every day calculate 3 options:
            1 day pass + n+1's best price
            7 day pass + n+7's best price
            30 day pass + n+30's best price
        the min of these options becomes the current day's best price

        if n is not a travel date, just copy n+1's best price


        --
        idea 3:
        same as idea 2, except just loop from days.length..0
        instead of maintaing bestPrices as a 365 elmenet array, just keep the travel days
        and use math instead of index counts

         */


        [DataTestMethod]
        [DataRow("[1,4,6,7,8,20]", "[2,7,15]", 11)]
        [DataRow("[1,2,3,4,5,6,7,8,9,10,30,31]", "[2,7,15]", 17)]
        [DataRow("[1,2,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,20,21,24,25,27,28,29,30,31,34,37,38,39,41,43,44,45,47,48,49,54,57,60,62,63,66,69,70,72,74,76,78,80,81,82,83,84,85,88,89,91,93,94,97,99]", "[9,38,134]", 423)]
        [DataRow("[1,4,6,7,8,365]", "[2,7,15]", 11)]
        public void Test(string daysStr, string costStr, int expected)
        {
            var cost = JsonConvert.DeserializeObject<int[]>(costStr);
            var days = JsonConvert.DeserializeObject<int[]>(daysStr);

            var s = new Solution();

            s.MincostTickets(days, cost).Should().Be(expected);
        }

        // idea 3
        public class Solution
        {
            public int MincostTickets(int[] days, int[] costs)
            {
                var daysHash = new HashSet<int>();
                foreach (var day in days)
                {
                    daysHash.Add(day);
                }
                var bestPrices = new int[days.Length + 1];

                for (var i = days.Length - 1; i >= 0; i--)
                {
                    int d1 = i + 1; // guaranteed to be safe becuase bestPrices is length + 1
                    int d7 = i;
                    int d30 = i;

                    // find the element in the days array that is 7 or 30 days past i
                    while (d7 < days.Length && days[d7] < days[i] + 7) d7++;
                    while (d30 < days.Length && days[d30] < days[i] + 30) d30++;

                    var d1Pass = costs[0] + bestPrices[d1];
                    var d7Pass = costs[1] + bestPrices[d7];
                    var d30Pass = costs[2] + bestPrices[d30];

                    bestPrices[i] = Math.Min(Math.Min(d1Pass, d7Pass), d30Pass);
                }

                return bestPrices[0];
            }
        }

        // idea 2
        //public class Solution
        //{
        //    public int MincostTickets(int[] days, int[] costs)
        //    {
        //        var daysHash = new HashSet<int>();
        //        foreach (var day in days)
        //        {
        //            daysHash.Add(day);
        //        }
        //        var bestPrices = new int[366];

        //        for(var i = 365; i >= 1; i--)
        //        {
        //            if (!daysHash.Contains(i))
        //            {
        //                bestPrices[i] = bestPrices.Length > i + 1 ? bestPrices[i + 1] : 0;
        //                continue;
        //            }

        //            var d1Pass = costs[0] + (bestPrices.Length > i + 1 ? bestPrices[i + 1] : 0);
        //            var d7Pass = costs[1] + (bestPrices.Length > i + 7 ? bestPrices[i + 7] : 0);
        //            var d30Pass = costs[2] + (bestPrices.Length > i + 30 ? bestPrices[i + 30] : 0);

        //            bestPrices[i] = Math.Min(Math.Min(d1Pass, d7Pass), d30Pass);
        //        }

        //        return bestPrices[1];
        //    }
        //}


        // idea 1
        //public class Solution
        //{
        //    public int MincostTickets(int[] days, int[] costs)
        //    {
        //        var daysHash = new HashSet<int>();
        //        foreach(var day in days)
        //        {
        //            daysHash.Add(day);
        //        }

        //        return MinCostImpl(daysHash, costs, 0, 0);
        //    }

        //    public int MinCostImpl(HashSet<int> days, int[] costs, int i, int runningTotal)
        //    {
        //        if (i > 365)
        //        {
        //            return runningTotal;
        //        }

        //        if (!days.Contains(i))
        //        {
        //            return MinCostImpl(days, costs, i + 1, runningTotal);
        //        }

        //        var d1Pass = MinCostImpl(days, costs, i + 1, runningTotal + costs[0]);
        //        var d7Pass = MinCostImpl(days, costs, i + 7, runningTotal + costs[1]);
        //        var d30Pass = MinCostImpl(days, costs, i + 30, runningTotal + costs[2]);

        //        return Math.Min(Math.Min(d1Pass, d7Pass), d30Pass);
        //    }
        //}
    }
}