using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GithubProjectsSearcher.ViewModels
{
    public class RepoViewModel
    {
        public long Id { get; set; }
        public string RepoName { get; set; }
        public string AuthorName { get; set; }
        public int Stargazers { get; set; }
        public int Watchers { get; set; }
        public string Url { get; set; }

        public RepoViewModel(long id, string repoName, string authorName, int stargazers, int watchers, string url)
        {
            Id = id;
            RepoName = repoName;
            AuthorName = authorName;
            Stargazers = stargazers;
            Watchers = watchers;
            Url = url;
        }

        public RepoViewModel() { }

    }
}
