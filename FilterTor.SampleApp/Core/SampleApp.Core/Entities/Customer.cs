namespace SampleApp.Core.Entities;

using SampleApp.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Customer : IHasKey<long>
{
    // possibly encapsulation violation
    public required long Id { get; init; }

    public string Name { get; private set; }

    public DateTime RegisteredDate { get; private set; }

    public decimal Credit { get; private set; }

    // Encapsulating collection
    private readonly List<CustomerGroup> _customerGroups;

    public IEnumerable<CustomerGroup> CustomerGroups
    {
        // doesn’t make a copy of the list’s contents
        // link: https://ardalis.com/encapsulated-collections-in-entity-framework-core/
        get => _customerGroups.AsReadOnly();
    }


    public Customer(string name, DateTime registeredDate, decimal credit)
    {
        this.Name = name;
        this.RegisteredDate = registeredDate;
        this.Credit = credit;
        this._customerGroups = new List<CustomerGroup>();
    }


    public void AddCustomerGroup(CustomerGroupType type, DateTime date)
    {
        if (RegisteredDate > date)
            throw new NotImplementedException("Customer > AddCustomerGroup");

        _customerGroups.Add(new CustomerGroup { Id = 0, Type = type, CreateTime = date });
    }

    public void RemoveCustomerGroup(CustomerGroupType type)
    {
        var customerGroup = _customerGroups.FirstOrDefault(cg => cg.Type == type);

        if (customerGroup is null)
            return;

        _customerGroups.Remove(customerGroup);
    }

}
