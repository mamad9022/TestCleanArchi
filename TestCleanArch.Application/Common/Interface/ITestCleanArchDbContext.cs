using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Common.Interface
{
   public interface ITestCleanArchDbContext: IDisposable
    {
        DbSet<Person> Persons { get; set; }
        Task SaveAsync(CancellationToken cancellationToken);

    }
}
