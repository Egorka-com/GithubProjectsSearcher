using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GithubProjectsSearcher.DB.Models
{
    public class RequestDBModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? RequestText { get; set; }
        public string? RequestResultJson { get; set; }

        public RequestDBModel(string requestText, string requestResultJson)
        {
            Id = Guid.NewGuid();
            RequestText = requestText;
            RequestResultJson = requestResultJson;
        }

    }
}
