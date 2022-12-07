namespace GridEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// COMPOSITE DESIGN PATTERN:
// Leaf
public interface ILeafCondition : ICondition
{
    string Entity { get; }
}
