using Microsoft.EntityFrameworkCore;
using MyCollaborator.Backend.Contexts;
using MyCollaborator.Backend.Services.Interfaces;
using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Services;

public class ChattingService : IChattingService
{
    private readonly ApplicationDbContext _context;

    public ChattingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Response<IReadOnlyList<Friends>>> GetFriendsAsync(Guid user)
    {
        var rawData = await _context.Friend
            .Include(f => f.User)
            .Where(f => f.UserRequestee == user)
            .ToListAsync();

        return new Response<IReadOnlyList<Friends>>(Status.SUCCESS, "fetched", rawData);
    }

    public async ValueTask<Response<string>> SaveUserConnectionId(ConnectionId connectionId)
    {
        if (await _context.User.AnyAsync(u => u.Id == connectionId.Id))
        {
            return new Response<string>(Status.FAILED, "user not found");
        }
        
        var connection = new UserConnection
        {
            Id = Guid.NewGuid(),
            UserId = connectionId.Id,
            Connection = connectionId.connectionId
        };

        await _context.UserConnection.AddAsync(connection);
        await _context.SaveChangesAsync();

        return new Response<string>(Status.SUCCESS, "new connection added");
    }
}