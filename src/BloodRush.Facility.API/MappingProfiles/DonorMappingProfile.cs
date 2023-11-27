#region

using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Entities;

#endregion

namespace BloodRush.DonationFacility.API.MappingProfiles;

public class DonorMappingProfile : Profile
{
    public DonorMappingProfile()
    {
        CreateMap<Donor, DonorDto>();
        CreateMap<DonorDto, Donor>();
    }
}