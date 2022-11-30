using Microsoft.EntityFrameworkCore;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
    {
    }
    
    public  virtual DbSet<User> User { get; set; }
    public  virtual DbSet<Message> Message { get; set; }
    public virtual DbSet<Friends> Friend { get; set; }
    public virtual DbSet<UserConnection> UserConnection { get; set; }
}