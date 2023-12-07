using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using MediatR;

namespace BloodRush.API.Handlers.Account;

public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IProfilePictureRepository _profilePictureRepository;

    public DeletePhotoCommandHandler(
        IUserContextAccessor userContextAccessor,
        IProfilePictureRepository profilePictureRepository
        )
    {
        _userContextAccessor = userContextAccessor;
        _profilePictureRepository = profilePictureRepository;
    }
    public async Task Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        await _profilePictureRepository.DeleteProfilePictureByDonorIdAsync(donorId);
    }
}

public record DeletePhotoCommand :IRequest;