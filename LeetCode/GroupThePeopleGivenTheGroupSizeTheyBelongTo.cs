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
    public class GroupThePeopleGivenTheGroupSizeTheyBelongTo
    {
        // https://leetcode.com/problems/group-the-people-given-the-group-size-they-belong-to/

        [TestMethod]
        [DataTestMethod]
        [DataRow("[3,3,3,3,3,1,3]", "[[0,1,2],[5],[3,4,6]]")]
        [DataRow("[2,1,3,3,3,2]", "[[1],[2,3,4],[0,5]]")]
        public void Test(string inputStr, string expectedStr)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var output = GroupThePeople(input);

            var outputStr = JsonConvert.SerializeObject(output);

            outputStr.Should().Be(expectedStr);
        }

        /*

        Dictionary<int, List<int>> opengroups;
        foreach person
           add person to the opengroup
           if the open group is full, put it in final groups

        */

        public IList<IList<int>> GroupThePeople(int[] groupSizes)
        {
            var result = new List<IList<int>>();
            var openGroups = new Dictionary<int, IList<int>>();

            for (var i = 0; i < groupSizes.Length; ++i)
            {
                var groupSize = groupSizes[i];
                if (groupSize == 1)
                {
                    result.Add(new List<int> { i });
                    continue;
                }

                if (!openGroups.ContainsKey(groupSize))
                {
                    openGroups.Add(groupSize, new List<int>());
                }
                var group = openGroups[groupSize];
                group.Add(i);

                if (group.Count == groupSize)
                {
                    result.Add(group);
                    openGroups.Remove(groupSize);
                }
            }
            return result;
        }
    }
}
