using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using MediatR;

namespace BloodRush.API.Handlers.Account;

public class GetPhotoQueryHandler : IRequestHandler<GetPhotoQuery, Stream>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IProfilePictureRepository _profilePictureRepository;

    public GetPhotoQueryHandler(
        IUserContextAccessor userContextAccessor,
        IProfilePictureRepository profilePictureRepository
        )
    {
        _userContextAccessor = userContextAccessor;
        _profilePictureRepository = profilePictureRepository;
    }
    public Task<Stream> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        return _profilePictureRepository.GetProfilePictureByDonorIdAsync(donorId);
    }
}

public record GetPhotoQuery : IRequest<Stream>;