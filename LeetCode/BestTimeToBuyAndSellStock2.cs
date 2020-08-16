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
    public class BestTimeToBuyAndSellStock2
    {
        // https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/

        /*

        lastBuy = prices[0]
        loop through prices
        if uptick, do nothing
        if downtick and no buy exists, buy
        if downtick, and prev price higher than buy, sell = prev, buy = new
        if downtick, and price lower than buy, buy = new


         */

        [TestMethod]
        [DataTestMethod]
        [DataRow("[7,1,5,3,6,4]", 7)] // 1-5, 3-6
        [DataRow("[1,2,3,4,5]", 4)] // 1-5
        [DataRow("[7,6,4,3,1]", 0)] // no transactions
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            MaxProfit(input).Should().Be(expected);
        }

        public int MaxProfit(int[] prices)
        {
            var buy = prices[0];
            var profit = 0;
            for(var i = 1; i < prices.Length; ++i)
            {
                var newPrice = prices[i];
                var prevPrice = prices[i - 1];

                // downtick
                if (newPrice < prevPrice)
                {
                    // made profit
                    if (prevPrice > buy)
                    {
                        profit += prevPrice - buy;
                        buy = newPrice;
                    }

                    // found a new entrypoint
                    else if (newPrice < buy)
                    {
                        buy = newPrice;
                    }
                }
            }

            // add in remaining profit
            var lastPrice = prices[prices.Length - 1];
            if (lastPrice > buy)
            {
                profit += lastPrice - buy;
            }

            return profit;
        }
    }
}