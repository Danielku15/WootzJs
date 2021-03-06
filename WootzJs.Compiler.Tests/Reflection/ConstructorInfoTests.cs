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

using System;
using System.Linq;
using System.Runtime.WootzJs;
using WootzJs.Testing;

namespace WootzJs.Compiler.Tests.Reflection
{
    public class ConstructorInfoTests : TestFixture
    {
        [Test]
        public void InvokeParameterless()
        {
            var type = typeof(TestClass);
            var constructor = type.GetConstructors().Single(x => x.GetParameters().Length == 0);
            var testClass = (TestClass)constructor.Invoke(new object[0]);
            AssertEquals(testClass.Foo, "parameterless");
        }

        [Test]
        public void InvokeNoConstructor()
        {
            var type = typeof(NoConstructorClass);
            var constructor = type.GetConstructors().Single(x => x.GetParameters().Length == 0);
            var testClass = (NoConstructorClass)constructor.Invoke(new object[0]);
            AssertEquals(testClass.Foo, "noconstructor");
        }

        [Test]
        public void Generic()
        {
            var realInstance = new GenericClass<string>();
            var type = typeof(GenericClass<string>);
            var instance = (GenericClass<string>)Activator.CreateInstance(type);
            AssertTrue(instance is GenericClass<string>);
        }

        public class TestClass
        {
            public string Foo;

            public TestClass()
            {
                Foo = "parameterless";
            }
        }

        public class NoConstructorClass
        {
            public string Foo = "noconstructor";
        }

        public class GenericClass<T>
        {
        }
    }
}
