#region License
//-----------------------------------------------------------------------
// <copyright>
// The MIT License (MIT)
// 
// Copyright (c) 2014 Kirk S Woll
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using System.Linq;
using WootzJs.Testing;

namespace WootzJs.Compiler.Tests.Collections.Generic
{
    public class DictionaryTests : TestFixture
    {
        [Test]
        public void Count()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("one", 1);
            AssertEquals(dictionary.Count, 1);
        }
         
        [Test]
        public void ContainsKey()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("one", 1);
            AssertTrue(dictionary.ContainsKey("one"));
            AssertTrue((!dictionary.ContainsKey("two")));
        }
         
        [Test]
        public void Keys()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("one", 1);
            dictionary.Add("two", 2);
            var keys = dictionary.Keys.ToArray();
            AssertTrue((keys[0] == "one" || keys[1] == "one"));
            AssertTrue((keys[0] == "two" || keys[1] == "two"));
        }
         
        [Test]
        public void Add()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("one", 1);
            dictionary.Add("two", 2);
            AssertEquals(dictionary["one"], 1);
        }
         
        [Test]
        public void Set()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary["one"] = 1;
            dictionary["two"] = 2;
            AssertEquals(dictionary["one"], 1);
            AssertEquals(dictionary["two"], 2);
        }
         
        [Test]
        public void DupsDontChangeCount()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary["one"] = 1;
            dictionary["one"] = 2;
            AssertEquals(dictionary["one"], 2);
            AssertEquals(dictionary.Count, 1);
        }
         
        [Test]
        public void Remove()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary["one"] = 1;
            dictionary.Remove("one");
            AssertEquals(dictionary.Count, 0);
        }
         
        [Test]
        public void Clear()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary["one"] = 1;
            dictionary.Clear();
            AssertEquals(dictionary.Count, 0);
        }
         
        [Test]
        public void TryGetValue()
        {
            var dictionary = new Dictionary<string, int>();
            dictionary["one"] = 1;
            dictionary["two"] = 2;
            int i;
            dictionary.TryGetValue("two", out i);
            AssertEquals(i, 2);
        }

        [Test]
        public void ArbitraryClass()
        {
            var one = new MyClass();
            var two = new MyClass();
            var three = new MyClass();
            var dictionary = new Dictionary<MyClass, int>();
            dictionary[one] = 1;
            dictionary[two] = 2;
            dictionary[three] = 3;

            AssertEquals(dictionary[one], 1);
            AssertEquals(dictionary[two], 2);
            AssertEquals(dictionary[three], 3);
        }

        public class MyClass
        {
        }
    }
}
