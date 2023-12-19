using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Handlers.Donations;

namespace BloodRush.DonationFacility.API.MappingProfiles;

public class DonationMappingProfile : Profile
{
    public DonationMappingProfile()
    {
        CreateMap<Donation, AddNewDonationCommand>();
        CreateMap<AddNewDonationCommand, Donation>();
        CreateMap<DonationDto, Donation>();
        CreateMap<Donation, DonationDto>();
    }
}