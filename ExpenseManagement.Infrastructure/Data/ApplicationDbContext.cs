using ExpenseManagement.Base.Entities;
using ExpenseManagement.Infrastructure.Fluents;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExpenseManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserFluent).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<ExpenseDocument> ExpenseDocuments { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

}
