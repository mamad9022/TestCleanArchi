using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Persistence.Context
{
  public  class TestCleanArchDbContext:DbContext,ITestCleanArchDbContext
    {
        public TestCleanArchDbContext()
        {
            
        }
        public TestCleanArchDbContext(DbContextOptions<TestCleanArchDbContext> options)
            : base(options)
        {
        }
      
        public DbSet<Person> Persons { get; set; }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=DESKTOP-HBEGIIU\SQLEXPRESS;Initial Catalog =TestDb;MultipleActiveResultSets=true;Integrated Security=True;User Id=sa;Password=AbC123_-");
            }
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestCleanArchDbContext).Assembly);
        }
    }
}
