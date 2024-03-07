using System.Text;
using GithubProjectsSearcher.Models;
using GithubProjectsSearcher.Services.Intefaces;
using GithubProjectsSearcher.ViewModels;

namespace GithubProjectsSearcher.Services
{
    public class HtmlService : IHtmlService
    {
        private readonly IRequestService _requestService;

        public HtmlService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public string GetRepoCardsHtml(List<RepoViewModel> repos)
        {

            string rowTemp = "<div class=\"row\">\r\n{0}</div>";
            StringBuilder cardsHtml = new();

            foreach (var repo in repos)
            {
                cardsHtml.Append($"<div class=\"card repoCard\" style=\"width: 14rem;\">\r\n" +
                                    $"<div class=\"card-header\">\r\n            " +
                                        $"<h5 class=\"card-title\"><span id=\"repoName\">{repo.RepoName}</span></h5>\r\n" +
                                    $"</div>\r\n" +
                                    $"<div class=\"card-body\">\r\n" +
                                        $"<p class=\"card-text\">Author: <span id=\"authorName\">{repo.AuthorName}</span></p>\r\n" +
                                        $"<p class=\"card-text\">Stars: <span id=\"statsCount\">{repo.Stargazers}</span></p>\r\n" +
                                        $"<p class=\"card-text\">Watchers: <span id=\"watchersCount\">{repo.Watchers}</span></p>\r\n" +
                                        $"<a href=\"{repo.Url}\" target=\"_blank\" class=\"btn btn-primary\">Открыть</a>\r\n" +
                                    $"</div>\r\n" +
                                 $"</div>\r\n");
            }

            string htmlResult = string.Format(rowTemp, cardsHtml);

            return htmlResult;
        }

    }
}
