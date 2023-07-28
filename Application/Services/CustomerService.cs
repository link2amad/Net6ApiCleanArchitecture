using Application.Dto;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Customer> _customerRepository;

        public CustomerService(
            IGenericRepository<Customer> customerRepository,
            IMapper mapper
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        #region Customer

        public async Task<List<CustomerDto>> GetAllCustomers()
        {
            var items = _customerRepository.GetAll().Result.ToList();
            return _mapper.Map<List<Customer>, List<CustomerDto>>(items);
        }

        public async Task<CustomerDto> GetCustomer(int ID)
        {
            var item = _customerRepository.GetFirst(x => x.ID == ID);
            return _mapper.Map<Customer, CustomerDto>(item);
        }

        public async Task<CustomerDto> DeleteCustomer(int ID)
        {
            var item = _customerRepository.GetFirst(x => x.ID == ID);
            if (item != null)
            {
                _customerRepository.Delete(item);
                _customerRepository.Complete();
            }
            return _mapper.Map<Customer, CustomerDto>(item);
        }

        public async Task<CustomerDto> UpsertCustomer(CustomerDto item)
        {
            Customer DBitem;
            if (item.ID == 0)
            {
                DBitem = _mapper.Map<CustomerDto, Customer>(item);
                await _customerRepository.Add(DBitem);
            }
            else
            {
                DBitem = _customerRepository.GetFirst(x => x.ID == item.ID);
                if (DBitem != null)
                {
                    item.CreatedDate = DBitem.CreatedDate;
                    item.CreatedBy = DBitem.CreatedBy;
                    DBitem = _mapper.Map<CustomerDto, Customer>(item, DBitem);
                    _customerRepository.Update(DBitem);
                }
            }
            if (DBitem != null)
                _customerRepository.Complete();
            return _mapper.Map<Customer, CustomerDto>(DBitem);
        }

        #endregion Customer
    }
}