using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using V.GithubViewer.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace V.GithubViewer.WebAPI
{
    public class Startup
    {
        private readonly IConfigurationRoot _rootConfiguration;

        public Startup(IHostingEnvironment env, IConfigurationRoot rootConfiguration)
        {
            _rootConfiguration = rootConfiguration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(x => x.AddPolicy("*", _ => _.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<RepoContext>(options => options.UseNpgsql(_rootConfiguration["db.connection.string"]));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseCors("*");
        }
    }
}