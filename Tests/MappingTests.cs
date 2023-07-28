using Application.Dto;
using Application.Mapper;
using AutoMapper;
using Domain.Entities;
using System.Runtime.Serialization;

namespace Tests
{
    public class MappingTests : BaseTest
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerMapperProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }
        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Customer), typeof(CustomerDto))]
        [InlineData(typeof(CustomerDto), typeof(Customer))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(origin);

            _mapper.Map(instance, origin, destination);
        }
    }
}
