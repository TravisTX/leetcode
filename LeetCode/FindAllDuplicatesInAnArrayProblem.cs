using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    [TestClass]
    public class FindAllDuplicatesInAnArrayProblem
    {

        [TestMethod]
        public void Test_1()
        {
            var input = new int[] { 4, 3, 2, 7, 8, 2, 3, 3, 1};
            var output = FindDuplicates(input);
            output.Count().Should().Be(2);
            output.Should().Contain(2);
            output.Should().Contain(3);
        }

        public IList<int> FindDuplicates(int[] nums)
        {
            var hs = new HashSet<int>();
            var results = new HashSet<int>();

            for(var i = 0; i < nums.Length; ++i)
            {
                var num = nums[i];
                if (hs.Contains(num))
                {
                    results.Add(num);
                }
                hs.Add(num);
            }

            return results.ToList();
        }
    }
}
