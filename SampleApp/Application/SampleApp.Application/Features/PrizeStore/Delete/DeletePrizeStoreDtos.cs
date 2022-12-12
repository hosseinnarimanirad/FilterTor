namespace SampleApp.Application.Features;

using SampleApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DeletePrizeStoreCommand : IRequest<DeletePrizeStoreResponse>
{
    public int PrizeStoreId { get; set; }
}

public class DeletePrizeStoreResponse  
{ 
    public bool IsSucceed { get; set; } = false;
}

