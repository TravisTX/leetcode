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
    public class DisplayTableOfFoodOrders
    {
        // https://leetcode.com/problems/display-table-of-food-orders-in-a-restaurant/

        [DataTestMethod]
        [DataRow("[[\"David\",\"3\",\"Ceviche\"],[\"Corina\",\"10\",\"Beef Burrito\"],[\"David\",\"3\",\"Fried Chicken\"],[\"Carla\",\"5\",\"Water\"],[\"Carla\",\"5\",\"Ceviche\"],[\"Rous\",\"3\",\"Ceviche\"]]",
            "[[\"Table\",\"Beef Burrito\",\"Ceviche\",\"Fried Chicken\",\"Water\"],[\"3\",\"0\",\"2\",\"1\",\"0\"],[\"5\",\"0\",\"1\",\"0\",\"1\"],[\"10\",\"1\",\"0\",\"0\",\"0\"]]")]
        public void Test(string inputStr, string expectedStr)
        {
            var input = JsonConvert.DeserializeObject<IList<IList<string>>>(inputStr);

            var s = new Solution();

            var output = s.DisplayTable(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expectedStr);
        }

        public class Solution
        {
            public IList<IList<string>> DisplayTable(IList<IList<string>> orders)
            {
                var tables = new List<string>();
                var foodItems = new List<string>();
                var dict = new Dictionary<(string table, string foodItem), int>();

                foreach (var order in orders)
                {
                    var table = order[1];
                    var foodItem = order[2];

                    if (!tables.Contains(table))
                    {
                        tables.Add(table);
                    }

                    if (!foodItems.Contains(foodItem))
                    {
                        foodItems.Add(foodItem);
                    }

                    var hashKey = (table, foodItem);
                    if (!dict.ContainsKey(hashKey))
                    {
                        dict.Add(hashKey, 0);
                    }
                    dict[hashKey]++;
                }

                tables.Sort((a,b) => int.Parse(a).CompareTo(int.Parse(b)));
                foodItems.Sort(StringComparer.Ordinal);

                var output = new List<IList<string>>();
                var header = new List<string>();
                header.Add("Table");
                foreach (var foodItem in foodItems)
                {
                    header.Add(foodItem);
                }
                output.Add(header);

                foreach (var table in tables)
                {
                    var row = new List<string>();
                    row.Add(table);
                    foreach (var foodItem in foodItems)
                    {
                        var hashKey = (table, foodItem);

                        if (dict.ContainsKey(hashKey))
                        {
                            row.Add(dict[hashKey].ToString());
                        }
                        else
                        {
                            row.Add("0");
                        }
                    }
                    output.Add(row);
                }

                return output;
            }
        }

    }
}