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
/// سرویس‌های لیست
/// </summary>
[ApiController]
[Route("/api/filtertor/v1.0/[controller]")]
public class TableController : ControllerBase
{
    private readonly IMediator _mediator;

    public TableController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// مشتریان
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    [HttpPost("Customers")]
    public async Task<ActionResult<ListCustomerResponse>> ListCustomers([FromBody] ListCustomerQuery vm)
    {
        return await _mediator.Send(vm);
    }

    /// <summary>
    /// فاکتورها
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    [HttpPost("Invoices")]
    public async Task<ActionResult<ListInvoiceResponse>> ListInvoices([FromBody] ListInvoiceQuery vm)
    {
        return await _mediator.Send(vm);
    }

}
