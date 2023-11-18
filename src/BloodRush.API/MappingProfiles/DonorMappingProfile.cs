#region

using AutoMapper;
using BloodRush.API.Dtos;
using BloodRush.API.Entities;
using BloodRush.API.Handlers;

#endregion

namespace BloodRush.API.MappingProfiles;

public class DonorMappingProfile : Profile
{
    public DonorMappingProfile()
    {
        CreateMap<Donor, DonorDto>();
        CreateMap<DonorDto, Donor>();


        CreateMap<Donor, AddNewDonorCommand>();
        CreateMap<AddNewDonorCommand, Donor>();
    }
}