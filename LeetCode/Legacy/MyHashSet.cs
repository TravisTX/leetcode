using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode
{
    public class MyHashSetProblem
    {
        /*
Design a HashSet without using any built-in hash table libraries.

To be specific, your design should include these functions:

add(value): Insert a value into the HashSet. 
contains(value) : Return whether the value exists in the HashSet or not.
remove(value): Remove a value in the HashSet. If the value does not exist in the HashSet, do nothing.

Example:

MyHashSet hashSet = new MyHashSet();
hashSet.add(1);         
hashSet.add(2);         
hashSet.contains(1);    // returns true
hashSet.contains(3);    // returns false (not found)
hashSet.add(2);          
hashSet.contains(2);    // returns true
hashSet.remove(2);          
hashSet.contains(2);    // returns false (already removed)

Note:

All values will be in the range of [0, 1000000].
The number of operations will be in the range of [1, 10000].
Please do not use the built-in HashSet library.

         */

        public void Solve()
        {
            TestHashSet();
        }

        private void TestHashSet()
        {
            MyHashSet hashSet = new MyHashSet();
            hashSet.Add(1);
            hashSet.Add(2);
            Console.WriteLine("true:" + hashSet.Contains(1));    // returns true
            Console.WriteLine("false:" + hashSet.Contains(3));    // returns false (not found)
            hashSet.Add(2);
            Console.WriteLine("true:" + hashSet.Contains(2));    // returns true
            hashSet.Remove(2);
            Console.WriteLine("false:" + hashSet.Contains(2));    // returns false (already removed)
        }

        public class MyHashSet
        {
            int value = 0;

            /** Initialize your data structure here. */
            public MyHashSet()
            {

            }

            public void Add(int key)
            {
                value |= key;
            }

            public void Remove(int key)
            {
                //todo
            }

            /** Returns true if this set contains the specified element */
            public bool Contains(int key)
            {
                return (value & key) > 0;
            }
        }

        public class MyHashSetBitArray
        {
            bool[] value = new bool[1000000];

            public void Add(int key)
            {
                value[key] = true;
            }

            public void Remove(int key)
            {
                value[key] = false;
            }

            /** Returns true if this set contains the specified element */
            public bool Contains(int key)
            {
                return value[key];
            }
        }


    }
}
