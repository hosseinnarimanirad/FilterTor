namespace SampleApp.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHasKey<T> where T : struct
{
    public T Id { get; set; }
}
