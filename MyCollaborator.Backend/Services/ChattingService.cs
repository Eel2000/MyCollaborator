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
}