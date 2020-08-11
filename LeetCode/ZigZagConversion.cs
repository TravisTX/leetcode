using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LeetCode
{
    [TestClass]
    public class ZigZagConversion
    {
        /*

        The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows like this: (you may want to display this pattern in a fixed font for better legibility)
PAYPALISHIRING
        P   A   H   N
        A P L S I I G
        Y   I   R
        And then read line by line: "PAHNAPLSIIGYIR"

        Write the code that will take a string and make this conversion given a number of rows:

        string convert(string s, int numRows);
        Example 1:

        Input: s = "PAYPALISHIRING", numRows = 3
        Output: "PAHNAPLSIIGYIR"
        Example 2:

        Input: s = "PAYPALISHIRING", numRows = 4
        Output: "PINALSIGYAHRPI"
        Explanation:

        P     I    N
        A   L S  I G
        Y A   H R
        P     I


                  1111
        01234567890123
        PAYPALISHIRING

           1
        0482 13
        PAHN AP




         */

        [TestMethod]
        public void Test_1()
        {
            Convert("PAYPALISHIRING", 3).Should().Be("PAHNAPLSIIGYIR");
        }

        [TestMethod]
        public void Test_2()
        {
            Convert("PAYPALISHIRING", 4).Should().Be("PINALSIGYAHRPI");
        }

        [TestMethod]
        public void Test_3()
        {
            Convert("AB", 1).Should().Be("AB");
        }

        public string Convert(string s, int numRows)
        {
            if (numRows == 1)
            {
                return s;
            }

            var matrix = new char[numRows, s.Length];
            var row = 0;
            var col = 0;
            var goingDown = true;

            for (var x = 0; x < numRows; ++x)
            {
                for (var y = 0; y < s.Length; ++y)
                {
                    matrix[x, y] = ' ';
                }
            }

            for (var i = 0; i < s.Length; ++i)
            {
                matrix[row, col] = s[i];

                if (goingDown)
                {
                    if (row < numRows - 1)
                    {
                        row++;
                    }
                    else
                    {
                        goingDown = false;
                        col++;
                        row--;
                    }
                }
                else
                {
                    if (row > 0)
                    {
                        col++;
                        row--;
                    }
                    else
                    {
                        goingDown = true;
                        row++;
                    }
                }
            }

            string output = "";

            for (var x = 0; x < numRows; ++x)
            {
                for (var y = 0; y < s.Length; ++y)
                {
                    var c = matrix[x, y];
                    if (c != ' ')
                        output += matrix[x, y];
                }
            }
            return output;
        }
    }
}
