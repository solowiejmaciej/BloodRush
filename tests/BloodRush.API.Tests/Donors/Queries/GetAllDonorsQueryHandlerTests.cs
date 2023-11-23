using AutoMapper;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces;
using BloodRush.API.MappingProfiles;
using BloodRush.API.Tests.Mocks;
using Moq;
using Should.Fluent;

namespace BloodRush.API.Tests.Donors.Queries;

public class GetAllDonorsQueryHandlerTests
{
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly IMapper _mapper;
    public GetAllDonorsQueryHandlerTests()
    {
        var config = new MapperConfiguration(
            cfg => cfg.AddProfile(new DonorMappingProfile()));
        _mapper = config.CreateMapper();
        _donorRepositoryMock = DonorRepositoryMock.GetDonorRepositoryMock();
    }
    
    [Fact]
    public void GetAllDonorsQueryHandler_ShouldReturnAllDonors()
    {
        var handler = new GetAllDonorsQueryHandler(_donorRepositoryMock.Object, _mapper);
        var result = handler.Handle(new GetAllDonorsQuery(), CancellationToken.None).Result;
        result.Should().Not.Be.Null();
        result.Count.Should().Equal(2);
    }
}