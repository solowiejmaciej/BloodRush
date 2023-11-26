using AutoMapper;
using BloodRush.API.Entities;
using BloodRush.API.Handlers.Donations;

namespace BloodRush.API.MappingProfiles;

public class DonationMappingProfile : Profile
{
    public DonationMappingProfile()
    {
        CreateMap<Donation, AddNewDonationCommand>();
        CreateMap<AddNewDonationCommand, Donation>();
    }
}