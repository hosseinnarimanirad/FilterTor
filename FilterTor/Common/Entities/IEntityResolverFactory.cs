namespace GridEngineCore.Common.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IEntityResolverFactory
{
    IEntityResolver<T> CreateResolver<T>(string entityType);
}
