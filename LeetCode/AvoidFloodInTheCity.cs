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
    public class AvoidFloodInTheCity
    {
        // https://leetcode.com/problems/avoid-flood-in-the-city/

        [TestMethod]
        [DataTestMethod]
        [DataRow("[1,2,3,4]", "[-1,-1,-1,-1]")]
        [DataRow("[1,2,0,0,2,1]", "[-1,-1,2,1,-1,-1]")]
        [DataRow("[1,2,0,1,2]", "[]")]
        [DataRow("[69,0,0,0,69]", "[-1,69,1,1,-1]")]
        [DataRow("[10,20,20]", "[]")]
        public void Test(string inputStr, string expected)
        {
            var input = JsonConvert.DeserializeObject<int[]>(inputStr);
            var output = AvoidFlood(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expected);
        }

        public int[] AvoidFlood(int[] rains)
        {
            var output = new int[rains.Length];
            var zeros = new List<int>();
            var filled = new Dictionary<int, int>(); // key = lake, val = day filled
            for (var i = 0; i < rains.Length; ++i)
            {
                var rain = rains[i];
                if (rain > 0)
                {
                    var lake = rain;
                    if (filled.ContainsKey(lake))
                    {
                        // need to drain
                        bool handled = false;
                        for(var j = 0; j < zeros.Count; ++j)
                        {
                            var zeroDay = zeros[j];
                            if (zeroDay > filled[lake])
                            {
                                output[zeroDay] = lake;
                                handled = true;
                                filled.Remove(lake);
                                zeros.RemoveAt(j);
                                break;
                            }
                        }

                        if (!handled)
                        {
                            // not possible to avoid flood
                            return new int[0];
                        }
                    }

                    filled.Add(rain, i);
                    output[i] = -1;
                }
                else
                {
                    output[i] = 0;
                    zeros.Add(i);
                }
            }

            for (var i = 0; i < output.Length; ++i)
            {
                if (output[i] == 0)
                {
                    output[i] = 1;
                }
            }

            return output;
        }


        // O(N^2)
        //public int[] AvoidFlood(int[] rains)
        //{
        //    var output = new int[rains.Length];
        //    var lakesFilled = new HashSet<int>();
        //    for (var i = 0; i < rains.Length; ++i)
        //    {
        //        var rain = rains[i];
        //        if (rain > 0)
        //        {
        //            if (lakesFilled.Contains(rain))
        //            {
        //                // not possible to avoid flood
        //                return new int[0];
        //            }

        //            lakesFilled.Add(rain);
        //            output[i] = -1;
        //        }
        //        else
        //        {
        //            // decide what to drain
        //            int lakeToDrain = 0;
        //            for (var j = i + 1; j < rains.Length; ++j)
        //            {
        //                if (rains[j] > 0 && lakesFilled.Contains(rains[j]))
        //                {
        //                    lakeToDrain = rains[j];
        //                    break;
        //                }
        //            }

        //            if (lakeToDrain == 0)
        //            {
        //                lakeToDrain = 1;
        //            }

        //            output[i] = lakeToDrain;
        //            lakesFilled.Remove(lakeToDrain);
        //        }
        //    }

        //    return output;
        //}
    }
}