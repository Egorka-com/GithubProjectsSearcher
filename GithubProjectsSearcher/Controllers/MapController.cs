using System.Text.Json.Nodes;
using GithubProjectsSearcher.DB;
using GithubProjectsSearcher.DB.Models;
using GithubProjectsSearcher.Models;
using GithubProjectsSearcher.Services;
using GithubProjectsSearcher.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using NuGet.Common;

namespace GithubProjectsSearcher.Controllers
{
    [ApiController]
    [Route("api/find")]
    public class MapController : Controller
    {
        [HttpPost]
        public async Task Post(IRequestService requestService)
        {
            try
            {
                JsonObject obj = await Request.ReadFromJsonAsync<JsonObject>();
                string? requestText = obj["requestText"].ToString();
                await requestService.Search(requestText);
                Ok();
            }
            catch
            {
                NotFound();
            }
        }

        [HttpGet]
        public async Task Get(IHtmlService _htmlService,IRequestService requestService)
        {
            try
            {
                var requestModel = requestService.Get();

                Response.ContentType = "text/html; charset=utf-8";
                Response.Headers.Accept = "text/html; charset=utf-8";
                Response.Cookies.Append("requestId", requestModel.Id.ToString());
                Response.StatusCode = 200;

                await Response.WriteAsync(_htmlService.GetRepoCardsHtml(requestModel.Repos));
            }
            catch
            {
                NotFound();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id, IRequestService requestService)
        {
            try
            {
                await requestService.Delete(id);
                Ok();
            }
            catch 
            {
                Problem("DeleteRequestException");
            }
        }
    }
}
