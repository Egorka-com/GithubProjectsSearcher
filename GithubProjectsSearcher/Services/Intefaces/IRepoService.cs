using System.Diagnostics.CodeAnalysis;
using GithubProjectsSearcher.DB.Models;
using GithubProjectsSearcher.Models;
using GithubProjectsSearcher.ViewModels;
using Microsoft.Build.Evaluation;

namespace GithubProjectsSearcher.Services.Intefaces
{
    public interface IRepoService
    {
        public Task<List<RepoViewModel>> Get(string resultJsonString);
    }
}
