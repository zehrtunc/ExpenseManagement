using Microsoft.EntityFrameworkCore;

namespace Transact.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Domain.Customer> Customers { get; set; }
    public DbSet<Domain.Account> Accounts { get; set; }
    public DbSet<Domain.AccountTransaction> AccountTransactions { get; set; }
    public DbSet<Domain.CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<Domain.CustomerPhone> CustomerPhones { get; set; }
    public DbSet<Domain.EftTransaction> EftTransactions { get; set; }
    public DbSet<Domain.MoneyTransfer> MoneyTransfers { get; set; }
}
