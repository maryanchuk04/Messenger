using System.Reflection;
using Messenger.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.db.EF;

public class MessengerContext : DbContext
{
    public MessengerContext(DbContextOptions<MessengerContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Message> Messages { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<UserToken> UserTokens { get; set; }
}