using AutoMapper;
using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.MappingProfiles;
using BloodRush.API.Tests.Mocks;
using Moq;
using Should.Fluent;

namespace BloodRush.API.Tests.Donors.Commands;

public class AddNewDonorCommandHandlerTests
{
    private static readonly Donor Donor = new()
    {
        Id = Guid.Parse("9a1cbadd-7571-4d0c-bdc2-b5487149d276"),
        FirstName = "Maciej",
        BloodType = EBloodType.ABPositive,
        Email = "solowiej@gmail.com",
        Surname = "Doe",
        Password = "string",
        Sex = ESex.Male,
        DateOfBirth = DateTime.Today,
        PhoneNumber = "123456789",
        HomeAddress = "string",
        Pesel = "123456789"
    };
    private readonly Mock<IDonorRepository> _donorRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IEventPublisher> _eventPublisher;
    private readonly Mock<ILoginManager> _loginManager;
    
    public AddNewDonorCommandHandlerTests()
    {
        var config = new MapperConfiguration(
            cfg => cfg.AddProfile(new DonorMappingProfile()));
        _mapper = config.CreateMapper();
        _donorRepository = DonorRepositoryMock.GetDonorRepositoryMock();
        _eventPublisher = EventPublisherMock.GetEventPublisherMock();
        _loginManager = LoginManagerMock.GetLoginManagerMock(Donor);
    }


    [Fact]
    public void AddNewDonorCommandHandler_ShouldAddNewDonor()
    {
        var command = new AddNewDonorCommand
        {
            FirstName = Donor.FirstName,
            Surname = Donor.Surname,
            Password = Donor.Password,
            Sex = ESex.Male,
            DateOfBirth = DateTime.Today,
            BloodType = EBloodType.APositive,
            PhoneNumber = Donor.PhoneNumber,
            Email = Donor.Email,
            HomeAddress = Donor.HomeAddress,
            Pesel = Donor.Pesel
        };
        var handler = new AddNewDonorCommandHandler(_donorRepository.Object, _mapper, _eventPublisher.Object,
            _loginManager.Object);
        
        var id = handler.Handle(command, CancellationToken.None).Result;
        
        id.Should().Equal(Donor.Id);
    }
}