namespace SampleApp.Application.Features;

using SampleApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UpdatePolyFilterCommand : IRequest<UpdatePolyFilterResponse>
{
    public int PolyFilterId { get; set; }

    public string Title { get; set; }

    public string Note { get; set; }
}

public class UpdatePolyFilterResponse 
{
    public bool IsSucceed { get; set; }
}

