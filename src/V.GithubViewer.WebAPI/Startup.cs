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
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddCors(x => x.AddPolicy("*", _ => _.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<RepoContext>(options => options.UseNpgsql("User ID=uubkfodbjbuxzq;Password=918527cd2bce2bf52d2d86a75d6f4cddde6594b19f4e3cd6c16ad7f1b59120a0;Server=ec2-107-22-251-55.compute-1.amazonaws.com;Port=5432;Database=d7d0bt0hhfglgg;Pooling=true;Persist Security Info=True;Trust Server Certificate=True;SSL Mode=Require"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseCors("*");
        }
    }
}
