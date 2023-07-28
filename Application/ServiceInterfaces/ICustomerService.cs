using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface ICustomerService : IGenericService<Customer>
    {
        #region Customer

        Task<List<CustomerDto>> GetAllCustomers();

        Task<CustomerDto> GetCustomer(int ID);

        Task<CustomerDto> DeleteCustomer(int ID);

        Task<CustomerDto> UpsertCustomer(CustomerDto item);

        #endregion Customer

    }
}