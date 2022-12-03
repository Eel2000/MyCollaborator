using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyCollaborator.Backend.Contexts;
using MyCollaborator.Backend.Hubs.Interfaces;
using MyCollaborator.Backend.Services.Interfaces;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Hubs;

public class ChattingHub : Hub<IChattingHub>
{
    private readonly ApplicationDbContext _context;
    private readonly ICachingService _cachingService;

    public ChattingHub(ApplicationDbContext context, ICachingService cachingService)
    {
        _context = context;
        _cachingService = cachingService;
    }

    public async ValueTask BroadCast(Message message)
    {
        var caching = await _cachingService
            .SaveItemInTheCacheAsync<Message>(message.From.ToString(), message, DateTimeOffset.Now.AddHours(1));
        if (!caching)
        {
            //if the caching fails, save directly into database
            await _context.Message.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        await Clients.Others.ReceiveFromAll(message);
    }

    public async ValueTask SpecificSend(Message message)
    {
        var to = await _context.UserConnection
            .Where(u => u.UserId == message.To)
            .ToListAsync();

        await _cachingService
            .SaveItemInTheCacheAsync<Message>(message.From.ToString(), message, DateTimeOffset.Now.AddHours(1));

        await Clients.Clients(to.Select(t => t.Connection)).ReceiveFromFriend(message);
    }
}