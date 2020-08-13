using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    [TestClass]
    public class IteratorForCombination
    {
        // https://leetcode.com/problems/iterator-for-combination/

        [TestMethod]
        [DataTestMethod]
        [DataRow("abc", 2, "[\"ab\",\"ac\",\"bc\"]")]
        [DataRow("abcde", 3, "[\"abc\",\"abd\",\"abe\",\"acd\",\"ace\",\"ade\",\"bcd\",\"bce\",\"bde\",\"cde\"]")]
        public void Test_1(string characters, int length, string expected)
        {
            var iterator = new CombinationIterator(characters, length);
            var json = JsonConvert.SerializeObject(iterator.combinations);
            json.Should().Be(expected);
        }

        [TestMethod]
        [DataTestMethod]
        public void Test_2()
        {
            var iterator = new CombinationIterator("abc", 2);
            iterator.Next().Should().Be("ab");
            iterator.HasNext().Should().BeTrue();
            iterator.Next().Should().Be("ac");
            iterator.HasNext().Should().BeTrue();
            iterator.Next().Should().Be("bc");
            iterator.HasNext().Should().BeFalse();
        }

        public class CombinationIterator
        {
            public List<string> combinations = new List<string>();
            string chars;
            int index = 0;

            public CombinationIterator(string characters, int combinationLength)
            {
                chars = characters;
                GetCombinations("", 0, combinationLength);
            }

            private void GetCombinations(string accum, int start, int length)
            {
                if (length == 0)
                {
                    combinations.Add(accum);
                    return;
                }

                for (var i = start; i <= chars.Length - length; ++i)
                {
                    GetCombinations(accum + chars[i], i + 1, length - 1);
                }
            }

            public string Next()
            {
                return combinations[index++];
            }

            public bool HasNext()
            {
                return index < combinations.Count;
            }
        }
    }
}
