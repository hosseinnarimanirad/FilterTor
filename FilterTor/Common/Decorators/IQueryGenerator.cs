﻿namespace FilterTor.Decorators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IQueryGenerator<T> 
{
    IQueryable<T> Query(); 
}
