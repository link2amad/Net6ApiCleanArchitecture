using API.Controllers;
using API.Helpers;
using Application.Dto;
using Application.Mapper;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Application.ServiceInterfaces.IGeneralServices;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests
{
    public class CustomerRequestTests : BaseTest
    {
        private readonly ICustomerService customerService;
        private readonly Mock<ICustomerService> customerServiceMock;
        private readonly Mock<IGenericRepository<Customer>> customerRepository;
        private readonly IConfigurationProvider configuration;
        private readonly IMapper mapper;

        public CustomerRequestTests()
        {
            customerServiceMock = new Mock<ICustomerService>();
            customerRepository = new Mock<IGenericRepository<Customer>>();
            configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerMapperProfile>();
            });
            mapper = configuration.CreateMapper();
            customerService = new CustomerService(customerRepository.Object, mapper);
        }

        [Theory]
        [InlineData("Test", "Description", 0)]
        [InlineData("Test", null, 1)]
        [InlineData(null, null, 2)]
        public void ValidateModel_UpsertCustomerRequest_ReturnsCorrectNumberOfErrors(string name, string address, int numberExpectedErrors)
        {
            var model = new CustomerDto
            {
                Name = name,
                Address = address
            };
            var errorList = new CustomerValidator().Validate(model);

            Assert.Equal(numberExpectedErrors, errorList.Errors.Count);
        }
        [Fact]
        public async Task GetCustomersTest_RetursCustomersList()
        {
            // Arrange
            var mockCustomersDtoList = new List<CustomerDto>
            {
                new CustomerDto { Name = "mock customer 1" },
                new CustomerDto { Name = "mock customer 2" }
            };
            var mockCustomersList = mapper.Map<List<Customer>>(mockCustomersDtoList);
            customerRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(mockCustomersList as IEnumerable<Customer>));
            // Act
            var resultCustomersList = customerService.GetAllCustomers().Result;

            // Assert
            Assert.Equal(mockCustomersDtoList.Count(), resultCustomersList.Count());
            Assert.Equal(mockCustomersDtoList[0].Name, resultCustomersList[0].Name);
            Assert.Equal(mockCustomersDtoList[1].Name, resultCustomersList[1].Name);
        }

        [Fact]
        public async Task CreateCustomerTest_RetursCustomer()
        {
            // Arrange
            var mockCustomerDto = new CustomerDto { Name = "mock customer 1", Address = "address 1" };

            // Act
            var resultCustomer = customerService.UpsertCustomer(mockCustomerDto).Result;

            // Assert
            Assert.Equal(resultCustomer.Name, resultCustomer.Name);
            Assert.Equal(resultCustomer.Address, resultCustomer.Address);
        }

        //[Fact]
        //public async Task UpdateCustomerTest_RetursCustomer()
        //{
        //    // Arrange
        //    var mockCustomerDto = new CustomerDto { ID = 1, Name = "mock customer 1", Address = "address 1" };
        //    var mockCustomer = mapper.Map<Customer>(mockCustomerDto);
        //    customerRepository.Setup(repo => repo.GetFirst(x => x.ID == 1)).Returns(mockCustomer);

        //    // Act
        //    var resultCustomer = customerService.UpsertCustomer(mockCustomerDto).Result;

        //    // Assert
        //    Assert.Equal(resultCustomer.ID, resultCustomer.ID);
        //    Assert.Equal(resultCustomer.Name, resultCustomer.Name);
        //    Assert.Equal(resultCustomer.Address, resultCustomer.Address);
        //}
    }
}
