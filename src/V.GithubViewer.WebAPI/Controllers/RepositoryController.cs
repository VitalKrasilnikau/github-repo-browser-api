using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using V.GithubViewer.DAL.Model;
using V.GithubViewer.DAL.Repository;
using V.GithubViewer.WebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace V.GithubViewer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("*")]
    public class RepositoryController
    {
        private readonly RepoContext _repoContext;

        public RepositoryController(RepoContext repoContext)
        {
            _repoContext = repoContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Repository>> GetAsync()
        {
            return (await _repoContext.Repos.ToListAsync()).Select(Map);
        }

        private static Repository Map(RepoEntity repoEntity) =>
            new Repository
            {
                Name = repoEntity.Id,
                Title = repoEntity.Title,
                Description = repoEntity.Description,
                Url = repoEntity.Url,
                ImgSrc = repoEntity.ImgSrc
            };
    }
}