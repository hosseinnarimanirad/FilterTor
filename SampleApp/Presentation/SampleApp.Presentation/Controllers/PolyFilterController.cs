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
[Route("/api/grid/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PolyFilterController : ControllerBase
{
    private readonly IMediator _mediator;

    public PolyFilterController(IMediator mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// دریافت اطلاعات شرط چندگانه
    /// </summary>
    /// <returns></returns>
    [HttpGet] 
    public async Task<ActionResult<GetPolyFilterResponse>> Get([FromQuery] GetPolyFilterQuery vm)
    {
        return await _mediator.Send(vm);
    }


    /// <summary>
    /// دریافت اطلاعات شرط چندگانه (حالت چندتایی)
    /// </summary>
    /// <returns></returns>
    [HttpGet("ListByIds")]
    
    public async Task<ActionResult<ListByIdsPolyFilterResponse>> ListByIds([FromQuery] ListByIdsPolyFilterQuery vm)
    {
        return await _mediator.Send(vm);
    }


    /// <summary>
    /// حذف شرط چندگانه (حالت چندتایی)
    /// </summary>
    /// <returns></returns>
    [HttpDelete("DeleteByIds")]
    
    public async Task<ActionResult<DeleteByIdsPolyFilterResponse>> DeleteByIds([FromQuery] DeleteByIdsPolyFilterCommand vm)
    {
        return await _mediator.Send(vm);
    }


    /// <summary>
    /// اضافه کردن شرط چندگانه
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    
    public async Task<ActionResult<AddPolyFilterResponse>> Add([FromBody] AddPolyFilterCommand vm)
    {
        return await _mediator.Send(vm);
    }


    /// <summary>
    /// ویرایش شرط چندگانه
    /// </summary>
    /// <returns></returns>
    [HttpPut()]
    
    public async Task<ActionResult<UpdatePolyFilterResponse>> Update([FromBody] UpdatePolyFilterCommand vm)
    {
        return await _mediator.Send(vm);
    }

     
}

