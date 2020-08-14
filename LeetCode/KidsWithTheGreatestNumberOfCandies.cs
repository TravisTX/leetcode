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
    public class KidsWithTheGreatestNumberOfCandies
    {
        // https://leetcode.com/problems/kids-with-the-greatest-number-of-candies/

        [TestMethod]
        [DataTestMethod]
        [DataRow("[2,3,5,1,3]", 3, "[true,true,true,false,true]")]
        [DataRow("[4,2,1,1,2]", 1, "[true,false,false,false,false]")]
        public void Test(string inputStr, int extraCandies, string expectedStr)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var output = KidsWithCandies(input, extraCandies);

            var outputStr = JsonConvert.SerializeObject(output);

            outputStr.Should().Be(expectedStr);
        }


        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            var max = candies[0];
            for(var i = 0; i < candies.Length; ++i)
            {
                max = Math.Max(max, candies[i]);
            }

            var result = new bool[candies.Length];
            for (var i = 0; i < candies.Length; ++i)
            { 
                if (candies[i] + extraCandies >= max)
                {
                    result[i] = true;
                }
            }
            return result;
        }
    }
}
