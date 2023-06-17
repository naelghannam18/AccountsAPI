using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Context.Context;

public class AccountsContext : DbContext, IDbContext
{
    #region Constructor

    public AccountsContext(DbContextOptions options) : base(options)
    {

    }
    #endregion

    #region On Model Creating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Sender)
            .WithMany(a => a.SentTransactions)
            .HasForeignKey(t => t.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Receiver)
            .WithMany(a => a.ReceivedTransactions)
            .HasForeignKey(t => t.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);


        base.OnModelCreating(modelBuilder);
    }
    #endregion

    #region DbSets
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
    #endregion

    #region Save Changes With Concurrency Handling
    public async Task<int> SaveChangesWithConcurrencyHandlingAsync()
    {
        try
        {
            return await SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                if (entry.State == EntityState.Deleted)
                {
                    throw; // TODO: LOG
                }
                else
                {
                    entry.OriginalValues.SetValues(await entry.GetDatabaseValuesAsync());
                }
            }
            throw;
        }
    }
    #endregion
}

#region Interface to Implement Save changes with Concurrency Handling
internal interface IDbContext
{
    Task<int> SaveChangesWithConcurrencyHandlingAsync();
}
#endregion
