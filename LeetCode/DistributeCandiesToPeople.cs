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
    public class DistributeCandiesToPeople
    {
        // https://leetcode.com/problems/distribute-candies-to-people/

        [TestMethod]
        [DataTestMethod]
        [DataRow(7, 4, "[1,2,3,1]")]
        [DataRow(10, 3, "[5,2,3]")]
        public void Test(int candies, int numPeople, string expected)
        {
            var output = DistributeCandies(candies, numPeople);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public int[] DistributeCandies(int candies, int num_people)
        {
            var people = new int[num_people];
            var amount = 1;
            var person = 0;

            while (candies > 0)
            {
                if (candies > amount)
                {
                    people[person] += amount;
                    candies -= amount;
                }
                else
                {
                    people[person] += candies;
                    candies = 0;
                }

                amount++;
                person = ++person % num_people;
            }

            return people;
        }
    }
}