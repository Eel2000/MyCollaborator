using Microsoft.EntityFrameworkCore;
using MyCollaborator.Backend.Contexts;
using MyCollaborator.Backend.Services.Interfaces;
using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext _context;

    public AuthenticationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Response<User>> AuthenticateAsync(AuthenticationQuery query)
    {
        if (_context.User.Any(u => u.Telephone == query.Telephone))
        {
            var usr = await _context.User.FirstOrDefaultAsync(u => u.Telephone == query.Telephone);
            return new Response<User>(Status.SUCCESS, "connected", usr);
        }

        User user = new()
        {
            Id = Guid.NewGuid(),
            Username = query.Username,
            Telephone = query.Telephone,
        };
        await _context.User.AddRangeAsync(user);
        await _context.SaveChangesAsync();
        return new Response<User>(Status.SUCCESS, "Account created & authenticated!", user);
    }

    public async ValueTask<Response<string>> UpdateAccountDetailsAsync(EditAccount editAccount)
    {
        throw new NotImplementedException();
    }
}