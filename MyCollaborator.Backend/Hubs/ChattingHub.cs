using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyCollaborator.Backend.Contexts;
using MyCollaborator.Backend.Hubs.Interfaces;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Hubs;

public class ChattingHub : Hub<IChattingHub>
{
    private readonly ApplicationDbContext _context;

    public ChattingHub(ApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask BroadCast(Message message)
    {
        await _context.Message.AddAsync(message);
        await Clients.Others.ReceiveFromAll(message);
        await _context.SaveChangesAsync();
    }

    public async ValueTask SpecificSend(Message message)
    {
        var to = await _context.UserConnection
            .Where(u => u.UserId == message.To)
            .ToListAsync();
        await Clients.Clients(to.Select(t => t.Connection)).ReceiveFromFriend(message);
    }
    
    
}