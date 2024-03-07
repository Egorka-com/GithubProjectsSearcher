using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
using GithubProjectsSearcher.DB;
using GithubProjectsSearcher.DB.Models;
using GithubProjectsSearcher.Models;
using GithubProjectsSearcher.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Versioning;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace GithubProjectsSearcher.Services
{
    public class RequestService: IRequestService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IRepoService _repoService;
        private readonly ILogger<RequestService> _logger;
        private readonly IConfiguration _config;
        private readonly MemoryCache _cache;

        public RequestService
                    (ApplicationDBContext dbContext, 
                    IRepoService repoService,
                    ILogger<RequestService> logger, 
                    IConfiguration config,
                    MemoryCache cache)
        {
            _dbContext = dbContext;
            _repoService = repoService;
            _logger = logger;
            _config = config;
            _cache = cache;
        }

        public async Task Search(string requestText)
        {
            _logger.LogInformation($"Search by {requestText}");
            try
            {
                if (!_dbContext.Requests.Any(x=>x.RequestText == requestText))
                {
                    _logger.LogInformation($"Search on Github");

                    using (var httpClient = new HttpClient())
                    {
                        string gitApiRequestText = _config.GetValue<string>("GithubApiUrl") + requestText;

                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                        var httpResponseJson = httpClient.GetAsync(gitApiRequestText).Result;

                        if (httpResponseJson.IsSuccessStatusCode)
                        {
                            var requestModel = new RequestDBModel(requestText, httpResponseJson.Content.ReadAsStringAsync().Result);

                            _logger.LogInformation($"Add request in db");
                            _dbContext.Requests.Add(requestModel);

                            await _dbContext.SaveChangesAsync();

                        }
                        else
                        {
                            _logger.LogError( "Couldn't get data from Api");
                        }
                    }
                }
                _cache.Set("requestText", requestText);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Search error");
            }

        }


        public RequestViewModel? Get(string? requestText)
        {
            _logger.LogInformation("Get request view");
            try
            {
                if (!_dbContext.Requests.Any() && requestText != null)
                {
                    throw new Exception("Repo request not found");
                }

                var requestModel =  _dbContext.Requests.Single(x => x.RequestText == requestText) ?? throw new Exception("Repo request not found");
                var repos = _repoService.Get(requestModel.RequestResultJson).Result;

                RequestViewModel requestView = new RequestViewModel()
                {
                    Id = requestModel.Id,
                    Repos = repos,
                    RequestText = requestModel.RequestText
                };

                return requestView;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,null);
                return null;
            }

        }

        public RequestViewModel? Get()
        {
            string? requestText = _cache.Get<string>("requestText");
            return Get(requestText);
        }



        public async Task Delete(Guid id)
        {
            try
            {
                var requestModel = _dbContext.Requests.Find(id);
                if (requestModel == null)
                {
                    return;
                }

                _dbContext.Requests.Remove(requestModel);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Couldn't delete request ({0}) in db",id);
                throw;
            }
        }

    }
}
