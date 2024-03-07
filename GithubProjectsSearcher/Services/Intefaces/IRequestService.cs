using GithubProjectsSearcher.DB.Models;
using GithubProjectsSearcher.Models;

namespace GithubProjectsSearcher.Services.Intefaces
{
    public interface IRequestService
    {
        public Task Search(string requestText);
        public RequestViewModel? Get(string requestText);
        public RequestViewModel? Get();
        public Task Delete(Guid id);
    }
}
