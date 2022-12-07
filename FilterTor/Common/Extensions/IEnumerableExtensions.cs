﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridEngineCore.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
