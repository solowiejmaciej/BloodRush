using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using MediatR;

namespace BloodRush.API.Handlers.Account;

public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand>
{
    private readonly IProfilePictureRepository _profilePictureRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public UploadPhotoCommandHandler(
        IProfilePictureRepository profilePictureRepository,
        IUserContextAccessor userContextAccessor
        )
    {
        _profilePictureRepository = profilePictureRepository;
        _userContextAccessor = userContextAccessor;
    }
    public async Task Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        await _profilePictureRepository.AddProfilePictureAsync(donorId, request.Photo);
    }
}

public class UploadPhotoCommand : IRequest
{
    public IFormFile Photo { get; set; }
}