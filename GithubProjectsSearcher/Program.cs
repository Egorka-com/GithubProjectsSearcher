using GithubProjectsSearcher.DB;
using GithubProjectsSearcher.Services;
using GithubProjectsSearcher.Services.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GithubProjectsSearcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            builder.Services.AddRazorPages();

            string connection = builder.Configuration.GetConnectionString("MySqlConnectionString")!;

            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(connection,new MySqlServerVersion(new Version(8,0,36))));

            builder.Services.AddScoped<IRequestService,RequestService>();
            builder.Services.AddTransient<IRepoService,RepoService>();
            builder.Services.AddTransient<IHtmlService,HtmlService>();
            builder.Services.AddSingleton<MemoryCache>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.UseEndpoints(x =>
            {
                x.MapControllers();
            });

            app.Run();
        }
    }
}
