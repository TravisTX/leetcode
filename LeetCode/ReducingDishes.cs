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

namespace LeetCode
{
    [TestClass]
    public class ReducingDishes
    {
        // https://leetcode.com/problems/reducing-dishes/

        /*
        -1,-8,0,5,-9
        -9,-8,-1,0,5 = -9*1 + -8*2 + -1*3 + 0*4 + 5*5 = -3
        -8,-1,0,5 = -8*1 + -1*2 + 0*3 + 5*4 = 10
        -1,0,5 = -1*1 + 0*2 + 5*3 = 14
        0,5 = 0*1 + 5*2 = 10
         */


        [TestMethod]
        [DataTestMethod]
        [DataRow("[-1,-8,0,5,-9]", 14)] // remove index 1 and 4
        [DataRow("[4,3,2]", 20)] // remove nothing
        [DataRow("[-1,-4,-5]", 0)] // remove everything
        [DataRow("[-2,5,-1,0,3,-3]", 35)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            MaxSatisfaction(input).Should().Be(expected);
        }

        public int MaxSatisfaction(int[] satisfaction)
        {
            Array.Sort(satisfaction);
            int answer = ComputeSatisfaction(satisfaction);

            for (var i = 0; i <= satisfaction.Length; ++i)
            {
                var newSatisfaction = new ArraySegment<int>(satisfaction, i, satisfaction.Length - i).ToArray();
                var newAnswer = ComputeSatisfaction(newSatisfaction);
                if (newAnswer < answer)
                {
                    break;
                }
                answer = newAnswer;
            }
            return answer;
        }

        private int ComputeSatisfaction(int[] arr)
        {
            var sum = 0;
            for (var i = 0; i < arr.Length; ++i)
            {
                sum += arr[i] * (i + 1);
            }
            return sum;
        }
    }
}
