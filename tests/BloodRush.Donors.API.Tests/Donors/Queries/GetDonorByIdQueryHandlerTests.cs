using AutoMapper;
using BloodRush.API.Dtos;
using BloodRush.API.Exceptions;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.MappingProfiles;
using BloodRush.API.Tests.Mocks;
using Moq;
using Should.Fluent;

namespace BloodRush.API.Tests.Donors.Queries;

public class GetDonorByIdQueryHandlerTests
{
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly IMapper _mapper;
    public GetDonorByIdQueryHandlerTests()
    {
        var config = new MapperConfiguration(
            cfg => cfg.AddProfile(new DonorMappingProfile()));
        _mapper = config.CreateMapper();
        _donorRepositoryMock = DonorRepositoryMock.GetDonorRepositoryMock();
    }

    [Fact]
    public void GetDonorByIdQueryHandler_ShouldReturn_DonorDto_When_Donor_Exists()
    {
        var handler = new GetDonorByIdQueryHandler(_donorRepositoryMock.Object, _mapper);
        var result = handler.Handle(new GetDonorByIdQuery { Id = new Guid("9a1cbadd-7571-4d0c-bdc2-b5487149d276") },
            CancellationToken.None).Result;
        result.Should().Not.Be.Null();
        result.Should().Be.OfType<DonorDto>();
    }
    
    [Fact]
    public void GetDonorByIdQueryHandler_ShouldThrow_DonorNotFoundException_When_Donor_Does_Not_Exist()
    {
        var handler = new GetDonorByIdQueryHandler(_donorRepositoryMock.Object, _mapper);
        
        var exception = Assert.Throws<AggregateException>(() => handler.Handle(new GetDonorByIdQuery { Id = new Guid("9a1cbadd-7571-4d0c-bdc2-b5487149d275") },
            CancellationToken.None).Result);
        
        Assert.IsType<DonorNotFoundException>(exception.InnerException);
        
    }
}