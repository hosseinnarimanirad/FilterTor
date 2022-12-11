namespace SampleApp.Application.Features;

using SampleApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DeleteByIdsPolyFilterCommand : IRequest<DeleteByIdsPolyFilterResponse>
{
    public List<int> PolyFilterIds { get; set; }
}

public class DeleteByIdsPolyFilterResponse  
{ 
    public bool IsSucceed { get; set; } = false;
}

