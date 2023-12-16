#region

using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using Moq;

#endregion

namespace BloodRush.API.Tests.Mocks;

public static class DonorRepositoryMock
{
    public static Mock<IDonorRepository> GetDonorRepositoryMock()
    {
        static List<Donor> GetDonors()
        {
            var list = new List<Donor>
            {
                new Donor
                {
                    Id = new Guid("9a1cbadd-7571-4d0c-bdc2-b5487149d277"),
                    FirstName = "Jane",
                    BloodType = EBloodType.ABPositive,
                    Email = "jane@gmail.com",
                    Surname = "Kowalski",
                    Password = "string",
                    Sex = ESex.Female,
                    DateOfBirth = DateTime.Today,
                    PhoneNumber = "987654321",
                    HomeAddress = "string",
                    Pesel = "987654321",
                    MaxDonationRangeInKm = 0
                }
            };
            list.Add(new Donor
            {
                Id = new Guid("9a1cbadd-7571-4d0c-bdc2-b5487149d276"),
                FirstName = "John",
                BloodType = EBloodType.ABPositive,
                Email = "john@gmail.com",
                Surname = "Doe",
                Password = "string",
                Sex = ESex.Male,
                DateOfBirth = DateTime.Today,
                PhoneNumber = "123456789",
                HomeAddress = "string",
                Pesel = "123456789",
                MaxDonationRangeInKm = 0
            });
            return list;
        }

        var donorRepositoryMock = new Mock<IDonorRepository>();
        donorRepositoryMock.Setup(x => x.GetAllDonorsAsync()).ReturnsAsync(GetDonors());
        donorRepositoryMock.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>()))!.ReturnsAsync((Guid id) =>
        {
            var donors = GetDonors();
            var result = donors.FirstOrDefault(x => x.Id == id);
            if (result is null) throw new DonorNotFoundException();
            return result;
        });

        donorRepositoryMock.Setup(x => x.GetDonorByPhoneNumberAsync(It.IsAny<string>())).ReturnsAsync(
            (string phoneNumber) =>
            {
                var donors = GetDonors();
                var result = donors.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
                if (result is null) throw new DonorNotFoundException();
                return result;
            });


        donorRepositoryMock.Setup(x => x.AddDonorAsync(It.IsAny<Donor>()))
            .ReturnsAsync((Donor newDonor) =>
        {
            var donors = GetDonors();
            donors.Add(newDonor);
            return newDonor.Id;
        });


        donorRepositoryMock.Setup(x => x.DeleteDonorAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var donors = GetDonors();
            var result = donors.FirstOrDefault(x => x.Id == id);
            if (result is null) throw new DonorNotFoundException();
            donors.Remove(result);
            return true;
        });


        return donorRepositoryMock;
    }
}