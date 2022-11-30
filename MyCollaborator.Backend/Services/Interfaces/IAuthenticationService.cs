using MyCollaborator.Shared.DTOs;

namespace MyCollaborator.Backend.Services.Interfaces;

public interface IAuthenticationService
{
    ValueTask<Response<AuthenticateResponse>> AuthenticateAsync(AuthenticationQuery query);

    ValueTask<Response<string>> UpdateAccountDetailsAsync(EditAccount editAccount);
}