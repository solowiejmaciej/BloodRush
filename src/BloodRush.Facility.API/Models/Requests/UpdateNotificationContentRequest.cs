using BloodRush.Contracts.Enums;
using FluentValidation;

namespace BloodRush.DonationFacility.API.Models.Requests;

public class UpdateNotificationContentRequest
{
    public ENotificationType NotificationType { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

public class UpdateNotificationContentRequestValidator : AbstractValidator<UpdateNotificationContentRequest>
{
    public UpdateNotificationContentRequestValidator()
    {
        RuleFor(x => x.NotificationType).IsInEnum();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
    }
}