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
    public class StreamOfCharacters
    {
        // https://leetcode.com/problems/stream-of-characters/

        /*
        
        idea: (TOO SLOW)
         create a trie of all words
        when a new char comes in, assume it could be the start of a new word
        so maintain a list of all working trie roots

        when a new char comes in, start a new entry to the working set from root, add it to the list
        then go through every trie in the list and see if char is a possible next char in any working tries

        if so, add this child node to the working list
        else, remove from working set
         
        if so and it's the end of word, return true

        --

        new idea:
        store in trie in reverse
        keep track of the stack of characters that have come in
        on query: look for the new char in the root's children - this is the end of words
        if found, iterate through the stack, if chars keep being found and we eventually get to 
        a end of word (beginning of word), return true


        --
         */


        [DataTestMethod]
        [DataRow("cd,f,kl", "a,b,c,d,e,f,g,h,i,j,k,l", "[false,false,false,true,false,true,false,false,false,false,false,true]")]
        [DataRow("ab,ba,aaab,abab,baa", "a,a,a,a,a,b,a,b,a,b,b,b,a,b,a,b,b,b,b,a,b,a,b,a,a,a,b,a,a,a", "[false,false,false,false,false,true,true,true,true,true,false,false,true,true,true,true,false,false,false,true,true,true,true,true,true,false,true,true,true,false]")]
        public void Test(string inputWordStr, string inputQueryStr, string expectedStr)
        {
            var inputWords = inputWordStr.Split(',');
            var inputQueries = inputQueryStr.Split(',').Select(x => x[0]).ToArray();
            var expected = JsonConvert.DeserializeObject<bool[]>(expectedStr);

            var checker = new StreamChecker(inputWords);

            for (var i = 0; i < inputQueries.Length; ++i)
            {
                checker.Query(inputQueries[i]).Should().Be(expected[i], $"i:{i} query:{inputQueries[i]} expected:{expected[i]}");
            }
        }

        public class StreamChecker
        {
            TrieNode root = new TrieNode();
            List<int> queryChars = new List<int>();

            public StreamChecker(string[] words)
            {
                for (var i = 0; i < words.Length; ++i)
                {
                    Insert(words[i]);
                }
            }

            private void Insert(string word)
            {
                TrieNode curr = root;
                for (var i = word.Length - 1; i >= 0; --i)
                {
                    var charIndex = word[i] - 'a';
                    if (curr.children[charIndex] == null)
                    {
                        curr.children[charIndex] = new TrieNode();
                    }
                    curr = curr.children[charIndex];
                }

                curr.isEndOfWord = true;
            }

            public bool Query(char letter)
            {
                queryChars.Insert(0, letter - 'a');
                var node = root;

                for(var i = 0; i < queryChars.Count; ++i)
                {
                    var cIndex = queryChars[i];
                    node = node.children[cIndex];

                    if (node == null)
                    {
                        return false;
                    }

                    if (node.isEndOfWord)
                    {
                        return true;
                    }
                }

                return false;
            }

            class TrieNode
            {
                static readonly int ALPHABET_SIZE = 26;
                public TrieNode[] children = new TrieNode[ALPHABET_SIZE];

                public bool isEndOfWord;

                public TrieNode()
                {
                    isEndOfWord = false;
                    for (int i = 0; i < ALPHABET_SIZE; i++)
                        children[i] = null;
                }
            }
        }




        // TOO SLOW

        //public class StreamChecker
        //{
        //    TrieNode root = new TrieNode();
        //    List<TrieNode> workingSet = new List<TrieNode>();

        //    public StreamChecker(string[] words)
        //    {
        //        for (var i = 0; i < words.Length; ++i)
        //        {
        //            Insert(words[i]);
        //        }
        //    }

        //    private void Insert(string word)
        //    {
        //        TrieNode curr = root;
        //        for (var i = 0; i < word.Length; ++i)
        //        {
        //            var charIndex = word[i] - 'a';
        //            if (curr.children[charIndex] == null)
        //            {
        //                curr.children[charIndex] = new TrieNode();
        //            }
        //            curr = curr.children[charIndex];
        //        }

        //        curr.isEndOfWord = true;
        //    }

        //    public bool Query(char letter)
        //    {
        //        var letterIndex = letter - 'a';
        //        var newWorkingSet = new List<TrieNode>();
        //        bool foundWord = false;

        //        var rootChild = root.children[letterIndex];
        //        if (rootChild != null)
        //        {
        //            newWorkingSet.Add(rootChild);
        //            if (rootChild.isEndOfWord)
        //            {
        //                foundWord = true;
        //            }
        //        }

        //        for (var i = 0; i < workingSet.Count; ++i)
        //        {
        //            var trieNode = workingSet[i];
        //            var child = trieNode.children[letterIndex];
        //            if (child != null)
        //            {
        //                newWorkingSet.Add(child);
        //                if (child.isEndOfWord)
        //                {
        //                    foundWord = true;
        //                }
        //            }
        //        }

        //        workingSet = newWorkingSet;

        //        return foundWord;
        //    }

        //    class TrieNode
        //    {
        //        static readonly int ALPHABET_SIZE = 26;
        //        public TrieNode[] children = new TrieNode[ALPHABET_SIZE];

        //        public bool isEndOfWord;

        //        public TrieNode()
        //        {
        //            isEndOfWord = false;
        //            for (int i = 0; i < ALPHABET_SIZE; i++)
        //                children[i] = null;
        //        }
        //    }
        //}
    }
}