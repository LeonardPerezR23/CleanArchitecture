using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArquitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class StreamerRepository : RepositoryBase<Streamer>, IStreamerRepository
    {
        public StreamerRepository(StreamerDbContext context) : base(context)
        {

        }
    }
}
