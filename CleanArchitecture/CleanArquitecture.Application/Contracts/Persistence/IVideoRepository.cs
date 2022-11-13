using CleanArchitecture.Domain;

namespace CleanArquitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<Video> GetVideoByNombre(string nombreVideo);
        Task<IEnumerable<Video>> GetVideoByUserName(string username);
    }
}
