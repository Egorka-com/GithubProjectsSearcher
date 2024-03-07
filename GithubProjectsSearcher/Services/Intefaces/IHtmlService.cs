using GithubProjectsSearcher.ViewModels;

namespace GithubProjectsSearcher.Services.Intefaces
{
    public interface IHtmlService
    {
        public string GetRepoCardsHtml(List<RepoViewModel> repos);
    }
}
