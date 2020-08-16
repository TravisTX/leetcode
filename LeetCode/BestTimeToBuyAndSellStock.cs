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
    public class BestTimeToBuyAndSellStock
    {
        // https://leetcode.com/problems/best-time-to-buy-and-sell-stock/

        [TestMethod]
        [DataTestMethod]
        [DataRow("[7,1,5,3,6,4]", 5)]
        [DataRow("[7,6,4,3,1]", 0)]
        [DataRow("[2,1,4]", 3)]
        [DataRow("[2,1,2,1,0,1,2]", 2)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            MaxProfit(input).Should().Be(expected);
        }

        public int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length < 2)
            {
                return 0;
            }

            var bestBuy = int.MaxValue;
            var bestProfit = 0;

            for (var i = 0; i < prices.Length; ++i)
            {
                var newPrice = prices[i];
                if (newPrice < bestBuy)
                {
                    bestBuy = newPrice;
                }
                else
                {
                    // consider if newPrice is a sell
                    bestProfit = Math.Max(bestProfit, newPrice - bestBuy);
                }
            }

            return bestProfit;
        }
    }
}
