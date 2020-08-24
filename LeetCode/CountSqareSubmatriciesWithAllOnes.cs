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
    public class CountSqareSubmatriciesWithAllOnes
    {
        // https://leetcode.com/problems/count-square-submatrices-with-all-ones/

        /*

        BRUTE FORCE: O(N^4) ?
        for every possible size of square
            for every possible startY
                for every possible startX
                    determine if this is a valid square

        psudo code:
        for sideCount in 0..Math.min(matrix.length, matrix[0].length)
            for startY in 0..matrix.length
            for startX in 0..matrix[0].length
                starting position = startY,startX
                var isComplete = true
                for y in startY..(startY + sideCount)
                for x in startX..(startX + sideCount)
                    if matrix[y][x]==0
                        isComplete = false

                if (isComplete)
                    result++;

        --

        BETTER IDEA:
        find every 1, count that as 1 square
        consider that 1 as startY, startX, and increase the square size
        if this is valid, count that as another square, and repeat

         */


        [DataTestMethod]
        [DataRow("[[0,1,1,1],[1,1,1,1],[0,1,1,1]]", 15)]
        [DataRow("[[1,0,1],[1,1,0],[1,1,0]]", 7)]
        public void Test(string inputStr, int expected)
        {
            var input = JsonConvert.DeserializeObject<int[][]>(inputStr);
            var s = new Solution();
            s.CountSquares(input).Should().Be(expected);
        }

        public class Solution
        {
            public int CountSquares(int[][] matrix)
            {
                var result = 0;

                for(var startY = 0; startY < matrix.Length; startY++)
                {
                    for(var startX = 0; startX < matrix[0].Length; startX++)
                    {
                        if (matrix[startY][startX] == 1)
                        {
                            int squareSize = 1;
                            var complete = true;
                            while (complete)
                            {
                                result++;
                                squareSize++;
                                complete = IsComplete(matrix, squareSize, startY, startX);
                            }
                        }
                    }
                }

                return result;
            }

            private bool IsComplete(int[][] matrix, int squareSize, int startY, int startX)
            {
                if (startY + squareSize > matrix.Length) return false;
                if (startX + squareSize > matrix[0].Length) return false;

                // check right edge of the square
                for (var y = startY; y < startY + squareSize; y++)
                {
                    if (matrix[y][startX + squareSize - 1] == 0)
                    {
                        return false;
                    }
                }

                // check bottom edge of the square
                for (var x = startX; x < startX + squareSize; x++)
                {
                    if (matrix[startY + squareSize -1][x] == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        // BRUTE FORCE
        //public class Solution
        //{
        //    public int CountSquares(int[][] matrix)
        //    {
        //        var result = 0;

        //        for (var squareSize = 1; squareSize <= Math.Min(matrix.Length, matrix[0].Length); ++squareSize)
        //        {
        //            for (var startY = 0; startY < matrix.Length - squareSize + 1; startY++)
        //            {
        //                for (var startX = 0; startX < matrix[0].Length - squareSize + 1; startX++)
        //                {
        //                    if (IsComplete(matrix, squareSize, startY, startX))
        //                    {
        //                        result++;
        //                    }
        //                }
        //            }
        //        }

        //        return result;
        //    }

        //    private bool IsComplete(int[][] matrix, int squareSize, int startY, int startX)
        //    {
        //        for (var y = startY; y < startY + squareSize; y++)
        //        {
        //            for (var x = startX; x < startX + squareSize; x++)
        //            {
        //                if (matrix[y][x] == 0)
        //                {
        //                    return false;
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //}
    }
}