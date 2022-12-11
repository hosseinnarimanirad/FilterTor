namespace SampleApp.Presentation.Controllers;
 
using MediatR;
using Microsoft.AspNetCore.Mvc; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// سرویس‌های شرط چندگانه
/// </summary>
[ApiController]
[Route("/api/grid/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TableController : ControllerBase
{
    private readonly IMediator _mediator;

    public TableController(IMediator mediator)
    {
        _mediator = mediator;
    }

    ///// <summary>
    ///// </summary>
    ///// <returns></returns>
    //[HttpPost("Fund")]
    //public async Task<ActionResult<ListFundResponse>> ListFund([FromBody] ListFundQuery vm)
    //{
    //    return await _mediator.Send(vm);
    //}

}
