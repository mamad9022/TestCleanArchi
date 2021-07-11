using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestCleanArch.Application.Common.Interface;

namespace TestCleanArch.Application.System.Commands.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest
    {
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand, Unit>
    {
        private readonly ITestCleanArchDbContext _context;

        public SeedSampleDataCommandHandler(ITestCleanArchDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_context);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
