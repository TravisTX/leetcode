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
    public class RottingOranges
    {
        /*

         */

        [TestMethod]
        public void Test_1()
        {
            //Input:[[2,1,1],[1,1,0],[0,1,1]]
            //Output: 4

            var input = JsonConvert.DeserializeObject<int[][]>("[[2,1,1],[1,1,0],[0,1,1]]");
            var output = OrangesRotting(input);
            output.Should().Be(4);
        }
        [TestMethod]
        public void Test_2()
        {

            var input = JsonConvert.DeserializeObject<int[][]>("[[2,1,1],[0,1,1],[1,0,1]]");
            var output = OrangesRotting(input);
            output.Should().Be(-1);
        }

        public int OrangesRotting(int[][] grid)
        {
            var minutes = 0;
            while (true)
            {
                var changes = false;
                var fresh = false;
                var newGrid = CloneGrid(grid);
                for (var i = 0; i < grid.Length; i++)
                    for (var j = 0; j < grid[0].Length; j++)
                    {
                        var cell = grid[i][j];
                        if (cell == 2)
                        {
                            changes = RotNeighbor(newGrid, i - 1, j) || changes;
                            changes = RotNeighbor(newGrid, i + 1, j) || changes;
                            changes = RotNeighbor(newGrid, i, j - 1) || changes;
                            changes = RotNeighbor(newGrid, i, j + 1) || changes;
                        }
                        fresh = fresh || cell == 1;
                    }

                if (!changes)
                {
                    if (fresh) return -1;
                    return minutes;
                }

                grid = newGrid;
                minutes++;
            }
        }

        private int[][] CloneGrid(int[][] grid)
        {
            var newGrid = new int[grid.Length][];

            for (var i = 0; i < grid.Length; i++)
            {
                newGrid[i] = new int[grid[i].Length];
                for (var j = 0; j < grid[0].Length; j++)
                {
                    newGrid[i][j] = grid[i][j];
                }
            }
            return newGrid;
        }

        private bool RotNeighbor(int[][] grid, int i, int j)
        {
            if (i >= 0 && i < grid.Length &&
                j >= 0 && j < grid[0].Length &&
                grid[i][j] == 1)
            {
                grid[i][j] = 2;
                return true;
            }
            return false;
        }
    }
}
