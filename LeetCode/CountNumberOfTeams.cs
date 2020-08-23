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
    public class CountNumberOfTeams
    {
        // https://leetcode.com/problems/count-number-of-teams/

        /*
        
        1,2,3,4
        
        ans 4:
        1,2,3
        1,2,4
        1,3,4
        2,3,4


        three nested for loops, iterate over all possible elements and
        keep track of whether they meet critiera
        nested for loops, select each starting character, 


        --
         */


        [DataTestMethod]
        [DataRow("[2,5,3,4,1]", 3)]
        [DataRow("[2,1,3]", 0)]
        [DataRow("[1,2,3,4]", 4)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var s = new Solution();
            s.NumTeams(input).Should().Be(expected);
        }


        public class Solution
        {
            public int NumTeams(int[] rating)
            {
                var result = 0;
                for(var i = 0; i < rating.Length - 2; ++i)
                {
                    var itemI = rating[i];
                    for(var j = i + 1; j < rating.Length - 1; ++j)
                    {
                        var itemJ = rating[j];
                        var ascending = itemJ > itemI;
                        for(var k = j + 1; k < rating.Length; ++k)
                        {
                            var itemK = rating[k];
                            if ((ascending && itemK > itemJ) ||
                                (!ascending && itemK < itemJ))
                            {
                                result++;
                            }
                        }
                    }
                }
                return result;
            }
        }
    }
}