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
    public class SortMatrixDiagonally
    {
        // https://leetcode.com/problems/sort-the-matrix-diagonally/

        [DataTestMethod]
        [DataRow("[[3,3,1,1],[2,2,1,2],[1,1,1,2]]", "[[1,1,1,1],[1,2,2,2],[1,2,3,3]]")]
        [DataRow("[[11,25,66,1,69,7],[23,55,17,45,15,52],[75,31,36,44,58,8],[22,27,33,25,68,4],[84,28,14,11,5,50]]", "[[5,17,4,1,52,7],[11,11,25,45,8,69],[14,23,25,44,58,15],[22,27,31,36,50,66],[84,28,75,33,55,68]]")]
        public void Test(string inputStr, string expectedStr)
        {
            var input = JsonConvert.DeserializeObject<int[][]>(inputStr);
            var s = new Solution();
            var output = s.DiagonalSort(input);
            var outputStr = JsonConvert.SerializeObject(output);
            outputStr.Should().Be(expectedStr);
        }

        public class Solution
        {
            public int[][] DiagonalSort(int[][] mat)
            {
                // get diagonals from the top
                for (var x = 0; x < mat[0].Length; ++x)
                {
                    var diagonal = new List<int>();
                    for (var i = 0; i < mat.Length && x + i < mat[0].Length; ++i)
                    {
                        diagonal.Add(mat[0 + i][x + i]);
                    }
                    diagonal.Sort();

                    for (var i = 0; i < mat.Length && x + i < mat[0].Length; ++i)
                    {
                        mat[0 + i][x + i] = diagonal[i];
                    }
                }

                // get diagonals from the left side
                for (var y = 1; y < mat.Length; ++y)
                {
                    var diagonal = new List<int>();
                    for (var i = 0; y + i < mat.Length && i < mat[0].Length; ++i)
                    {
                        diagonal.Add(mat[y + i][0 + i]);
                    }
                    diagonal.Sort();

                    for (var i = 0; y + i < mat.Length && i < mat[0].Length; ++i)
                    {
                        mat[y + i][0 + i] = diagonal[i];
                    }
                }

                return mat;
            }
        }
    }
}