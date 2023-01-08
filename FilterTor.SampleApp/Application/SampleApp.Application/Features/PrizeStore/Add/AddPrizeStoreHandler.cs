namespace SampleApp.Application.Features;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using SampleApp.Application.Gateways;
using SampleApp.Core.Entities;
using FilterTor.Factory;
using SampleApp.FilterTorEx.Entities;

public sealed class AddPrizeStoreHandler : IRequestHandler<AddPrizeStoreCommand, AddPrizeStoreResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddPrizeStoreHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddPrizeStoreResponse> Handle(AddPrizeStoreCommand request, CancellationToken cancellationToken)
    {
        if (request.CustomerCondition is not null && new CustomerResolver().Validate(request.CustomerCondition) == false)
            throw new NotImplementedException("AddPrizeStoreHandler > invalid CustomerCondition");

        if (request.InvoiceCondition is not null && new InvoiceResolver().Validate(request.InvoiceCondition) == false)
            throw new NotImplementedException("AddPrizeStoreHandler > invalid InvoiceCondition");

        var prizeStore = PrizeStore.Create(
                request.StartDate,
                request.EndDate,
                request.InvoiceCondition,
                request.CustomerCondition);

        await _unitOfWork.PrizeStores.AddAsync(prizeStore);

        await _unitOfWork.SaveChangesAsync();

        return new AddPrizeStoreResponse() { IsSucceed = true };
    }
}
