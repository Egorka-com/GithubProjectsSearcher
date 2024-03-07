using GithubProjectsSearcher.ViewModels;

namespace GithubProjectsSearcher.Models
{
    public class RequestViewModel
    {
        public Guid Id { get; set; }
        public string RequestText{ get; set; }
        public List<RepoViewModel> Repos { get; set; }


    }
}
