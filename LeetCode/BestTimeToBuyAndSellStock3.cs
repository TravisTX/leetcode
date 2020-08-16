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
    public class BestTimeToBuyAndSellStock3
    {
        // https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/

        /*
         
        create two arrays, profitForwards and profitBackwards
        pF contains the best profit seen so far on each date traversing forwards
        pB contains the best profit seen so far on each date traversing backwards

            [3,3,5,0,0,3,1,4]
        pf:  0,0,2,2,2,3,3,4
        pB:  $,4,4,4,4,3,3,0
        
        pB explanation: 
        if we buy on day 6 ($1) and sell on day 7 ($4), the profit is 3, hense pB[6]=3
        if we buy on day 4 ($0) and sell on day 7 ($4), profit is 4, pb[4]=4


        now loop through prices.length-1, add up the possible sums for each date
        and return the max sum found for a particular date
        
        this finds the optimal legal time to end transaction 1 and begin transaction 2

         */

        [TestMethod]
        [DataTestMethod]
        [DataRow("[3,3,5,0,0,3,1,4]", 6)] // 0-3, 1-4
        [DataRow("[1,2,3,4,5]", 4)] // 1-5
        [DataRow("[7,6,4,3,1]", 0)] // no transactions
        [DataRow("[1,2,4,2,5,7,2,4,9,0]", 13)] // ans= 1-7 (6), 2-9 (7) // incorrect ans = 1-4 (3), 2-7 (5), 2-9 (7)
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            MaxProfit(input).Should().Be(expected);
        }

        public int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length < 2) return 0;

            var profitForward = new int[prices.Length];
            var profitBackwards = new int[prices.Length];

            var rollingProfit = 0;
            var bestBuy = int.MaxValue;

            // forwards
            for (var i = 0; i < prices.Length; ++i)
            {
                var curr = prices[i];

                if (curr - bestBuy > rollingProfit)
                {
                    rollingProfit = curr - bestBuy;
                }
                if (curr < bestBuy)
                {
                    bestBuy = curr;
                }

                profitForward[i] = rollingProfit;
            }

            rollingProfit = 0;
            var bestSell = 0;

            // backwards
            for (var i = prices.Length - 1; i >= 0; --i)
            {
                var curr = prices[i];

                if (bestSell - curr > rollingProfit)
                {
                    rollingProfit = bestSell - curr;
                }
                if (curr > bestSell)
                {
                    bestSell = curr;
                }

                profitBackwards[i] = rollingProfit;
            }

            // find best
            var bestProfit = 0;
            for (var i = 0; i < prices.Length; ++i)
            {
                bestProfit = Math.Max(bestProfit, profitForward[i] + profitBackwards[i]);
            }
            return bestProfit;
        }
    }
}