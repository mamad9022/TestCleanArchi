using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.System.Commands.SeedSampleData
{
    public class SampleDataSeeder
    {
        private readonly ITestCleanArchDbContext _context;

        public SampleDataSeeder(ITestCleanArchDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.Persons.Any())
            {
                return;
            }

            await SeedPersonsAsync(cancellationToken);
           
        }

        private async Task SeedPersonsAsync(CancellationToken cancellationToken)
        {
            List<Person> personList = new List<Person>();

            for (int i = 0; i < 10; i++)
            {
                personList.Add(new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = "person firstname" + i,
                    LastName = "person lastname" + i,
                    Email = "mr28416@gmail.com"
                });
            }

            _context.Persons.AddRange(personList);

            await _context.SaveAsync(cancellationToken);
        }
  
    }

   
}