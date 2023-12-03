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
    private static readonly Guid LoggedInDonorId = Guid.Parse("9a1cbadd-7571-4d0c-bdc2-b5487149d276");
    private readonly Mock<IDonorRepository> _donorRepositoryMock = DonorRepositoryMock.GetDonorRepositoryMock();
    private readonly Mock<IUserContextAccessor> _userContextAccessorMock = UserContextAccessorMock.GetUserContextAccessorMock(LoggedInDonorId);
    private readonly IMapper _mapper;
    public GetDonorByIdQueryHandlerTests()
    {
        var config = new MapperConfiguration(
            cfg => cfg.AddProfile(new DonorMappingProfile()));
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void GetDonorByIdQueryHandler_ShouldReturn_DonorDto_When_Donor_Exists()
    {
        var handler = new GetDonorByIdQueryHandler(_donorRepositoryMock.Object, _mapper, _userContextAccessorMock.Object);
        var result = handler.Handle(new GetDonorByIdQuery { Id = new Guid("9a1cbadd-7571-4d0c-bdc2-b5487149d276") },
            CancellationToken.None).Result;
        result.Should().Not.Be.Null();
        result.Should().Be.OfType<DonorDto>();
    }
    
    [Fact]
    public void GetDonorByIdQueryHandler_ShouldThrow_DonorNotFoundException_When_Donor_Does_Not_Exist()
    {
        var handler = new GetDonorByIdQueryHandler(_donorRepositoryMock.Object, _mapper, _userContextAccessorMock.Object);
        
        var exception = Assert.Throws<AggregateException>(() => handler.Handle(new GetDonorByIdQuery { Id = new Guid("9a1cbadd-7571-4d0c-bdc2-b5487149d275") },
            CancellationToken.None).Result);
        
        Assert.IsType<DonorNotFoundException>(exception.InnerException);
        
    }
}