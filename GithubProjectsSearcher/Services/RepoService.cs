using GithubProjectsSearcher.Models;
using GithubProjectsSearcher.Services.Intefaces;
using GithubProjectsSearcher.ViewModels;
using Newtonsoft.Json.Linq;

namespace GithubProjectsSearcher.Services
{
    public class RepoService : IRepoService
    {
        private readonly ILogger<RepoService> _logger;
        public RepoService(ILogger<RepoService> logger)
        {
            _logger = logger;
        }

        public Task<List<RepoViewModel>> Get(string json)
        {
            JObject obj = JObject.Parse(json);

            List<RepoViewModel> repos = obj["items"].Select(repo => new RepoViewModel
            {
                Id = int.Parse(repo["id"].ToString()),
                RepoName = repo["name"].ToString(),
                AuthorName = repo["owner"]["login"].ToString(),
                Watchers = int.Parse(repo["watchers_count"].ToString()),
                Stargazers = int.Parse(repo["stargazers_count"].ToString()),
                Url = repo["html_url"].ToString()

            }).ToList<RepoViewModel>();

            return Task.FromResult(repos);
        }
    }
}
