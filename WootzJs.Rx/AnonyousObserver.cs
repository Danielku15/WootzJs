﻿#region License

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

namespace System.Reactive
{
    /// <summary>
    /// Class to create an IObserver&lt;T&gt; instance from delegate-based implementations of the On* methods.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    public sealed class AnonymousObserver<T> : ObserverBase<T>
    {
        private readonly Action<T> _onNext;
        private readonly Action<Exception> _onError;
        private readonly Action _onCompleted;

        /// <summary>
        /// Creates an observer from the specified OnNext, OnError, and OnCompleted actions.
        /// </summary>
        /// <param name="onNext">Observer's OnNext action implementation.</param>
        /// <param name="onError">Observer's OnError action implementation.</param>
        /// <param name="onCompleted">Observer's OnCompleted action implementation.</param>
        /// <exception cref="ArgumentNullException"><paramref name="onNext"/> or <paramref name="onError"/> or <paramref name="onCompleted"/> is null.</exception>
        public AnonymousObserver(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            if (onNext == null)
                throw new ArgumentNullException("onNext");
            if (onError == null)
                throw new ArgumentNullException("onError");
            if (onCompleted == null)
                throw new ArgumentNullException("onCompleted");

            _onNext = onNext;
            _onError = onError;
            _onCompleted = onCompleted;
        }

        /// <summary>
        /// Creates an observer from the specified OnNext action.
        /// </summary>
        /// <param name="onNext">Observer's OnNext action implementation.</param>
        /// <exception cref="ArgumentNullException"><paramref name="onNext"/> is null.</exception>
        public AnonymousObserver(Action<T> onNext)
            : this(onNext, Stubs.Throw, Stubs.Nop)
        {
        }

        /// <summary>
        /// Creates an observer from the specified OnNext and OnError actions.
        /// </summary>
        /// <param name="onNext">Observer's OnNext action implementation.</param>
        /// <param name="onError">Observer's OnError action implementation.</param>
        /// <exception cref="ArgumentNullException"><paramref name="onNext"/> or <paramref name="onError"/> is null.</exception>
        public AnonymousObserver(Action<T> onNext, Action<Exception> onError)
            : this(onNext, onError, Stubs.Nop)
        {
        }

        /// <summary>
        /// Creates an observer from the specified OnNext and OnCompleted actions.
        /// </summary>
        /// <param name="onNext">Observer's OnNext action implementation.</param>
        /// <param name="onCompleted">Observer's OnCompleted action implementation.</param>
        /// <exception cref="ArgumentNullException"><paramref name="onNext"/> or <paramref name="onCompleted"/> is null.</exception>
        public AnonymousObserver(Action<T> onNext, Action onCompleted)
            : this(onNext, Stubs.Throw, onCompleted)
        {
        }

        /// <summary>
        /// Calls the onNext action.
        /// </summary>
        /// <param name="value">Next element in the sequence.</param>
        protected override void OnNextCore(T value)
        {
            _onNext(value);
        }

        /// <summary>
        /// Calls the onError action.
        /// </summary>
        /// <param name="error">The error that has occurred.</param>
        protected override void OnErrorCore(Exception error)
        {
            _onError(error);
        }

        /// <summary>
        /// Calls the onCompleted action.
        /// </summary>
        protected override void OnCompletedCore()
        {
            _onCompleted();
        }

        internal IObserver<T> MakeSafe(IDisposable disposable)
        {
            return new AnonymousSafeObserver<T>(_onNext, _onError, _onCompleted, disposable);
        }
    }
}