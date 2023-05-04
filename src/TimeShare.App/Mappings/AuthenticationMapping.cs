using Mapster;
using TimeShare.Application.Authentication.Common;
using TimeShare.Contracts.Authentication;

namespace TimeShare.App.Mappings;

public class AuthenticationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.Value)
            .Map(dest => dest, src => src.User);
    }
}