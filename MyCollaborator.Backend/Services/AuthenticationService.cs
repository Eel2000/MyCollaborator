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

    public async ValueTask<Response<AuthenticateResponse>> AuthenticateAsync(AuthenticationQuery query)
    {
        if (_context.User.Any(u => u.Telephone == query.Telephone))
        {
            var usr = await _context.User.FirstOrDefaultAsync(u => u.Telephone == query.Telephone);
            var msgs = await _context.Message.Where(m => m.To == usr.Id).ToListAsync();
            var frds = await _context.Friend
                .Include(f => f.User)
                .ThenInclude(uf => uf.UserConnections)
                .Where(f => f.UserRequestee == usr.Id)
                .ToListAsync();
            AuthenticateResponse response = new()
            {
                User = usr,
                Messages = msgs,
                FriendsCollection = frds
            };
            return new Response<AuthenticateResponse>(Status.SUCCESS, "connected", response);
        }

        User user = new()
        {
            Id = Guid.NewGuid(),
            Username = query.Username,
        };
        await _context.User.AddRangeAsync(user);
        await _context.SaveChangesAsync();

        AuthenticateResponse authenticateResponse = new()
        {
            User = user
        };

        return new Response<AuthenticateResponse>(Status.SUCCESS, "Account created & authenticated!", authenticateResponse);
    }

    public async ValueTask<Response<string>> UpdateAccountDetailsAsync(EditAccount editAccount)
    {
        throw new NotImplementedException();
    }
}