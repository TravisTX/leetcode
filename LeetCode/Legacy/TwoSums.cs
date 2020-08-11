using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    public class TwoSums
    {
        // https://leetcode.com/problems/two-sum/

        /*
        Given an array of integers, return indices of the two numbers such that they add up to a specific target.

        You may assume that each input would have exactly one solution, and you may not use the same element twice.

        Example:

        Given nums = [2, 7, 11, 15], target = 9,

        Because nums[0] + nums[1] = 2 + 7 = 9,
        return [0, 1].

         */

        public void Solve()
        {
            var nums = new int[] { 2, 7, 11, 15 };
            var target = 9;
            var result = TwoSum(nums, target);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        public int[] TwoSum(int[] numsArr, int target)
        {
            var nums = numsArr.ToList();

            var map = new Dictionary<int, int>();

            for(int i = 0; i < nums.Count; ++i)
            {
                int compliment = target - nums[i];
                if (map.ContainsKey(compliment) && map[compliment] != i)
                {
                    return new int[] { i, map[compliment] };
                }
                map.Add(nums[i], i);
            }

            return new int[] { };
        }
    }
}
