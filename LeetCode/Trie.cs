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
    public class TrieProblem
    {
        /*



         */

        [TestMethod]
        public void Test_1()
        {
            Trie trie = new Trie();

            trie.Insert("apple");
            trie.Search("apple").Should().BeTrue();
            trie.Search("app").Should().BeFalse();
            trie.StartsWith("app").Should().BeTrue();
            trie.Insert("app");
            trie.Search("app").Should().BeTrue();
        }

        public class Trie
        {
            TrieNode root = new TrieNode();

            /** Initialize your data structure here. */
            public Trie()
            {

            }

            /** Inserts a word into the trie. */
            public void Insert(string word)
            {
                TrieNode curr = root;
                for (var i = 0; i < word.Length; ++i)
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

            /** Returns if the word is in the trie. */
            public bool Search(string word)
            {
                TrieNode curr = root;
                for (var i = 0; i < word.Length; ++i)
                {
                    var charIndex = word[i] - 'a';
                    var nextNode = curr.children[charIndex];
                    if (nextNode == null)
                    {
                        return false;
                    }
                    curr = nextNode;
                }
                return curr.isEndOfWord;
            }

            /** Returns if there is any word in the trie that starts with the given prefix. */
            public bool StartsWith(string prefix)
            {
                TrieNode curr = root;
                for (var i = 0; i < prefix.Length; ++i)
                {
                    var charIndex = prefix[i] - 'a';
                    var nextNode = curr.children[charIndex];
                    if (nextNode == null)
                    {
                        return false;
                    }
                    curr = nextNode;
                }
                return true;
            }
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
        };

    }
}
