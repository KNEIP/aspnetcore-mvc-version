// Copyright (c) KNEIP Communication S.A.. All rights reserved.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kneip.AspNetCore.Mvc.Versioning
{
    /// <summary>
    /// Helper methods for the <see cref="Assert.ThrowsException{T}(Action)"/> methods.
    /// </summary>
    internal static class ExceptionExtensions
    {
        /// <summary>
        /// Allows to chain assertion to the exception after a Assert.ThrowsException method call.
        /// </summary>
        /// <typeparam name="T">The type of the exception.</typeparam>
        /// <param name="exception">The exception to be checked.</param>
        /// <param name="assert">The assertion executed on the <paramref name="exception"/>.</param>
        public static void Assert<T>(this T exception, Action<T> assert)
            where T : Exception
        {
            assert(exception);
        }
    }
}
