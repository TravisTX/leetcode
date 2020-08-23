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
    public class RevealCardsInIncreasingOrder
    {
        // https://leetcode.com/problems/reveal-cards-in-increasing-order/

        /*

        idea:
        sort the array desc
        place the cards into a new array using a reverse of the pattern


        [17,13,11,7,5,3,2] < sorted desc

        add 17
        [17]

        move length-1 to 0 and add 13
        [17]
        [13,17]

        move length-1 to 0 and add 11
        [17,13]
        [11,17,13]

        move length-1 to 0 and add 7
        [13,11,17]
        [7,13,11,17]

        [17,7,13,11]
        [5,17,7,13,11]

        [11,5,17,7,13]
        [3,11,5,17,7,13]

        [13,3,11,5,17,7]
        [2,13,3,11,5,17,7]

         
         */


        [DataTestMethod]
        [DataRow("[17,13,11,2,3,5,7]", "[2,13,3,11,5,17,7]")]
        [DataRow("[0,1,2,3,4,5,6,7,8,9,10]", "[0,8,1,6,2,10,3,7,4,9,5]")]
        public void Test(string inputStr, string expectedStr)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var s = new Solution();
            var output = s.DeckRevealedIncreasing(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expectedStr);
        }

        public class Solution
        {
            public int[] DeckRevealedIncreasing(int[] deck)
            {
                // sort descending
                Array.Sort(deck, (a, b) => (a > b ? -1 : 1));

                var result = new List<int>();

                for (var i = 0; i < deck.Length; ++i)
                {
                    if (result.Count > 1)
                    {
                        // move end to beginning
                        result.Insert(0, result[result.Count - 1]);
                        result.RemoveAt(result.Count - 1);
                    }

                    // insert new card
                    result.Insert(0, deck[i]);
                }

                return result.ToArray();
            }
        }
    }
}