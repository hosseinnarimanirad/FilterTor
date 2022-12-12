namespace SampleApp.Presentation.Controllers;
 
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Application.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// سرویس‌های شرط چندگانه
/// </summary>
[ApiController]
//[Route("/api/grid/v{version:apiVersion}/[controller]")]
//[ApiVersion("1.0")]
[Route("/api/grid/v1.0/[controller]")]
public class PrizeStoreController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrizeStoreController(IMediator mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// دریافت
    /// </summary>
    /// <returns></returns>
    [HttpGet] 
    public async Task<ActionResult<GetPrizeStoreResponse>> Get([FromQuery] GetPrizeStoreQuery vm)
    {
        return await _mediator.Send(vm);
    }
      

    /// <summary>
    /// حذف
    /// </summary>
    /// <returns></returns>
    [HttpDelete()]
    
    public async Task<ActionResult<DeletePrizeStoreResponse>> DeleteByIds([FromQuery] DeletePrizeStoreCommand vm)
    {
        return await _mediator.Send(vm);
    }


    /// <summary>
    /// درج
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    
    public async Task<ActionResult<AddPrizeStoreResponse>> Add([FromBody] AddPrizeStoreCommand vm)
    {
        return await _mediator.Send(vm);
    } 
}

