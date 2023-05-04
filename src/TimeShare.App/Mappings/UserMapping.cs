using Mapster;
using TimeShare.Application.Users.Commands.UpdateUserName;
using TimeShare.Contracts.Users;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.App.Mappings;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Guid, UserId>()
            .MapWith(src => UserId.Create(src));
        config.NewConfig<(Guid UserId, UpdateUserNameRequest Request), UpdateUserNameCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
        
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}