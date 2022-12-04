using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Services.Interfaces;

public interface IAuthenticationService
{
    ValueTask<Response<User>> AuthenticateAsync(AuthenticationQuery query);

    ValueTask<Response<string>> UpdateAccountDetailsAsync(EditAccount editAccount);
}