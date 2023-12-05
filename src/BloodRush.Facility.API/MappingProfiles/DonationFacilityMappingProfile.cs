using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;

namespace BloodRush.DonationFacility.API.MappingProfiles;

public class DonationFacilityMappingProfile : Profile
{
    public DonationFacilityMappingProfile()
    {
        CreateMap<Entities.DonationFacility, DonationFacilityDto>();
        CreateMap<DonationFacilityDto, Entities.DonationFacility>();
    }
}