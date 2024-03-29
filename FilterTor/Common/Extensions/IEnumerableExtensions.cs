﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilterTor.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable is null || !enumerable.Any();
        }
    }
}
